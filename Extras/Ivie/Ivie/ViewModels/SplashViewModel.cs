using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services.Configuration;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ivie.Models.Local;
using TheXDS.Ivie.Properties;
using TheXDS.Ivie.Services;
using TheXDS.Ivie.ViewModels.Settings;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.ServicePool.Extensions;
using TheXDS.Triton.Services;
using Sp = TheXDS.ServicePool.ServicePool;
using St = TheXDS.Ivie.Resources.Strings.ViewModels.SplashViewModel;

namespace TheXDS.Ivie.ViewModels;

/// <summary>
/// ViewModel in charge of app initialization and login.
/// </summary>
public class SplashViewModel : OperationDialogViewModel
{
#if DEBUG
    private const string settingsFile = "./settings.dev.json";
#else
    private const string settingsFile = "./settings.json";
#endif

    private readonly ICloseable app;
    private bool _isExecutingOperation;

    /// <summary>
    /// Gets a value that indcates whether or not this ViewModel is execuing an
    /// operation.
    /// </summary>
    public bool IsExecutingOperation
    {
        get => _isExecutingOperation;
        private set => Change(ref _isExecutingOperation, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SplashViewModel"/> class.
    /// </summary>
    public SplashViewModel()
    {
        app = Sp.CommonPool.Resolve<ICloseable>()!;
    }

    /// <inheritdoc/>
    protected override async Task OnCreated()
    {
        IsBusy = false;
        if (!await InitializeApp(Sp.CommonPool))
        {
            app.Close();
            return;
        }
        Title = St.Login;
        while (true)
        {
            if (await DialogService!.GetCredential(St.Login, St.EnterCreds) is { Success: true, Result: { } cred })
            {
                if (await Login(cred)) break;
            }
            else
            {
                app.Close();
                break;
            }
        }
    }

    private async Task<bool> Login(Credential cred)
    {
        var svc = Sp.CommonPool.Resolve<IIvieService>() ?? throw new TamperException();
        var result = await svc.Authenticate(cred.User, cred.Password);
        if (result is { Success: true, Result: { } session })
        {
            LocalSession s = new()
            {
                RemoteSessionId = session.Id,
                LogonTimestamp = session.Timestamp,
                DisplayName = await svc.GetEmployeeDisplayNameAsync(session.Credential.Id)
            };
            NavigationService!.NavigateAndReset<HomepageViewModel, LocalSession>(s);
            return true;
        }
        else
        {
            await DialogService!.Error(St.LoginError, result.Reason == FailureReason.Forbidden ? St.Forbidden : result.Message);
        }
        return false;
    }

    private async Task<bool> InitializeApp(Sp pool)
    {
        IsExecutingOperation = true;
        var progress = new Progress<ProgressReport>(ReportProgress);

        var conf = await LoadSettings(progress, pool);
        if (conf.DataProvider == Guid.Empty)
        {
            await RunInitialSetup(pool);
            if (conf.DataProvider == Guid.Empty)
            {
                return false;
            }
        }

        foreach (var step in ApplicationSetup.GetInitializationSteps())
        {
            await step.Invoke(progress, pool);
        }
        Message = string.Empty;
        IsExecutingOperation = false;
        return true;
    }

    private static async Task<Configuration> LoadSettings(IProgress<ProgressReport> progress, Sp pool)
    {
        progress.Report(St.LoadingConfig);
        pool.Register(() => new JsonConfiguration(new LocalFileSettingsStore(settingsFile)), false);
        var repo = pool.Resolve<JsonConfiguration>()!;
        return ((await repo.Load()) ?? Configuration.GetDefaults()).RegisterInto(pool);
    }

    private async Task RunInitialSetup(Sp pool)
    {
        await DialogService!.CustomDialog(new DbConnectionSettingsViewModel(pool));
    }

    private void ReportProgress(ProgressReport p)
    {
        Progress = p.Progress;
        if (p.Status is not null)
        {
            Message = p.Status;
        }
    }
}

using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Models.Context;
using TheXDS.Ganymede.Models.Local;
using TheXDS.Ganymede.Properties;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.ServicePool.Triton;
using TheXDS.Triton.Services;
using Sp = TheXDS.ServicePool.ServicePool;
using St = TheXDS.Ivie.Resources.Strings.ViewModels.SplashViewModel;

namespace TheXDS.Ivie.ViewModels;

/// <summary>
/// ViewModel in charge of app initialization and login.
/// </summary>
public class SplashViewModel : OperationDialogViewModel
{
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
        await InitializeApp();
        Title = St.Login;
        while (true)
        {
            if (await DialogService!.GetCredential(St.Login, St.EnterCreds) is { Success: true, Result: { } cred })
            {
                var svc = Sp.CommonPool.Resolve<IvieService>() ?? throw new TamperException();
                var result = await ((IUserService)svc).Authenticate(cred.User, cred.Password);
                if (result is { Success: true, Result: { } session })
                {
                    LocalSession s = new()
                    {
                        RemoteSessionId = session.Id,
                        LogonTimestamp = session.Timestamp,
                        DisplayName = await svc.GetEmployeeDisplayNameAsync(session.Credential.Id)
                    };
                    NavigationService!.NavigateAndReset<HomepageViewModel, LocalSession>(s);
                    break;
                }
                else
                {
                    await DialogService!.Error(St.LoginError, result.Reason == FailureReason.Forbidden ? St.Forbidden : result.Message);
                }
            }
            else
            {
                app.Close();
                break;
            }
        }
    }

    private async Task InitializeApp()
    {
        IsExecutingOperation = true;
        void ReportProgress(ProgressReport p)
        {
            Progress = p.Progress;
            if (p.Status is not null)
            {
                Message = p.Status;
            }
        }
        if (Sp.CommonPool.ResolveTritonService<IvieContext>() is { } svc)
        {
            var progress = new Progress<ProgressReport>(ReportProgress);
            foreach (var step in TritonConfiguration.GetSeedingSteps())
            {
                await step.Invoke(progress, svc);
            }
        }
        Message = string.Empty;
        IsExecutingOperation = false;
    }
}

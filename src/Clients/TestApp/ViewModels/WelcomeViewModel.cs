#pragma warning disable CS1591

using System.Windows.Input;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Types.Base;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Security;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Faker;
using TheXDS.Triton.Services.Base;
using SP = TheXDS.ServicePool.ServicePool;
using St = TheXDS.Ganymede.Resources.Strings.Views.WelcomeView;

namespace TheXDS.Ganymede.ViewModels;

public class WelcomeViewModel : ViewModel
{
    public WelcomeViewModel()
    {
        var cb = new CommandBuilder<WelcomeViewModel>(this);
        LogoutCommand = cb.BuildBusyOperation(OnLogout);
        TestNavigationCommand = cb.BuildNavigate<DummyViewModel>();
        TestBusyCommand = cb.BuildBusyOperation(() => Task.Delay(5000));
        TryProteusCommand = cb.BuildNavigate<ProteusDemoViewModel>();
        TryDialogDemoCommand = cb.BuildNavigate<DialogDemoViewModel>();
    }

    public ICommand LogoutCommand { get; }

    public ICommand TestNavigationCommand { get; }

    public ICommand TestBusyCommand { get; }

    public ICommand TryProteusCommand { get; }

    public ICommand TryDialogDemoCommand { get; }

    private async Task OnLogout(IProgress<ProgressReport> progress)
    {
        progress.Report(St.LoggingOut);
        await Task.Delay(2500);
        NavigationService!.HomePage = new LoginViewModel();
    }


    /// <inheritdoc/>
    protected override Task OnCreated()
    {
        if (SP.CommonPool.Resolve<ITritonService>() is not { } svc) return Task.CompletedTask;
        using var trans = svc.GetWriteTransaction();
        var users = new[]
        {
            new User()
            {
                Id = "root",
                DisplayName = "Super User",
                Enabled = false,
                Password = PasswordStorage.CreateHash<Pbkdf2Storage>("r00t".ToSecureString())
            },
            new User()
            {
                Id = "admin",
                DisplayName = "Administrator",
                Password = PasswordStorage.CreateHash<Pbkdf2Storage>("@dmin1234".ToSecureString())
            }
        }.Concat(Enumerable.Range(0, 10)
            .Select(_ => Person.Adult())
            .Select(p => new User()
            {
                Id = p.UserName,
                DisplayName = p.Name,
                Password = PasswordStorage.CreateHash<Pbkdf2Storage>("1234".ToSecureString())
            })).ToArray();
        trans.Create(users);
        var admPost = new Post()
        {
            Title = Text.Lorem(4),
            Content = Text.Lorem(200, 8, 3),
            CreationDate = DateTime.Now,
            Creator = users[1],
            Id = Guid.NewGuid(),
        };
        var comments = users[2..].Select(u => new Comment()
        {
            Id = Guid.NewGuid(),
            Content = Text.Lorem(10),
            CreationDate = DateTime.Now,
            Creator = u,
            Post = admPost
        });
        admPost.Comments = comments.ToList();
        trans.Create(admPost);
        trans.Create(comments.ToArray());
        return trans.CommitAsync();
    }
}

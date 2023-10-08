using System.Windows.Input;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types.Base;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services.Base;
using SP = TheXDS.ServicePool.ServicePool;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Implements a ViewModel that includes a few demos for Proteus technology.
/// </summary>
public class ProteusDemoViewModel : ViewModel
{
    /// <summary>
    /// Gets a command that manages users.
    /// </summary>
    public ICommand ManageUsersCommand { get; }

    /// <summary>
    /// Gets a command that manages posts.
    /// </summary>
    public ICommand ManagePostsCommand { get; }

    /// <summary>
    /// Gets a command that manages comments.
    /// </summary>
    public ICommand ManageCommentsCommand { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProteusDemoViewModel"/>
    /// class.
    /// </summary>
    public ProteusDemoViewModel()
    {
        CommandBuilder<ProteusDemoViewModel> cb = new(this);
        ManageUsersCommand = cb.BuildSimple(OnManage<User, UserDescriptor>);
        ManagePostsCommand = cb.BuildSimple(OnManage<Post, PostDescriptor>);
        ManageCommentsCommand = cb.BuildSimple(OnManage<Comment, CommentDescriptor>);
    }

    private void OnManage<TModel, TDescriptor>() where TModel : Model, new() where TDescriptor : ICrudDescriptor, new()
    {
        var svc = SP.CommonPool.Resolve<ITritonService>()!;
        using var trans = svc.GetReadTransaction();
        var tbl = trans.All<TModel>().Cast<Model>().ToList();
        var vm = new CrudPageViewModel(tbl, new[] { new TDescriptor().Description }, svc) { DialogService = DialogService };
        NavigationService?.Navigate(vm);
    }
}

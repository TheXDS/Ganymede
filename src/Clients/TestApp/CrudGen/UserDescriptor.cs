using TheXDS.Ganymede.Models;
using TheXDS.MCART.Security;

namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Describes the <see cref="User"/> model.
/// </summary>
public class UserDescriptor : CrudDescriptor<User>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<User> m)
    {
        m.LabelResource<TheXDS.Ganymede.Resources.Strings.Models.User>();
        m.ConfigureProperties(c => { 
            c.Property(p => p.Id);
            c.Property(p => p.DisplayName);
            c.Property(p => p.Password)
                .Password()
                .Algorithm<Argon2Storage, Argon2Settings>(Argon2Storage.GetDefaultSettings());
            c.Property(p => p.Enabled);
            c.Property(p => p.Description).Nullable().Kind(TextKind.Big);
        });
    }
}

/// <summary>
/// Describes the <see cref="Post"/> model.
/// </summary>
public class PostDescriptor : CrudDescriptor<Post>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Post> m)
    {
        m.ConfigureProperties(c => {
            c.Property(p => p.Title);
            c.Property(p => p.Creator).Selectable();
            c.Property(p => p.Content).Big();
            c.Property(p => p.CreationDate).WithTime().HideFromEditor();
            c.Property(p => p.Comments)
                .HideFromDetails()
                .WidgetSize(WidgetSize.Large)
                .Creatable()
                .AvailableModels(new CommentDescriptor().Description);
        });
        m.SaveProlog(p => { if (p.Id == default) p.Id = Guid.NewGuid(); });
        m.SaveProlog(p => p.CreationDate ??= DateTime.Now);
    }
}

/// <summary>
/// Describes the <see cref="Comment"/> model.
/// </summary>
public class CommentDescriptor : CrudDescriptor<Comment>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<Comment> m)
    {
        m.ConfigureProperties(c => {
            c.Property(p => p.Content).Big();
            c.Property(p => p.CreationDate).HideFromEditor();
        });
        m.SaveProlog(p => p.CreationDate ??= DateTime.Now);
    }
}
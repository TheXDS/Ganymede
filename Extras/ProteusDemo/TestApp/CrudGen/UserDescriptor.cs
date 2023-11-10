﻿using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels;
using TheXDS.Ganymede.ViewModels.CrudGen;
using St = TheXDS.Ganymede.Resources.Strings.Models;
namespace TheXDS.Ganymede.CrudGen;

/// <summary>
/// Describes the <see cref="User"/> model.
/// </summary>
public class UserDescriptor : CrudDescriptor<User>
{
    /// <inheritdoc/>
    protected override void OnDescribeModel(IModelConfigurator<User> m)
    {
        m.LabelResource<St.User>();
        m.ConfigureProperties(c => { 
            c.Property(p => p.Id);
            c.Property(p => p.DisplayName).Icon("👤");
            c.Property(p => p.Password).Password().Argon2();
            c.Property(p => p.Enabled);
            c.Property(p => p.Description).Nullable().Kind(TextKind.Paragraph);
        });
        m.ListViewProperties(p => p.Id, p => p.DisplayName);
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
            c.Property(p => p.Content).Paragraph(WidgetSize.Large);
            c.Property(p => p.CreationDate).WithTime().HideFromEditor();
            c.Property(p => p.Comments)
                .HideFromDetails()
                .WidgetSize(WidgetSize.Large)
                .Creatable();
        });
        m.DetailsViewModel<PostDetailsViewModel>();
        m.AddDefaultGuidIdProlog();
        m.AddSaveProlog(p => p.CreationDate ??= DateTime.Now);
        m.ListViewProperties(p => p.Title, p => p.CreationDate, p => p.Creator);
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
            c.Property(p => p.Post).Selectable();
            c.Property(p => p.Creator).Selectable();
            c.Property(p => p.Content).Paragraph();
            c.Property(p => p.CreationDate).WithTime().HideFromEditor();
        });
        m.AddDefaultGuidIdProlog();
        m.AddSaveProlog(p => p.CreationDate ??= DateTime.Now);
        m.ListViewProperties(p => p.CreationDate, p => p.Creator);
    }
}
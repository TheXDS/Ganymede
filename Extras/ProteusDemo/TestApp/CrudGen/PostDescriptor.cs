using TheXDS.Ganymede.Models;
using TheXDS.Ganymede.ViewModels.CrudGen;

namespace TheXDS.Ganymede.CrudGen;

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

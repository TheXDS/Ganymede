using TheXDS.Ganymede.Models;

namespace TheXDS.Ganymede.CrudGen;

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
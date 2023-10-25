using TheXDS.Ganymede.CrudGen;
using TheXDS.Triton.Models.Base;
using St = TheXDS.Ganymede.Resources.Strings.ProteusCommon;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// ViewModel that allows the user to display the details of a selected entity.
/// </summary>
public class CrudDetailsViewModel : DynamicCrudViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CrudDetailsViewModel"/>
    /// class.
    /// </summary>
    /// <param name="entity">
    /// Entity to visualize.
    /// </param>
    /// <param name="description">
    /// Model description for the entities.
    /// </param>
    public CrudDetailsViewModel(Model entity, ICrudDescription description) : base(entity, description)
    {
        Title = string.Format(St.DetailsOfX, description.FriendlyName, entity.IdAsString);
    }
}

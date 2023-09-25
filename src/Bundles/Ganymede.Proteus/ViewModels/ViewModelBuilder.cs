using System.Reflection;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Models;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;

namespace TheXDS.Ganymede.ViewModels;

/// <summary>
/// Contains methods that can build ViewModels for Proteus.
/// </summary>
public static class ViewModelBuilder
{
    /// <summary>
    /// Builds a new instance of the <see cref="CrudEditorViewModel"/> class
    /// tailored to the specified model.
    /// </summary>
    /// <param name="entity">
    /// Entity to be edited by the newly created ViewModel instance.
    /// </param>
    /// <param name="description">
    /// Model description for the specified entity.
    /// </param>
    /// <param name="context">
    /// Indicates the context and status information of the generated 
    /// ViewModel.
    /// </param>
    /// <returns>
    /// A <see cref="CrudEditorViewModel"/> that contains all the read/write
    /// properties from the model as NPC properties.
    /// </returns>
    public static CrudEditorViewModel BuildEditorFrom(Model entity, ICrudDescription description, CrudEditorViewModelContext context)
    {
        return new CrudEditorViewModel(entity, description, context);
    }

    /// <summary>
    /// Builds a new instance of the <see cref="CrudEditorViewModel"/> class
    /// tailored to the specified model.
    /// </summary>
    /// <param name="entity">
    /// Entity to be edited by the newly created ViewModel instance.
    /// </param>
    /// <param name="description">
    /// Model description for the specified entity.
    /// </param>
    /// <returns>
    /// A <see cref="CrudEditorViewModel"/> that contains all the read/write
    /// properties from the model as NPC properties.
    /// </returns>
    public static CrudDetailsViewModel BuildDetailsFrom(Model entity, ICrudDescription description)
    {
        return new CrudDetailsViewModel(entity, description);
    }
}

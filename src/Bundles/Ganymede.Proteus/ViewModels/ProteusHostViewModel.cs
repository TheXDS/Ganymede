using TheXDS.Ganymede.Component;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.Helpers;
using TheXDS.Ganymede.Types;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.ViewModels;

public class DesignProteusHostViewModel : ProteusHostViewModel
{
    public DesignProteusHostViewModel() : base(null!)
    {
    }
}

/// <summary>
/// Implements a <see cref="HostViewModelBase"/> that contains functionality to
/// generate children CRUD pages using Proteus.
/// </summary>
public abstract class ProteusHostViewModel : HostViewModelBase
{
    /// <summary>
    /// Gets a reference to an <see cref="ITritonService"/> instance to make
    /// available to all CRUD pages that are navigated to.
    /// </summary>
    protected ITritonService Service { get; }

    /// <summary>
    /// Gets a grouped enumeration of interactions to be made available on the
    /// ViewModel.
    /// </summary>
    public IEnumerable<IGrouping<string, ButtonInteraction>>? SidebarInteractions { get; protected init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProteusHostViewModel"/>
    /// class.
    /// </summary>
    /// <param name="service">
    /// Service to make available to all CRUD pages that get navigated to on 
    /// this instance.
    /// </param>
    protected ProteusHostViewModel(ITritonService service)
    {
        Service = service;
    }

    /// <summary>
    /// Creates a new <see cref="CrudButtonInteraction"/> that may be shown on
    /// this ViewModel.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of model descriptor for which to generate a CRUD page.
    /// </typeparam>
    /// <param name="label">Label of the new interaction.</param>
    /// <param name="group">Group of the interaction.</param>
    /// <param name="essential">
    /// Indicates whether or not this interaction should be treated as
    /// essential.
    /// </param>
    /// <param name="clearStack">
    /// If set to <see langword="true"/>, the navigation service on this
    /// instance will be cleared before navigating to the CRUD page.
    /// </param>
    /// <returns>
    /// A new <see cref="CrudButtonInteraction"/> that can be used to navigate
    /// to the required CRUD page upon invocation.
    /// </returns>
    protected CrudButtonInteraction CreateCrudInteraction<TDescriptor>(string label, string? group = null, bool essential = false, bool clearStack = false) where TDescriptor : ICrudDescriptor, new()
    {
        return new(() =>
        {
            if (clearStack) ChildNavService!.Reset();
            ChildNavService!.NavigateToCrud<TDescriptor>(Service);
        }, label) { Group = group ?? string.Empty, Essential = essential };
    }
}

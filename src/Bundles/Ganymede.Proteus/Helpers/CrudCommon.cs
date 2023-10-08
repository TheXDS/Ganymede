using System.Collections;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Contains helper methods used to generate and manage CRUD ViewModels.
/// </summary>
public static class CrudCommon
{
    /// <summary>
    /// Launches a new <see cref="CrudEditorViewModel"/> and asyncronously
    /// awaits for its completion.
    /// </summary>
    /// <param name="settings">
    /// Settings to use when generating the new
    /// <see cref="CrudEditorViewModel"/> instance.
    /// </param>
    /// <returns>
    /// A task that can be used to await the async operation. The task will
    /// return <see langword="true"/> if the used closes the editor by saving
    /// the changes, or <see langword="false"/> if the user cancels the edition
    /// or navigates back.
    /// </returns>
    public static async Task<bool> LaunchEditor(LaunchEditorSettings settings)
    {
        var vm = ViewModelBuilder.BuildEditorFrom(settings.Entity, settings.Description, settings.Context);
        var b = new CommandBuilder<CrudEditorViewModel>(vm);
        vm.CrudActions = new ButtonInteraction[] {
            new(b.BuildBusyOperation(vm.OnSave), "Save"),
            new(b.BuildSimple(vm.OnCancel), "Back")
        };
        vm.DialogService = settings.DialogService;
        settings.NavigationService.Navigate(vm);
        if (await vm.WaitForCompletion())
        {
            settings.Description.SaveProlog?.Invoke(settings.Entity);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Dynamically copies the elements from one collection onto another.
    /// </summary>
    /// <param name="source">Source collection.</param>
    /// <param name="target">Target collection.</param>
    public static void DynamicPopulateCollection(object source, object target)
    {
        var type = target.GetType();
        var itemType = type.GetCollectionType();
        var add = type.GetMethod("Add")!;
        var cast = typeof(Enumerable).GetMethod("Cast")!.MakeGenericMethod(itemType);

        type.GetMethod(nameof(ICollection<object>.Clear))!.Invoke(target, Type.EmptyTypes);
        foreach (var item in (IEnumerable)cast.Invoke(null, new object[] { source })!)
        {
            add.Invoke(target, new object[] { item });
        }
    }
    public static void DynamicAdd(object collection, object item)
    {
        var type = collection.GetType();
        var add = type.GetMethod("Add")!;
        add.Invoke(collection, new object[] { item });
    }
}

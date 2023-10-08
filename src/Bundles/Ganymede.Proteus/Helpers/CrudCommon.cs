using System.Collections;
using System.Reflection;
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
    /// Resolves a localized text from a resource ID, or infers the
    /// corresponding text from the resource ID.
    /// </summary>
    /// <param name="resourceType">
    /// Type that contains the requested localized string resource.
    /// </param>
    /// <param name="resourceId">Id of the resource to get.</param>
    /// <returns>
    /// The localized resource text from the resource type, or an inferred
    /// string from the resource ID.
    /// </returns>
    public static string GetLabel(this Type? resourceType, string resourceId)
    {
        return resourceType?.GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public, null, typeof(string), Type.EmptyTypes, null)?.GetValue(null) as string ?? SplitByUppercase(resourceId);
    }

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

    /// <summary>
    /// Dinamically adds an item to a collection.
    /// </summary>
    /// <param name="collection">Collection to add the item to.</param>
    /// <param name="item">Item to add to the list.</param>
    public static void DynamicAdd(object collection, object item)
    {
        var type = collection.GetType();
        var add = type.GetMethod("Add")!;
        add.Invoke(collection, new object[] { item });
    }

    private static string SplitByUppercase(string name)
    {
        var sb = new System.Text.StringBuilder();
        foreach (char c in name)
        {
            if (char.IsUpper(c))
            {
                sb.Append(' ');
            }
            sb.Append(c);
        }
        return sb.ToString().TrimStart();
    }
}

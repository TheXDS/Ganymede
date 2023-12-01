using System.Collections;
using System.Reflection;
using TheXDS.Ganymede.CrudGen;
using TheXDS.Ganymede.CrudGen.Descriptions;
using TheXDS.Ganymede.Services;
using TheXDS.Ganymede.Types;
using TheXDS.Ganymede.ViewModels;
using TheXDS.MCART.Types.Extensions;
using TheXDS.Triton.Models.Base;
using TheXDS.Triton.Services;
using TheXDS.Triton.Services.Base;

namespace TheXDS.Ganymede.Helpers;

/// <summary>
/// Contains helper methods used to generate and manage CRUD ViewModels.
/// </summary>
public static class CrudCommon
{
    /// <summary>
    /// Navigates to a new Crud ViewModel.
    /// </summary>
    /// <typeparam name="TDescriptor">
    /// Type of descriptor to use when building the new Crud ViewModel.
    /// </typeparam>
    /// <param name="navigationService">
    /// Navigation service to use when navigating to the Crud ViewModel.
    /// </param>
    /// <param name="tritonService">
    /// Triton Service to use for fetching and saving data.
    /// </param>
    public static void NavigateToCrud<TDescriptor>(this INavigationService navigationService, ITritonService tritonService) where TDescriptor : ICrudDescriptor, new()
    {
        var ep = new TritonFlatEntityProvider(tritonService, new TDescriptor().Description);
        var vm = new CrudPageViewModel(new[] { new TDescriptor().Description }, tritonService, ep);
        navigationService.Navigate(vm);
    }

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
        string Infer()
        {
            var l = SplitByUppercase(resourceId);
            return $"{l.ToUpper()[0]}{l.ToLower()[1..]}";
        }
        return resourceType?.GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public, null, typeof(string), Type.EmptyTypes, null)?.GetValue(null) as string ?? Infer();
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
            foreach (var saveProlog in settings.Description.SavePrologs)
            {
                saveProlog.Invoke(settings.Entity);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Exposes a non-generic version of the method
    /// <see cref="ICrudReadTransaction.All{TModel}"/>.
    /// </summary>
    /// <param name="transaction">
    /// Transaction to call the methond from.
    /// </param>
    /// <param name="model">Type of entity to get.</param>
    /// <returns>
    /// A service result that contains the resulting query.
    /// </returns>
    public static QueryServiceResult<Model> All(this ICrudReadTransaction transaction, Type model)
    {
        var m = transaction.GetType().GetMethod(nameof(All), 1, BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null)!.MakeGenericMethod(model);
        object o = m.Invoke(transaction, Array.Empty<object>())!;
        ServiceResult r = (ServiceResult)o;
        if (r.Success)
        {
            return new QueryServiceResult<Model>((IQueryable<Model>)o);
        }
        else
        {
            return new QueryServiceResult<Model>(r.Reason ?? FailureReason.Unknown, r.Message);
        }
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
        CollectionDynamicInvoke(collection, item, "Add");
    }

    /// <summary>
    /// Dinamically removes an item from a collection.
    /// </summary>
    /// <param name="collection">Collection to remove the item from.</param>
    /// <param name="item">Item to remove from the list.</param>
    public static void DynamicRemove(object collection, object item)
    {
        CollectionDynamicInvoke(collection, item, "Remove");
    }

    /// <summary>
    /// Scans the available types and finds all the 
    /// <see cref="ICrudDescription"/> instances that can be used to describe
    /// potential values assignable to the specified property description.
    /// </summary>
    /// <param name="property">
    /// Property description for wich to infer the available CRUD descriptors.
    /// </param>
    /// <returns>
    /// An enumeration of new instances of <see cref="ICrudDescription"/> that
    /// can be used to describe values that are assignable to the described
    /// property.
    /// </returns>
    public static IEnumerable<ICrudDescription> InferDescriptions(IPropertyDescription property)
    {
        var types = property.Property.PropertyType.ResolveCollectionType().Derivates().Where(p => p.IsInstantiable());
        foreach (var type in types)
        {
            foreach (var k in EnumerateDescriptors(type))
            {
                yield return k.New<ICrudDescriptor>().Description;
            }
        }
    }

    private static IEnumerable<Type> EnumerateDescriptors(Type type)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies().Where(p => !p.IsDynamic)
            .SelectMany(p => p.ExportedTypes)
            .Where(t => t.IsAssignableTo(typeof(CrudDescriptor<>).MakeGenericType(type)) && t.IsInstantiable());
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

    private static void CollectionDynamicInvoke(object collection, object item, string methodName)
    {
        var type = collection.GetType();
        var method = type.GetMethod(methodName)!;
        method.Invoke(collection, new object[] { item });
    }
}

namespace TheXDS.Ganymede.Resources.Strings;

internal static class AttributeErrorMessages
{
    public const string CallStackAccess = "The method dynamically accesses the call stack, so it is not compatible with trimming.";
    public const string ClassHeavilyUsesReflection = "The class extensively uses reflection, so it is not compatible with trimming.";
    public const string ClassCallsDynamicCode = "The class makes calls to dynamic code, so it is not compatible with trimming.";
    public const string MethodCreatesDelegates = "The method dynamically creates delegates, so it is not compatible with trimming.";
    public const string ClassScansForTypes = "Members of the class obtain a collection of types without direct references, so the class is not compatible with trimming.";
    public const string MethodScansForTypes = "The method obtains a collection of types without direct references, so it is not compatible with trimming.";
    public const string MethodGetsTypeMembersByName = "The method gets members of a type by their name, so it is not compatible with trimming.";
    public const string MethodAnalyzesTypeMembers = "The method accesses and analyzes type members dynamically, so it is not compatible with trimming.";
    public const string MethodCallsDynamicCode = "The method makes calls to dynamic code, so it is not compatible with trimming.";
    public const string MethodCreatesNewTypes = "The method dynamically creates new types, so it is not compatible with trimming.";
    public const string MethodLoadsAssemblyResources = "The method dynamically loads resources from the assembly, so it is not compatible with trimming.";
    public const string Net6Deprecation = "This class uses deprecated methods in .Net 6.";
    public const string UseLicenseUriAttributeInstead = "Use LicenseUriAttribute instead.";

    public const string JsonConfigurationRepository_DynCode = "JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. You may choose to implement your own IConfigurationRepository<T> and use System.Text.Json source generation for native AOT applications.";
    public const string JsonConfigurationReposotory_UnrefCode = "JSON serialization and deserialization might require types that cannot be statically analyzed. You may choose to implement your own IConfigurationRepository<T> and serialize Json data using a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.";

}

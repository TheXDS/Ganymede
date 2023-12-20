namespace TheXDS.Ganymede.Types.Base;

internal interface IViewModel_Internal : IViewModel
{
    Task InvokeOnCreated();
}
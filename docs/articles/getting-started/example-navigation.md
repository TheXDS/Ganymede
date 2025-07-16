# Example: Navigation on Ganymede
In this quick guide, we'll explore the navigation services provided by Ganymede. For the purposes of this guide, we'll be in the context of just the ViewModel, so this knowledge can be applied to any UI framework that Ganymede supports, such as Avalonia or WPF.

## Navigation Stack
We'll now explore the *navigation stack* concept, which is one kind of App Lifecycle management paradigm that allows for ViewModels to be navigated to linearly.

### What is a navigation stack?
When creating an app using Ganymede, you have the option to use what is known as a *Navigation stack* as your navigation component. As its name implies, a navigation stack is a Last-in/First-out (LIFO) list where the item on the top of the stack (in this case, the last ViewModel that was added to the stack) is the active element.

Upon requesting navigation to a specific ViewModel using the `NavigationService` property available in a Ganymede's `ViewModel`, it will be added to the top of the stack. This, in turn, will trigger a navigation event, where the *Navigation host* will display the currently active ViewModel. Because the navigation state is represented by a stack it's possible to navigate back, which will remove the ViewModel at the top and trigger a refresh of which element should be the active view.

### Limitations of a navigation *stack*
As stated above; when navigating back, the ViewModel at the top of the stack is removed. This would make it impossible to navigate forward to it again. There's some workarounds, like saving the active ViewModel instance somewhere else and then navigating back to that same reference using the available navigation API call for it, but this is not an ideal way of doing it.

Another issue is the inability to activate a previous ViewModel at an arbitrary position on the stack. Again, this could be solved by manually getting references to the desired ViewModel instance, but it can introduce errors and unwanted references (as well as the possibility to allow the user to navigate to *`null` references*.)

A navigation stack is therefore ideal when an app workflow is linear, with no need to directly reference any sibling ViewModels that might've been active previously, like the majority of mobile apps. A stack would not translate well for apps that require a tabbed interface, or when a ViewModel can be activated arbitrarily by the user with the ability to continue back at the same point on the original ViewModel. However, it's still possible to create a serviceable app using the stack paradigm, as long as you keep in mind the limitations of it and employ [`NavigateAndReset`](https://thexds.github.io/Ganymede/api/TheXDS.Ganymede.Services.INavigationService.html#TheXDS_Ganymede_Services_INavigationService_NavigateAndReset_TheXDS_Ganymede_Types_Base_IViewModel_) calls.

## Using the Navigation Stack
From within a ViewModel, you can access the [`INavigationService`](https://thexds.github.io/Ganymede/api/TheXDS.Ganymede.Services.INavigationService.html) through the `NavigationService` property. This service provides methods to navigate to other ViewModels, as well as to manage the navigation stack.

Within any ViewModel, you can navigate to another ViewModel by calling the `Navigate` method on the `NavigationService`. For example:
```csharp
private void OnNavigateToNextViewModel()
{
	// Assuming NextViewModel is a ViewModel you want to navigate to
	NavigationService.Navigate<NextViewModel>();
}
```
The `Navigate` method also supports passing a ViewModel instance directly, allowing you to navigate to a specific instance rather than creating a new one:
```csharp
private void OnNavigateToSpecificViewModel()
{
	// Assuming specificViewModel is an instance of a ViewModel you want to navigate to
	var specificViewModel = new SpecificViewModel();
	NavigationService.Navigate(specificViewModel);
}
```
This can allow you to perform some custom initialization on the ViewModel before navigating to it.

You can go back to the previous ViewModel by calling the `NavigateBackBack` method:
```csharp
private void OnNavigateBackBack()
{
	// This will navigate back to the previous ViewModel in the stack
	NavigationService.NavigateBack();
}
```

Simmilarly, you can navigate to a ViewModel and reset the navigation stack by using the `NavigateAndReset` method:
```csharp
private void OnNavigateAndResetToViewModel()
{
	// Assuming ResetViewModel is a ViewModel you want to navigate to and reset the stack
	NavigationService.NavigateAndReset<ResetViewModel>();
}
```
This will clear the current navigation stack and set the specified ViewModel as the new active one. You'll be unable to navigate back to the previous ViewModel after this operation.

There is also the ability to manually clear the navigation stack using the `Clear` method:
```csharp
private void OnClearNavigationStack()
{
	// This will clear the entire navigation stack
	NavigationService.Clear();
}
```
Note that clearing the stack is the equivalent of navigating all the way back to the home ViewModel, or `null` if no home ViewModel is set.

## APIs exposed by the ViewModels for navigation
In your ViewModel class, you can override a few methods to fine-tune the navigation behavior. These methods are called by the `NavigationService` when navigating to or from a ViewModel.

### The `OnNavigateAway` method
This method is called when the Navigation Service is navigating forward, away from the current ViewModel. You can override this method to perform any additional steps before the ViewModel is replaced in the UI.
```csharp
public override Task OnNavigateAway(CancelFlag cancelNavigation)
{
	// Perform some operation here
}
```
Note that you can use the `CancelFlag` parameter to cancel the navigation if needed. This is useful if you want to prevent navigation under certain conditions, such as unsaved changes.

### The `OnNavigateBack` method
Closely related to `OnNavigateAway`, this method is called when the Navigation Service is navigating back to a previous ViewModel in the stack. You can override this method to perform any specific actions when navigating back.
```csharp
public override Task OnNavigateBack(CancelFlag cancelNavigation)
{
	// Perform actions specific to navigating back here
}
```
This is similar to `OnNavigateAway`, but this method is specifically for when the user navigates back in the stack, allowing you to handle any special logic that should occur in that case, such as saving data or performing cleanup operations before the ViewModel is removed from the navigation stack.

### The `OnCreated` method
This method is called when the ViewModel is created, which is typically when it is first added to the navigation stack. You can override this method to perform any initialization that needs to happen before the ViewModel is displayed.
```csharp
protected override Task OnCreated()
{
	// Perform initialization here
}
```
You can set the `IsBusy` property to `true` if the ViewModel is performing some lengthy asynchronous operation that should be reflected in the UI, such as loading data. Please remember to set it back to `false` when the operation is complete.

## Conclusion
In this guide, we've explored the navigation stack concept in Ganymede, which provides a simple yet effective way to manage ViewModel navigation in a linear fashion. While it has its limitations, such as the inability to navigate forward or to arbitrary positions in the stack, it is well-suited for many app workflows, especially those that follow a linear progression.
# Example: Navigation on Ganymede
In this quick guide, we'll explore the navigation services provided by Ganymede.

## Navigation Stack
We'll now explore the *navigation stack* concept, which is one kind of App Lifecycle management paradigm that allows for ViewModels to be navigated to linearly.

### What is a navigation stack?
When creating an app using Ganymede, you have the option to use what is known as a *Navigation stack* as your navigation component. As its name implies, a navigation stack is a Last-in/First-out (LIFO) list where the item on the top of the stack (in this case, the last ViewModel that was added to the stack) is the active element.

Upon requesting navigation to a specific ViewModel using the `NavigationService` property available in a Ganymede's `ViewModel`, it will be added to the top of the stack. This, in turn, will trigger a navigation event, where the *Navigation host* will display the currently active ViewModel. Because the navigation state is represented by a stack it's possible to navigate back, which will remove the ViewModel at the top and trigger a refresh of which element should be the active view.

### Limitations of a navigation *stack*
As stated above; when navigating back, the ViewModel at the top of the stack is removed. This would make it impossible to navigate forward to it again. There's some workarounds, like saving the active ViewModel instance somewhere else and then navigating back to that same reference using the available navigation API call for it, but this is not an ideal way of doing it.

Another issue is the inability to activate a previous ViewModel at an arbitrary position on the stack. Again, this could be solved by manually getting references to the desired ViewModel instance, but it can introduce errors and unwanted references (as well as the possibility to allow the user to navigate to *`null` references*.)

A navigation stack is therefore ideal when an app workflow is linear, with no need to directly reference any sibling ViewModels that might've been active previously, like the majority of mobile apps. A stack would not translate well for apps that require a tabbed interface, or when a ViewModel can be activated arbitrarily by the user with the ability to continue back at the same point on the original ViewModel.
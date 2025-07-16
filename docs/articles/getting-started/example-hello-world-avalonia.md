# Example: Hello world with Ganymede using Avalonia
In this quick guide, we'll see how to use Ganymede to create an app with a button that, upon being clicked, will display a dialog message. We'll explore the architecture of a Ganymede app, as well as how to properly set it up.

## Some important requirements
Ganymede was built using [.NET 8](https://dotnet.microsoft.com/), so if you haven't installed it already, please do. If you're running a newer version of .NET, you can install the .NET 8 targeting pack instead, which will allow you to build and execute .NET 8 apps on a newer version of .NET.

Also, while multi-platform support is to some extent present in Ganymede, this tutorial will use the Avalonia bindings. Avalonia is available for Windows, Linux, Mac and WebAssembly, but for the purposes of this tutorial, we will use Windows as the target platform.

If you haven't installed Avalonia yet, you can follow the [official installation guide](https://docs.avaloniaui.net/docs/installation) to get started. This guide assumes you have at least the Avalonia templates installed, which can be done by executing the following command in a terminal:
```sh
dotnet new install Avalonia.Templates
```

## Starting a new Avalonia project
To start with, you can use the `dotnet` command in a terminal to create your new Avalonia project: 
```sh
dotnet new avalonia.app
```
You may use Visual Studio, or your preferred IDE for this task instead of using `dotnet`.

At this point, you may build and run your project to make sure that your tooling is working properly.

## Installing and setting up Ganymede
The only package that you will need to reference to get `Ganymede` on your project is `TheXDS.Ganymede.Avalonia`. This package will include a set of dependencies as well (namely, [MCART](https://github.com/TheXDS/MCART/) and the core Ganymede library) so you don't have to install them explicitly.

If using `dotnet`, this can be achieved by executing:
```sh
dotnet add package TheXDS.Ganymede.Avalonia
```
At this point you may rebuild your solution. Again, this ensures that your toolkit is working properly, as well as checking that you are targeting the proper version and platform of .NET.

Next, go into your `App.axaml` file and add the following to your `Application.Styles`:
```xml
    <gn:GanymedeDictionary/>
```
> You need to add the `xmlns:gn="using:TheXDS.Ganymede.Markup"` namespace declaration to the `<Application>` tag. Your IDE should be able to do this, or provide you with a way to do it automatically. If not, you can add it manually.

This XAML code will take care of loading the necessary resources for Ganymede to work properly.

## Creating a `ViewModel`
As per the [MVVM pattern](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm), we need to define a ViewModel that will contain the business logic for our view. While it's true that MVVM may be considered overkill for small apps, Ganymede removes a lot of guesswork and boilerplate code that would otherwise fall into a developer to properly implement.

Note that Ganymede uses MCART's MVVM implementation, which should be somewhat similar to other MVVM frameworks that you might have used in the past, such as *CommunityToolkit.Mvvm*. Reason for using a bespoke MVVM implementation is that it allows me to provide a more integrated experience with Ganymede, as well as providing a few extra features that are not available in other MVVM frameworks. Avalonia templates might include the `CommunityToolkit.Mvvm` package by default, but you should remove it if you wish to use Ganymede's MVVM implementation instead. There could also be a few class stubs that you might need to remove, such as the `ViewModelBase` class, which is not used in Ganymede (a base class of the same name is provided by MCART).

For our example, we'll define a simple ViewModel that only exposes a command property. To begin with, go ahead and create a new `.cs` file, and name it `HelloViewModel.cs`.

It's very important that you keep in mind the recommended naming conventions for Ganymede. ViewModel class names should end with the `ViewModel` suffix, and views must match the class name, subsituting the `ViewModel` suffix for the `View` suffix. This enables Ganymede to resolve the views corresponding to the ViewModel without explicit registration. We'll explore this concept later on.

As for the class definition for our ViewModel, we need to inherit from the `ViewModel` class, available under the `TheXDS.Ganymede.Types.Base` namespace. Next, we can define the command by adding the following:
```csharp
public ICommand HelloCommand { get; }
```
Next, we'll create the method we want to execute when the command is invoked:
```csharp
private async Task OnHello()
{
    await DialogService.Message("Hello!", "This is a dialog message");
}
```

Any class inheriting from the `ViewModel` base class will include the `DialogService` property, which will expose a service used to invoke dialogs from within the ViewModel. In our example, we are invoking a simple message.

All dialogs in Ganymede are [asyncronous](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/), meaning that you can properly await them as you would any task. This could be used for example to run some process in the background while waiting for the user to fill in some information on a dialog.

Now, we'll bind the command property to the method we just created on the instance constructor:
```csharp
public HelloViewModel()
{
    var cb = CommandBuilder.For(this);
    HelloCommand = cb.BuildSimple(OnHello);
}
```
The `CommandBuilder` class is available under the `TheXDS.Ganymede.Helpers` namespace. It's a handy helper class that allows you to simplify the syntax required to create commands, and it includes a variety of methods that will generate either [`SimpleCommand`](https://thexds.github.io/MCART/api/TheXDS.MCART.Component.SimpleCommand.html) or [`ObservingCommand`](https://thexds.github.io/MCART/api/TheXDS.MCART.Component.ObservingCommand.html) instances.

In this case, we are creating a `SimpleCommand` bound to the `OnHello` method. It's not necessary to use the `CommandBuilder` class, but it's helpful when defining commands that may require some configuration or want to execute operations using the integrated busy operation dialogs available on Ganymede. Still, Ganymede is compatible with any kind of `ICommand` you use.

In this example, we are defining the ViewModel inside the same UI project. This is not necessary, and you may define your ViewModels in a separate project if you wish to do so. In fact, this is a recommended practice when building larger applications, as it allows you to keep your UI code separate from your business logic, or even allow you to implement a single business logic base for multiple UI framework and/or platforms. In this case, however, we are keeping it simple and defining the ViewModel in the same project as the UI.

## Setting up a navigation host
The *Navigation Host* is the component that will actually present Views for your app. You may define and place it wherever you need to have navigation, but it's typically placed as the sole child of a `Window` (depending on your version of Avalonia and template used, normally the `MainWindow` or the `MainView` class).

In our case, we'll be opening the `MainView.axaml` file and setting the content as follows:
```xml
<UserControl
	xmlns="https://github.com/avaloniaui"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	xmlns:gn="using:TheXDS.Ganymede.Controls"
	xmlns:gn2="using:TheXDS.Ganymede.Markup"
    xmlns:vm="using:AvaloniaApp1"
    mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
    x:Class="AvaloniaApp1.MainView">
	<gn:NavigationHost
		DialogService="{gn2:NavDialogService}"
		Navigator="{gn2:NavService Home={x:Type vm:HelloViewModel}}"
		VisualResolver="{gn2:ConventionResolver}"/>
</UserControl>

```
The `NavigationHost` is a custom Avalonia control that receives instances of a *Dialog service*, *Navigator* and a *Visual resolver*.

### Dialog service
In our example, we're setting the dialog service to the `NavDialogService` markup extension. This will set up the host to use a dialog service that uses ViewModels and Views in a navigation stack as an overlay on top of your content.

### Navigator
This is the main navigation component. In this case, includes a navigation stack that will contain the active ViewModels and expose the one at the top of the stack. There will be other types of navigation components (for example, tabbed navigation) in the future.

The `NavService` markup extension allows you to specify a ViewModel to be used as a home page for the stack using the `Home` property. This will be the ViewModel presented by default when the stack is empty. It supports setting the home ViewModel by specifying either its type, name or by referencing an actual `IViewModel` instance. You might also leave this unset (or set to `{x:Null}`) in case you want to present nothing when the stack is empty.

### Visual resolver
The visual resolver is the component in charge of obtaining the view to be presented for a given ViewModel. In our case, it's a `ConventionVisualResolver` (using the `ConventionResolver` markup extenison). This resolver does not require additional setup, but requires Views and ViewModels to have matching names, differring only in the `View`/`ViewModel` suffix.

Other resolvers are available on Ganymede, like the `DictionaryVisualResolver` which requires explicit ViewModel to View mapping, or the `VisualResolverStack` class which allows you to specify a collection of resolers to be used sequentially when trying to resolve a view (useful when using a dictionary resolver and providing a convention resolver as a fallback)

Please note that using convention resolution will make your app incompatible with [assembly trimming](https://learn.microsoft.com/en-us/dotnet/core/deploying/trimming/trim-self-contained), due to the convention resolver needing to scan the AppDomain for a view with the same name as the ViewModel. If you don't plan on distributing your app as a self-contained/trimmed app, you can use convention resolution without any problems.

You can also implement your own resolver by implementing the `IVisualResolver<T>` interface from the `TheXDS.Ganymede.Types.Base` namespace.

## Implementing the View
Finally, we'll implement the view to use for `HelloViewModel`, which is going to be an Avalonia `UserControl`. As we are using the convention resolver, we must use a name that matches the ViewModel, so add a new `UserControl` named `HelloView.axaml`.

In our case, this is going to be a simple user control with bindings to ViewModel properties, common to apps using the MVVM pattern:

```xml
<UserControl
    x:Class="AvaloniaApp1.HelloView"
	xmlns="https://github.com/avaloniaui"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:vm="using:AvaloniaApp1"
    mc:Ignorable="d"
    d:DataContext="{d:DesignInstance Type=local:HelloViewModel}">
    <Design.DataContext>
		<vm:HelloViewModel />
	</Design.DataContext>
    <Button
        Command="{Binding HelloCommand, Mode=OneWay}"
        Content="Click me"
        Width="120" Height="30"/>
</UserControl>
```

## Conclusion
After defining the ViewModels and Views, we can run our app. When you execute it, you'll see a window containing a button. In reality, this is the navigation stack being presented on the main window, which will navigate to the *home page* as set up on the navigation stack by resolving the corresponding View for the ViewModel.

When clicking on the `Click me` button, the hello command gets invoked, and a dialog message is presented on the UI. As you can see, this is a custom dialog that lives within the main window, instead of being a regular Windows `MessageBox` dialog. This paradigm can translate well when porting your apps to other platforms (there's some work in progress already to bring support for Xamarin and Uno Platform) and have a uniform experience on platforms that do not support additional separate windows (like web and mobile platforms).
# Getting started
To use Ganymede, it's important to understand the concepts used on it, as well as the resulting architecture of an App built with Ganymede.

## Application structure
When creating an App with Ganymede, there's a few layers doing a specific job that will provide the required elements that conform the UI.

Ganymede is built to take advantage of the MVVM paradigm. As such, the basis for all bussiness logic definition is the `ViewModel` class. Its base type is the `ViewModelBase` class from [MCART.Mvvm](https://github.com/TheXDS/MCART), and it includes some properties that expose UI services to the logic defined on its derivates.

Next is of course the UI services. Right now, Ganymede provides two kinds of UI services: **navigation** and **dialogs**.

The topmost layer is the navigation host. Its job is to present the Views on the UI, as well as linking the rest of the UI services to expose to the hosted ViewModels. It takes references to a dialog service, a navigator and a visual resolver.

Abstracted away from the ViewModels is the **visual resolver**. It's in charge of returning the view to be presented based on which ViewModel is currently active on the navigation host.

## Undestanding `ViewModels`
As per the MVVM paradigm, a *ViewModel* is a class that defines the bussiness logic independently of the UI framework to be used by an app. This allows for code reutilization when creating an app that will target different platforms.

In order to provide interactivity with the user, a ViewModel exposes properties for all of its inputs and outputs. Then, a *View* built using an UI framework (such as WPF) could use controls that *Bind* to these properties and allow the state of the ViewModel to be controlled.

This of course presents some challenges, namely the interaction with the user in terms of dialogs and navigation. Each platform may have its own set of quirks and calling conventions. Also, by norm, a ViewModel should **NOT** know anything about the details of its target platform (for example, a ViewModel should not be calling methods from the `MessageBox` class in WPF).

With that in mind, if abstracted correctly, a ViewModel may access UI services if those are defined in a platform agnostic interface and passed as an injectable dependency.
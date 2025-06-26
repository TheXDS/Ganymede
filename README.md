# Ganymede

[![CodeFactor](https://www.codefactor.io/repository/github/thexds/ganymede/badge)](https://www.codefactor.io/repository/github/thexds/ganymede)
[![Build Ganymede](https://github.com/TheXDS/Ganymede/actions/workflows/build.yml/badge.svg)](https://github.com/TheXDS/Ganymede/actions/workflows/build.yml)
[![Publish Ganymede](https://github.com/TheXDS/Ganymede/actions/workflows/publish.yml/badge.svg)](https://github.com/TheXDS/Ganymede/actions/workflows/publish.yml)
[![Issues](https://img.shields.io/github/issues/TheXDS/Ganymede)](https://github.com/TheXDS/Ganymede/issues)
[![MIT](https://img.shields.io/github/license/TheXDS/Ganymede)](https://mit-license.org/)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/TheXDS/Ganymede)

Ganymede is an app-template using common MVVM-frameworks. It includes helpers for navigation, base ViewModel classes, custom dialog services and common entry points. You just need to provide your actual ViewModels and views, not having to worry about implementing auxiliary UI services for them.

## Releases
Release | Link
--- | ---
Latest stable version: | [![Stable Version](https://img.shields.io/nuget/v/TheXDS.Ganymede)](https://www.nuget.org/packages/TheXDS.Ganymede/)
Latest development version: | [![Development version](https://img.shields.io/nuget/vpre/TheXDS.Ganymede)](https://www.nuget.org/stats/packages/TheXDS.Ganymede?groupby=Version)

**Package Manager**  
```sh
Install-Package TheXDS.Ganymede
```

**.NET CLI**  
```sh
dotnet add package TheXDS.Ganymede
```

**Paket CLI**  
```sh
paket add TheXDS.Ganymede
```

**Package reference**  
```xml
<PackageReference Include="TheXDS.Ganymede" Version="1.0.0" />
```

## Building
Ganymede can be built on any platform or CI environment supported by dotnet.

### Prerequisites
- [.Net SDK 8.0](https://dotnet.microsoft.com/).

### Build Ganymede
```sh
dotnet build ./src/Ganymede.sln
```
The resulting binaries will be in the `./Build/bin` directory.

## Usage
Ganymede provides a few library implementations to build apps on different frameworks. As of now, a library for [WPF](https://github.com/dotnet/wpf) has been implemented in an experimental state, one for [Avalonia UI](https://www.avaloniaui.net/) is in very early stages of development and other UI frameworks are under consideration.

### Usage (With WPF bindings)
To use the WPF implementation of Ganymede, you can install the [`TheXDS.Ganymede.Wpf`](https://www.nuget.org/packages/TheXDS.Ganymede.Wpf/) NuGet package.

#### TL;DR for WPF

- Create a new .NET 8 WPF project. Make sure that the project targets at least `net8.0-windows10.0.19041`
- Install `TheXDS.Ganymede.Wpf` on the project.
- On `App.xaml`:
    ``` xaml
    <Application
        x:Class="WpfApp1.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:gn="http://schemas.thexds.local/ganymede"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        StartupUri="MainWindow.xaml">
        <Application.Resources>
            <ResourceDictionary>
                <ResourceDictionary.MergedDictionaries>
                    <gn:GanymedeDictionary/>
                </ResourceDictionary.MergedDictionaries>
            </ResourceDictionary>
        </Application.Resources>
    </Application>
    ```
- On `MainWindow.xaml`:  
    ```xml
    <Window
        x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:gn="http://schemas.thexds.local/ganymede"
        xmlns:local="clr-namespace:WpfApp1"
        Width="500" Height="400" >
        <gn:NavigationHost
            DialogService="{gn:WpfNavDialogService}"
            Navigator="{gn:NavService Home={x:Type local:TestViewModel}}"
            VisualResolver="{gn:ConventionResolver}"/>
    </Window>
    ```
- On `TestViewModel.cs` (new file):  
    ```csharp
    using System.Windows.Input;
    using TheXDS.Ganymede.Helpers;
    using TheXDS.Ganymede.Types.Base;
    
    namespace WpfApp1;
    
    public class TestViewModel : ViewModel
    {
        public ICommand HelloCommand { get; }

        public TestViewModel()
        {
            var cb = CommandBuilder.For(this);
            HelloCommand = cb.BuildSimple(OnHello);
        }

        private async Task OnHello()
        {
            await DialogService.Message("Hello!", "This is a dialog message");
        }
    }
    ```
- On `TestView.xaml` (new user control):  
    ```xml
    <UserControl
        x:Class="WpfApp1.TestView"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
        mc:Ignorable="d"
        d:DataContext="{d:DesignInstance Type=local:TestViewModel}"
        xmlns:local="clr-namespace:WpfApp1">
        <Button
            Command="{Binding HelloCommand, Mode=OneWay}"
            Content="Click me"
            Width="120" Height="30"/>
    </UserControl>
    ```
## Contribute
[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/W7W415UCHY)

If `Ganymede` is useful to you, or if you're interested in donating to sponsor the project, feel free to to a donation via [PayPal](https://paypal.me/thexds), [Ko-fi](https://ko-fi.com/W7W415UCHY) or just contact me directly.

Sadly, I cannot offer other means of sending donations as of right now due to my country (Honduras) not being supported by almost any platform.

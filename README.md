# Ganymede

[![CodeFactor](https://www.codefactor.io/repository/github/thexds/ganymede/badge)](https://www.codefactor.io/repository/github/thexds/ganymede)
[![Build Ganymede](https://github.com/TheXDS/Ganymede/actions/workflows/build.yml/badge.svg)](https://github.com/TheXDS/Ganymede/actions/workflows/build.yml)
[![Publish Ganymede](https://github.com/TheXDS/Ganymede/actions/workflows/publish.yml/badge.svg)](https://github.com/TheXDS/Ganymede/actions/workflows/publish.yml)
[![Issues](https://img.shields.io/github/issues/TheXDS/Ganymede)](https://github.com/TheXDS/Ganymede/issues)
[![codecov](https://codecov.io/gh/TheXDS/Ganymede/branch/master/graph/badge.svg?token=Rve4awcyup)](https://codecov.io/gh/TheXDS/Ganymede)
[![MIT](https://img.shields.io/github/license/TheXDS/Ganymede)](https://mit-license.org/)

Ganymede is an app-template using common MVVM-frameworks. It includes helpers for navigation, base ViewModel classes, custom dialog services and common entry points. You just need to provide your actual ViewModels and views, not having to worry about implementing auxiliary UI services for them.

## Releases
Release | Link
--- | ---
Latest public release: | [![Latest stable NuGet package](https://buildstats.info/nuget/TheXDS.Ganymede)](https://www.nuget.org/packages/TheXDS.Ganymede/)  
Latest development release: | [![Latest development NuGet package](https://buildstats.info/nuget/TheXDS.Ganymede?includePreReleases=true)](https://www.nuget.org/packages/TheXDS.Ganymede/)

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

**C# interactive window (CSI)**  
```
#r "nuget: TheXDS.Ganymede, 1.0.0"
```

## Building
Ganymede can be built on any platform or CI environment supported by dotnet.

### Prerequisites
- [.Net SDK 6.0](https://dotnet.microsoft.com/).

### Build Ganymede
```sh
dotnet build ./src/Ganymede.sln
```
The resulting binaries will be in the `./Build/bin` directory.

### Testing Ganymede
```sh
dotnet test ./src/Ganymede.sln
```

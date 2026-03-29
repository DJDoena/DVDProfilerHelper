# DVDProfiler.Helper

A .NET helper library providing common functionality for applications that integrate with DVD Profiler by Invelos LLC.

## About DVD Profiler

DVD Profiler is a comprehensive DVD collection management software developed by Invelos LLC. For more information, visit [invelos.com](https://www.invelos.com).

## Overview

DVDProfiler.Helper is a utility library that simplifies the development of plugins and extensions for DVD Profiler. It provides reusable components for common tasks such as registry access, online version checking, UI dialogs, and profile photo management.

## Features

- **Registry Access**: Simplified reading and writing of application settings to the Windows Registry
- **Online Version Checking**: Automatic checking for new versions of your plugin with user notification dialogs
- **Profile Photo Helpers**: Utilities for generating file names from cast and crew credit information
- **Standard UI Components**:
  - About Box dialog
  - Help Form with embedded browser
  - Progress Window for long-running operations
  - New Version Available notification dialog
- **Helper Utilities**:
  - XML serialization helpers
  - UTF-8 string writer
  - Enhanced COM exception handling
  - TripleDES encryption support
  - Assembly loading utilities

## Installation

Install via NuGet Package Manager:

```
Install-Package DoenaSoft.DVDProfiler.Helper
```

Or via .NET CLI:

```
dotnet add package DoenaSoft.DVDProfiler.Helper
```

## Target Frameworks

This library supports multiple .NET versions:

- .NET Framework 4.7.2
- .NET 10.0 (Windows)

## Usage

### Initialize Registry Access

```csharp
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

RegistryAccess.Init("YourCompany", "YourProduct");
```

### Check for New Version Online

```csharp
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

OnlineAccess.Init("YourCompany", "YourProduct");
OnlineAccess.CheckForNewVersion(
    "https://yoursite.com/versions.xml",
    parent: this,
    linkAnchor: "YourProduct",
    assembly: Assembly.GetExecutingAssembly()
);
```

### Generate Profile Photo File Names

```csharp
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

string fileName = ProfilePhotoHelper.FileNameFromCreditName(
    firstName: "John",
    middleName: "",
    lastName: "Doe",
    birthYear: 1970
);
// Result: "Doe_John__1970"
```

### Clean File Names

```csharp
string cleanName = ProfilePhotoHelper.CleanupFilename("Invalid:Name?");
// Removes or replaces invalid file name characters
```

### Show About Dialog

```csharp
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

var aboutBox = new AboutBox(Assembly.GetExecutingAssembly());
aboutBox.ShowDialog();
```

## Dependencies

- DoenaSoft.AbstractionLayer.Web.Default (1.0.0)
- DoenaSoft.ToolBox (3.0.3)
- System.Net.Http (4.3.4)
- System.Resources.Extensions (10.0.5)
- System.Runtime.CompilerServices.Unsafe (6.1.2)

## License

This project is licensed under the MIT License.

## Author

DJ Doena (Doena Soft.)

## Repository

Source code: [https://github.com/DJDoena/DVDProfilerHelper](https://github.com/DJDoena/DVDProfilerHelper)

## Copyright

Copyright (c) Doena Soft. 2012 - 2026

## Support

For issues, feature requests, or contributions, please visit the GitHub repository.

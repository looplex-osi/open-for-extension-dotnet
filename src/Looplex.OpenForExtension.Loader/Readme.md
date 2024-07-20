# Looplex.OpenForExtension.Loader

## Description

**Looplex.OpenForExtension.Loader** provides a default loader for dynamically loading assemblies that contain plugins (implementing `IPlugin` from `Looplex.OpenForExtension.Abstractions`). This enables flexible and modular extension of your application.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [License](#license)
- [Contact](#contact)

## Installation

To install Looplex.OpenForExtension.Loader, use the following command:

```bash
dotnet add package Looplex.OpenForExtension.Loader
```

## Usage

```csharp
(new PluginLoader())
    .LoadPlugins("path to assemblies", ["Foo.Bar", "Foo2.Bar2"])
    .ToList()
```
The second argument is a list composed of "{className}.{methodName}". Only plugins instances that are subscribed to an element of this list will be loaded.

For more detailed examples, refer to the samples project on the [repository page](https://github.com/looplex-osi/open-for-extension-dotnet) .

## License

This project is licensed under the Looplex Limited Public License. Feel free to edit and distribute this template as you like.

See [`LICENSE.md`](/LICENSE.md) for more information.

## Contact

If you have any questions or feedback, feel free to reach out:

	• Email: guilherme.camara@outlook.com.br
	• Email: guilherme.camara@looplex.com.br
	• Email: dev@looplex.com.br

# Looplex.OpenForExtension.Abstractions

## Description

Looplex.OpenForExtension.Abstractions offers a robust set of interfaces designed to facilitate the implementation of kernel architecture in projects. It embodies the open-for-extension principle, providing a concrete and flexible tool for extending and customizing your application’s core functionality.

## Table of Contents

- [Installation](#installation)
- [Commands](#commands)
- [Usage](#usage)
- [License](#license)
- [Contact](#contact)

## Installation

To install Looplex.OpenForExtension.Abstractions, use the following command:

```bash
dotnet add package Looplex.OpenForExtension.Abstractions
```

## Commands

Commands are the extension points available in you kernel. 

The available interfaces define the possible extension points for the plugins. The following shows how the default commands may be used:

	•	IHandleInput: Handles kernel input.
	•	IValidateInput: Validates kernel input.
	•	IDefineRoles: Defines kernel roles.
	•	IBind: Binds kernel events.
	•	IBeforeAction: Executes logic before the default action.
	•	IAfterAction: Executes logic after the default action.
	•	IReleaseUnmanagedResources: Releases unmanaged resources (dispose).


## Usage

Here’s an example of how to use the interfaces provided by Looplex.OpenForExtension.Abstractions in your project:

```csharp
public async Task ServiceMethod(IContext context, CancellationToken cancellationToken)
{
    var example = context.State.Example; // Kernel input handling
    await context.Plugins.ExecuteAsync<IHandleInput>(context, cancellationToken);
    
    Validate(example); // Kernel input validation
    await context.Plugins.ExecuteAsync<IValidateInput>(context, cancellationToken);

    context.Roles["Role"] = example; // Kernel roles definition
    await context.Plugins.ExecuteAsync<IDefineActors>(context, cancellationToken);

    context.Roles["Role"].On("event", (sender, e) => { }); // Kernel event binding (`example` instance implements IHasEventHandlerTrait)
    await context.Plugins.ExecuteAsync<IBind>(context, cancellationToken); 

    await context.Plugins.ExecuteAsync<IBeforeAction>(context, cancellationToken);

    if (!context.SkipDefaultAction)
    {
        // Execute service kernel default action
    }

    await context.Plugins.ExecuteAsync<IAfterAction>(context, cancellationToken); 

    await context.Plugins.ExecuteAsync<IReleaseUnmanagedResources>(context, cancellationToken); 
}
```

For more detailed examples, refer to the samples project on the [repository page](https://github.com/looplex-osi/open-for-extension-dotnet) .

## License

This project is licensed under the Looplex Limited Public License. Feel free to edit and distribute this template as you like.

See [`LICENSE.md`](/LICENSE.md) for more information.

## Contact

If you have any questions or feedback, feel free to reach out:

	•	Email: guilherme.camara@outlook.com.br
	•	Email: guilherme.camara@looplex.com.br
	•	Email: dev@looplex.com.br

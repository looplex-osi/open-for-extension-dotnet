# Looplex.OpenForExtension

## Description

This is a simple framework that allows an easy aplication of the Open for Extension SOLID principle in .Net projects. 

## The IPlugin interface 

This is the contract to implement a plugin. It is composed of N commands of a specific type.

### The AbstracPlugin class

You can use inherit this abstract class instead of using the IPlugin interface to take advantage of the already implemented Execute and ExecuteAsync methods.

## The ICommand interface

This is the contract to implement the commands (extended behaviour) of your plugin class.

## The IPluginContext interface

This is the context in which you can define configuration, settings and shared data between the plugins and the application.

## The AbstractPluginContext interface

You can use inherit this abstract class instead of using the IPluginContext interface to take advantage of the already defined default properties.

## The PluginExtensionMethods class

This is a class that contains extension methods to allow calling the Execute or ExecuteAsync method directly from a IEnumerable of IPlugin.

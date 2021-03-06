# AllTdL Prism App - WPF

## Features
- WPF .NETCore 3.1
- Prism Modularity
- Real-Time stock quotes from both [Cnbc] & [Webull]

## Configuration

### Modules

##### App.config
```xml
<modules>
    <module assemblyFile="ModuleManager.dll" moduleType="Module.Manager.ManagerModule, ModuleManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="ModuleManager" startupLoaded="True" />
    <module assemblyFile="ModuleA.dll" moduleType="ModuleA.ModuleAModule, ModuleA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="ModuleAModule" startupLoaded="True" />
    <module assemblyFile="allTdL.Mods.ToDo.ToDoModule.dll" moduleType="allTdL.Mods.ToDo.ToDoModule, allTdL.Mods.ToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="ToDoModule" startupLoaded="True" />
    <module assemblyFile="ModuleStocks.dll" moduleType="ModuleStocks.ModuleStocksModule, ModuleStocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="ModuleStocksModule" startupLoaded="True" />
</modules>
```

| Assembly File | Module Type | Startup Loaded |
| ----------- | ----------- | ----------- |
| ModuleManager.dll | Module.Manager.ManagerModule, ModuleManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null | True
| ModuleA.dll | ModuleA.ModuleAModule, ModuleA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null | True
| allTdL.Mods.ToDo.ToDoModule.dll | allTdL.Mods.ToDo.ToDoModule, allTdL.Mods.ToDo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null | True
| ModuleStocks.dll | ModuleStocks.ModuleStocksModule, ModuleStocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null | True

##### Project Reference
Add modules to the override ConfigureModuleCatalog method in the app.xml.cs file.
```c
protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
{
    moduleCatalog.AddModule<ModuleFileBrowserModule>();
    //moduleCatalog.AddModule<SomeOtherModule>();
}
```
## Shell Window

## Region Names
- ShellContentRegion
    1. ContentRegion
    2. LeftRegion
    3. RightRegion

## Tech

allTdL uses a number of open source projects to work properly:

- [Prism] - Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF, Xamarin Forms, and Uno / Win UI Applications
- [LewisFam.Common] - A simple C# common library. Free and open source.
- [LewisFam.Stocks] - A Stock and Option prices API.

And of course allTdL itself is open source with a [public repository][allTdL] on GitHub.

## License

MIT

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)

   [dotnet.core]: <https://github.com/dotnet/core>
   [allTdL Prism App]: <https://github.com/Lewis-Fam/super-journey/tree/main/src/wpf/prism>
   [allTdL]: <https://github.com/Lewis-Fam/super-journey/tree/main/src/wpf/prism>
   [CNbc]: <https://cnbc.com>
   [Webull]: <https://webull.com>
   [Prism]: <https://github.com/PrismLibrary>
   [LewisFam.Common]: <https://github.com/Lewis-Fam/LewisFam.Common>
   [LewisFam.Stocks]: <https://github.com/Lewis-Fam/Stocks/tree/main/src/LewisFam.Stocks>

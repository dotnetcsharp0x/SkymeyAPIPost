# Steps to run the project #

## 1 step ##

Before starting the project, put the following file into the project directory:

appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConfigPath": "absolute path to the main appsettings.json"
}
```
```csharp
/*
"ConfigPath": "absolute path to the main appsettings.json" = absolute path to your main settings. Example: C:\\projects\\Skymey\\Libs\\SkymeyLibs\\SkymeyLibs
*/

```

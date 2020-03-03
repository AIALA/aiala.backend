# aiala.backend
Backend services, business logic, persistence and authentication for AIALA

## Project structure
AIALA backend constist of two separate web applications.

_Backend_ and is responsible to provide all RESTful api services, business logic and persistence.

_Token server_ is responsible for authentication and issuing tokens to use against _backend_. Therefore [IdentityServer4](https://identityserver4.readthedocs.io/en/latest/) will be used.

## How to build
Both AIALA backend projects are build against ASP.NET Core 2.1 and are using several 3rd party libraries and components.
* Install the [required .NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/2.1)
* Ensure [NuGet](https://www.nuget.org/) is installed and configured 
* Connect to xappido Portal package feed ([ask for access and license](mailto:aiala@xappido.com))

```
dotnet restore
dotnet build
dotnet run
```

## Package feed
Configure package feeds (especially xappido Portal package feed) either globally or locally within aiala.backend.

Add xappido feed globally by using `nuget cli` and authorize by using credential provider
> `nuget sources Add -Name "xappido" -Source https://xappido.pkgs.visualstudio.com/`

If your prefer configure feed locally, use Visual Studios' NuGet Package Manager or add a nuget.config file to your project, in the same folder as .sln file.

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="xappido" value="https://xappido.pkgs.visualstudio.com/..." />
  </packageSources>
</configuration>
```

## Configuration
All required options for _backend_ and as well for _token server_ are within their project located on file `appsettings.json`.

While development time it's recommended to use [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#how-the-secret-manager-tool-works) to store sensitive data within `secrets.json` file.

## How to run
COnfigure both projects _backend_ and _token server_ as startup projects and run those. Launch options are stored on `Properties/launchSettings.json` and are defined as default to:
* Backend > http://localhost:5500
* Token Server > http://localhost:5500

## Documentation
See mentioned documentation for any further information. If you like to use AIALA within your organisation feel free and get in touch with [AIALA Project Team](mailto:aiala@xappido.com).

Backend API documentation will be genereated at runtime and is available at http://localhost:5500/api/docs.



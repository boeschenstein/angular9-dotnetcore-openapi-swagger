# Auto-Generate Backend-Services in Angular 9 for .NET Core 3.1 Backend

## Install Swagger/OpenApi in new WebAPI Project

Current Versions
- Visual Studio 2019 16.5.2
- .NET core 3.1
- SwashBuckle 5.3.1
- Angular 9.1

### Create new .NET WebAPI project

create a folder "c:\temp\mySwaggerDemo\backend\mywebapi"

create a new webapi project

````cmd
dotnet new webapi -n mywebapi
```

open file explorer

````cmd
explorer .
```

### Add swashbuckle

open project in Visual Studio
open Package Manager Console (Tools -> Nuget Package Manager -> Package Manager Console)

```powershell
Install-Package Swashbuckle.AspNetCore
```

### Configure Swagger/OpenApi

configure Swagger in Startup.cs

``` c#
using Microsoft.OpenApi.Models;
```

Register swagger generator (simple):

``` c#
public void ConfigureServices(IServiceCollection services)
{
	services.AddControllers();

	// Register the Swagger generator, defining 1 or more Swagger documents
	services.AddSwaggerGen(c =>
	{
		c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
	});
}
```

Register swagger generator (extended version with more details):

``` c#
// Register the Swagger generator, defining 1 or more Swagger documents
services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "ToDo API",
		Description = "A simple example ASP.NET Core Web API",
		TermsOfService = new Uri("https://boeschenstein.net/terms"),
		Contact = new OpenApiContact
		{
			Name = "Patrik Böschenstein",
			Email = string.Empty,
			Url = new Uri("https://twitter.com/patrikbo"),
		},
		License = new OpenApiLicense
		{
			Name = "Use under LICX",
			Url = new Uri("https://boeschenstein.net/license"),
		}
	});
});
```

Activate swagger in development mode:

```c#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	if (env.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();

		// Enable middleware to serve generated Swagger as a JSON endpoint.
		app.UseSwagger();

		// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
		// specifying the Swagger JSON endpoint.
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		});
	}

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthorization();

	app.UseEndpoints(endpoints =>
	{
		endpoints.MapControllers();
	});
}

```

Toolbar: switch startup from "IIS Express" to "mywebapi"
Run application
Open swagger endpoint

``` html
https://localhost:5000/swagger
``` 

### Check the swagger document and API

``` html

ToDo API v1 OAS3

/swagger/v1/swagger.json

A simple example ASP.NET Core Web API
Terms of service
Patrik Böschenstein - Website
Use under LICX

```

Execute the WeatherForecast api: "Get", "Try it out", "Execute"
you should see the data from the API

## Angular 9

### Create new Angular project

create a folder "mySwaggerDemo\frontend" and go to it

``` cmd
md c:\temp\mySwaggerDemo\frontend
cd c:\temp\mySwaggerDemo\frontend
``` 

make sure you have the latest LTS of node/npm installed: <https://nodejs.org/en/download/>
Install angular cli globally. Details see <https://angular.io/guide/setup-local>

``` cmd
npm install -g @angular/cli
``` 

Create new Angular app

``` cmd
ng new myFrontend
``` 

Check new Angular app

``` cmd
cd myFrontend
ng serve --open
``` 

## OpenAPI

### Create backend

This step generates a tgz file from the webapi backend. This needs to be run whenever your WebApi did change.

``` cmd
npm install @openapitools/openapi-generator-cli -g
```

If you don't have it yete, install Java for openapi-generator: https://java.com/download/

``` cmd
openapi-generator generate -g typescript-angular -i http://localhost:5000/swagger/v1/swagger.json -o backend --additional-properties npmName=@backend/api,snapshot=true,ngVersion=9.1.0
```

openapi-generator generate -g typescript-angular -i http://localhost:5000/swagger/v1/swagger.json -o backendApi 
openapi-generator generate -g typescript-angular -i http://localhost:5000/swagger/v1/swagger.json -o src\backendApi 

to avoid npm error, change version in package.json

``` json
"version": "1.0.0",
```

build backend

``` cmd
npm install
npm run build  
npm pack
```

### Adopt backend in Angular project

This step installs the tgz file and is needed only once.

``` cmd
c:\temp\mySwaggerDemo\frontend>npm install .\backend\backend-api-1.0.0.tgz
```

## Links
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle
- https://github.com/domaindrivendev/Swashbuckle.AspNetCore
- https://github.com/OpenAPITools/openapi-generator
- https://github.com/swagger-api/swagger-ui
- https://swagger.io/specification/
- https://angular.io/

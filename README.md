# Xpand

## Implementation

### API

For the API I chose to use .Net 7, SQL Server and Entity Framework Core. I used the basic Web API template and added the following NuGet packages:

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.AspNetCore.Authentication.JwtBearer

### Web

For the client I chose to use Vue3 and Typescript. For creating the project I used the Vite template, to which I added Tailwind CSS.

## Running the project

### API

To run the API, you need to have SQL Server installed and running. The connection string is located in the `appsettings.json` file. The default is `Server=localhost;Database=Xpand;Trusted_Connection=True;`. You can change the connection string depending on your SQL Server configuration.

To run the API, you need to run the following commands in the `Xpand.Api` folder:

- `dotnet restore`
- `dotnet ef database update`
- `dotnet run`

### Client

To run the client, you need to run the following commands in the `Xpand.Web` folder:

- `npm install`
- `npm run dev`

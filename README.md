# OscApp
This is a generic project template which we can use to fork whenever we start new web application projects at EventMAP.  It features the Osc.Db submodule for the shared data set.

## Important
Please do not push changes intended for your new project to this repo.  Only changes to the wireframe should be pushed here.  To start your project, create a mirror copy of this repo, following these steps: https://help.github.com/articles/duplicating-a-repository/

## Prerequisites
- .NET Core 2.0 SDK
- Node & NPM
- Webpack and webpack-dev-server `npm install -g webpack` `npm install -g webpack-dev-server`
- Development database server
  - If you're on Windows, SQL Server would be the easiest to get going with.  You can definitely use other providers such as Sqlite, MySql, Postgresql etc.  You'll just need to install the provider with Nuget and modify the config in appsettings.json and Startup.cs.

## Quick start
- Run `npm install` in **OscApp.Web**
- Run `npm start` in **OscApp.Web**
- Run the database migration; in the Package Manager Console run `Update-Database` with **Osc.Db** targetted, or using the dotnet cli
- Run `git submodule init` then `git submodule update` in repo's root
- Run `git checkout master` in **Osc.Db**
- Debug TimetablerTm.Web in Visual Studio or using the dotnet cli

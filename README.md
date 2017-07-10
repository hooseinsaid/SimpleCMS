# SimpleCMS


Welcome to the SimpleCMS wiki! ASP.Net CMS Application

1. Description

This is the basic functionality of the CMS system: Admin account:

Email: admin@aubg.edu

Password: “PassworD1”

Admin can:

    Create users
    Edit users’ data
    Delete users

Users can:

    Create news stories
    Edit their news stories
    Delete their news stories

The second application can:

    List news stories only if the date of creation has passed
    List them chronologically, newest first
    Details page where you can read the news story, see the Author, keywords etc.

I implemented the application in Asp.Net using C#. I have 2 projects in the solution. One is for the CMS application and the other one is for outputting the news stories from the database.

2. Database I have used Code-First approach with Migrations in order to create the database. Here is a representation of the database model:

3. Instructions I have built the application using Visual Studio 2015 and the LocalDb is an instance of MS SQL Server Express 2014. I recommend opening the applications with the same Visual Studio 2015 and having MS SQL Server Express 2014 installed, otherwise there might be some inconsistencies with the database version. The two application share the same .mdf file in order to work with the same LocalDB. Ideally I would have created an SQL Server Express database and set the two projects connect to the database but since it is a simple application I decided to just share the .mdf file. In order to share the .mdf file between the two projects I had to hardcode the path of the .mdf file in the connection string of the “NewsApp” project. So here are the two connection strings:

    “CMS” project’s connection string in Web.config <connectionStrings> <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-CMS-20160508024324.mdf;Initial Catalog=aspnet-CMS-20160508024324;Integrated Security=True" providerName="System.Data.SqlClient" /> </connectionStrings>

    “NewsApp” project’s connection string in Web.config <connectionStrings> <add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\klast\OneDrive\Documents\Visual Studio 2015\Projects\CMS\CMS\App_Data\aspnet-CMS-20160508024324.mdf;Initial Catalog=aspnet-CMS-20160508024324;Integrated Security=True" providerName="System.Data.SqlClient" /> </connectionStrings>

So, in order to run the database you will have to change the path of the .mdf file in the Web.config file of the “NewsApp” project. You can see the path like this: Right click on the .mdf file located in the App_Data folder of “CMS” project -> Click Properties -> Full Path

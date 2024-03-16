# Introduction 
Microservice .Net7 API template.
<br>
Provides a base structure for a microservice.

It consist of the following projects:

 - WebApi
 - Communication
 - Persistence
 - Persistence.Initializer

<br>

# Install template from repository
In order to install the template on your local machine perform the following steps:
1.	Clone the project locally - 
2.	Open powershell (or windows terminal) and navigate to the projects directory
3.	Inside projects directory (C:\Users\\{path}\EmployeesAPI) you will see .sln file. Run the following commands:
 - `dotnet new install . --force`  # This command will install the template locally
 - `dotnet new list` # This command will show all available templates. You will be able to see "tbswebapi" template in the list
 <br>
 Close the terminal!`

<br>

# Install template from Nuget package
In order to install the template on your local machine from TbsGet nuget package perform the following steps:
1. Go to: https://tlproddev.visualstudio.com/TBS-Development-Toolkit/_artifacts/feed/TBSGet/NuGet/EmployeesAPI/
2. Download nuget package
3. Go to the directory where `EmployeesAPI.1.0.x.nupkg` is downloaded
4. Open terminal and run the following command:
<br>
`dotnet new -i .\EmployeesAPI.1.0.x.nupkg --force`

<br>

 > IMPORTANT! Once the template is installed on your local machine these steps are no longer needed.

<br>

# Create new projects
Open new terminal in the location where you want to create the new project and run the following command:
 - `dotnet new tbswebapi --dbContext {dbContextName}` # This command will create new project based on "`tbswebapi`" template.

 > When a new project is created, a few pre-build actions will be executed. Тhey must be confirmed.
 <br>

 Parameter `--dbContext {dbContextName}` is used to setup the dbContext for the project. This parameter `mandatory`.
 
 For example:

`dotnet new tbswebapi -o Omnia.Camera --dbContext CameraDbContext --communication-client CameraClient`
 <br>
 will create folder `Omnia.Camera` with projects: `Omnia.Camera.WebApi`, `Omnia.Camera.Persistence`, etc... and include all deployment files and communication client.
 <br>

 > If you want to create new project inside existing folder `-o Omnia.Camera` parameter is not needed and the name of the folder will be used to name the new service!
 <br>
 `dotnet new tbswebapi --dbContext CameraDbContext --communication-client CameraClient`
 <br>
 This command (without `-o` param) will create new solution and all folders and components inside the directory where command is executed.

  > IMPORTANT! For MAC users: In order to run the scripts, you need to download and install PowerShell for macOS!
  <br>
https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-macos?view=powershell-7.3
  
<br>

# Parameter options
 - --dbContext - this parameter is used to setup the dbContext for the project. If missing, the project will be created with default "`{dbContextName}.cs`" context.
 <br>
 `dotnet new tbswebapi -o {projectName} --dbContext {dbContextName}`

  - communication-client - this parameter is used to setup the communication client in Communication project. If missing, the project will be created with default "`WebApiClient`".
 <br>
 `dotnet new tbswebapi -o {projectName} --dbContext {dbContextName} --communication-client {clientName}`

 - exclude-communication - this parameter is used if you want to create new webApi project without Communication Client.
 <br>
 `dotnet new tbswebapi -o {projectName} --dbContext {dbContextName} --exclude-communication`
 
 - exclude-deployment - this parameter is used if you want to exclude all the deploy files with "deploy" folder.
 <br>
 `dotnet new tbswebapi -o {projectName} --dbContext {dbContextName} --exclude-deployment`
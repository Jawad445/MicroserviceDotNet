# Description
    This is the basic .Net microservice project created for practice By following the Julio Casal tutorial on youtube
    Here is the link for this https://www.youtube.com/watch?v=ByYyk8eMG6c

# Docker Commands
    docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo    -----for mongo db stand alone
    docker-compose up                                                               -----for runing docker composed file
    docker-compose up                                                               -----for runing docker composed file

# Commands:
    dotnet clean                                                    ----- To clean the project
    dotnet build                                                    ----- To build the project
    dotnet pack -o ..\packages\                                     -----To Create a nuget package
    dotnet nuget add source D:\packages -n PersonalMachinePackages  -----To add nugetpackage manager from your local machine

# Readme

## ASP.NET 5

- DNVM - The .NET Version Manager

DNVM is a version manager tool for the command line. As its 
name implies, DNVM provides the functionality needed to
 configure your .NET runtime. We can use DNVM to specify 
which version of the .NET Execution Environment to use at 
the process, user, or machine level.

> dnvm - basic output
> dnvm list - list installed framework versions
> dnvm install latest - install latest version of libs
> dvnm upgrade - recommended option for upgrading tools
> dnvm use 1.0.0-beta4-11566 - select currect version

- DNX - The .NET Execution Environment

The .NET Execution Environment (DNX) is a software development kit (SDK) 
and runtime environment.

It provides a host process, CLR hosting logic and managed entry
point discovery.

- DNU - .NET Development Utilities

DNU is a command-line tool which provides a variety of utility 
functions to assist with development in ASP.NET. 

Most commonly, we will use 
DNU to install and manage library packages in our application, and/or 
to package and publish our own application.

> dnu restore - restore dependencies (basically from NuGet)

[More info at codeproject.com](http://www.codeproject.com/Articles/1005145/DNVM-DNX-and-DNU-Understanding-the-ASP-NET-Runtime)
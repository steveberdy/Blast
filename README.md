# Blast
Weird encoders that I've created.

## Building

Run `dotnet build`. This creates a folder named `src/nupkg` with the Nuget package.

Then, run `dotnet tool update -g blast --add-source src/nupkg` from the root of the project. This will install/update the tool as a global tool.

You can now run `blast` in your terminal. Run `blast -h` for help.

# build/build.ps1

param(
    [string]$configuration = "Release"
)

Write-Host "Starting build in $configuration configuration..."

dotnet restore ../YourSolutionName.sln
dotnet build ../YourSolutionName.sln --configuration $configuration --no-restore
dotnet test ../tests/YourProject.*.Tests/YourProject.*.Tests.csproj --configuration $configuration --no-build

Write-Host "Build and tests complete."

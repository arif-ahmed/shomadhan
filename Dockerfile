# Use .NET 8.0 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Shomadhan.sln", "."]
COPY ["src/Shomadhan.API/Shomadhan.API.csproj", "src/Shomadhan.API/"]
COPY ["src/Shomadhan.Application/Shomadhan.Application.csproj", "src/Shomadhan.Application/"]
COPY ["src/Shomadhan.Domain/Shomadhan.Domain.csproj", "src/Shomadhan.Domain/"]
COPY ["src/Shomadhan.Infrastructure/Shomadhan.Infrastructure.csproj", "src/Shomadhan.Infrastructure/"]
COPY ["tests/Shomadhan.API.Tests/Shomadhan.API.Tests.csproj", "tests/Shomadhan.API.Tests/"]
COPY ["tests/Shomadhan.Application.Tests/Shomadhan.Application.Tests.csproj", "tests/Shomadhan.Application.Tests/"]
COPY ["tests/Shomadhan.Domain.Tests/Shomadhan.Domain.Tests.csproj", "tests/Shomadhan.Domain.Tests/"]
COPY ["tests/Shomadhan.Infrastructure.Tests/Shomadhan.Infrastructure.Tests.csproj", "tests/Shomadhan.Infrastructure.Tests/"]


# Restore dependencies
RUN dotnet restore "Shomadhan.sln"

# Copy the rest of the source code
COPY . .

# Publish the API
WORKDIR "/src/src/Shomadhan.API"
RUN dotnet publish "Shomadhan.API.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Shomadhan.API.dll"]

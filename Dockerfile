# Use .NET 9.0 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Somadhan.sln", "."]
COPY ["src/Somadhan.API/Somadhan.API.csproj", "src/Somadhan.API/"]
COPY ["src/Somadhan.Application/Somadhan.Application.csproj", "src/Somadhan.Application/"]
COPY ["src/Somadhan.Domain/Somadhan.Domain.csproj", "src/Somadhan.Domain/"]
COPY ["src/Somadhan.Infrastructure/Somadhan.Infrastructure.csproj", "src/Somadhan.Infrastructure/"]
COPY ["tests/Somadhan.API.Tests/Somadhan.API.Tests.csproj", "tests/Somadhan.API.Tests/"]
COPY ["tests/Somadhan.Application.Tests/Somadhan.Application.Tests.csproj", "tests/Somadhan.Application.Tests/"]
COPY ["tests/Somadhan.Domain.Tests/Somadhan.Domain.Tests.csproj", "tests/Somadhan.Domain.Tests/"]
COPY ["tests/Somadhan.Infrastructure.Tests/Somadhan.Infrastructure.Tests.csproj", "tests/Somadhan.Infrastructure.Tests/"]

# Restore dependencies
RUN dotnet restore "Somadhan.sln"

# Copy the rest of the source code
COPY . .

# Publish the API
WORKDIR "/src/src/Somadhan.API"
RUN dotnet publish "Somadhan.API.csproj" -c Release -o /app/publish

# Runtime image using .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Somadhan.API.dll"]

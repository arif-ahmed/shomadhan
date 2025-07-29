# Somadhan

Somadhan is a multi-tenant application built with .NET 9, following a clean architecture that separates concerns into four main projects: Domain, Application, Infrastructure, and a web API. This structure ensures a modular, scalable, and maintainable codebase, making it an ideal starting point for more complex systems.

## Table of Contents

- [Somadhan](#somadhan)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Technologies](#technologies)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
  - [Running the Application](#running-the-application)
  - [Testing](#testing)
  - [API Documentation](#api-documentation)
  - [Contributing](#contributing)
  - [License](#license)

## Features

- **Multi-Tenancy Support:** Easily manage multiple tenants with isolated data and configurations.
- **Clean Architecture:** A well-structured solution with separate layers for domain, application logic, infrastructure, and API.
- **CQRS with MediatR:** Implements the Command Query Responsibility Segregation pattern for cleaner and more maintainable code.
- **Built with .NET 9:** Leverages the latest features and improvements from the .NET ecosystem.
- **Ready-to-use API:** Comes with a pre-configured API project, including Swagger for interactive documentation.

## Technologies

- **.NET 9:** The core framework for building the application.
- **ASP.NET Core:** For creating a robust and high-performance web API.
- **Entity Framework Core:** As the primary ORM for data access.
- **MediatR:** To implement the CQRS pattern.
- **FluentValidation:** For clear and concise validation rules.
- **Swagger:** For API documentation and testing.
- **xUnit & Moq:** For writing and running tests.

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or another compatible IDE
- [Git](https://git-scm.com/downloads)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/Somadhan.git
   cd Somadhan
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

## Running the Application

To run the application, you can either use the .NET CLI or Visual Studio.

- **Using the .NET CLI:**
  ```bash
  dotnet run --project src/Somadhan.API/Somadhan.API.csproj
  ```

- **Using Visual Studio:**
  1. Open `Somadhan.sln` in Visual Studio.
  2. Set `Somadhan.API` as the startup project.
  3. Press `F5` to run the application.

The API will be available at `http://localhost:5000` (or another port specified in the launch settings).

## Testing

To run the tests, execute the following command in the root directory:

```bash
dotnet test
```

## API Documentation

The API is documented using Swagger. Once the application is running, you can access the interactive documentation at `http://localhost:5000/swagger`.

## Contributing

Contributions are welcome! If you have any suggestions or find any issues, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

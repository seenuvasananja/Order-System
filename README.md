# OrderSystem

OrderSystem is a .NET 10 application for managing orders, inventory and basic customer workflows. This README covers setup, build, run, test, and contribution guidance for the repository located at `D:\PROJECTS\OrderSystem\`.

## Table of contents
- [Requirements](#requirements)
- [Getting started](#getting-started)
- [Build & run](#build--run)
- [Configuration](#configuration)
- [Testing](#testing)
- [Docker](#docker)
- [Project layout](#project-layout)
- [Development notes](#development-notes)
- [Contributing](#contributing)
- [License](#license)
- [Troubleshooting](#troubleshooting)

## Requirements
- .NET 10 SDK (install from https://dotnet.microsoft.com)
- PowerShell (preferred terminal)
- Visual Studio Community 2026 (18.5.2) or later (optional for IDE experience)

## Getting started
1. Clone the repository
   - PowerShell:
     ```powershell
     git clone <repo-url> "D:\PROJECTS\OrderSystem\"
     cd "D:\PROJECTS\OrderSystem\"
     ```
2. Restore dependencies:
   - PowerShell:
     ```powershell
     dotnet restore
     ```
3. Open in Visual Studio:
   - Open the solution file `*.sln` in Visual Studio or use the CLI. In Visual Studio use the __Open a project or solution__ command.

## Build & run
- CLI (recommended for cross-platform reproducibility):
  - Build:
    ```powershell
    dotnet build -c Release
    ```
  - Run (from the project directory that contains `Program.cs`):
    ```powershell
    dotnet run --project src/OrderSystem.Api/OrderSystem.Api.csproj
    ```
- Visual Studio:
  - Use __Build Solution__ to compile and __Debug > Start Debugging__ (F5) or __Debug > Start Without Debugging__ (Ctrl+F5) to run.

## Configuration
- Configuration is provided via `appsettings.json` and environment variables.
- Common settings:
  - `ConnectionStrings:DefaultConnection` — database connection string
  - `Logging:LogLevel` — logging level
- Override settings with environment variables (e.g., `ASPNETCORE_ENVIRONMENT=Development`) or a user secrets store for sensitive data during development.

## Testing
- Run unit and integration tests:
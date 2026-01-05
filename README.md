# BookTrack

A book review application built with ASP.NET Core that syncs books from a Library API.

## Features

- Book review management
- Automatic synchronization with Library API using HostedService
- Clean Architecture (Core, Application, Infrastructure, API layers)
- CQRS with MediatR
- Entity Framework Core

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- Library API running (see Library project)

## Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BookTrack
   ```

2. **Configure Application Settings**

   Copy `appsettings.Example.json` to `appsettings.Development.json`:
   ```bash
   cp BookTrack.API/appsettings.Example.json BookTrack.API/appsettings.Development.json
   ```

   Edit `appsettings.Development.json` and configure:
   - Database connection string
   - Library API URL and port
   - Sync API key (must match Library project)
   - Polling interval (in seconds)

3. **Run Database Migrations**
   ```bash
   dotnet ef database update --project BookTrack.Infra --startup-project BookTrack.API
   ```

4. **Run the Application**
   ```bash
   cd BookTrack.API
   dotnet run
   ```

## Library Sync Configuration

The application automatically syncs books from the Library API using a background HostedService.

Configure in `appsettings.Development.json`:
```json
{
  "LibrarySync": {
    "ApiBaseUrl": "https://localhost:YOUR_LIBRARY_PORT",
    "ApiKey": "YOUR_SYNC_API_KEY_HERE",
    "PollingIntervalInSeconds": 30
  }
}
```

**Important:**
- The API key must match the `SyncApiKey` in the Library project
- The HostedService polls the Library API every N seconds (configurable)
- Books are synced based on ISBN (duplicates are skipped)

## Security Notes

- `appsettings.Development.json` contains secrets and is excluded from version control
- For production, use environment variables or Azure Key Vault for secrets
- The SSL certificate bypass in `ApplicationModule.cs` is **ONLY for development**

## Project Structure

```
BookTrack/
├── BookTrack.API/              # Web API layer
├── BookTrack.Application/      # Application logic & CQRS
├── BookTrack.Core/            # Domain entities & interfaces
├── BookTrack.Infra/           # Data access & infrastructure
└── BookTrack.Shared/          # Shared DTOs & models
```

## Contributing

1. Create a feature branch
2. Make your changes
3. Submit a pull request

## License

[Your License Here]

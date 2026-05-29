# Umbraco Aspire Starter

This repository is an Umbraco starter site running inside a .NET Aspire distributed application.

The AppHost orchestrates:

- Umbraco web app (`AspireApp1.Web`)
- SQL Server container (persistent)
- Azure App Configuration emulator (persistent)
- Azure Storage emulator with Blob storage (persistent)

## Architecture

- `src/AspireApp1.AppHost`: Aspire orchestration project
- `src/AspireApp1.Web`: Umbraco site (Backoffice + Website + Delivery API)
- `src/AspireApp1.ServiceDefaults`: shared Aspire defaults for service discovery, telemetry, and resilience

`aspire.config.json` points Aspire to the AppHost project:

```json
{
	"appHost": {
		"path": "src/AspireApp1.AppHost/AspireApp1.AppHost.csproj"
	}
}
```

## Prerequisites

Before running this solution, make sure your machine is ready for .NET Aspire:

- Official Aspire prerequisites: https://aspire.dev/get-started/prerequisites/
- .NET SDK 10.0
- Docker Desktop (running), for SQL Server and local emulators

## Getting Started

1. Clone the repo.
2. Restore dependencies:

	 ```bash
	 dotnet restore AspireApp1.slnx
	 ```

3. Run the Aspire AppHost:

	 ```bash
	 dotnet run --project src/AspireApp1.AppHost
	 ```

4. Open the Aspire Dashboard from the URL printed in the console.
5. From the dashboard, open the `webfrontend` endpoint to access the Umbraco site.

## What Starts Automatically

When AppHost starts, it provisions and wires:

- `sql` (SQL Server container with persistent lifetime)
- `database` (SQL database resource)
- `config` (Azure App Configuration emulator, persistent)
- `storage` + `blobs` (Azure Storage emulator + Blob service, persistent)
- `webfrontend` (Umbraco web application)

The web app waits for required dependencies before starting.

## Aspire Dashboard

<img width="1524" height="866" alt="Aspire Dashboard" src="https://github.com/user-attachments/assets/ddbdd829-934b-46aa-a6b3-8dc707c7b991" />

## Running Containers

<img width="1911" height="613" alt="Containers" src="https://github.com/user-attachments/assets/85403a5b-5876-4a2c-b77f-34203e8b448f" />

## Notes

- Resource lifetimes are set to `Persistent` in AppHost, so container state is kept across restarts.
- This project is configured for local development defaults; review and harden settings before production use.

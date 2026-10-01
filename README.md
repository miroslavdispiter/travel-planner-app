# Travel Planner Web Application

**English** | [Srpski](README.sr.md)

A web application for trip planning that keeps everything you need for a trip in one place — destinations, activities, budget and a checklist — and lets you share your travel plan via a QR code.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contact](#contact)

## Features

- **Destinations** – add and organize the places you plan to visit
- **Activities** – plan what you will do on your trip
- **Budget** – plan and keep track of your trip expenses
- **Checklist** – make sure nothing is forgotten before you leave
- **QR code sharing** – share your travel plan with others via a QR code
- **User accounts** – registration and login secured with JWT authentication

## Tech Stack

| Layer          | Technology                             |
| -------------- | -------------------------------------- |
| Frontend       | React (Vite), TypeScript               |
| Backend        | ASP.NET Core, Microsoft Service Fabric |
| Database       | Microsoft SQL Server                   |
| ORM            | Entity Framework Core                  |
| Authentication | JWT                                    |

## Architecture

The backend is built as a Service Fabric application. The React frontend communicates with `WebApiService` over HTTP/REST, which forwards requests to the internal services using Service Fabric Remoting.

| Service          | Type      | Responsibility                                       | Storage                                         |
| ---------------- | --------- | ---------------------------------------------------- | ----------------------------------------------- |
| `WebApiService`  | Stateless | Entry point for the frontend (HTTP/REST, port 7001)  | —                                               |
| `UserService`    | Stateless | User accounts, authentication and JWT                | `UsersDb` (SQL Server)                          |
| `TravelService`  | Stateless | Travel plans, activities and PDF export              | `TravelDb` (SQL Server)                         |
| `SharingService` | Stateful  | Share tokens used for sharing travel plans           | Reliable Dictionary (`string → SharingToken`)   |

### System architecture

![System architecture](images/Arhitektura.png)

### Use case diagram

![Use case diagram](images/Use%20case%20dijagram.png)

### Database model

![Database model](images/Model%20baze%20podataka.png)

## Prerequisites

Make sure you have the following installed:

- **Windows** (required for the local Service Fabric cluster)
- **Visual Studio 2022** with the *ASP.NET and web development* and *Azure development* workloads
- **.NET 9 SDK**
- **Microsoft Service Fabric SDK** with a running local cluster
- **Microsoft SQL Server** (Express or Developer edition) and **SQL Server Management Studio (SSMS)**
- **Node.js** (LTS) and **npm**

## Installation

### 1. Clone the repository

```bash
git clone https://github.com/miroslavdispiter/travel-planner-app.git
```

### 2. Create the databases

Open SQL Server Management Studio and create two empty databases:

- `UsersDb`
- `TravelDb`

### 3. Configure the services

Create or update the `appsettings.json` files for `UserService`, `TravelService` and `WebApiService`, as well as the frontend `.env` file, as described in the [Configuration](#configuration) section.

### 4. Apply database migrations

1. Open the solution in Visual Studio.
2. Open the Package Manager Console: **Tools → NuGet Package Manager → Package Manager Console**
3. Run the following commands:

```powershell
Update-Database -Project UserService -StartupProject UserService
Update-Database -Project TravelService -StartupProject TravelService
```

These commands automatically create all required tables using the existing migrations.

### 5. Run the backend

1. In Visual Studio, set the Service Fabric application project as the **Startup Project**.
2. Click **Run** (or press `F5`).

The backend will be available at: `https://localhost:7001`

### 6. Run the frontend

Make sure the `.env` file has been created (see [Configuration](#configuration)), then run:

```bash
cd travel-planner-app/client
npm install
npm run dev
```

## Usage

Once both the backend and the frontend are running:

1. Open the URL shown in the terminal after `npm run dev` (by default `http://localhost:5173`).
2. Register a new account and log in.
3. Create a new trip and add destinations, activities, budget items and checklist items.
4. Share your trip with others by generating a QR code.

## Configuration

> **Note:**
> - Replace the JWT `Secret` with your own random string of at least 32 characters. The `JwtSettings` values must be identical in `UserService` and `WebApiService`.
> - Make sure the `Server` value in the connection strings matches your SQL Server instance (e.g. `localhost` for a default instance or `.\SQLEXPRESS` for SQL Server Express).
> - Do not commit real secrets to the repository.

### UserService

`UserService/PackageRoot/Config/appsettings.json`

```json
{
  "JwtSettings": {
    "Secret": "your-very-strong-secret-key-min-32-characters-long!",
    "Issuer": "TravelPlannerApp",
    "Audience": "TravelPlannerApp",
    "ExpirationMinutes": 15
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=UsersDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### TravelService

`TravelService/PackageRoot/Config/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=TravelDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### WebApiService

Create an `appsettings.json` file inside the `Config` folder of the `WebApiService` project:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "TravelDb": "Server=localhost;Database=TravelDb;Trusted_Connection=True;TrustServerCertificate=True;",
    "UsersDb": "Server=localhost;Database=UsersDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Secret": "your-very-strong-secret-key-min-32-characters-long!",
    "Issuer": "TravelPlannerApp",
    "Audience": "TravelPlannerApp",
    "ExpirationMinutes": 15
  }
}
```

### Frontend

Create a `.env` file in the `travel-planner-app/client` folder:

```env
VITE_API_URL=http://localhost:7001/api
```

## Contact

**Miroslav Dišpiter**

- GitHub: [@miroslavdispiter](https://github.com/miroslavdispiter)
- Email: miroslav.dispiter.it@gmail.com
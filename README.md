# Product Tracking System

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Inventory and RMA tracker built with ASP.NET Core MVC, Entity Framework Core, and SQLite.

## Why this exists

Support teams often track products, serial numbers, RMAs, and request history in separate spreadsheets. This app keeps those records in one searchable MVC web app with date filters and CSV export.

## What this code shows

- ASP.NET Core MVC structure with controllers, Razor views, and model validation.
- Entity Framework Core migrations against a local SQLite database.
- Inventory and RMA records with serial number, requester, category, date, and record type.
- Search, category filtering, date filtering, newest/oldest sorting, and CSV export.

## Stack

| Layer | Tech |
|---|---|
| Web app | ASP.NET Core MVC, Razor views |
| Data | Entity Framework Core, SQLite |
| UI | Bootstrap |
| Runtime | .NET 8 |

## Local development

```bash
git clone https://github.com/sonnymay/product-tracking-system.git
cd product-tracking-system
dotnet restore
dotnet ef database update
dotnet run
```

The app starts on the URL printed by `dotnet run`, usually `http://localhost:5000` or `https://localhost:5001`.

## Quality check

```bash
dotnet build --configuration Release
```

## License

MIT

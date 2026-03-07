# 🌌 Human Design Engine

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![Architecture](https://img.shields.io/badge/Architecture-Clean-blue.svg)](#architecture)

A high-performance, professional-grade astrological computation engine built with **.NET 10**. This backend powers complex Human Design calculations, dynamic SVG chart generation, and AI-driven personality insights.

## 🚀 Key Features

- **Precision Calculations**: Native integration with `SharpAstrology` and `SwissEph` for accurate planetary positions and Bodygraph mapping.
- **Dynamic SVG Engine**: A custom-ported SVG rendering logic that generates high-quality, customizable Bodygraph charts in real-time.
- **AI Interpretation**: Leverages OpenAI's GPT-4o models to generate deep, personalized Human Design readings and tactical advice.
- **Seed Data Management**: Robust infrastructure for seeding complex relational data, featuring a custom SQL-to-JSON migration pipeline.
- **Geography & Timezones**: Built-in support for global location lookups and timezone offsets via `Nominatim` and `GeoTimeZone`.

---

## 🏗️ Architecture

The project follows **Clean Architecture** principles, ensuring a scalable and maintainable codebase:

- **Domain**: Pure business logic, core enums, and entity definitions.
- **Application**: Use cases, DTOs, interfaces, and service logic (Calculators, Report Builders).
- **Infrastructure**: External concerns like Database (EF Core/Dapper), AI Services, and Data Seeding.
- **API**: ASP.NET Core controllers and endpoints for frontend integration.

---

## 🛠️ Tech Stack

- **Framework**: .NET 10 (ASP.NET Core)
- **Astrology**: `SharpAstrology`, `SwissEph`
- **Database**: MySQL with Entity Framework Core & Dapper
- **AI**: OpenAI SDK
- **Caching**: Memory Cache for performance optimization

---

## 🚦 Getting Started

### Prerequisites

- .NET 10 SDK
- MySQL Server

### Installation

1. Clone the repository.
2. Configure your connection string in `appsettings.json`.
3. Set your OpenAI API key in environment variables or user secrets.
4. Run the application:
   ```bash
   dotnet run --project HumanDesign
   ```

The application will automatically seed the database with initial reference data on the first run.

---

> Built for the future of self-discovery.

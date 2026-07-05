# Unit Converter API

A small **ASP.NET Core Web API** that converts values between units of measurement —
things like meters to feet, Celsius to Fahrenheit, or kilograms to grams.

I built it with a layered architecture and a **Strategy pattern**, so adding new
categories or units later stays simple and doesn't mean touching the code that already
works.

## 🚀 Live Demo

The API is deployed on **Render**, and I've left **Swagger turned on in production** so
you can try it right away — no cloning or setup needed:

**👉 https://unitconverter-a6il.onrender.com/swagger/index.html**

> Heads-up: it runs on Render's free tier, so if it's been idle the first request can
> take a few seconds to wake the server up. 

---

## Table of Contents

- [Live Demo](#-live-demo)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Running the Project Locally](#running-the-project-locally)
- [API Reference](#api-reference)
- [Supported Conversions](#supported-conversions)
- [Design Decisions & Trade-offs](#design-decisions--trade-offs)
- [Extending the API](#extending-the-api)
- [Roadmap](#roadmap)

---

## Features

- A single REST endpoint to convert a value from one unit to another.
- Three categories out of the box — length, temperature, and weight — and it's easy to add more.
- Consistent JSON responses that always follow the same shape (`isSuccess`, `message`, `data`).
- Central error handling, so failures come back with sensible status codes and clear messages.
- Interactive docs with **Swagger / OpenAPI**.
- Logging throughout the request pipeline.

## Tech Stack

- **.NET 10** / **ASP.NET Core**
- **Swashbuckle (Swagger / OpenAPI)** for interactive documentation
- **Microsoft.Extensions.Logging** for structured logging

## Project Structure

The solution follows a layered architecture with a clear separation of concerns:

```
UnitConverter.Domain      -> Core conversion logic: rules, converters, enums, exceptions
UnitConverter.Services    -> Conversion strategies and business orchestration
UnitConverter.API         -> The web layer: controllers, DTOs, middleware, and DI wiring
```

| Layer     | Responsibility                                                                 |
| --------- | ------------------------------------------------------------------------------ |
| `Domain`  | Pure, framework-independent conversion logic and rules. No external dependencies. |
| `Services`| Selects the correct conversion strategy for a requested category.              |
| `API`     | Handles HTTP concerns: routing, validation, serialization, and error handling. |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- (Optional) An IDE such as **Visual Studio 2022+**, **VS Code**, or **JetBrains Rider**

Verify the SDK is installed:

```bash
dotnet --version
```

## Running the Project Locally

From the repository root:

```bash
# 1. Restore dependencies
dotnet restore

# 2. Build the solution
dotnet build

# 3. Run the API
dotnet run --project UnitConverter.API
```

Once running, the API is available at:

| Profile | URL                     |
| ------- | ----------------------- |
| HTTPS   | https://localhost:7093  |
| HTTP    | http://localhost:5205   |

Then open the Swagger UI in your browser:

```
https://localhost:7093/swagger
```

> Don't feel like running it locally? Just use the [live version on Render](https://unitconverter-a6il.onrender.com/swagger/index.html) instead.

## API Reference

### Convert a value

```
POST /api/conversions/convert
Content-Type: application/json
```

**Request body**

| Field      | Type   | Description                                             |
| ---------- | ------ | ------------------------------------------------------ |
| `category` | string | Conversion category: `Length`, `Temperature`, or `Weight`. |
| `fromUnit` | string | The unit to convert from (case-insensitive).           |
| `toUnit`   | string | The unit to convert to (case-insensitive).             |
| `value`    | number | The numeric value to convert.                          |

**Example request**

```bash
curl -X POST https://localhost:7093/api/conversions/convert \
  -H "Content-Type: application/json" \
  -d '{
        "category": "Length",
        "fromUnit": "meters",
        "toUnit": "feet",
        "value": 10
      }'
```

**Example success response** (`200 OK`)

```json
{
  "isSuccess": true,
  "message": "Conversion successful",
  "data": {
    "originalValue": 10,
    "fromUnit": "meters",
    "toUnit": "feet",
    "convertedValue": 32.808398950131235
  }
}
```

**Example error response** (`400 Bad Request`)

```json
{
  "isSuccess": false,
  "message": "The unit 'lightyears' is not supported by the system.",
  "data": null
}
```

## Supported Conversions

| Category      | Units                                  |
| ------------- | -------------------------------------- |
| `Length`      | `meters`, `feet`                       |
| `Temperature` | `celsius`, `fahrenheit`                |
| `Weight`      | `kilogram`, `gram`                     |

> Unit names are **case-insensitive**. For now, the units and conversion factors live
> in code (in the `Domain` layer) to keep things simple.

## Design Decisions & Trade-offs

- **Layered architecture (Domain / Services / API).** Keeps the core conversion logic
  independent of the web framework, which improves testability and makes the domain
  reusable. The trade-off is more projects/boilerplate for a small app, but it reflects
  how a real, team-maintained solution is structured for growth.

- **Strategy pattern for categories.** Each conversion category implements
  `IConversionStrategy`. The service resolves the correct strategy at runtime based on
  the requested category. This makes adding new categories an *additive* change rather
  than modifying existing code (Open/Closed Principle).

- **Base-unit conversion factors for linear units.** Length and weight units are defined
  relative to a canonical base unit (meters and kilograms respectively), so any two units
  convert through the base without needing an explicit entry for every pair. This scales
  cleanly to hundreds of units.

- **Formula-based conversions for non-linear units.** Temperature is not a simple ratio,
  so it uses explicit conversion functions. This is the pragmatic choice for units that
  require offsets rather than pure scaling factors.

- **Consistent response envelope (`ApiResponse<T>`).** Every response — success or
  failure — shares the same shape, making the API predictable for clients.

- **Centralized exception handling middleware.** Domain exceptions (unsupported unit,
  category, or conversion) are translated into `400 Bad Request`, while unexpected
  errors return `500`. This keeps controllers thin and error handling consistent.

- **Hardcoded rules (for now).** The units and factors live in code for now, but they're
  kept in one place so they can move to a database or config later without touching the
  API or service layers.

## Extending the API

To add a new conversion category (for example, **Area** or **Volume**):

1. Add the category to `ConversionCategory` (in `UnitConverter.Domain`).
2. Define its units/factors under `ConversionRules` and a corresponding converter.
3. Implement a new `IConversionStrategy` in `UnitConverter.Services`.
4. Register the strategy in `ServiceCollectionExtensions`.

No changes to the controller or existing strategies are required.

## Roadmap

Potential future enhancements for a production-grade system:

- Add more units to existing categories (km, cm, miles, inches, Kelvin, pounds, ounces, …).
- Move conversion rules from hardcoded data to a **database** or configuration store.
- Add an automated **unit/integration test** project.



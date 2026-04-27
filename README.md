# LegacyOrderService

Small .NET 8 application for processing and saving customer orders.

## Project structure

- 'src/LegacyOrderService' => application code
- 'tests/LegacyOrderServicel.Tests' => unit and integration tests

## How to run

```bash
dotnet run --project src/LegacyOrderService
```

## How to test

```bash
dotnet test
```

## Key Improvements

- Parameterized SQL queries
- Proper disposal of database resources
- Layered structure with dependency injection
- Input validation
- Friendly error handling with logging
- Unit tests for services and validation
- Integration tests for repository / database behavior

## Future Improvements

The current implementation focuses on improving reliability, structure, and testability while keeping the application simple. Some possible future improvements include:

- Move database configuration into 'appsettings.json' or environnment variables
- Add database migrations instead of manually relying on schema setup
- Add more integration tests around database constraints and failure scenarios
- Introduce service interfaces where they provide clear testability or decoupling benefits
- Add external logging provider instead if console-only logging
- Improve error categorisation for different failure types
- Add async repository methods if database operations become more frequent or performance-sensitive
- Replace the console UI with an API or background worker while reusing the service layer

## Future Project Structure

As the application grow, the current structure could be split into separate projects to enforece clearer boundaries between layers.

### Possible future structure 

```text
src/
    LegacyOrderService.Console/
    LegacyOrderService.Application/
    LegacyOrderService.Domain/
    LegacyOrderService.Infrastructure/
tests/
    LegacyOrderService.UnitTests/
    LegacyOrderService.IntegrationTests/
```

### Why this could help

- **Console** would only handle user interaction and application startup
- **Application** would coordinate use cases and workflow
- **Domain** would contain core models and business rules
- **Infrastructure** would contain database access, logging and external integrations
- **Tests** could be separated between fast unit tests and slower integration tests

This was not introduced to reduce unnecessary complexity, but the current structure keeps responsabilities separated enough to make this transition possible later.
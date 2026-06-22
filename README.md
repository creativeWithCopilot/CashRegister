# CashRegister API

A simple ASP.NET Core Web API for managing categories, products, and transactions in a cash register system. The project uses Entity Framework Core with SQLite and provides RESTful endpoints for CRUD operations.

## Features

- Category management
- Product management
- Transaction creation and retrieval
- SQLite database persistence
- Swagger / OpenAPI documentation

## Technologies

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swashbuckle (Swagger)

## Prerequisites

- .NET SDK 8.0 or higher
- PowerShell, Command Prompt, or another terminal

## Getting Started

1. Clone or open the repository.
2. Restore dependencies and build the project:

```powershell
dotnet restore
dotnet build
```

3. Run the application:

```powershell
dotnet run
```

4. Open the Swagger UI to explore the API:

- `https://localhost:5001/swagger` (or the URL shown by the application)

## Configuration

The SQLite database connection string is defined in `appsettings.json`:

```json
"ConnectionStrings": {
  "EFCoreDBConnection": "Data Source=CashRegister.db"
}
```

The database file will be created in the application working directory.

## API Endpoints

### Category

- `POST /api/Category/CreateCategory`
  - Body: `CategoryCreateDTO`
- `GET /api/Category/GetCategoryById/{id}`
- `PUT /api/Category/UpdateCategory`
  - Body: `CategoryUpdateDTO`
- `DELETE /api/Category/DeleteCategory/{id}`
- `GET /api/Category/GetAllCategories`

### Product

- `POST /api/Product/CreateProduct`
  - Body: `ProductCreateDTO`
- `GET /api/Product/GetProductById/{pluCode}`
- `PUT /api/Product/UpdateProduct`
  - Body: `ProductUpdateDTO`
- `DELETE /api/Product/DeleteProduct/{pluCode}`
- `GET /api/Product/GetAllProducts`
- `GET /api/Product/GetAllProductsByCategory/{categoryId}`

### Transaction

- `POST /api/Transaction/CreateTransaction`
  - Body: `TransactionCreateDTO`
- `GET /api/Transaction/GetTransactionById/{id}`
- `GET /api/Transaction/GetAllTransaction`

## DTOs

### CategoryCreateDTO

- `Name` (string, required, 3-50 characters)
- `Description` (string, max 200 characters)
- `IsActive` (bool)

### CategoryUpdateDTO

- `Id` (int, required)
- `Name` (string, required, 3-50 characters)
- `Description` (string, max 200 characters)
- `IsActive` (bool)

### CategoryResponseDTO

- `Id` (int)
- `Name` (string)
- `Description` (string)
- `IsActive` (bool)

### ProductCreateDTO / ProductUpdateDTO

- `PLUCode` (string, required, 5 digits)
- `Description` (string, max 200 characters)
- `Price` (decimal, required, 0.01 to 1,000,000.00)
- `IsActive` (bool)
- `CategoryId` (int)

### ProductResponseDTO

- `PLUCode` (string)
- `Description` (string)
- `Price` (decimal)
- `IsActive` (bool)
- `CategoryId` (int)

### TransactionCreateDTO

- `SaleItems` (array of `SaleItemCreateDTO`, required)

### SaleItemCreateDTO

- `PLUCode` (string, required)
- `Quantity` (decimal, required)

### TransactionResponseDTO

- `Id` (long)
- `OrderDate` (DateTime)
- `Subtotal` (decimal)
- `TaxTotal` (decimal)
- `Total` (decimal)
- `SaleItems` (array of `SaleItemResponseDTO`)

### SaleItemResponseDTO

- `Id` (long)
- `PLUCode` (string)
- `Quantity` (decimal)
- `UnitPrice` (decimal)
- `LineTotal` (decimal)

## Example Requests

### Create Category

```bash
curl -X POST https://localhost:5001/api/Category/CreateCategory \
  -H "Content-Type: application/json" \
  -d '{"name":"Beverages","description":"Drinks and refreshments","isActive":true}'
```

### Create Product

```bash
curl -X POST https://localhost:5001/api/Product/CreateProduct \
  -H "Content-Type: application/json" \
  -d '{"pluCode":"00001","description":"Bottled Water","price":1.50,"isActive":true,"categoryId":1}'
```

### Create Transaction

```bash
curl -X POST https://localhost:5001/api/Transaction/CreateTransaction \
  -H "Content-Type: application/json" \
  -d '{"saleItems":[{"pluCode":"00001","quantity":2}]}'
```

## Notes

- Swagger is enabled in the development environment.
- Authorization middleware is configured, but no authentication scheme is required by default.
- Use the `CashRegister.db` file to inspect database content if needed.

## License

This project is licensed under the MIT License. See `LICENSE` for details.

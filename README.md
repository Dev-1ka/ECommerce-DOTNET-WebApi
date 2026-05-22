# E-Commerce Backend (ASP.NET Core Web API)

This repository contains the backend implementation of an E-Commerce Web Application developed using ASP.NET Core Web API. The project follows Clean Architecture principles and implements the CQRS pattern using MediatR for scalable and maintainable application development.

## Features

### Authentication & Authorization
- JWT Authentication
- Role-Based Authorization
- Secure API Endpoints

### Product Management
- Create Product
- Retrieve Product Details
- Update Product Information
- Delete Products
- Product Listing with Pagination, Searching, and Sorting

### Cart Management
- Add Products to Cart
- Update Cart Quantity
- Remove Products from Cart
- View Cart Details

### Order Management
- Place Orders
- View Order History
- Manage Customer Orders

### Analytics
- Total Revenue
- Revenue by Date
- Top Selling Products
- Orders Per Day
- Daily, Weekly, and Monthly Sales Trends

## Architecture

The project follows Clean Architecture with clear separation of concerns:

- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

### Design Patterns & Technologies
- CQRS (Command Query Responsibility Segregation)
- MediatR
- Repository Pattern
- Dependency Injection
- FluentValidation
- Entity Framework Core

## Technology Stack

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- Swagger/OpenAPI

## Project Structure

```text
Backend/
│
├── API/
├── Application/
├── Domain/
├── Infrastructure/
└── Persistence/
```

## API Features

### Controllers
- Authentication Controller
- Product Controller
- Cart Controller
- Order Controller
- Analytics Controller

### Application Layer
- Commands
- Queries
- Handlers
- Validators
- Interfaces
- DTOs

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 / VS Code

### Clone the Repository

```bash
git clone <repository-url>
```

### Configure Database

Update the connection string in:

```text
appsettings.json
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run
```

### Swagger Documentation

After running the application, open:

```text
https://localhost:<port>/swagger
```

## Key Functionalities

- JWT-Based Authentication
- Product CRUD Operations
- Product Pagination
- Product Searching
- Product Sorting
- Shopping Cart Management
- Order Processing
- Sales Analytics
- Request Validation
- API Documentation using Swagger

## Future Enhancements

- Payment Gateway Integration
- Product Reviews and Ratings
- Wishlist Functionality
- Email Notifications
- Inventory Management
- Advanced Reporting Dashboard

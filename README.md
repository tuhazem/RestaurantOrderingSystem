🍽️ Restaurant System API

A complete Restaurant Management API built with ASP.NET Core 8, following Clean Architecture principles.
It handles menu items, categories, orders, customers, and authentication using JWT tokens.

🚀 Features
🔐 Authentication & Authorization

Secure login and registration using JWT (JSON Web Token).

Role-based authorization (e.g., Admin, User).

🍔 Menu Management

Create, update, delete, and list menu items.

Each menu item belongs to a category.

Supports image upload (optional feature).

Includes validation for input data (DTO constraints).

📦 Category Management

Manage menu item categories.

Supports one-to-many relationship with menu items.

🧾 Order Management

Create customer orders containing multiple menu items.

Auto-calculates total price based on menu item prices and quantities.

Supports payment method (Cash, Credit Card, etc.) and status tracking (Pending, Completed).

Linked with registered customers.

👤 Customer Management

Add and manage customers.

Linked with orders.

⚙️ Infrastructure & Architecture

Follows Clean Architecture:

API → Presentation Layer

Application → Business Logic

Domain → Entities and Core Models

Infrastructure → Database & Repositories

Repository Pattern used for data access.

DTO Mapping handled with AutoMapper.

Global Exception Handling Middleware for unified error responses.

🧠 Validation

Full Data Annotation Validation on all DTOs.

Automatically handled via ModelState in controllers.

📜 Documentation

Fully documented using Swagger UI.

JWT support directly in Swagger for testing secured endpoints.

🧩 Additional Enhancements

Global Exception Handling Middleware.

Pagination & Filtering (optional extension).

Soft Delete support (optional extension).

Seed Data for initial setup (optional extension).

Unit tests for services layer (optional).

🏗️ Project Structure
RestaurantSystem/
│
├── RestaurantSystem.API/             # Presentation Layer (Controllers, Middleware, etc.)
│   ├── Controllers/
│   ├── Middlewares/
│   └── Program.cs
│
├── RestaurantSystem.Application/     # Application Logic (DTOs, Services, Mapping Profiles)
│   ├── DTOs/
│   ├── Services/
│   └── MappingProfiles/
│
├── RestaurantSystem.Domain/          # Core Entities and Interfaces
│   └── Entities/
│
├── RestaurantSystem.Infrastructure/  # Data Access (EF Core, Repository Implementations)
│   ├── Repositories/
│   └── DbContext.cs
│
└── README.md                         # You're here!

🧰 Technologies Used
Tool	Purpose
ASP.NET Core 8	Backend Framework
Entity Framework Core	ORM for database operations
SQL Server	Database
AutoMapper	Mapping Entities ↔ DTOs
JWT Authentication	Secure Authentication
Swagger / Swashbuckle	API Documentation
xUnit (optional)	Unit Testing
Clean Architecture	Project structure & separation of concerns
⚙️ Setup & Run Locally
🧱 Prerequisites

.NET 8 SDK

SQL Server

Visual Studio 2022 or VS Code

🪜 Steps

Clone the repository

git clone https://github.com/tuhazem/RestaurantOrderingSystem.git
cd RestaurantSystem-API


Configure the connection string

Open appsettings.json inside RestaurantSystem.API

Update the DefaultConnection to match your SQL Server settings.

"ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True"
}


Apply Migrations

cd RestaurantSystem.Infrastructure
dotnet ef database update


Run the Project

cd ../RestaurantSystem.API
dotnet run


Open Swagger

Navigate to: https://localhost:5109/swagger

You can test all endpoints directly from the Swagger UI.

🧑‍💻 Example Endpoints
🔐 Authentication

Login:

POST /api/Auth/Login


Register:

POST /api/Auth/Register

🍔 Menu Items

Get all items:

GET /api/MenuItems


Create new item:

POST /api/MenuItems
Authorization: Bearer {token}


Response Example:

{
  "name": "Hot Coffee",
  "description": "You’ll love it!",
  "price": 70,
  "categoryId": 3,
  "categoryName": "Drinks"
}

🧾 Orders

Create an order:

POST /api/Orders
Authorization: Bearer {token}


Body:

{
  "customerId": 1,
  "paymentMethod": "Cash",
  "items": [
    { "menuItemId": 4, "quantity": 2 }
  ]
}

📸 Future Improvements

 Image upload for menu items.

 Pagination & filtering.

 Unit tests for services.

 Dashboard with total sales and most-ordered items.

🧑‍🎓 Author

👨‍💻 Hazem Mohamed
📧 hazemamer221@gmail.com

💻 Passionate .NET Backend Developer | Building Scalable APIs with Clean Code.

⭐ Show Your Support

If you found this project helpful, please give it a ⭐ on GitHub — it really helps!

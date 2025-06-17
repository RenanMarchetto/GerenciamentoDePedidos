# GerenciamentoDePedidos

**GerenciamentoDePedidos** is a web application for managing customer orders, products, and customers. Built with ASP.NET Core (.NET 8) using Razor Pages and MVC patterns, it provides a simple and efficient interface for order management in small businesses or as a learning project.

## Features

- **Customer Management**
  - List, search, create, edit, and delete customers.
  - View customer details.

- **Product Management**
  - List, search, create, edit, and delete products.
  - View product details.
  - Track product stock quantity.

- **Order Management**
  - List and filter orders by customer or status.
  - Create new orders with multiple products.
  - View order details, including items and customer info.
  - Update order status (e.g., "Novo").
  - Stock is automatically decremented when orders are placed.

- **Modern UI**
  - Responsive Bootstrap-based interface.
  - Navigation for Customers, Orders, and Products.

## Technologies Used

- ASP.NET Core 8 (Razor Pages & MVC)
- C# 12
- Dapper (micro-ORM for data access)
- SQL Server (LocalDB)
- Bootstrap 5

## Project Structure

- `Controllers/` — MVC controllers for Customers, Products, and Orders.
- `Models/` — Entity classes for Customer, Product, Order, and OrderItem.
- `Repositories/` — Data access layer using Dapper.
- `Views/` — Razor views for UI.
- `Data/DapperContext.cs` — Database connection factory.
- `appsettings.json` — Configuration, including connection string.

## Database

- The connection string in `appsettings.json` points to a LocalDB `.mdf` file.

## Usage

- Use the main menu to navigate between Customers, Orders, and Products.
- Add, edit, or delete records as needed.
- When creating an order, select a customer and add one or more products. The system will check stock and update it accordingly.

## Configuration

- **Connection String:**  
  Edit `appsettings.json` to match your SQL Server LocalDB setup:
  "ConnectionStrings": { "DefaultConnection": "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=PATH_TO_YOUR_MDF;Integrated Security=True;Connect Timeout=30" }
  


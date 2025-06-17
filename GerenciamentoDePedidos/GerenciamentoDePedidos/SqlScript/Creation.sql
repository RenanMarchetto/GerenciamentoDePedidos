-- CREATE DATABASE GerenciamentoDePedidos
-- ON (FILENAME = 'C:\...\GerenciamentoDePedidos.mdf')
-- FOR ATTACH;


-- Customers
CREATE TABLE Customers (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- Products
CREATE TABLE Products (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL
);

-- Orders
CREATE TABLE Orders (
    Id INT IDENTITY PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);

-- OrderItems
CREATE TABLE OrderItems (
    Id INT IDENTITY PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Data
INSERT INTO Customers (Name, Email, Phone) VALUES
('Alice Smith', 'alice@example.com', '123456789'),
('Bob Johnson', 'bob@example.com', '987654321');

INSERT INTO Products (Name, Description, Price, StockQuantity) VALUES
('Laptop', '14 inch, 8GB RAM', 2500.00, 10),
('Mouse', 'Wireless', 50.00, 100);
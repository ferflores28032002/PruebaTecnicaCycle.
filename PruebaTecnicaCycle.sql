-- Create the PruebaTecnicaCycle database
CREATE DATABASE PruebaTecnicaCycle;
GO

USE PruebaTecnicaCycle;
GO

-- Create the Catalog schema
CREATE SCHEMA Catalog;
GO

-- Create the Categories table
CREATE TABLE Catalog.Categories (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(150) NOT NULL
);
GO

-- Create the Products table with foreign key reference to Categories
CREATE TABLE Catalog.Products (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(150) NOT NULL,
    Price numeric(10, 2) CHECK (Price > 0),
    CategoryId int FOREIGN KEY REFERENCES Catalog.Categories(Id),
    Description varchar(500),
    Image varchar(max),
    Status bit NOT NULL
);
GO

-- Create a stored procedure to retrieve the list of products
CREATE PROCEDURE Catalog.ListProducts
AS
BEGIN
    SELECT P.*, C.Name AS CategoryName
    FROM Catalog.Products AS P
    INNER JOIN Catalog.Categories AS C ON P.CategoryId = C.Id;
END;
GO

-- Create a stored procedure to retrieve the list of categories
CREATE PROCEDURE Catalog.ListCategories
AS
BEGIN
    SELECT * FROM Catalog.Categories;
END;
GO


-- Insert two categories
INSERT INTO Catalog.Categories (Name) VALUES ('Electronics'), ('Clothing');
GO

-- Insert two products with corresponding category IDs
INSERT INTO Catalog.Products (Name, Price, CategoryId, Description, Image, Status)
VALUES ('Laptop', 1200.00, 1, 'High-performance laptop', 'laptop_image.jpg', 1),
       ('T-shirt', 19.99, 2, 'Comfortable cotton T-shirt', 'tshirt_image.jpg', 1);
GO


-- Test the stored procedures (these blocks cannot be executed here, but they are provided to be executed on the user's SQL Server)
EXEC Catalog.ListProducts;
EXEC Catalog.ListCategories;

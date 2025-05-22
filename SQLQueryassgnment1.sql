CREATE DATABASE EmployeeDB;
GO
USE EmployeeDB
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Position NVARCHAR(50),
    Salary DECIMAL(10, 2)
);
USE EmployeeDB;
SELECT * FROM Employees;

INSERT INTO Employees (Name, Position, Salary)
VALUES ('Test User', 'Developer', 50000.00);

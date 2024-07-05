-- Создание базы данных ValiullinDR
CREATE DATABASE ValiullinDR;
GO
USE ValiullinDR;
GO

-- Создание таблицы Staff с необходимыми полями
CREATE TABLE Staff (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15),
    Status NVARCHAR(20)
);
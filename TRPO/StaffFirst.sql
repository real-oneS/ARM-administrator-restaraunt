-- �������� ���� ������ ValiullinDR
CREATE DATABASE ValiullinDR;
GO
USE ValiullinDR;
GO

-- �������� ������� Staff � ������������ ������
CREATE TABLE Staff (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15),
    Status NVARCHAR(20)
);
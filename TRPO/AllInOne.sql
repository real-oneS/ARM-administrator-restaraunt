-- ё������� ���� ������ ValiullinDR
CREATE DATABASE ValiullinDR;
GO
USE ValiullinDR;
GO

-- ё������� ������� Staff � ������������ ������
CREATE TABLE Staff (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LastName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    Position NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15),
    Status NVARCHAR(20)
);
CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Number INT NOT NULL,
    Components NVARCHAR(50) NULL,
    Status NVARCHAR(50) NULL,
    Reason NVARCHAR(50) null,
    Ready NVARCHAR(20) null
);
CREATE TABLE Menu (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name nvarchar(max) NOT NULL,
    Components NVARCHAR(max)NOT NULL,
    Price int not NULL
);
USE ValiullinDR;

-- ������� ������ ���������� ������� � ������� Staff
INSERT INTO Staff (LastName, FirstName, Position, PhoneNumber, Status)
VALUES ('������', '����', '�����', '+79545351223', '��������'), 
('������', '����', '���-�����', '+79545545213', '��������'), 
('�������', '������', '��������', '+79545545212', '�� �������'), 
('�������', '��������', '�������', '+79436734241', '��������'), 
('�����', '�����', '��������', '+79412334241', '��������'), 
('����������', '����', '��������', '+79123456789', '��������');

USE ValiullinDR;

INSERT INTO Orders (Number, Components, Status, Reason, Ready)
VALUES (1, '����� ���������, ��� �����, ���������, ���������', '�����������', null, '���'), 
(2, '�����, ����� ������, ����� ������, ����, ��������', '�������������', '��������� �����������', '���'),
(3, '��������, ��������� ����������', '�����', null, '��'),
(4, '����� ������, ������, ����� ���������', '�������', '������ �������������', '���'),
(5, '����, ��� ����, �����', '�����������', null, '���'),
(6, null, null, null, null);

USE ValiullinDR;

INSERT INTO Menu (Name, Components, Price)
VALUES ('����� ���������', '���������� �������� � �������, �����, ����� �������� � ������� �������.', 300),
('����� ������', '������������ ����� � �������� �������� ����, ����� ��������, �������� � ������ ������.', 250),
('����� �� ��������', '���������� ��������� ����� �������� ������� �� ����� � �������� � ������� ���������� � ������.', 500),
('������ ������', '����� ���������� ������ ���� (������, ����� ��� ������) � ����������� ��������, ��������� � ��������� ������.', 400),
('�������� �������', '������ �������, �������������� �� ������� �� �����, � �����, �������� � ��������, �������� � ������������ ����.', 350),
('������ �� �����', '������������ ����������� ����� � �������� ������, ���������� � ���������.', 250),
('����� ���������', '���������� �������� � �������, �����, ����� �������� � ������� �������.', 300),
('���������� ������', '������� � ������ ���������� ���� � ������ ���������� ��������, �������� � ���������.', 200);

CREATE PROCEDURE [dbo].[sp_CreateStaff]
    @lastname nvarchar(max),
	@firstname nvarchar(max),
	@position nvarchar(max),
	@phonenumber nvarchar(max),
	@status nvarchar(max),
    @Id int out
AS
    INSERT INTO Staff(LastName,FirstName,Position,PhoneNumber,Status)
    VALUES (@lastname,@firstname,@position,@phonenumber,@status)
  
    SET @Id=SCOPE_IDENTITY()
GO

CREATE PROCEDURE [dbo].[sp_CreateOrders]
    @number int,
	@components nvarchar(max),
	@status nvarchar(max),
	@reason nvarchar(max),
	@ready nvarchar(max),
    @Id int out
AS
    INSERT INTO Orders(Number,Components,Status,Reason,Ready)
    VALUES (@number,@components,@status,@reason,@ready)
  
    SET @Id=SCOPE_IDENTITY()
GO

CREATE PROCEDURE [dbo].[sp_CreateMenu]
    @name nvarchar(max),
	@components nvarchar(max),
	@price int,
    @Id int out
AS
    INSERT INTO Menu(Name,Components,Price)
    VALUES (@name,@components,@price)
  
    SET @Id=SCOPE_IDENTITY()
GO

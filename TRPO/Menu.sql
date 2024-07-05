CREATE TABLE Menu (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name nvarchar(max) NOT NULL,
    Components NVARCHAR(max)NOT NULL,
    Price int not NULL
);
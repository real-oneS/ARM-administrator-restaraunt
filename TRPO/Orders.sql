CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Number INT NOT NULL,
    Components NVARCHAR(50) NULL,
    Status NVARCHAR(50) NULL,
    Reason NVARCHAR(50) null,
    Ready NVARCHAR(20) null
);
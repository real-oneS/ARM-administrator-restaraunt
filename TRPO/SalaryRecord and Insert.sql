-- Создание таблицы SalaryRecord с полями ФИО, Оклад, Премия, Дата_выплаты
CREATE TABLE SalaryRecord (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StaffId INT,
    Salary INT,
    Bonus INT,
    LastPaymentDate DATE,
    FOREIGN KEY (StaffId) REFERENCES Staff(Id)
);
-- Вставка случайных данных в таблицу SalaryRecord
INSERT INTO SalaryRecord (StaffId, Salary, Bonus, LastPaymentDate)
VALUES 
(1, 50000.00, 2000.00, '2024-03-15'),
(2, 70000.00, 3000.00, '2024-03-20'),
(3, 40000.00, 1500.00, '2024-03-18'),
(4, 30000.00, 1000.00, '2024-03-22'),
(5, 60000.00, 2500.00, '2024-03-25'),
(6, 45000.00, 1800.00, '2024-03-28');
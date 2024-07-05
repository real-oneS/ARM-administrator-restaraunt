-- —Создание базы данных ValiullinDR
CREATE DATABASE ValiullinDR;
GO
USE ValiullinDR;
GO

-- —Создание таблицы Staff с необходимыми пол€ми
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

-- Вставка одного случайного примера в таблицу Staff
INSERT INTO Staff (LastName, FirstName, Position, PhoneNumber, Status)
VALUES ('Иванов', 'Петр', 'Повар', '+79545351223', 'Выходной'), 
('Петров', 'Иван', 'Шеф-повар', '+79545545213', 'Работает'), 
('Валидзе', 'Роберт', 'Официант', '+79545545212', 'На отпуске'), 
('Голышев', 'Владимир', 'Уборщик', '+79436734241', 'Работает'), 
('Шелби', 'Томас', 'Менеджер', '+79412334241', 'Выходной'), 
('Лохмаченко', 'Граф', 'Официант', '+79123456789', 'Работает');

USE ValiullinDR;

INSERT INTO Orders (Number, Components, Status, Reason, Ready)
VALUES (1, 'Пицца Маргарита, Сок Вишни, Карбонара, Гамбургер', 'Выполняется', null, 'Нет'), 
(2, 'Стейк, Салат Цезарь, Салат Оливье, Пиво, Круассан', 'Задерживается', 'Отсуствие компонентов', 'Нет'),
(3, 'Капучино, Пироженое Муравейник', 'Готов', null, 'Да'),
(4, 'Салат Цезарь, Бургер, Пицца Пепперони', 'Отменен', 'Долгое приготовление', 'Нет'),
(5, 'Кола, Чай Пуэр, Эклер', 'Выполняется', null, 'Нет'),
(6, null, null, null, null);

USE ValiullinDR;

INSERT INTO Menu (Name, Components, Price)
VALUES ('Паста Карбонара', 'Обжаренные спагетти с беконом, яйцом, сыром пармезан и свежими травами.', 300),
('Салат Цезарь', 'Классический салат с кубиками куриного мяса, сыром пармезан, гренками и соусом Цезарь.', 250),
('Стейк из говядины', 'Аппетитный мраморный кусок говядины жареный на гриле и подается с жареным картофелем и соусом.', 500),
('Рыбный тартар', 'Мелко нарезанная свежая рыба (лосось, тунец или форель) с добавлением луковицы, каперсами и оливковым маслом.', 400),
('Пармская свинина', 'Сочная свинина, приготовленная по рецепту из Пармы, с медом, горчицей и специями, подается с картофельным пюре.', 350),
('Лосось на гриле', 'Классическая итальянская пицца с томатным соусом, моцареллой и базиликом.', 250),
('Пицца Маргарита', 'Обжаренные спагетти с беконом, яйцом, сыром пармезан и свежими травами.', 300),
('Шоколадный фондан', 'Горячий и мягкий шоколадный торт с жидким шоколадным начинкой, подается с мороженым.', 200);

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

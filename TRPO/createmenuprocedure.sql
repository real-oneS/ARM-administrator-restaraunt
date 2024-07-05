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

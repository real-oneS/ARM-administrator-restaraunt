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

CREATE PROCEDURE [dbo].[sp_CreateSalaryRecord]
    @staffid int,
	@salary int,
	@bonus int,
	@lastpaymentdate date,
    @Id int out
AS
    INSERT INTO SalaryRecord(StaffId,Salary,Bonus,LastPaymentDate)
    VALUES (@staffid,@salary,@bonus,@lastpaymentdate)
  
    SET @Id=SCOPE_IDENTITY()
GO

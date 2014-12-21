-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER DeleteUser 
   ON  [dbo].[Users] 
   INSTEAD OF DELETE
AS 
BEGIN
	declare @id int;
	select @id=d.Id from deleted d;
	delete from Orders where User_Id=@id;
	delete from Users where Id=@id;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER DeleteOrders 
   ON  [dbo].[Orders] 
   INSTEAD OF DELETE
AS 
BEGIN
	declare @id int;
	select @id=d.Id from deleted d;
	delete from Orders_Books where Order_Id=@id;
	delete from Orders where Id=@id;
END
GO

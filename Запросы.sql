
/*������ �� ����������� 
select Users.Id , Users.Email , Users.Pwd , Roles.Value from Users , Persons , Roles where Users.Id = Persons.Id and  Users.Role_Id = Roles.Id and Users.Pwd = 'qweasd' and Users.Email  = 'v@mail.ru' ;
*/

--��������� ������� ����������� ������� ������ ������ + ������� ���� �� ������� �������� � � ����������� �� ��� ������������ sqlCommad qweasd
--ALTER PROCEDURE GetWithFilter
-- @name varchar(1000),
-- @ISBN varchar(20)
-- AS
--	DECLARE @sqlCommand varchar(1000)
--	DECLARE @tables varchar(1000)
--	SET @sqlCommand = 'SELECT * FROM Books'
--	if @name is not null OR @ISBN is not null 
--	begin
--		SET @sqlCommand += ' Where ';
--		if @name is not null
--			SET @sqlCommand += ' Name = ''' + @name + '''';
--		if @ISBN is not null and @name is not null
--			SET @sqlCommand += ' and ';
--		if @ISBN is not null
--			SET @sqlCommand += 'ISBN = ''' + @ISBN + '''';
--	end
--	EXEC (@sqlCommand)
-- GO


 
--EXEC GetWithFilter  @name = '������ � ���������' , @ISBN = '123123123' ;
 
 
/* ALTER PROCEDURE DeleteWithFilter
 @condition varchar(1000),
 @tables varchar(1000)
 AS
 DECLARE @sqlCommand varchar(1000)
	SET @sqlCommand = 'DELETE  FROM ' + @tables + ' ' + @condition
	EXEC (@sqlCommand)
 GO

 EXEC DeleteWithFilter  @tables = ' Discounts '  ,@condition = ' WHERE User_Id = 1 ';*/


 /*
 Create PROCEDURE DOSql
 @sqlCommand varchar(1000)
 AS
	EXEC (@sqlCommand)
 GO

 EXEC DOSql  @sqlCommand = 'Select * FROM Books';
 */

 
 /*UNION*/
 --ALL ���� ������������ �� ��������� ��� ������ � ���� ��� �� ������� ������������� ������
 -- �������� ��� ISBN ������ ��� ����� ��� � ����� ����
 
 --GO
 --SELECT ISBN  FROM Books
 --UNION 
 --Select ISBN_Tome FROM Books where ISBN_Tome IS NOT NULL;


 -- ����� ������� �� ���� �� ����������
--GO
-- SELECT ISBN FROM Books
-- Except
-- Select Book_Id FROM Orders;

 --����� ������� ���������� ���� ���
--GO
-- SELECT ISBN FROM Books
--  Intersect
-- Select Book_Id FROM Orders;


--���������� ������������� ������������ ����� + Having qweasd
--SELECT  Orders.Book_Id  ,  Sum(Orders.Cost) as SumCost FROM Orders Group By Orders.Book_Id Having Sum(Cost) > 100


--���������� ���� �� �������������
--Select Book_Id ,SUM(Orders.Cost) As Suma , Count(Orders.Book_Id) as CNT ,RANK() OVER 
--    ( ORDER BY Count(Orders.Book_Id) DESC) AS Rank 
--	FROM Orders  Group By Orders.Book_Id 


--���������� ��� ������� ������������ ������� �� �� ��������� 
--Select Book_Id  ,  Orders.User_Id,Orders.Cost,RANK() OVER 
--    (PARTITION BY Orders.User_Id ORDER BY Orders.Cost DESC) AS Rank 
--	FROM Orders  


--�������� ����� ������� ��� ������� ������������
--SELECT U.Email , Ssa
--	FROM Users AS U
--CROSS APPLY 
--	(Select Sum(Orders.Cost) as Ssa  From Orders Where U.Id=Orders.User_Id Group by Orders.User_Id) AS ST;

--��������� ������ � ������� ��� ������������� 
--MERGE Discounts AS TARGET
--USING UpdatedDiscounts AS SOURCE 
--ON (TARGET.User_Id = SOURCE.User_Id) 
----When records are matched, update 
----the records if there is any change
--WHEN MATCHED AND TARGET.Value <> SOURCE.Value 
--THEN 
--UPDATE SET TARGET.Value = SOURCE.Value
----When no records are matched, insert
----the incoming records from source
----table to target table
--WHEN NOT MATCHED BY TARGET THEN 
--INSERT ( User_Id, Value) 
--VALUES ( SOURCE.User_Id, SOURCE.Value)
----When there is a row that exists in target table and
----same record does not exist in source table
----then delete this record from target table
--WHEN NOT MATCHED BY SOURCE THEN 
--DELETE
----$action specifies a column of type nvarchar(10) 
----in the OUTPUT clause that returns one of three 
----values for each row: 'INSERT', 'UPDATE', or 'DELETE', 
----according to the action that was performed on that row
--OUTPUT $action, 
--DELETED.Id AS  Id, 
--DELETED.User_Id AS User_Id, 
--DELETED.Value AS Value, 
--INSERTED.Id AS  Id, 
--INSERTED.User_Id AS User_Id, 
--INSERTED.Value AS Value; 
--SELECT @@ROWCOUNT;
--GO





--�������� ���� �� ������ �� ���������� �����  +  ��� ���������� � output ��������� � Select update Delete
--ALTER PROCEDURE GetOUTPUT
-- @bookISBN varchar(75),
-- @OUT varchar(75) OUTPUT
-- AS
--BEGIN
--	DECLARE @c2 int
--		SELECT @c2 = COUNT(*) FROM Orders WHERE  Book_Id = @bookISBN
--		if @c2 > 1
--		BEGIN
--            SET @OUT = 'We Have Orders on this book';
--            RETURN;
--        END
--		SET @OUT = 'We Have not Orders on this book';
--	 RETURN;
--END
 
-- Go
-- DECLARE  @O varchar(75);
-- EXEC GetOUTPUT   
--   '123123123',
--   @O  output;

--SELECT @O AS RESULT;

--Insert into Discounts 
--OUTPUT inserted.* 
--values (2,15)

--Delete from Discounts 
--OUTPUT deleted.*
--where User_Id = 2



Select * from Authors as a, Authors_Books as ab where a.Id = ab.Author_Id
CROSS APPLY
Select Count(*) as Ssa  From Orders  Group by Orders.Book_Id Having ad.Book_Id = Orders.Book_Id;


--	FROM Users AS U
--CROSS APPLY 
--	(Select Sum(Orders.Cost) as Ssa  From Orders Where U.Id=Orders.User_Id Group by Orders.User_Id) AS ST;




--SELECT U.Email , Ssa
--	FROM Users AS U
--CROSS APPLY 
--	(Select Sum(Orders.Cost) as Ssa  From Orders Where U.Id=Orders.User_Id Group by Orders.User_Id) AS ST;



/*запрос на авторизацию 
select Users.Id , Users.Email , Users.Pwd , Roles.Value from Users , Persons , Roles where Users.Id = Persons.Id and  Users.Role_Id = Roles.Id and Users.Pwd = 'qweasd' and Users.Email  = 'v@mail.ru' ;
*/

/*процедура которая динамически достает нужные данные
ALTER PROCEDURE GetWithFilter
 @columnList varchar(75),
 @condition varchar(1000),
 @tables varchar(1000)
 AS
 DECLARE @sqlCommand varchar(1000)
	SET @sqlCommand = 'SELECT ' + @columnList + ' FROM ' + @tables + ' WHERE ' + @condition
	EXEC (@sqlCommand)
 GO

 
 EXEC GetWithFilter  @tables = ' Books ' , @columnList = 'ISBN, FilePath, Name' ,@condition = ' Name = ''Мастер и Маргарита''';*/
 
 
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
 --ALL если используется то выводятся все строки а если нет то удаляет повторяющиеся строки
 -- получаем все ISBN номера как томов так и самих книг
 
 --GO
 --SELECT ISBN  FROM Books
 --UNION 
 --Select ISBN_Tome FROM Books where ISBN_Tome IS NOT NULL;


 -- книги которые ни разу не заказывали
--GO
-- SELECT ISBN FROM Books
-- Except
-- Select Book_Id FROM Orders;

 --книги которые заказывали хоть раз
--GO
-- SELECT ISBN FROM Books
--  Intersect
-- Select Book_Id FROM Orders;


--количество пользователей определенных ролей
--SELECT  Users.Role_Id  ,  Count(Users.Id) FROM Users  Group By Users.Role_Id 


--сортировка книг по скачиваемости
--Select Book_Id ,SUM(Orders.Cost) As Suma , Count(Orders.Book_Id) as CNT ,RANK() OVER 
--    ( ORDER BY Count(Orders.Book_Id) DESC) AS Rank 
--	FROM Orders  Group By Orders.Book_Id 


--сортировка для каждого пользователя заказов по их стоймости 
--Select Book_Id  ,  Orders.User_Id,Orders.Cost,RANK() OVER 
--    (PARTITION BY Orders.User_Id ORDER BY Orders.Cost DESC) AS Rank 
--	FROM Orders  


--получаем сумму заказов для каждого пользователя
--SELECT U.Email , S
--	FROM Users AS U
--CROSS APPLY 
--	(Select Sum(Orders.Cost) as S From Orders Where U.Id=Orders.User_Id Group by Orders.User_Id) AS ST;

--обновляем данные о скидках для пользователей 
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














--проверка есть ли заказы на конкретную книгу 
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













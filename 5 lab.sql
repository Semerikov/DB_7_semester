
/*запрос на авторизацию 
select Users.Id , Users.Email , Users.Pwd , Roles.Value from Users , Persons , Roles where Users.Id = Persons.Id and  Users.Role_Id = Roles.Id and Users.Pwd = 'qweasd' and Users.Email  = 'v@mail.ru' ;
*/

/*получение списка книг с использованием фильтров

DECLARE @sqlCommand varchar(1000)
DECLARE @columnList varchar(75)
DECLARE @condition varchar(1000)
DECLARE @tables varchar(1000)
SET @tables = ' Books '
SET @columnList = 'ISBN, FilePath, Name'
SET @condition = ' Name = ''ћастер и ћаргарита'''
SET @sqlCommand = 'SELECT ' + @columnList + ' FROM ' + @tables + ' WHERE ' + @condition
EXEC (@sqlCommand)*/


/*процедура котора€ динамически достает нужные данные
CREATE PROCEDURE GetWithFilter
 @columnList varchar(75),
 @condition varchar(1000),
 @tables varchar(1000)
 AS
 DECLARE @sqlCommand varchar(1000)
	SET @sqlCommand = 'SELECT ' + @columnList + ' FROM ' + @tables + ' WHERE ' + @condition
	EXEC (@sqlCommand)
 GO

 
 EXEC GetWithFilter  @tables = ' Books ' , @columnList = 'ISBN, FilePath, Name' ,@condition = ' Name = ''ћастер и ћаргарита''';
 
 */
 /*UNION*/
 /*ALL если используетс€ то вывод€тс€ все строки а если нет то удал€ет повтор€ющиес€ строки
/*
 GO
 SELECT Name , ISBN FROM Books
 UNION ALL
 Select CartNumber , Role_Id FROM Users;

 */*/

 /*
 GO
 SELECT Name , ISBN FROM Books
 Except
 Select CartNumber , Role_Id FROM Users;

 
 GO
 SELECT Name , ISBN FROM Books
 Intersect
 SELECT Name , ISBN FROM Books
 
  */

/*
SELECT Users.Role_Id ,  Count(Users.Id) FROM Users  Group By Users.Role_Id
*/


/*
Select Users.Role_Id  , Users.Pwd , Users.Email,RANK() OVER 
    (PARTITION BY Users.Role_Id ORDER BY Users.Pwd DESC) AS Rank 
	FROM Users  */

	/*
	SELECT U.Email , ST.Cost
FROM Users AS U
    CROSS APPLY 
	(Select * From Orders Where U.Id=Orders.User_Id) AS ST;*/

/*
MERGE Discounts AS TARGET
USING UpdatedDiscounts AS SOURCE 
ON (TARGET.User_Id = SOURCE.User_Id) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND TARGET.Value <> SOURCE.Value 
THEN 
UPDATE SET TARGET.Value = SOURCE.Value
--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT ( User_Id, Value) 
VALUES ( SOURCE.User_Id, SOURCE.Value)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: 'INSERT', 'UPDATE', or 'DELETE', 
--according to the action that was performed on that row
OUTPUT $action, 
DELETED.Id AS  Id, 
DELETED.User_Id AS User_Id, 
DELETED.Value AS Value, 
INSERTED.Id AS  Id, 
INSERTED.User_Id AS User_Id, 
INSERTED.Value AS Value; 
SELECT @@ROWCOUNT;
GO*/


/*процедура котора€ динамически достает нужные данные
DROP PROCEDURE GetWithFilt;*/


/*
ALTER PROCEDURE GetOUTPUT
 @c varchar(75),
 @OUT varchar(122) OUTPUT
 AS
BEGIN
	IF @c >= '1'
        BEGIN
            SET @OUT = 'HELLO';

            RETURN;
        END
	RETURN
END
 

 DECLARE  @O varchar(122);
 EXEC GetOUTPUT   
   '1',
   @O  output;

 SELECT @O;*/
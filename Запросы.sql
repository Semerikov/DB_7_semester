
/*������ �� ����������� 
select Users.Id , Users.Email , Users.Pwd , Roles.Value from Users , Persons , Roles where Users.Id = Persons.Id and  Users.Role_Id = Roles.Id and Users.Pwd = 'qweasd' and Users.Email  = 'v@mail.ru' ;
*/

/*��������� ������� ����������� ������� ������ ������
ALTER PROCEDURE GetWithFilter
 @columnList varchar(75),
 @condition varchar(1000),
 @tables varchar(1000)
 AS
 DECLARE @sqlCommand varchar(1000)
	SET @sqlCommand = 'SELECT ' + @columnList + ' FROM ' + @tables + ' WHERE ' + @condition
	EXEC (@sqlCommand)
 GO

 
 EXEC GetWithFilter  @tables = ' Books ' , @columnList = 'ISBN, FilePath, Name' ,@condition = ' Name = ''������ � ���������''';*/
 
 
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
































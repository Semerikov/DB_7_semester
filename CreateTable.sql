Use BookShop;


DROP TABLE Discounts;
DROP TABLE Authors_Books;
DROP TABLE Authors;
DROP TABLE Orders;
DROP TABLE Users;
DROP TABLE Persons;
DROP TABLE Costs;
DROP TABLE Books;
DROP TABLE Roles;




CREATE TABLE Roles(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Value varchar(10) NOT NULL ,
CHECK (Value in ('ADMIN','USER'))
)

CREATE TABLE Books (
ISBN varchar(20) NOT NULL PRIMARY KEY,
FilePath varchar(256) NOT NULL,
Cover varchar(256),
Description varchar(1500),
Name varchar(300) NOT NULL,
ISBN_Tome varchar(20),
RealiseYear INT,
NextPart_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
PreviousPart_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
Check(((NextPart_Id!=null OR PreviousPart_Id!=null) AND ISBN_Tome!=null)OR(NextPart_Id = null AND PreviousPart_Id = null AND ISBN_Tome = null) OR ISBN_Tome!=null)
)

CREATE TABLE Costs (
Id varchar(20) NOT NULL FOREIGN KEY REFERENCES Books(ISBN), 
Cost FLOAT NOT NULL,
Discount FLOAT NOT NULL , 
PRIMARY KEY (Id)
);

CREATE TABLE Persons(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name varchar(50) NOT NULL,
Surname varchar(50) NOT NULL,
BirthDay DATE NOT NULL
)

CREATE TABLE Users(
Email varchar(255) NOT NULL,
Pwd varchar(50) NOT NULL,
CartNumber varchar(16) NOT NULL CHECK (LEN(CartNumber)=16),
Role_Id INT NOT NULL FOREIGN KEY REFERENCES Roles(Id),
Id INT NOT NULL FOREIGN KEY REFERENCES Persons(Id) PRIMARY KEY
)

CREATE TABLE Authors(
DeathDay DATE,
Photo nvarchar(255),
Id INT NOT NULL FOREIGN KEY REFERENCES Persons(Id) PRIMARY KEY
)

CREATE TABLE Orders(
Book_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
User_Id INT FOREIGN KEY REFERENCES Users(Id),
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Creation_Date DATE , 
Cost Float Not Null
)

CREATE TABLE Discounts(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
User_Id INT FOREIGN KEY REFERENCES Users(Id),
Value float NOT NULL ,
CHECK (Value>0)
)

CREATE TABLE Authors_Books
(
Author_Id INT FOREIGN KEY REFERENCES Authors(Id),
Book_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN)
CONSTRAINT Id PRIMARY KEY (Author_Id,Book_Id)
)
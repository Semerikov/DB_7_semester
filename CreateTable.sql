Use BookShop;


DROP TABLE Discounts;
DROP TABLE Authors_Books;
DROP TABLE Authors;
DROP TABLE Orders;
DROP TABLE Users;
DROP TABLE Persons;
DROP TABLE Books;
DROP TABLE Costs;
DROP TABLE Roles;


CREATE TABLE Costs (
Id INT NOT NULL IDENTITY(1,1), 
Cost FLOAT NOT NULL,
Discount FLOAT NOT NULL , 
PRIMARY KEY (Id)
);

CREATE TABLE Roles(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Value varchar(10) NOT NULL ,
CHECK (Value in ('ADMIN','USER'))
)

CREATE TABLE Books (
ISBN varchar(20) NOT NULL PRIMARY KEY,
FilePath varchar(256) NOT NULL,
Description varchar(1500),
Name varchar(300) NOT NULL,
Cost_Id Int NOT NULL FOREIGN KEY REFERENCES Costs(Id),
ISBN_Tome varchar(20),
RealiseDate DATE,
NextPart_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
PreviousPart_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
Check(((NextPart_Id!=null OR PreviousPart_Id!=null) AND ISBN_Tome!=null)OR(NextPart_Id = null AND PreviousPart_Id = null AND ISBN_Tome = null) OR ISBN_Tome!=null)
)

CREATE TABLE Persons(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Name varchar(50) NOT NULL,
Surname varchar(50) NOT NULL,
BirthDay DATE NOT NULL
)

CREATE TABLE Users(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
Email varchar(255) NOT NULL,
Pwd varchar(50) NOT NULL,
CartNumber varchar(16) NOT NULL CHECK (LEN(CartNumber)=16),
Role_Id INT NOT NULL FOREIGN KEY REFERENCES Roles(Id),
Person_Id INT NOT NULL FOREIGN KEY REFERENCES Persons(Id)
)

CREATE TABLE Authors(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
DeathDay DATE,
Photo Image,
Person_Id INT NOT NULL FOREIGN KEY REFERENCES Persons(Id)
)

CREATE TABLE Orders(
Book_Id varchar(20) FOREIGN KEY REFERENCES Books(ISBN),
User_Id INT FOREIGN KEY REFERENCES Users(Id),
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
ExpirationDate DATE , 
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
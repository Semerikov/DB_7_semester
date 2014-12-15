use BookShop;

-- insert roles
SET IDENTITY_INSERT Roles ON;

INSERT INTO Roles(Id, Value) VALUES
	(1,'ADMIN'),
	(2,'USER')

SET IDENTITY_INSERT Roles OFF;

-- insert authors
SET IDENTITY_INSERT Persons ON;

INSERT INTO Persons(Id, Name, Surname, BirthDay) VALUES
	(1, 'Чак', 'Паланик', '1962-02-21'),
	(2, 'Виктор','Пелевин','1962-11-22'),
	(3, 'Лев','Толстой','1828-08-28'),
	(4, 'Оскар','Уайльд','1854-10-16'),
	(5, 'Иван','Тургенев','1818-10-28'),
	(6, 'Борис','Акунин','1956-05-20'),
	(7, 'Джоан','Роулинг','1965-07-31'),
	(8, 'Маргарет','Митчелл','1900-11-8'),
	(9, 'Мариам','Петросян','1969-08-10'),
	(10,'Аркадий','Стругацкий','1925-08-28'),
	(11, 'Борис','Стругацкий','1933-04-15')

SET IDENTITY_INSERT Persons OFF;

DECLARE @ImageDir nvarchar(50);
SET @ImageDir = 'c:/Users/Sinitsin/Desktop/DbImages/';

INSERT INTO Authors VALUES
	(null, @ImageDir+'Palanik.jpg', 1),
	(null, @ImageDir+'Pelevin.jpg', 2),
	('1910-11-10', @ImageDir+'Tolstoy.jpg', 3),
	('1900-11-30', @ImageDir+'Wilde.jpeg', 4),
	('1883-09-03', @ImageDir+'Turgenev.jpg', 5),
	(null, @ImageDir+'Akunin.jpg', 6),
	(null, @ImageDir+'Rowling.jpg', 7),
	('1949-08-16', @ImageDir+'Mitchell.jpg',8),
	(null, @ImageDir+'Petrosian.jpg', 9),
	('1991-10-12', @ImageDir+'AStrugatskiy.jpg', 10),
	('2012-11-19', @ImageDir+'BStrugatskiy.jpg', 11)
	
-- insert users
SET IDENTITY_INSERT Persons ON;

INSERT INTO Persons(Id, Name, Surname, BirthDay) VALUES
	(12, 'Виктор', 'Семериков', '1994-01-07'),
	(13, 'Андрей','Сасимович','1992-01-14'),
	(14, 'Кирилл','Косой','1988-07-28'),
	(15, 'Геннадий','Клочко','1981-10-06'),
	(16, 'Мария','Кирилленко','1989-01-08')

SET IDENTITY_INSERT Persons OFF;

INSERT INTO Users VALUES
	('semerikov@mail.ru', 'admin1234', '1234567890123456', 1, 12),
	('sasimovich@gmail.ru', 'password', '1234567890654321', 1, 13),
	('kiruha1996@mail.ru', 'password', '0987654321654321', 2, 14),
	('kosoy17@tut.by', 'password', '1234509876123456', 2, 15),
	('nashamasha@tut.by', 'password', '0987612345123654', 2, 16)

--insert books

DECLARE @BookFilesDir nvarchar(50);
DECLARE @CoverFileDir nvarchar(50);
SET @BookFilesDir = 'c:/BookFiles/';
SET @CoverFileDir = 'c:/Users/Sinitsin/Desktop/BookCovers/';


INSERT INTO Books VALUES
	('978-5-699-53962-8', @BookFilesDir+'alpinist.txt', null,'«S.N.U.F.F.» - роман-утопия Виктора Пелевина о глубочайших тайнах женского сердца и высших секретах летного мастерства.',
		'S.N.U.F.F.', null, 2011, null, null),
	('978-5-699-64464-3', @BookFilesDir+'alpinist.txt', null,'
		Миром правят вампиры.
		Не вечно молодые, романтичные юноши и девушки с бессонными глазами и кровавыми губами, а вполне обыкновенные мужчины и женщины со следами жизненного цинизма на лице. Им одним открыт секрет «гламура» и «дискурса». И они — настоящая мировая элита, вербующая в свои ряды прохожих одним легким укусом.
		Выбор может пасть на каждого...
		',
		'Empire V', null, 2013, null, null),
	('978-5-17-081137-3', @BookFilesDir+'alpinist.txt',@CoverFileDir + 'BKlub.jpg',
		'
			Это - самая потрясающая и самая скандальная книга 1990-х.
			Книга, в которой устами Чака Паланика заговорило не просто "поколение икс", но - "поколение икс" уже озлобленное, уже растерявшее свои последние иллюзии.
			Вы смотрели фильм "Бойцовский клуб"?
			Тогда - читайте книгу, по которой он был снят!
		',
		'Бойцовский клуб', null, 2013, null, null),
	('978-5-17-081138-0', @BookFilesDir+'alpinist.txt', null, null, 'Удушье', null, 2014, null, null),
	('978-5-699-70287-9', @BookFilesDir+'alpinist.txt', null,null, 'Война и мир. Том I-II.', '000-0-000-00000-1', 2012, '978-5-699-70294-7', null),
	('978-5-699-70294-7', @BookFilesDir+'alpinist.txt', null,null, 'Война и мир. Том III-IV.', '000-0-000-00000-1', 2012, null, '978-5-699-70287-9'),
	('978-5-699-65394-2', @BookFilesDir+'alpinist.txt', null,null, 'Унесённые ветром. Том 1', '000-0-000-00000-3', 2008, '978-5-699-65397-3', null),
	('978-5-699-65397-3', @BookFilesDir+'alpinist.txt', null,null, 'Унесённые ветром. Том 2', '000-0-000-00000-3', 2008, null, '978-5-699-65394-2'),
	('978-5-17-066564-8', @BookFilesDir+'alpinist.txt', null,null, 'Портрет Доирана Грея', null, 2009, null, null),
	('978-5-699-76459-4', @BookFilesDir+'alpinist.txt', null,null, 'Месяц в деревне', null, 2010, null, null),
	('978-5-17-088391-2', @BookFilesDir+'alpinist.txt', null,null, 'Пелагия и черный монах', null, 2007, null, null),
	('978-5-389-05394-6', @BookFilesDir+'alpinist.txt', null,null, 'Случайная вакансия', null, 2010, null, null),
	('978-5-9689-0174-3', @BookFilesDir+'', null,null, 'Дом, в котором...', null, 2009, null, null),
	('5-699-18215-2', @BookFilesDir+'alpinist.txt',null, null, 'Отель "У погибшего альпиниста"', null, 2007, null, null),
	('978-5-389-07435-4', @BookFilesDir+'alpinist.txt',null, null, 'Гарри Поттер и философский камень', null, 2014, null, null),
	('978-5-359-07435-4', @BookFilesDir+'alpinist.txt',null, null, 'Гарри Поттер и тайная комната', null, 2014, null, null),
	('978-5-351-02435-4', @BookFilesDir+'alpinist.txt',null, null, 'Гарри Поттер и узник Азкабана', null, 2014, null, null),
	('978-5-321-02115-4', @BookFilesDir+'alpinist.txt',null, null, 'Гарри Поттер и Кубок Огня', null, 2014, null, null),
	('5-699-18325-2', @BookFilesDir+'alpinist.txt',null, null, 'Далёкая радука', null, 2002, null, null),
	('5-629-18215-2', @BookFilesDir+'alpinist.txt',null, null, 'Трудно быть богом', null, 2005, null, null)

-- insert books-authors relations
INSERT INTO Authors_Books VALUES
	(10, '5-629-18215-2'),
	(11, '5-629-18215-2'),
	(10, '5-699-18215-2'),
	(11, '5-699-18215-2'),
	(10, '5-699-18325-2'),
	(11, '5-699-18325-2'),
	(4,'978-5-17-066564-8'),
	(1,'978-5-17-081137-3'),
	(1,'978-5-17-081138-0'),
	(6,'978-5-17-088391-2'),
	(7,'978-5-321-02115-4'),
	(7,'978-5-351-02435-4'),
	(7,'978-5-359-07435-4'),
	(7,'978-5-389-05394-6'),
	(7,'978-5-389-07435-4'),
	(2,'978-5-699-53962-8'),
	(2,'978-5-699-64464-3'),
	(8,'978-5-699-65394-2'),
	(8,'978-5-699-65397-3'),
	(3,'978-5-699-70287-9'),
	(3,'978-5-699-70294-7'),
	(5,'978-5-699-76459-4'),
	(9,'978-5-9689-0174-3')
	
-- insert costs
INSERT INTO Costs VALUES
	('5-629-18215-2', 1000, 0),
	('5-699-18215-2', 1100, 10),
	('5-699-18325-2', 900, 10),
	('978-5-17-066564-8', 1500, 0),
	('978-5-17-081137-3', 1200, 10),
	('978-5-17-081138-0', 2000, 0),
	('978-5-17-088391-2', 900, 15),
	('978-5-321-02115-4', 1850, 0),
	('978-5-351-02435-4', 1300, 0),
	('978-5-359-07435-4', 1050, 5),
	('978-5-389-05394-6', 1340, 0),
	('978-5-389-07435-4', 900, 0),
	('978-5-699-53962-8', 1750, 5),
	('978-5-699-64464-3', 1650, 5),
	('978-5-699-65394-2', 2100, 0),
	('978-5-699-65397-3', 1900, 0),
	('978-5-699-70287-9', 1200, 0),
	('978-5-699-70294-7', 800, 10),
	('978-5-699-76459-4', 1100, 10),
	('978-5-9689-0174-3', 2000, 15)

-- insert discounts
SET IDENTITY_INSERT Discounts ON;

INSERT INTO Discounts(Id, User_Id, Value) VALUES
	(1, 14, 5),
	(2, 15, 10)

SET IDENTITY_INSERT Discounts OFF;

-- insert orders
SET IDENTITY_INSERT Orders ON;

INSERT INTO Orders(Id, Book_Id, User_Id, Cost, Creation_Date) VALUES
	(1, '978-5-17-081138-0', 16, 1000, '2013-09-15'),
	(2, '978-5-699-70294-7', 16, 1200, '2014-08-15'),
	(3, '978-5-699-53962-8', 16, 1950, '2014-10-11'),
	(4, '5-699-18215-2', 15, 2150, '2012-04-05'),
	(5, '978-5-17-081138-0', 15, 900, '2012-05-01'),
	(6, '978-5-9689-0174-3', 15, 1800, '2013-01-14'),
	(7, '978-5-699-76459-4', 15, 1200, '2013-03-29'),
	(8, '978-5-17-066564-8', 14, 1990, '2014-10-30')

SET IDENTITY_INSERT Orders OFF;
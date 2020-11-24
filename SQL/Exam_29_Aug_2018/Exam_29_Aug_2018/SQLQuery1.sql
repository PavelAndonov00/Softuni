--DDL
CREATE DATABASE Supermarket

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
)

CREATE TABLE Items(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	Price DECIMAL(15, 2) NOT NULL,
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id)
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Phone VARCHAR(12) NOT NULL,
	Salary DECIMAL(15, 2) NOT NULL
)

CREATE TABLE Orders(
	Id INT PRIMARY KEY IDENTITY,
	[DateTime] DATETIME NOT NULL,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id)
)

CREATE TABLE OrderItems(
	OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
	ItemId INT NOT NULL FOREIGN KEY REFERENCES Items(Id),
	Quantity INT NOT NULL CHECK (Quantity > 0) DEFAULT 1,

	CONSTRAINT PK_OrderItems PRIMARY KEY (OrderId, ItemId)
)

CREATE TABLE Shifts(
	Id INT IDENTITY NOT NULL,
	EmployeeId INT NOT NULL FOREIGN KEY REFERENCES Employees(Id),
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL,

	CONSTRAINT PK_Shifts PRIMARY KEY (Id, EmployeeId),
	CONSTRAINT CH_CheckOutMustBeAfterCheckIn CHECK(CheckOut > CheckIn)
)

--Insert
INSERT INTO Employees
VALUES
('Stoyan', 'Petrov',	'888-785-8573', 500.25),
('Stamat', 'Nikolov',	'789-613-1122', 999995.25),
('Evgeni', 'Petkov',    '645-369-9517', 1234.51),
('Krasimir', 'Vidolov', '321-471-9982', 50.25)

INSERT INTO Items
VALUES
('Tesla battery', 154.25, 8),
('Chess', 30.25, 8),
('Juice', 5.32, 1),
('Glasses', 10, 8),
('Bottle of water', 1, 1)

--Update
UPDATE Items
SET Price *= 1.27
WHERE CategoryId IN(1, 2, 3)

--Delete
DELETE
FROM OrderItems
WHERE OrderId = 48

--QUERING

--05. Richest People 
SELECT Id, FirstName
FROM Employees
WHERE Salary > 6500
ORDER BY FirstName, Id

--06. Cool Phone Numbers 
SELECT FirstName + ' ' + LastName AS FullName, Phone
FROM Employees
WHERE Phone LIKE '3%'
ORDER BY FullName, Phone

--07. Employee Statistics 
SELECT FirstName, LastName, COUNT(o.Id) AS OrdersCount
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
GROUP BY e.Id, e.FirstName, e.LastName
ORDER BY OrdersCount DESC, e.FirstName

--08. Hard Workers Club 
SELECT e.FirstName, e.LastName, AVG(DATEDIFF(HOUR, CheckIn, CheckOut)) AS [Work hours]
FROM Employees AS e
JOIN Shifts AS s ON s.EmployeeId = e.Id
GROUP BY e.Id, e.FirstName,  e.LastName
HAVING AVG(DATEDIFF(HOUR, CheckIn, CheckOut)) > 7
ORDER BY [Work hours] DESC, e.Id

--09. The Most Expensive Order
SELECT TOP(1) o.Id, SUM(i.Price * oi.Quantity) AS TotalPrice
FROM Items AS i
JOIN OrderItems AS oi ON oi.ItemId = i.Id
JOIN Orders AS o ON o.Id = oi.OrderId
GROUP BY o.Id
ORDER BY TotalPrice DESC

--10. Rich Item, Poor Item
SELECT TOP(10) o.Id AS OrderId, MAX(Price) AS ExpensivePrice, MIN(Price) AS CheapPrice
FROM Items AS i
JOIN OrderItems AS oi ON oi.ItemId = i.Id
JOIN Orders AS o ON o.Id = oi.OrderId
GROUP BY o.Id
ORDER BY ExpensivePrice DESC, OrderId 

--11. Cashiers 
SELECT DISTINCT e.Id, e.FirstName, e.LastName
FROM Employees AS e
JOIN Orders AS o ON o.EmployeeId = e.Id
ORDER BY e.Id

--12. Lazy Employees 
SELECT e.Id, e.FirstName + ' ' + e.LastName AS [Full Name]
FROM Employees AS e
JOIN Shifts AS s ON s.EmployeeId = e.Id
GROUP BY e.Id, e.FirstName,  e.LastName
HAVING MIN(DATEDIFF(HOUR, CheckIn, CheckOut)) < 4
ORDER BY e.Id

--13. Sellers 
SELECT e.FirstName + ' ' + e.LastName AS [Full Name], SUM(i.Price * oi.Quantity) AS [Total Price], SUM(oi.Quantity) AS Items
FROM Items AS i
JOIN OrderItems AS oi ON oi.ItemId = i.Id
JOIN Orders AS o ON o.Id = oi.OrderId
JOIN Employees AS e ON e.Id = o.EmployeeId
WHERE o.DateTime < '2018-06-15'
GROUP BY e.Id, e.FirstName, e.LastName
ORDER BY [Total Price] DESC 

--14. Tough Days 
SELECT e.FirstName + ' ' + e.LastName AS [Full Name],
		CASE
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 1 THEN 'Sunday'
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 2 THEN 'Monday'
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 3 THEN 'Tuesday'
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 4 THEN 'Wednesday'
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 5 THEN 'Thursday'
			 WHEN DATEPART(WEEKDAY, s.CheckIn) = 6 THEN 'Friday'
			 ELSE 'Saturday'
		END AS [Day of week]
FROM Employees AS e 
LEFT JOIN Orders AS o ON o.EmployeeId = e.Id
JOIN Shifts AS s ON s.EmployeeId = e.Id
WHERE o.EmployeeId IS NULL AND  > 12
ORDER BY e.Id

--15. Top Order per Employee 
SELECT emp.FirstName + ' ' + emp.LastName AS FullName, DATEDIFF(HOUR, s.CheckIn, s.CheckOut) AS WorkHours, e.TotalPrice AS TotalPrice FROM 
 (
    SELECT o.EmployeeId, SUM(oi.Quantity * i.Price) AS TotalPrice, o.DateTime,
	ROW_NUMBER() OVER (PARTITION BY o.EmployeeId ORDER BY o.EmployeeId, SUM(i.Price * oi.Quantity) DESC ) AS Rank
    FROM Orders AS o
    JOIN OrderItems AS oi ON oi.OrderId = o.Id
    JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY o.EmployeeId, o.Id, o.DateTime
) AS e 
JOIN Employees AS emp ON emp.Id = e.EmployeeId
JOIN Shifts AS s ON s.EmployeeId = e.EmployeeId
WHERE e.Rank = 1 AND e.DateTime BETWEEN s.CheckIn AND s.CheckOut
ORDER BY FullName, WorkHours DESC, TotalPrice DESC

--16. Average Profit per Day 
SELECT DATEPART(DAY, DateTime) AS DayOfMonth, CAST(AVG(i.Price * oi.Quantity) AS DECIMAL(15, 2)) AS TotalProfit
FROM Orders AS o
JOIN OrderItems AS oi ON oi.OrderId = o.Id
JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY DATEPART(DAY, DateTime)
ORDER BY DayOfMonth

--17. Top Products 
SELECT i.Name, c.Name, SUM(oi.Quantity) AS Count, SUM(i.Price * oi.Quantity) AS TotalPrice
FROM Items AS i
LEFT JOIN Categories AS c ON c.Id = i.CategoryId
LEFT JOIN OrderItems AS oi ON oi.ItemId = i.Id
LEFT JOIN Orders AS o ON o.Id = oi.OrderId
GROUP BY i.Name, c.Name
ORDER BY TotalPrice DESC, Count DESC
GO

--18. Promotion Days 
CREATE FUNCTION udf_GetPromotedProducts (@CurrentDate DATE, @StartDate DATE, @EndDate DATE, @Discount DECIMAL(15, 2), @FirstItemId INT, @SecondItemId INT, @ThirdItemId INT)
RETURNS NVARCHAR(MAX)
BEGIN
	IF((SELECT Id FROM Items WHERE Id = @FirstItemId) IS NOT NULL AND 
	   (SELECT Id FROM Items WHERE Id = @SecondItemId) IS NOT NULL AND 
	   (SELECT Id FROM Items WHERE Id = @ThirdItemId) IS NOT NULL AND 
	   (@CurrentDate BETWEEN @StartDate AND @EndDate))
	BEGIN
		DECLARE @DiscountRate DECIMAL(15, 2) = ((CAST((100 - @Discount) AS DECIMAL(15, 2))) / 100);
		DECLARE @FirstItemName NVARCHAR(MAX) = (SELECT Name FROM Items WHERE Id = @FirstItemId);
		DECLARE @FirstItemPrice DECIMAL(15, 2) = (SELECT Price FROM Items WHERE Id = @FirstItemId) * @DiscountRate;
		DECLARE @SecondItemName NVARCHAR(MAX) = (SELECT Name FROM Items WHERE Id = @SecondItemId);
		DECLARE @SecondItemPrice  DECIMAL(15, 2) = (SELECT Price FROM Items WHERE Id = @SecondItemId) * @DiscountRate;
		DECLARE @ThirdItemName NVARCHAR(MAX) = (SELECT Name FROM Items WHERE Id = @ThirdItemId);
		DECLARE @ThirdItemPrice DECIMAL(15, 2) = (SELECT Price FROM Items WHERE Id = @ThirdItemId) * @DiscountRate;
		RETURN @FirstItemName + ' price: ' + CAST(@FirstItemPrice AS NVARCHAR(50)) + ' <-> ' +
		 @SecondItemName + ' price: ' + CAST(@SecondItemPrice AS NVARCHAR(50)) + ' <-> ' + 
		 @ThirdItemName + ' price: ' + CAST(@ThirdItemPrice AS NVARCHAR(50));
	END
	ELSE IF((SELECT Id FROM Items WHERE Id = @FirstItemId) IS NULL OR 
	   (SELECT Id FROM Items WHERE Id = @SecondItemId) IS NULL OR
	   (SELECT Id FROM Items WHERE Id = @ThirdItemId) IS NULL)
	BEGIN
	    RETURN 'One of the items does not exists!';
	END

	RETURN 'The current date is not within the promotion dates!';
END

SELECT Price FROM Items WHERE Name = 'Water'


--19. Cancel Order 
CREATE PROC usp_CancelOrder(@OrderId INT, @CancelDate DATETIME)
AS
BEGIN
	IF((SELECT Id FROM Orders WHERE Id = @OrderId) IS NULL)
	BEGIN
		RAISERROR('The order does not exist!', 16, 1);
	END
	
	IF(DATEDIFF(DAY, (SELECT DateTime FROM Orders WHERE Id = @OrderId), @CancelDate) = 3)
	BEGIN
		RAISERROR('You cannot cancel the order!', 16, 1);
	END

	DELETE
	FROM OrderItems
	WHERE OrderId = @OrderId

	DELETE
    FROM Orders
	WHERE Id = @OrderId
	
END

EXEC usp_CancelOrder 1, '2018-06-15'
EXEC usp_CancelOrder 1, '2018-06-02'
SET IDENTITY_INSERT Orders OFF
INSERT INTO Orders (Id, DateTime, EmployeeId) VALUES
(1, '2018-06-01 11:59:06', 2)
SELECT COUNT(*) FROM Orders
SELECT COUNT(*) FROM OrderItems


--20
CREATE TABLE DeletedOrders (
	OrderId INT,
    ItemId INT, 
	ItemQuantity INT
)

CREATE TRIGGER t_DeleteOrders
    ON OrderItems AFTER DELETE
    AS
    BEGIN
	  INSERT INTO DeletedOrders (OrderId, ItemId, ItemQuantity)
	  SELECT d.OrderId, d.ItemId, d.Quantity
	    FROM deleted AS d
    END

DELETE FROM OrderItems
WHERE OrderId = 5

DELETE FROM Orders
WHERE Id = 5 

SELECT * FROM DeletedOrders
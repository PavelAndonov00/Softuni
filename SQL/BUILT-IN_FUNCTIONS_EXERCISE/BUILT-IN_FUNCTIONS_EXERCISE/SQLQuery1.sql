--01. Find Names of All Employees by First Name 
SELECT FirstName, LastName
  FROM Employees
 WHERE FirstName LIKE 'SA%'

 --02. Find Names of All Employees by Last Name
 SELECT FirstName, LastName
  FROM Employees
 WHERE LastName LIKE '%ei%'

 --03. Find First Names of All Employess
 SELECT FirstName
   FROM Employees
  WHERE DepartmentID IN(3, 10)
    AND YEAR(HireDate) BETWEEN 1995 AND 2005

--04. Find All Employees Except Engineers 
 SELECT FirstName, LastName
  FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%'

--05. Find Towns with Name Length 
SELECT [Name]
  FROM Towns
 WHERE LEN([Name]) IN(5, 6) 
ORDER BY [Name]

--06. Find Towns Starting With 
SELECT *
FROM Towns
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]

--07. Find Towns Not Starting With
SELECT *
FROM Towns
WHERE [Name] LIKE '[^RBD]%'
ORDER BY [Name]
GO

--08. Create View Employees Hired After
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
FROM Employees
WHERE YEAR(HireDate) > 2000
GO

--09. Length of Last Name 
SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5

--10. Countries Holding 'A'
SELECT CountryName, IsoCode
FROM Countries
WHERE CountryName LIKE '%a%a%a%'
ORDER BY IsoCode

--11. Mix of Peak and River Names
SELECT PeakName, RiverName, LOWER(PeakName + SUBSTRING(RiverName, 2, LEN(RiverName))) AS Mix
FROM Peaks 
JOIN Rivers ON LEFT(RiverName, 1) = RIGHT(PeakName, 1) 
ORDER BY Mix

--12. Games From 2011 and 2012 Year
SELECT TOP(50) Name , FORMAT(Start, 'yyyy-MM-dd') AS Start
FROM Games
WHERE YEAR(Start) IN(2011, 2012)
ORDER BY Start, Name

--13. User Email Providers 
SELECT Username, SUBSTRING(Email, CHARINDEX('@', Email)+1, LEN(Email)) AS EmailProvider
FROM Users
ORDER BY EmailProvider, Username

--14. Get Users with IPAddress Like Pattern 
SELECT Username, IpAddress
FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username

--15. Show All Games with Duration 
SELECT Name AS Game, 
		CASE 
			WHEN DATEPART(HOUR, Start) BETWEEN 0 and 11 THEN 'Morning'
			WHEN DATEPART(HOUR, Start) BETWEEN 12 AND 17 THEN 'Afternoon'
			WHEN DATEPART(HOUR, Start) BETWEEN 18 AND 24 THEN 'Evening'
		END AS [Part of the Day],
		CASE 
			WHEN Duration <= 3 THEN 'Extra Short'
			WHEN Duration IN(4, 5, 6) THEN 'Short'
			WHEN Duration > 6 THEN 'Long'
			WHEN Duration IS NULL THEN 'Extra Long'
		END AS [Duration]
FROM Games
ORDER BY Game, Duration, [Part of the Day]

--16. Orders Table 
SELECT ProductName, OrderDate, DATEADD(DAY, 3, OrderDate) AS [Pay Due], DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders

--17. People Table
CREATE TABLE People(
	Id INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	Birthdate DATETIME NOT NULL,

	CONSTRAINT PK_Id PRIMARY KEY (Id)
)

INSERT INTO People ([Name], Birthdate)
VALUES ('Victor', '2000-12-07 00:00:00.000'),
('Steven', '1992-09-10 00:00:00.000'),
('Stephen', '1910-09-19 00:00:00.000'),
('John',    '2010-01-06 00:00:00.000')

SELECT [Name],
	   DATEDIFF(YEAR, Birthdate, GETDATE()) AS [Age in Years], 
	   DATEDIFF(MONTH, Birthdate, GETDATE()) AS [Age in Months],
	   DATEDIFF(DAY, Birthdate, GETDATE()) AS [Age in Days], 
	   DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Minutes]
  FROM People

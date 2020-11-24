--01. Employees with Salary Above 35000 
CREATE OR ALTER PROCEDURE usp_GetEmployeesSalaryAbove35000 
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary > 35000

EXEC dbo.usp_GetEmployeesSalaryAbove35000
GO

--02. Employees with Salary Above Number 
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @number DECIMAL(18, 2) 
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary >= @number

EXEC dbo.usp_GetEmployeesSalaryAboveNumber @number = 48100
GO

--03. Town Names Starting With
CREATE PROCEDURE usp_GetTownsStartingWith (@startingString VARCHAR(50))
AS
	SELECT Name
	FROM Towns
	WHERE Name LIKE @startingString + '%'

EXEC dbo.usp_GetTownsStartingWith 'b'
GO

--04. Employees from Town
CREATE PROCEDURE usp_GetEmployeesFromTown (@townName VARCHAR(50))
AS
	SELECT e.FirstName, e.LastName
	FROM Employees AS e
	JOIN Addresses AS a
	ON a.AddressID = e.AddressID
	JOIN Towns AS t
	ON t.TownID = a.TownID
	WHERE t.Name = @townName

EXEC dbo.usp_GetEmployeesFromTown @townName = 'Sofia'
GO

--05. Salary Level Function 
CREATE FUNCTION ufn_GetSalaryLevel (@salary DECIMAL(18,4)) 
RETURNS VARCHAR(7)
AS 
BEGIN
	IF (@salary < 30000)
	BEGIN
		RETURN 'Low'
	END
	ELSE IF (@salary >= 30000 AND @salary <= 50000)
	BEGIN
		RETURN 'Average'
	END

	RETURN 'High'
END

SELECT Salary, dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level]
FROM Employees
GO

--06. Employees by Salary Level 
CREATE PROCEDURE usp_EmployeesBySalaryLevel (@levelOfSalary VARCHAR(7))
AS
SELECT FirstName, LastName
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @levelOfSalary

--07. Define Function 
CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(50), @word VARCHAR(50))
RETURNS BIT AS
BEGIN
	DECLARE @startPossition INT = 1;
	DECLARE @currentLetter VARCHAR(MAX) = SUBSTRING(@word, @startPossition, 1);
	DECLARE @charIndex INT = CHARINDEX(@currentLetter, @setOfLetters);
	WHILE @charIndex > 0
	BEGIN
		IF (@startPossition = LEN(@word))
		BEGIN
			RETURN 1;
		END

		SET @startPossition += 1;
		SET @currentLetter = SUBSTRING(@word, @startPossition, 1);
		SET @charIndex = CHARINDEX(@currentLetter, @setOfLetters);
	END
	
	RETURN 0;
END

SELECT dbo.ufn_IsWordComprised('oiw', 'Sofia')
GO

--08. Delete Employees and Departments 
CREATE PROCEDURE usp_DeleteEmployeesFromDepartment (@departmentId INT) AS
BEGIN

DELETE FROM EmployeesProjects
WHERE EmployeeID IN(SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

ALTER TABLE Departments
ALTER COLUMN ManagerID INT

UPDATE Employees
SET ManagerID = NULL
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

UPDATE Departments
SET ManagerID = NULL
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

DELETE FROM Employees
WHERE DepartmentID = @departmentId

DELETE FROM Departments
WHERE DepartmentID = @departmentId

SELECT COUNT(*)
FROM Employees
WHERE DepartmentID = (@departmentId)

END
GO

--09. Find Full Name
CREATE PROCEDURE usp_GetHoldersFullName AS
SELECT FirstName + ' ' + LastName AS [Full Name]
FROM AccountHolders

EXEC dbo.usp_GetHoldersFullName
GO

--10. People with Balance Higher Than 
CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@number DECIMAL(16, 2)) AS
BEGIN	
  WITH cte_TotalBalanceByAccountHolderId (AccountHolderId, TotalBalance) AS
  (SELECT a.AccountHolderId, SUM(a.Balance) AS TotalBalance
  FROM Accounts AS a
  GROUP BY AccountHolderId)

  SELECT ah.FirstName, ah.LastName
  FROM  cte_TotalBalanceByAccountHolderId AS ctb
  JOIN AccountHolders AS ah ON ah.Id = ctb.AccountHolderId
  WHERE ctb.TotalBalance > @number
  ORDER BY ah.FirstName, ah.LastName
END
GO

--11. Future Value Function
CREATE FUNCTION ufn_CalculateFutureValue(@Sum DECIMAL(16, 2), @Interest FLOAT, @Years INT) 
RETURNS DECIMAL(20, 4) AS
BEGIN
	RETURN @Sum * POWER((1 + @Interest), @Years);
END
GO

--12. Calculating Interest 
CREATE PROC usp_CalculateFutureValueForAccount (@AccountId INT, @InterestRate FLOAT) AS
BEGIN
	SELECT ah.Id, ah.FirstName, ah.LastName, a.Balance,
		  ROUND(dbo.ufn_CalculateFutureValue(a.Balance, @InterestRate, 5), 3) AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.AccountHolderId = ah.Id
	WHERE a.Id = @AccountId
END

EXEC usp_CalculateFutureValueForAccount 1, 0.1
GO

--13. *Cash in User Games Odd Rows 
CREATE FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(100))
RETURNS TABLE
AS
RETURN
SELECT SUM(tr.Cash) AS SumCash
FROM (SELECT ug.Cash, ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS RowNumber
	 FROM Games AS g
	 JOIN UsersGames AS ug ON ug.GameId = g.Id
	 WHERE g.Name = @gameName) AS tr
	 WHERE tr.RowNumber % 2 != 0
	 
--14. Create Table Logs 
CREATE TABLE Logs
(
	LogID INT PRIMARY KEY IDENTITY,
	AccountID INT FOREIGN KEY REFERENCES Accounts(Id),
	OldSum MONEY NOT NULL,
	NewSum MONEY NOT NULL
)
GO


CREATE TRIGGER tr_Accounts 
ON Accounts FOR UPDATE AS
BEGIN
	DECLARE @accountId INT = (
		SELECT i.Id FROM inserted AS i
	)
	DECLARE @oldSum DECIMAL = (
		SELECT d.Balance FROM deleted AS d
	)
	DECLARE @newSum DECIMAL = (
		SELECT i.Balance FROM inserted AS i
	) + 1

	INSERT INTO Logs
	VALUES
	(@accountId, @oldSum, @newSum + 0.12)
END 

--15. Create Table Emails 
CREATE TABLE NotificationEmails
(
	Id INT PRIMARY KEY IDENTITY,
	Recipient INT FOREIGN KEY REFERENCES Accounts(Id),
	Subject VARCHAR(100),
	Body VARCHAR(200)
)
GO

CREATE TRIGGER tr_Logs
ON Logs FOR INSERT AS
BEGIN
	INSERT INTO NotificationEmails
	SELECT i.AccountID,
		   'Balance change for account: ' + CAST(i.AccountID AS VARCHAR(50)),
		   'On ' + CONVERT(VARCHAR(50), GETDATE(), 100) 
		   + ' your balance was changed from ' + CAST(i.OldSum AS VARCHAR(20)) 
		   + ' to ' + CAST(i.NewSum AS VARCHAR(20))
	FROM inserted AS i
END

SELECT Id, Recipient, Subject FROM NotificationEmails
UPDATE Logs
SET OldSum = 200
WHERE LogID = 2
GO

--16. Deposit Money 
CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(20, 4)) AS
BEGIN
	IF (@MoneyAmount > 0 AND (SELECT AccountHolderId FROM Accounts WHERE Id = @AccountId) IS NOT NULL)
	BEGIN
		BEGIN TRAN
			UPDATE Accounts
			SET Balance += @MoneyAmount
			WHERE Id = @AccountId
		COMMIT
	END
END

EXEC dbo.usp_DepositMoney @AccountId = 1111, @MoneyAmount = 10

SELECT * FROM Accounts
GO

--17. Withdraw Money Procedure 
CREATE PROC usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(20, 4)) AS
BEGIN
		IF(@MoneyAmount > 0)
		BEGIN
			BEGIN TRAN
			UPDATE Accounts
			SET Balance -= @MoneyAmount
			WHERE Id = @AccountId

			IF (@@ROWCOUNT = 0)
			BEGIN
				ROLLBACK
				RAISERROR('Invalid account', 16, 1)
				RETURN
			END

			COMMIT 
		END
END

EXEC dbo.usp_WithdrawMoney @AccountId = 5, @MoneyAmount = 25

SELECT * FROM Accounts
GO

--18. Money Transfer 
CREATE PROC usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(20, 4)) AS
BEGIN 
	IF( (SELECT Id FROM Accounts WHERE Id = @SenderId) IS NOT NULL AND
		(SELECT Id FROM Accounts WHERE Id = @ReceiverId) IS NOT NULL AND
		@Amount > 0 AND (SELECT Balance FROM Accounts WHERE Id = @SenderId) >= @Amount)
	BEGIN
		BEGIN TRAN
			EXEC dbo.usp_DepositMoney @ReceiverId, @Amount

			EXEC dbo.usp_WithdrawMoney @SenderId, @Amount
		COMMIT
	END
END

EXEC dbo.usp_TransferMoney 3, 1, 1000000

SELECT * FROM Accounts
GO

--Trigger for Diablo database
CREATE TRIGGER tr_RestrictBuyingItemsWithHigherLevel
ON UserGameItems INSTEAD OF INSERT AS
BEGIN
	INSERT INTO UserGameItems
	SELECT i.Id, ug.Id
	FROM inserted
	JOIN UsersGames AS ug ON ug.Id = UserGameId
	JOIN Items AS i ON i.Id = ItemId
	WHERE i.MinLevel >= ug.Level
END
GO

UPDATE UsersGames
SET Cash += 50000
WHERE UserId IN (SELECT Id FROM Users WHERE Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')) AND
	  GameId = (SELECT Id FROM Games WHERE Name = 'Bali')
	  GO

CREATE PROCEDURE usp_BuyItems(@username VARCHAR(100)) AS
BEGIN
	DECLARE @userId INT = (SELECT Id FROM Users WHERE Username = @username);
	DECLARE @gameId INT = (SELECT Id FROM Games WHERE Name = 'Bali');
	DECLARE @userGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId);
	DECLARE @userGameLevel INT = (SELECT Level FROM UsersGames WHERE UserId = @userId AND GameId = @gameId);
	DECLARE @currentItemId INT = 250;
	DECLARE @currentItemPrice DECIMAL(15, 2);
	DECLARE @currentItemLevel INT;
	WHILE (@currentItemId < 540)
	BEGIN
		SET @currentItemId += 1;
		IF(@currentItemId = 300)
		BEGIN 
			SET @currentItemId = 501;
		END

		SET @currentItemPrice = (SELECT Price FROM Items WHERE Id = @currentItemId);
		SET @currentItemLevel = (SELECT MinLevel FROM Items WHERE Id = @currentItemId);

		DECLARE @currentCash DECIMAL(15, 2) = (SELECT Cash FROM UsersGames WHERE UserId = @userId AND GameId = @gameId);
		IF(@currentCash >= @currentItemPrice AND @currentItemLevel >= @userGameLevel)
		BEGIN 
			UPDATE UsersGames
			SET Cash -= @currentItemPrice
			WHERE Id = @userGameId

			INSERT INTO UserGameItems 
			VALUES
			(@currentItemId, @userGameId)
		END
	END
END

EXEC usp_BuyItems 'baleremuda'
EXEC usp_BuyItems 'loosenoise'
EXEC usp_BuyItems 'inguinalself'
EXEC usp_BuyItems 'buildingdeltoid'
EXEC usp_BuyItems 'monoxidecos'

SELECT u.Username, g.Name, ug.Cash, i.Name, i.Id
FROM UsersGames AS ug
JOIN Users AS u ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE u.Username IN('baleremuda', 'loosenoise', 'inguinalself', 'buildingdeltoid', 'monoxidecos')
  AND g.Name = 'Bali'	
ORDER BY u.Username, i.Name
GO

--20. *Massive Shopping 
DECLARE @userId INT = (SELECT Id FROM Users WHERE Username = 'Stamat');
DECLARE @gameId INT = (SELECT Id FROM Games WHERE Name = 'Safflower');
DECLARE @userGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId);
DECLARE @stamatCash DECIMAL = (SELECT Cash FROM UsersGames WHERE Id = @userGameId)
DECLARE @allItemsPrice DECIMAL = (SELECT SUM(Price) FROM Items WHERE (MinLevel BETWEEN 11 AND 12));

IF(@stamatCash >= @allItemsPrice)
BEGIN
	BEGIN TRAN
	 
	UPDATE UsersGames
	SET Cash -= @allItemsPrice
	WHERE Id = @userGameId

	INSERT INTO UserGameItems
	SELECT Id, @userGameId
    FROM Items
	WHERE MinLevel BETWEEN 11 AND 12

	COMMIT
END
		
SET @stamatCash = (SELECT Cash FROM UsersGames WHERE Id = @userGameId);
SET @allItemsPrice = (SELECT SUM(Price) FROM Items WHERE (MinLevel BETWEEN 19 AND 21));		

IF(@stamatCash >= @allItemsPrice)
BEGIN
	BEGIN TRAN
	 
	UPDATE UsersGames
	SET Cash -= @allItemsPrice
	WHERE Id = @userGameId

	INSERT INTO UserGameItems
	SELECT Id, @userGameId
    FROM Items
	WHERE MinLevel BETWEEN 19 AND 21

	COMMIT
END   

SELECT i.Name
FROM UsersGames AS ug
JOIN Users AS u ON u.Id = ug.UserId
JOIN Games AS g ON g.Id = ug.GameId
JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
JOIN Items AS i ON i.Id = ugi.ItemId
WHERE u.Username = 'Stamat' AND g.Name = 'Safflower'
ORDER BY i.Name

--21. Employees with Three Projects 
CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT) AS
BEGIN
	BEGIN TRANSACTION

	INSERT INTO EmployeesProjects
	VALUES
	(@emloyeeId, @projectID)

	DECLARE @projectsCount INT = (SELECT COUNT(ProjectID)
								    FROM EmployeesProjects
							    GROUP BY EmployeeID
								  HAVING EmployeeID = @emloyeeId);
	IF(@projectsCount > 3)
	BEGIN
		ROLLBACK;
		RAISERROR('The employee has too many projects!', 16, 1);
		RETURN;
	END
	
	COMMIT
END

EXEC usp_AssignProject 555, 1

--22. Delete Employees 
CREATE TABLE Deleted_Employees(
EmployeeId INT IDENTITY CONSTRAINT PK_DeletedEmployees PRIMARY KEY NOT NULL,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
MiddleName VARCHAR(50),
JobTitle VARCHAR(50) NOT NULL,
DepartmentId INT NOT NULL,
Salary DECIMAL(15, 2) NOT NULL
)
GO

CREATE TRIGGER tr_Employees
ON Employees FOR DELETE AS
BEGIN
	INSERT INTO Deleted_Employees	
	SELECT FirstName, LastName, MiddleName, JobTitle, DepartmentId, Salary
	FROM deleted
END

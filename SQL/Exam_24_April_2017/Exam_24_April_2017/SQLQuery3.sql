--DDL--
CREATE DATABASE WMS

CREATE TABLE Clients (
	ClientId INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Phone VARCHAR(12) NOT NULL
)	

CREATE TABLE Mechanics (
	MechanicId INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(255) NOT NULL
)

CREATE TABLE Models (
	ModelId INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) UNIQUE
)

CREATE TABLE Jobs (
	JobId INT PRIMARY KEY IDENTITY,
	ModelId INT FOREIGN KEY REFERENCES Models(ModelId) NOT NULL,
	[Status] NVARCHAR(11) CHECK([Status] IN('Pending', 'In Progress', 'Finished')) DEFAULT 'Pending' NOT NULL,
	ClientId INT FOREIGN KEY REFERENCES Clients(ClientId) NOT NULL,
	MechanicId INT FOREIGN KEY REFERENCES Mechanics(MechanicId),
	IssueDate DATE NOT NULL,
	FinishDate DATE
)

CREATE TABLE Orders (
	OrderId INT PRIMARY KEY IDENTITY,
	JobId INT FOREIGN KEY REFERENCES Jobs(JobId) NOT NULL,
	IssueDate DATE,
	Delivered BIT DEFAULT 0 NOT NULL
)

CREATE TABLE Vendors (
	VendorId INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) UNIQUE NOT NULL
)

CREATE TABLE Parts (
	PartId INT PRIMARY KEY IDENTITY,
	SerialNumber NVARCHAR(50) UNIQUE NOT NULL,
	[Description] NVARCHAR(255),
	Price DECIMAL(15, 2) CHECK(Price > 0 AND Price < 10000) NOT NULL,
	VendorId INT FOREIGN KEY REFERENCES Vendors(VendorId) NOT NULL,
	StockQty INT CHECK(StockQty >= 0) DEFAULT 0 NOT NULL
)

CREATE TABLE OrderParts (
	OrderId INT FOREIGN KEY REFERENCES Orders(OrderId) NOT NULL,
	PartId INT FOREIGN KEY REFERENCES Parts(PartId) NOT NULL,
	Quantity INT CHECK(Quantity > 0) DEFAULT 1,

	CONSTRAINT PK_OrderParts PRIMARY KEY (OrderId, PartId)
)

CREATE TABLE PartsNeeded (
	JobId INT FOREIGN KEY REFERENCES Jobs(JobId) NOT NULL,
	PartId INT FOREIGN KEY REFERENCES Parts(PartId) NOT NULL,
	Quantity INT CHECK(Quantity > 0) DEFAULT 1,

	CONSTRAINT PK_PartsNeeded PRIMARY KEY (JobId, PartId)
)

--INSERT--
INSERT INTO Clients (FirstName, LastName, Phone)
VALUES
('Teri', 'Ennaco', '570-889-5187'),
('Merlyn', 'Lawler', '201-588-7810'),
('Georgene', 'Montezuma', '925-615-5185'),
('Jettie', 'Mconnell', '908-802-3564'),
('Lemuel', 'Latzke', '631-748-6479'),
('Melodie', 'Knipp', '805-690-1682'),
('Candida', 'Corbley', '908-275-8357')

INSERT INTO Parts (SerialNumber, Description, Price, VendorId)
VALUES
('WP8182119', 'Door Boot Seal',	117.86,	2),
('W10780048', 'Suspension Rod', 42.81, 1),
('W10841140', 'Silicone Adhesive', 6.77, 4),
('WPY055980', 'High Temperature Adhesive', 13.94, 3)

		
--UPDATE--
UPDATE Jobs
SET MechanicId = 3
WHERE [Status] = 'Pending'

UPDATE Jobs
SET [Status] = 'In Progress'
WHERE [Status] = 'Pending'

SELECT * FROM Mechanics
		
--DELETE--
DELETE FROM OrderParts
WHERE OrderId = 19

DELETE FROM Orders
WHERE OrderId = 19

--QUERYING--

--05. Clients by Name 
SELECT FirstName, LastName, Phone
FROM Clients
ORDER BY LastName, ClientId

--06. Job Status 
SELECT [Status], IssueDate
FROM Jobs
WHERE [Status] <> 'Finished'
ORDER BY IssueDate, JobId

--07. Mechanic Assignments 
SELECT m.FirstName + ' ' + m.LastName, j.[Status], j.IssueDate
FROM Jobs AS j
JOIN Mechanics AS m ON m.MechanicId = j.MechanicId
ORDER BY m.MechanicId, j.IssueDate, j.JobId

--08. Current Clients 
SELECT c.FirstName + ' ' + c.LastName, DATEDIFF(DAY, j.IssueDate, '04-24-2017') AS [Days], j.[Status]
FROM Jobs AS j
JOIN Clients AS c ON c.ClientId = j.ClientId
WHERE j.FinishDate IS NULL
ORDER BY [Days] DESC, c.ClientId 

--09. Mechanic Performance 
SELECT m.FirstName + ' ' + m.LastName, AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate))
FROM Mechanics AS m
JOIN Jobs AS j ON j.MechanicId = m.MechanicId
WHERE j.FinishDate IS NOT NULL
GROUP BY m.MechanicId,  m.FirstName, m.LastName

--10. Hard Earners 
SELECT TOP 3 m.FirstName + ' ' + m.LastName, COUNT(j.JobId) AS Jobs
FROM Mechanics AS m
JOIN Jobs AS j ON j.MechanicId = m.MechanicId
WHERE j.FinishDate IS NULL
GROUP BY m.MechanicId, m.FirstName, m.LastName
HAVING COUNT(j.JobId) > 1
ORDER BY Jobs DESC

--11. Available Mechanics 
SELECT m.FirstName + ' ' + m.LastName
FROM Mechanics AS m
WHERE m.MechanicId NOT IN (SELECT MechanicId FROM Jobs WHERE Status <> 'Finished' AND MechanicId IS NOT NULL)
ORDER BY m.MechanicId

--12. Parts Cost 
SELECT ISNULL(SUM(op.Quantity * p.Price), 0) AS [Total Parts]
FROM Parts AS p
JOIN OrderParts AS op ON op.PartId = p.PartId
JOIN Orders AS o ON o.OrderId = op.OrderId
WHERE DATEDIFF(DAY, o.IssueDate, '04-24-2017') <= 21

BACKUP DATABASE WMS TO DISK = 'E:\WMS.backup'

--13. Past Expenses 
   SELECT j.JobId, ISNULL(SUM(op.Quantity * p.Price), 0) AS Total
     FROM Jobs AS j
FULL JOIN Orders AS o ON o.JobId = j.JobId
FULL JOIN OrderParts AS op ON op.OrderId = o.OrderId
FULL JOIN Parts AS p ON p.PartId = op.PartId
    WHERE j.[Status] = 'Finished'
 GROUP BY j.JobId
 ORDER BY Total DESC, j.JobId

--14. Model Repair Time 
	SELECT m.ModelId, m.Name, CAST(AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) AS NVARCHAR(100)) + ' days' AS [Average Service Time]
	  FROM Models AS m
	  JOIN Jobs AS j ON j.ModelId = m.ModelId
  GROUP BY m.ModelId, m.Name
  ORDER BY AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate))

--15. Faultiest Model
SELECT TOP 1 WITH TIES m.Name AS Model, ISNULL(COUNT(j.JobId), 0) AS [Times Serviced], 
    (SELECT ISNULL(SUM(op.Quantity * p.Price), 0)
	FROM Jobs AS j
	JOIN Orders AS o on o.JobId = j.JobId
	JOIN OrderParts AS op ON op.OrderId = o.OrderId
	JOIN Parts AS p ON p.PartId = op.PartId
	WHERE j.ModelId = m.ModelId) AS [Parts Total]
	FROM Models AS m
	JOIN Jobs AS j ON j.ModelId = m.ModelId
GROUP BY m.ModelId, m.Name
ORDER BY [Times Serviced] DESC


--16. Missing Parts 
SELECT p.PartId,
       p.Description,
       ISNULL(pn.Quantity, 0) AS Required,
       ISNULL(p.StockQty, 0) AS [In Stock],
       ISNULL(CASE
                  WHEN o.Delivered = 0
                  THEN op.Quantity
                  ELSE 0
              END, 0) AS Ordered
FROM Parts AS p
     JOIN PartsNeeded AS pn ON pn.PartId = p.PartId
     LEFT JOIN OrderParts AS op ON op.PartId = p.PartId
     JOIN Jobs AS j ON j.JobId = pn.JobId
     LEFT JOIN Orders AS o ON o.JobId = j.JobId
WHERE pn.Quantity > ISNULL(p.StockQty + CASE
                                            WHEN o.Delivered = 0
                                            THEN op.Quantity
                                            ELSE 0
                                        END, 0)
      AND j.Status != 'Finished'
ORDER BY p.PartId;

--17. Cost of Order 
CREATE FUNCTION udf_GetCost (@JobId INT)
RETURNS DECIMAL(15, 2)
AS
BEGIN
	RETURN
	(SELECT ISNULL(SUM(op.Quantity * p.Price), 0)
	FROM Jobs AS j
	JOIN Orders AS o ON o.JobId = j.JobId
	JOIN OrderParts AS op ON op.OrderId = o.OrderId
	JOIN Parts AS p ON p.PartId = op.PartId
	WHERE j.JobId = @JobId)
END

--18. Place Order 
CREATE PROC usp_PlaceOrder (@JobId INT, @SerialNumber NVARCHAR(50), @Quantity INT)
AS
BEGIN
		IF((SELECT Status FROM Jobs WHERE JobId = 1 AND Status = 'Finished') IS NOT NULL)
		BEGIN
			;THROW 50011, 'This job is not active!', 1
		END

		IF(@Quantity <= 0)
		BEGIN
			;THROW 50012, 'Part quantity must be more than zero!', 1
		END

		IF((SELECT JobId FROM Jobs WHERE JobId = @JobId) IS NULL)
		BEGIN
			;THROW 50013, 'Job not found!', 1
		END

		DECLARE @PartId INT = (SELECT PartId FROM Parts WHERE SerialNumber = @SerialNumber);
		IF(@PartId IS NULL)
		BEGIN
			;THROW 50014, 'Part not found!', 1
		END

		DECLARE @OrderId INT = (SELECT OrderId FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL);
		If(@OrderId IS NOT NULL)
		BEGIN
			DECLARE @CurrentId INT = (SELECT PartId
			FROM OrderParts WHERE OrderId = @OrderId)

			IF(@CurrentId = @PartId)
			BEGIN 
				UPDATE OrderParts
				SET Quantity += @Quantity
				WHERE OrderId = @OrderId
			END
			ELSE
			BEGIN
				UPDATE OrderParts
				SET PartId = @PartId
				WHERE OrderId = @OrderId
			END
		END
		ELSE
		BEGIN
			INSERT INTO Orders (JobId, IssueDate)
			VALUES
			(@JobId, NULL)
		END

END

DECLARE @err_msg AS NVARCHAR(MAX);
BEGIN TRY
  EXEC usp_PlaceOrder 1, 'ZeroQuantity', 0
END TRY

BEGIN CATCH
  SET @err_msg = ERROR_MESSAGE();
  SELECT @err_msg
END CATCH



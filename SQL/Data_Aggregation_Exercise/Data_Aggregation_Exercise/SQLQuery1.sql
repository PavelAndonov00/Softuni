--01. Records’ Count 
SELECT COUNT(Id) AS Count
FROM WizzardDeposits

--02. Longest Magic Wand 
SELECT MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits

--03. Longest Magic Wand per Deposit Groups 
SELECT DepositGroup, MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits
GROUP BY DepositGroup

--04. Smallest Deposit Group per Magic Wand Size
SELECT TOP(2) DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

--05. Deposits Sum 
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup

--06. Deposits Sum for Ollivander Family 
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

--07. Deposits Filter
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC

--08. Deposit Charge
SELECT DepositGroup,  MagicWandCreator, MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

--09. Age Groups 
SELECT AgeGroup, COUNT(*) AS WizzardCount FROM
		(SELECT CASE 
			WHEN Age <= 10 THEN '[0-10]'
			WHEN Age >= 11 AND Age <= 20 THEN '[11-20]'
			WHEN Age >= 21 AND Age <= 30 THEN '[21-30]'
			WHEN Age >= 31 AND Age <= 40 THEN '[31-40]'
			WHEN Age >= 41 AND Age <= 50 THEN '[41-50]'
			WHEN Age >= 51 AND Age <= 60 THEN '[51-60]'
			WHEN Age > 60 THEN '[61+]'
		END AS AgeGroup FROM WizzardDeposits) AS T
GROUP BY AgeGroup
ORDER BY AgeGroup

--10. First Letter 
SELECT LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName, 1)
ORDER BY FirstLetter

--11. Average Interest 
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired

--12. Rich Wizard, Poor Wizard 
SELECT SUM([Difference]) FROM (SELECT Wiz1.FirstName AS [Host Wizzard], Wiz1.DepositAmount AS [Host Wizzard Deposit],
       Wiz2.FirstName AS [Guest Wizzard], Wiz2.DepositAmount AS [Guest Wizzard Deposit],
	   Wiz1.DepositAmount - Wiz2.DepositAmount AS [Difference]
FROM WizzardDeposits AS Wiz1
JOIN WizzardDeposits AS Wiz2 ON Wiz1.Id = Wiz2.Id - 1) AS T

--13. Departments Total Salaries
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID

--14. Employees Minimum Salaries 
SELECT DepartmentID, MIN(Salary) AS MinSalary
FROM Employees
WHERE DepartmentID IN(2, 5, 7) AND HireDate > '01/01/2000'
GROUP BY DepartmentID

--15. Employees Average Salaries 
SELECT *
INTO EmployeesWithHighSalary
FROM Employees
WHERE Salary > 30000

DELETE 
FROM EmployeesWithHighSalary 
WHERE ManagerID = 42

UPDATE EmployeesWithHighSalary
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM EmployeesWithHighSalary
GROUP BY DepartmentID

--16. Employees Maximum Salaries 
SELECT DepartmentID, MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000 

--17. Employees Count Salaries 
SELECT COUNT(Salary) 
FROM Employees
WHERE ManagerID IS NULL

--18. 3rd Highest Salary 


--19. Salary Challenge 
SELECT TOP(10) a.FirstName, a.LastName, a.DepartmentID
FROM Employees AS a
WHERE a.Salary > 
(SELECT AVG(b.Salary)
FROM Employees AS b
WHERE b.DepartmentID = a.DepartmentID
GROUP BY b.DepartmentID)
ORDER BY a.DepartmentID




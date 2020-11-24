--01. Employee Address 
SELECT TOP(5) Emp.EmployeeID, Emp.JobTitle, Adr.AddressID, Adr.AddressText
FROM Employees AS Emp
JOIN Addresses AS Adr
ON Adr.AddressID = Emp.AddressID
ORDER BY Adr.AddressID

--02. Addresses with Towns 
SELECT TOP(50) Emp.FirstName, Emp.LastName, T.Name, Adr.AddressText
FROM Employees AS Emp
JOIN Addresses AS Adr
ON Adr.AddressID = Emp.AddressID
JOIN Towns AS T
ON T.TownID = Adr.TownID
ORDER BY Emp.FirstName, Emp.LastName

--03. Sales Employees 
SELECT Emp.EmployeeID, Emp.FirstName, Emp.LastName, Dep.Name AS DepartmentName
FROM Employees AS Emp
JOIN Departments AS Dep
ON Dep.DepartmentID = Emp.DepartmentID
WHERE Dep.Name = 'Sales'
ORDER BY Emp.EmployeeID

--04. Employee Departments 
SELECT TOP(5) Emp.EmployeeID, Emp.FirstName, Emp.Salary, Dep.Name AS DepartmentName
FROM Employees AS Emp
JOIN Departments AS Dep
ON Dep.DepartmentID = Emp.DepartmentID
WHERE Emp.Salary > 15000
ORDER BY Emp.DepartmentID

--05. Employees Without Projects 
SELECT TOP(3) Emp.EmployeeID, Emp.FirstName
FROM Employees AS Emp
WHERE Emp.EmployeeID NOT IN((SELECT EmployeeID FROM EmployeesProjects))
ORDER BY Emp.EmployeeID

--06. Employees Hired After 
SELECT Emp.FirstName, Emp.LastName, Emp.HireDate, Dep.Name
FROM Employees AS Emp
JOIN Departments AS Dep
ON Dep.DepartmentID = Emp.DepartmentID
WHERE Dep.Name IN('Sales', 'Finance') AND Emp.HireDate > '1.1.1999'
ORDER BY Emp.HireDate 

--07. Employees With Project
SELECT TOP(5) Emp.EmployeeID, Emp.FirstName, Proj.Name
FROM Employees AS Emp
JOIN EmployeesProjects AS EmpProj
ON EmpProj.EmployeeID = Emp.EmployeeID
JOIN Projects AS Proj
ON Proj.ProjectID = EmpProj.ProjectID
WHERE Proj.StartDate > '08/13/2002' AND Proj.EndDate IS NULL
ORDER BY Emp.EmployeeID 

--08. Employee 24 
SELECT Emp.EmployeeID, Emp.FirstName, 
		CASE
			WHEN YEAR(Proj.StartDate) >= 2005 THEN NULL
			ELSE Proj.Name
		END AS ProjectName
FROM Employees AS Emp
JOIN EmployeesProjects AS EmpProj
ON EmpProj.EmployeeID = Emp.EmployeeID
JOIN Projects AS Proj
ON Proj.ProjectID = EmpProj.ProjectID
WHERE Emp.EmployeeID = 24

--09. Employee Manager 
SELECT Emp.EmployeeID, Emp.FirstName, Emp.ManagerID, Managers.FirstName
FROM Employees AS Emp
JOIN Employees As Managers
ON Managers.EmployeeID = Emp.ManagerID
WHERE Emp.ManagerID IN(3, 7)
ORDER BY Emp.EmployeeID

--10. Employees Summary 
SELECT TOP(50) Emp.EmployeeID,
 Emp.FirstName + ' ' + Emp.LastName AS EmployeeName,
  Managers.FirstName + ' ' + Managers.LastName AS ManagerName,
   Dep.Name AS DepartmentName
FROM Employees AS Emp
JOIN Employees As Managers
ON Managers.EmployeeID = Emp.ManagerID
JOIN Departments AS Dep
ON Dep.DepartmentID = Emp.DepartmentID
ORDER BY Emp.EmployeeID

--11. Min Average Salary 
SELECT MIN(AvgSalary)
FROM (SELECT AVG(Salary) AS AvgSalary
FROM Employees
GROUP BY DepartmentID) AS AvgSalaries

--12. Highest Peaks in Bulgaria 
SELECT MounCoun.CountryCode, Moun.MountainRange, Pea.PeakName, Pea.Elevation
FROM Mountains AS Moun
JOIN MountainsCountries As MounCoun
ON MounCoun.MountainId = Moun.Id
JOIN Peaks AS Pea
ON Pea.MountainId = Moun.Id
WHERE MounCoun.CountryCode = 'BG' AND Pea.Elevation > 2835
ORDER BY Pea.Elevation DESC

--13. Count Mountain Ranges 
SELECT Coun.CountryCode, COUNT(MounCoun.MountainId) AS MountainRanges
FROM Countries AS Coun
JOIN MountainsCountries AS MounCoun
ON MounCoun.CountryCode = Coun.CountryCode
WHERE Coun.CountryName IN('United States', 'Russia', 'Bulgaria')
GROUP BY Coun.CountryCode

--14. Countries With or Without Rivers 
SELECT TOP(5) Coun.CountryName, Riv.RiverName
FROM Countries AS Coun
JOIN CountriesRivers AS CounRiv
ON CounRiv.CountryCode = Coun.CountryCode
JOIN Rivers AS Riv
ON Riv.Id = CounRiv.RiverId
WHERE Coun.ContinentCode = 'AF'
ORDER BY Coun.CountryName

--15. *Continents and Currencies 
WITH CTE_CountriesInfo (ContinentCode, CurrencyCode, CurrencyUsage) AS
(SELECT c.ContinentCode, c.CurrencyCode, COUNT(c.CurrencyCode) AS CurrencyUsage
FROM Countries AS c
GROUP BY c.ContinentCode, c.CurrencyCode
HAVING COUNT(c.CurrencyCode) > 1)

SELECT cci.ContinentCode, cci.CurrencyCode, cci.CurrencyUsage
FROM (SELECT cci.ContinentCode, MAX(cci.CurrencyUsage) AS maX
FROM CTE_CountriesInfo AS cci
GROUP BY cci.ContinentCode) AS c
JOIN CTE_CountriesInfo AS cci
ON cci.ContinentCode = c.ContinentCode AND cci.CurrencyUsage = c.maX

--16. Countries Without any Mountains
SELECT COUNT(c.CountryCode) AS CountryCode
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL

--17. Highest Peak and Longest River by Country 
SELECT TOP(5) c.CountryName, MAX(p.Elevation) AS HighestPeak, MAX(r.Length) AS LongestRiver
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON mc.CountryCode = c.CountryCode
LEFT JOIN Peaks AS p
ON p.MountainId = mc.MountainId
LEFT JOIN CountriesRivers AS cr
ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers AS r
ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeak DESC, LongestRiver DESC, c.CountryName

--18. *Highest Peak Name and Elevation by Country 
SELECT TOP(5) c.CountryName,
		ISNULL(p.PeakName ,'(no highest peak)') AS [Highest Peak Name],
		ISNULL(MAX(p.Elevation), 0) AS [Highest Peak Elevation],
		ISNULL(m.MountainRange, '(no mountain)') AS Mountain
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains AS m ON m.Id = mc.MountainId
LEFT JOIN Peaks AS p ON p.MountainId = mc.MountainId
GROUP BY c.CountryName, p.PeakName, m.MountainRange
ORDER BY c.CountryName



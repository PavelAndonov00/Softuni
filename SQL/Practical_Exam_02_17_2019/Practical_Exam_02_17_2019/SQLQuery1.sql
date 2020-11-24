CREATE TABLE Students(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(25),
	LastName NVARCHAR(30) NOT NULL,
	Age INT CHECK(Age >= 5 AND Age <= 100),
	[Address] NVARCHAR(50),
	Phone NVARCHAR(10)
)

CREATE TABLE Subjects(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	Lessons INT CHECK(Lessons > 0) NOT NULL
)

CREATE TABLE StudentsSubjects(
	Id INT PRIMARY KEY IDENTITY,
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id),
	Grade DECIMAL(6, 2) NOT NULL CHECK(Grade >= 2 AND Grade <= 6)
)

CREATE TABLE Exams(
	Id INT IDENTITY PRIMARY KEY,
	[Date] DATETIME,
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id)
)

CREATE TABLE StudentsExams(
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	ExamId INT NOT NULL FOREIGN KEY REFERENCES Exams(Id),
	Grade DECIMAL(6, 2) NOT NULL CHECK(Grade >= 2 AND Grade <= 6),

	CONSTRAINT PK_StudentsExams PRIMARY KEY(StudentId, ExamId)
)

CREATE TABLE Teachers(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	[Address] NVARCHAR(20) NOT NULL,
	Phone NVARCHAR(10),
	SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects(Id) 
)

CREATE TABLE StudentsTeachers(
	StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(Id),
	TeacherId INT NOT NULL FOREIGN KEY REFERENCES Teachers(Id),

	CONSTRAINT PK_StudentsTeachers PRIMARY KEY(StudentId, TeacherId)
)

INSERT INTO Teachers(FirstName, LastName, [Address], Phone, SubjectId)
VALUES
('Ruthanne', 'Bamb', '84948 Mesta Junction', '3105500146', 6),
('Gerrard', 'Lowin', '370 Talisman Plaza', '3324874824', 2),
('Merrile', 'Lambdin', '81 Dahle Plaza', '4373065154', 5),
('Bert', 'Ivie', '2 Gateway Circle', '4409584510', 4)

INSERT INTO Subjects([Name], Lessons)
VALUES
('Geometry', 12),
('Health', 10),
('Drama', 7),
('Sports', 9)

UPDATE StudentsSubjects
SET Grade = 6.00
WHERE SubjectId IN(1, 2) AND Grade >= 5.50

DELETE StudentsTeachers
WHERE TeacherId IN( (SELECT Id FROM Teachers 
WHERE Phone LIKE '%72%'))

DELETE Teachers
WHERE Phone LIKE '%72%'

--5
SELECT FirstName, LastName, Age
FROM Students
WHERE Age >= 12
ORDER BY FirstName, LastName

--6
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName, [Address]
FROM Students
WHERE [Address] LIKE '%road%'
ORDER BY FirstName, LastName, [Address]


--7
SELECT FirstName, [Address], Phone
FROM Students
WHERE MiddleName IS NOT NULL AND Phone LIKE '42%'
ORDER BY FirstName

--8
SELECT s.FirstName, s.LastName, COUNT(st.TeacherId)
FROM Students AS s
JOIN StudentsTeachers AS st ON st.StudentId = s.Id
GROUP BY s.Id, s.FirstName, s.LastName
ORDER BY s.LastName

--9
SELECT t.FirstName + ' ' + t.LastName AS FullName, s.[Name] + '-' + CAST(s.Lessons AS NVARCHAR(10)) AS Subjects, COUNT(st.StudentId)
FROM Teachers AS t
JOIN Subjects AS s ON s.Id = t.SubjectId
JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
GROUP BY t.Id, t.FirstName, t.LastName, s.Name, s.Lessons
ORDER BY COUNT(st.StudentId) DESC, FullName, Subjects

--10
SELECT FirstName + ' ' + LastName AS [FullName]
FROM Students
WHERE Id NOT IN(SELECT StudentId From StudentsExams)
ORDER BY [FullName]

--11
SELECT TOP(10) t.FirstName, t.LastName, COUNT(st.StudentId)
FROM Teachers AS t
JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
GROUP BY t.Id, t.FirstName, t.LastName
ORDER BY COUNT(st.StudentId) DESC, t.FirstName, t.LastName

--12
SELECT TOP(10) s.FirstName, s.LastName, CAST(AVG(se.Grade) AS DECIMAL(15, 2)) AS Grade
FROM Students AS s
JOIN StudentsExams AS se ON se.StudentId = s.Id
GROUP BY s.Id, s.FirstName, s.LastName
ORDER BY Grade DESC, s.FirstName, s.LastName

--13
SELECT DISTINCT s.FirstName, s.LastName, s.Grade
FROM (
  SELECT s.FirstName, s.LastName, ss.Grade, ROW_NUMBER() OVER(PARTITION BY s.FirstName, s.LastName ORDER BY ss.Grade DESC) dr
  FROM Students s JOIN StudentsSubjects ss ON ss.StudentId=s.Id) s
  WHERE s.dr = 2
  ORDER BY s.FirstName, s.LastName

--14

--15
SELECT t.FirstName + ' ' + t.LastName AS TeacherFullName,
		sub.Name AS SubjectName, stu.FirstName + ISNULL(' ' + stu.MiddleName, '') + ' ' + stu.LastName,
		AVG(sb.Grade)
FROM Teachers AS t
JOIN Subjects AS sub ON sub.Id = t.SubjectId
JOIN StudentsSubjects AS sb ON sb.SubjectId = sub.Id
JOIN Students AS stu ON stu.Id = sb.StudentId
GROUP BY t.Id, t.FirstName, t.LastName, stu.FirstName, stu.MiddleName, stu.LastName, sub.Name

SELECT * FROM StudentsSubjects

--16
SELECT s.Name, AVG(sb.Grade)
FROM Subjects AS s
JOIN StudentsSubjects AS sb ON sb.SubjectId = s.Id
GROUP BY s.Id, s.Name
ORDER BY s.Id

--17
WITH CTE_17 ([Quarter], [Name], Students) AS
(SELECT 
	CASE
		WHEN DATEPART(QUARTER, e.Date) = 1 THEN 'Q1'
		WHEN DATEPART(QUARTER, e.Date) = 2 THEN 'Q2'
		WHEN DATEPART(QUARTER, e.Date) = 3 THEN 'Q3'
		WHEN DATEPART(QUARTER, e.Date) = 4 THEN 'Q4'
		ELSE 'TBA' 
	END AS [Quarter]
, s.Name, COUNT(s.Id) AS Students
FROM Exams AS e
JOIN Subjects AS s ON s.Id = e.SubjectId
JOIN StudentsExams AS se ON se.ExamId = e.Id
WHERE Grade >= 4.00
GROUP BY s.Name, e.Date, s.Id)

SELECT [Quarter], [Name], SUM([Students])
FROM CTE_17
GROUP BY [Name], [Quarter]

--18
CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(6, 2))
RETURNS NVARCHAR(MAX) AS
BEGIN
	IF ( (SELECT Id
	FROM Students
	WHERE Id = @StudentId) IS NULL)
	BEGIN
		RETURN 'The student with provided id does not exist in the school!';
	END

	IF (@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!';
	END

	DECLARE @studentFirstName NVARCHAR(30) = (SELECT FirstName FROM Students WHERE Id = @studentId)
	DECLARE @studentGradesCount INT = (SELECT COUNT(*)
	FROM Students AS s
	JOIN StudentsExams AS se ON se.StudentId = s.Id
	WHERE se.Grade >= @grade AND se.Grade <= @grade + 0.50 AND s.Id = @studentId
	GROUP BY s.Id);

	RETURN 'You have to update ' + CAST(@studentGradesCount AS NVARCHAR(10)) + ' grades for the student ' + @studentFirstName
END

--19
CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT) AS
BEGIN
	IF ( (SELECT Id
	FROM Students
	WHERE Id = @StudentId) IS NULL)
	BEGIN
		RAISERROR('This school has no student with the provided id!', 16, 1);
		RETURN;
	END

	DELETE StudentsExams
	WHERE StudentId = @StudentId

	DELETE StudentsTeachers
	WHERE StudentId = @StudentId

	DELETE StudentsSubjects
	WHERE StudentId = @StudentId

	DELETE Students
	WHERE Id = @StudentId
END

--20
CREATE TABLE ExcludedStudents(
	StudentId INT,
	StudentName NVARCHAR(100)
)

CREATE TRIGGER Tr_ExcludedStudents ON Students AFTER DELETE AS
BEGIN
	INSERT INTO ExcludedStudents
	SELECT Id, FirstName + ' ' + LastName FROM deleted
END



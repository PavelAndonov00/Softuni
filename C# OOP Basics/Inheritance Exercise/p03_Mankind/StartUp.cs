using System;

class StartUp
{
    static void Main(string[] args)
    {
        try
        {
            var studentTokens = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var studentFirstName = studentTokens[0];
            var studentLastName = studentTokens[1];
            var studentFacultyNumber = studentTokens[2];


            var student = new Student(studentFirstName, studentLastName, studentFacultyNumber);
            Console.WriteLine(student);


            var workerTokens = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var workerFirstName = workerTokens[0];
            var workerLastName = workerTokens[1];
            var workerWeekSalary = Decimal.Parse(workerTokens[2]);
            var workerWorkHoursPerDay = Decimal.Parse(workerTokens[3]);

            var worker = new Worker(workerFirstName, workerLastName, workerWeekSalary, workerWorkHoursPerDay);
            Console.WriteLine(worker);

        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}
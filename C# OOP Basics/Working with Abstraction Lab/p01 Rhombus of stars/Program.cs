using System;

class Program
{
    static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            PrintRow(n, i);
        }
        for (int i = n - 1; i > 0; i--)
        {
            PrintRow(n, i);
        }
    }

    private static void PrintRow(int size, int currentRow)
    {
        for (int i = 0; i < size - currentRow; i++)
        {
            Console.Write(" ");
        }
        for (int i = 0; i < currentRow; i++)
        {
            Console.Write("* ");
        }
        Console.WriteLine();
    }
}


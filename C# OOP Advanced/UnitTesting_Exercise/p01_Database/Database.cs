using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Database
{
    private int[] array;
    private int currentIndex;

    public Database(int[] integers)
    {
        this.array = new int[16];
        this.currentIndex = -1;
        InitializeArray(integers);
    }

    private void InitializeArray(int[] integers)
    {
        if (integers.Length > 16)
        {
            throw new InvalidOperationException("Capacity must be exactly 16");
        }

        for (int i = 0; i < integers.Length; i++)
        {
            this.array[++currentIndex] = integers[i];
        }
    }

    public void Add(int integer)
    {
        if (currentIndex + 1 > 15)
        {
            throw new InvalidOperationException("There are 16 elements you cannot add 17th.");
        }

        this.array[++currentIndex] = integer;
    }

    public void Remove()
    {
        if (this.currentIndex == -1)
        {
            throw new InvalidOperationException("Database is empty! You cannot remove item from empty database.");
        }

        this.array[currentIndex] = 0;
        currentIndex--;
    }

    public int[] Fetch()
    {
        return this.array.Where(e => e != 0).ToArray();
    }
}


using System;
using System.Collections.Generic;
using System.Text;

public class DataModifier
{
    public string CalculateDifference(string date1, string date2)
    {
        var firstDate = DateTime.Parse(date1);
        var secondDate = DateTime.Parse(date2);

        var diff = new TimeSpan();
        if (firstDate < secondDate)
        {
            diff = secondDate - firstDate;
        }
        else
        {
            diff = firstDate - secondDate;
        }

        return diff.TotalDays.ToString();
    }
}


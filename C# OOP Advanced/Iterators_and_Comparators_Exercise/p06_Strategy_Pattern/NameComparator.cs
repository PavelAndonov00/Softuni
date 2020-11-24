namespace p06_Strategy_Pattern
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NameComparator : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int result = x.Name.Length.CompareTo(y.Name.Length);
            if(result == 0)
            {
                result = x.Name.ToLower().First().CompareTo(y.Name.ToLower().First());
            }

            return result;
        }
    }
}

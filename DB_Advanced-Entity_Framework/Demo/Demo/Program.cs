using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEnumerable<Person> people = new List<Person>();

            // Select gets a list of lists of phone numbers
            IEnumerable<IEnumerable<PhoneNumber>> phoneLists = people.Select(p => p.PhoneNumbers);

            // SelectMany flattens it to just a list of phone numbers.
            IEnumerable<PhoneNumber> phoneNumbers = people.SelectMany(p => p.PhoneNumbers);


        }

        public class PhoneNumber
        {
            public string Number { get; set; }
        }

        public class Person
        {
            public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
            public string Name { get; set; }
        }
    }
}

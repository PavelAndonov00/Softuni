using System;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person1 = new Person();
            var person2 = new Person(20);
            var person3 = new Person("Pesho", 33);
        }
    }
}

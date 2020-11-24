namespace p08_Pet_Clinic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Pet
    {
        public Pet(string name, int age, string kind)
        {
            this.Name = name;
            this.Age = age;
            this.Kind = kind;
        }

        public string Name { get; }
        public int Age { get; }
        public string Kind { get; }
    }
}

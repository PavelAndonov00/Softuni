using System;

namespace Farm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var puppy = new Puppy();
            puppy.Eat();
            puppy.Eat();
            puppy.Weep();
        }
    }
}

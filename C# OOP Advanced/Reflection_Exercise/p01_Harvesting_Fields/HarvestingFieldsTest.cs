namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            var classType = typeof(HarvestingFields);
            var fields = classType.GetFields((BindingFlags)60);
            while (input != "HARVEST")
            {
                switch (input)
                {
                    case "private":
                        foreach (var field in fields.Where(f => f.IsPrivate))
                        {
                            Console.WriteLine($"private {field.FieldType.Name} {field.Name}");
                        }
                        break;
                    case "protected":
                        foreach (var field in fields.Where(f => !f.IsPrivate && !f.IsPublic))
                        {
                            Console.WriteLine($"protected {field.FieldType.Name} {field.Name}");
                        }
                        break;
                    case "public":
                        foreach (var field in fields.Where(f => f.IsPublic))
                        {
                            Console.WriteLine($"public {field.FieldType.Name} {field.Name}");
                        }
                        break;
                    case "all":
                        foreach (var field in fields)
                        {
                            string prefix = "protected";
                            if (field.IsPublic)
                            {
                                prefix = "public";
                            }
                            else if (field.IsPrivate)
                            {
                                prefix = "private";
                            }

                            Console.WriteLine($"{prefix} {field.FieldType.Name} {field.Name}");
                        }
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

public class Spy
{
    public string StealFieldInfo(string className, params string[] namesOfFields)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Class under investigation: {className}");

        Type type = Type.GetType(className);
        FieldInfo[] fields = type.GetFields((BindingFlags)62);
        Object classInstance = Activator.CreateInstance(type);
        foreach (var propertyInfo in fields.Where(f => namesOfFields.Contains(f.Name)))
        {
            sb.AppendLine(propertyInfo.Name + " = " + propertyInfo.GetValue(classInstance));
        }

        return sb.ToString().Trim();
    }

    public string AnalyzeAcessModifier(string className)
    {
        StringBuilder sb = new StringBuilder();
        Type type = Type.GetType(className);

        var publicFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
        foreach (var publicField in publicFields)
        {
            sb.AppendLine($"{publicField.Name} must be private!");
        }

        var nonPublicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var nonPublicMethod in nonPublicMethods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{nonPublicMethod.Name} have to be public!");
        }

        var publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (var publicMethod in publicMethods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{publicMethod.Name} have to be private!");
        }

        return sb.ToString().Trim();
    }

    public string RevealPrivateMethods(string className)
    {
        StringBuilder sb = new StringBuilder();

        Type type = Type.GetType(className);
        sb.AppendLine($"All Private Methods of Class: {className}");
        sb.AppendLine($"Base Class: {type.BaseType.Name}");
        var privateFields = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var publicField in privateFields)
        {
            sb.AppendLine(publicField.Name);
        }        

        return sb.ToString().Trim();
    }

    public string Collector(string className)
    {
        StringBuilder sb = new StringBuilder();

        Type type = Type.GetType(className);
        sb.AppendLine($"All Private Methods of Class: {className}");
        sb.AppendLine($"Base Class: {type.BaseType.Name}");
        var allMethods = type.GetMethods((BindingFlags)62);
        var allGetMethods = allMethods.Where(m => m.Name.StartsWith("get"));
        foreach (var getMethod in allGetMethods)
        {
            sb.AppendLine($"{getMethod.Name} will return {getMethod.ReturnType}");
        }

        var allSetMethods = allMethods.Where(m => m.Name.StartsWith("set"));
        foreach (var setMethod in allSetMethods)
        {
            sb.AppendLine($"{setMethod.Name} will set field of {setMethod.GetParameters().First().ParameterType}");
        }

        return sb.ToString().Trim();
    }
}

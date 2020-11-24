using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

[TestFixture]
public class DatabaseTests
{
    private int[] array;
    private int[] bigArray;
    private int[] limitArray;

    [SetUp]
    public void Initialize()
    {
        array = Enumerable.Range(1, 5).ToArray();
        bigArray = Enumerable.Range(1, 17).ToArray();
        limitArray = Enumerable.Range(1, 16).ToArray();
    }

    [Test]
    public void ConstructorTestWithArrayWithLengthSmallThan16()
    {
        Database database = new Database(array);

        int[] actualResult = database.Fetch();

        Assert.AreEqual(array, actualResult);
    }

    [Test]
    public void ConstructorTestWithArrayWithLengthEqualTo16()
    {
        Database database = new Database(limitArray);

        int[] actualResult = database.Fetch();

        Assert.AreEqual(limitArray, actualResult);
    }

    [Test]
    public void ConstructorTestWithArrayWithLengthBiggerThan16()
    {
        Assert.Throws<InvalidOperationException>(() => new Database(bigArray));
    }

    [Test]
    public void AddMethodShouldAddOnce()
    {
        Database database = new Database(array);

        database.Add(5);
        int[] expected = array.Concat(new int[] { 5 }).ToArray();
        int[] actual = database.Fetch();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AddMethodShouldAddMultiply()
    {
        Database database = new Database(array);

        database.Add(1);
        database.Add(20);
        database.Add(300);
        database.Add(4000);
        int[] expected = array.Concat(new int[] { 1, 20, 300, 4000 }).ToArray();
        int[] actual = database.Fetch();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void AddMethodShouldThrowWhenTryToAdd17thInteger()
    {
        Database database = new Database(limitArray);

        Assert.Throws<InvalidOperationException>(() => database.Add(51521));
    }

    [Test]
    public void RemoveMethodShouldRemoveOnce()
    {
        Database database = new Database(array);

        database.Remove();
        int[] expected = array.Take(4).ToArray();
        int[] actual = database.Fetch();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void RemoveMethodShouldRemoveMultiply()
    {
        Database database = new Database(array);

        database.Remove();
        database.Remove();
        database.Remove();
        database.Remove();
        int[] expected = array.Take(1).ToArray();
        int[] actual = database.Fetch();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestForCurrentIndex()
    {
        var classType = typeof(Database);
        var database = (Database)Activator.CreateInstance(classType);      

        var currentIndex = (int)classType
            .GetField("currentIndex", BindingFlags.NonPublic |BindingFlags.Instance)
            .GetValue(database);

        

        Assert.That(currentIndex, Is.EqualTo(array.Length - 1));
    }
}

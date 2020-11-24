using NUnit.Framework;
using System;
using System.Linq;

[TestFixture]
public class DbExtendTests
{
    private Person[] normalArray;
    private Person[] biggerThanLimitArray;
    private Person[] limitArray;

    [SetUp]
    public void Initialize()
    {
        normalArray = (Person[])Array.CreateInstance(typeof(Person), 5);
        biggerThanLimitArray = (Person[])Array.CreateInstance(typeof(Person), 17);
        limitArray = (Person[])Array.CreateInstance(typeof(Person), 16);
    }

    [Test]
    public void AddMethodShouldAddOnePerson()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);
        Person[] expected = new Person[] { person };
        Person[] actual = database.Fetch();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AddMethodShouldAddMultiplePeople()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");
        Person person1 = new Person(12412, "Pesho");
        Person person2 = new Person(12424, "Kiro");
        Person person3 = new Person(12124, "Sasho");

        database.Add(person);
        database.Add(person1);
        database.Add(person2);
        database.Add(person3);
        Person[] expected = new Person[] { person, person1, person2, person3 };
        Person[] actual = database.Fetch();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AddMethodShouldAddOnePersonAndRemoveItWithRemoveMethod()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);
        database.Remove();
        Person[] actual = database.Fetch();

        Assert.That(actual, Is.EqualTo(new Person[0]));
    }

    [Test]
    public void AddMethodShouldAddMultiplePeopleAndRemoveSomeOfThemWithRemoveMethod()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");
        Person person1 = new Person(12412, "Pesho");
        Person person2 = new Person(12424, "Kiro");
        Person person3 = new Person(12124, "Sasho");

        database.Add(person);
        database.Add(person1);
        database.Remove();
        database.Add(person2);
        database.Remove();
        database.Add(person3);
        database.Remove();
        Person[] expected = new Person[] { person };
        Person[] actual = database.Fetch();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void FindByUsernameMethodShouldThrowIfNullParameterIsPassed()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);

        Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));
    }

    [Test]
    public void FindByUsernameMethodShouldThrowIfNoUserIsPresent()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);

        Assert.Throws<InvalidOperationException>(() => database.FindByUsername("Gooo"));
    }


    [Test]
    public void FindByUsernameMethodShouldReturnCorrectly()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);
        Person[] expected = new Person[] { person };
        Person actual = database.FindByUsername("Gosho");

        Assert.That(actual, Is.EqualTo(person));
    }

    [Test]
    public void FindByIdMethodShouldThrowWhenNegativeParameterIsPassed()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);

        Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-122));
    }

    [Test]
    public void FindByIdMethodShouldThrowWhenNoUserIsPresent()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);

        Assert.Throws<InvalidOperationException>(() => database.FindById(1111));
    }


    [Test]
    public void FindByIDMethodShouldReturnCorrectly()
    {
        Database database = new Database(normalArray);
        Person person = new Person(124124, "Gosho");

        database.Add(person);
        Person[] expected = new Person[] { person };
        Person actual = database.FindById(124124);

        Assert.That(actual, Is.EqualTo(person));
    }

}

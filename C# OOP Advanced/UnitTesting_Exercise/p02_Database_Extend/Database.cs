using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Database
{
    private Person[] array;
    private int currentIndex;

    public Database(Person[] people)
    {
        this.array = new Person[16];
        this.currentIndex = -1;
        InitializeArray(people);
    }

    private void InitializeArray(Person[] people)
    {
        if (people.Length > 16)
        {
            throw new InvalidOperationException("Capacity must be exactly 16");
        }

        for (int i = 0; i < people.Length; i++)
        {
            this.array[++currentIndex] = people[i];
        }
    }

    public void Add(Person person)
    {
        if (currentIndex + 1 > 15)
        {
            throw new InvalidOperationException("There are 16 elements you cannot add 17th.");
        }

        var notNullPeople = this.array.Where(p => p != null).ToArray();
        for (int i = 0; i < notNullPeople.Length; i++)
        {
            var current = notNullPeople[i];
            if(current.Username == person.Username)
            {
                throw new InvalidOperationException("Already there is a person with same username.");
            }

            if (current.Id == person.Id)
            {
                throw new InvalidOperationException("Already there is a person with same id.");
            }
        }
        this.array[++currentIndex] = person;
    }

    public void Remove()
    {
        if (this.currentIndex == -1)
        {
            throw new InvalidOperationException("Database is empty! You cannot remove item from empty database.");
        }

        this.array[currentIndex] = null;
        currentIndex--;
    }

    public Person[] Fetch()
    {
        return this.array.Where(e => e != null).ToArray();
    }

    public Person FindByUsername(string username)
    {
        if(username == null)
        {
            throw new ArgumentNullException("Provided username cannot be null.");
        }

        Person person = null;
        var notNullPeople = this.array.Where(p => p != null).ToArray();
        for (int i = 0; i < notNullPeople.Length; i++)
        {
            var current = notNullPeople[i];
            if (current.Username == username)
            {
                person = current;
            }           
        }

        if(person == null)
        {
            throw new InvalidOperationException("There is no such a person presented.");
        }

        return person;
    }

    public Person FindById(long id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException("Provided id cannot be negative.");
        }

        Person person = null;
        var notNullPeople = this.array.Where(p => p != null).ToArray();
        for (int i = 0; i < notNullPeople.Length; i++)
        {
            var current = notNullPeople[i];
            if (current.Id == id)
            {
                person = current;
            }
        }

        if (person == null)
        {
            throw new InvalidOperationException("There is no such a person presented.");
        }

        return person;
    }
}


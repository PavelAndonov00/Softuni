
using System;
using System.Collections.Generic;
using System.Text;

public class Person
{
    public Person(long id, string username)
    {
        this.Id = id;
        this.Username = username;
    }

    public long Id { get; }
    public string Username { get; }
}


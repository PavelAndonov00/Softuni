using Fluffy_Duffy_Munchkin_Cats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluffy_Duffy_Munchkin_Cats.Services
{
    public interface ICatServices
    {
        IEnumerable<Cat> GetAll();

        void Add(string Name, int Age, string Breed, string ImageUrl);

        Cat GetById(string Id);
    }
}

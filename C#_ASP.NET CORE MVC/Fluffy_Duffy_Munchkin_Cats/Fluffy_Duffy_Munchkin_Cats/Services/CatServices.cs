using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluffy_Duffy_Munchkin_Cats.Data;
using Fluffy_Duffy_Munchkin_Cats.Models;

namespace Fluffy_Duffy_Munchkin_Cats.Services
{
    public class CatServices : ICatServices
    {
        private readonly FluffyDbContext context;

        public CatServices(FluffyDbContext context)
        {
            this.context = context;
        }

        public void Add(string Name, int Age, string Breed, string ImageUrl)
        {
            var cat = new Cat
            {
                Name = Name,
                Age = Age,
                Breed = Breed,
                ImageUrl = ImageUrl
            };

            this.context.Cats.Add(cat);
            this.context.SaveChanges();
        }

        public IEnumerable<Cat> GetAll()
        {
            var cats = this.context.Cats.ToList();

            return cats;
        }

        public Cat GetById(string Id)
        {
            var cat = this.context.Cats.FirstOrDefault(c => c.Id == Id);

            return cat;
        }
    }
}

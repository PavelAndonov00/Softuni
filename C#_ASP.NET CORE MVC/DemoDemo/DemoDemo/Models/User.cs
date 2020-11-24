using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDemo.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Receipts = new HashSet<Receipt>();
            this.Packages = new HashSet<Package>();
        }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<Package> Packages { get; set; }
    }
}

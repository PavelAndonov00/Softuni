using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Suls.Data;
using Suls.Models;

namespace Suls.Service
{
    public class UsersService : IUsersService
    {
        private readonly SulsDbContext context;

        public UsersService(SulsDbContext context)
        {
            this.context = context;
        }

        public User CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = this.HashPassword(password)
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User GetUserById(string id)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }


        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var hashedPassword = this.HashPassword(password);
            var user = this
                .context
                .Users
                .FirstOrDefault(u => u.Username == username ||
                                     u.Email == username &&
                                     u.Password == hashedPassword);

            return user;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var encodedPassword = Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return encodedPassword;
            }
        }
    }
}

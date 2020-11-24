using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class UsersService : IUsersService
    {
        private readonly MusacaDbContext context;

        public UsersService(MusacaDbContext context)
        {
            this.context = context;
        }

        public User CreateUser(string Username, string Email, string Password)
        {
            var user = new User
            {
                Username = Username,
                Email = Email,
                Password = this.HashPassword(Password)
            };

            this.context.Users.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public User GetUserByUsernameAndPassword(string Username, string Password)
        {
            var hashedPassword = this.HashPassword(Password);
            var user = this.context.Users.FirstOrDefault(u => u.Username == Username || u.Email == Username && u.Password == hashedPassword);

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

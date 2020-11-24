using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Panda.Data;
using Panda.Models;

namespace Panda.Services
{
    public class UsersService : IUsersService
    {
        private readonly PandaDbContext context;

        public UsersService(PandaDbContext runesDbContext)
        {
            this.context = runesDbContext;
        }

        public User CreateUser(string username, string email, string password)
        {
            var userToCreate = new User
            {
                Username = username,
                Password = this.HashPassword(password),
                Email = email
            };

            var userToReturn = context.Users.Add(userToCreate).Entity;
            context.SaveChanges();

            return userToReturn;
        }

        public ICollection<string> GetAllUsersNamesWithoutCurrentLogged(string currentLoggedId)
        {
            var users = this.context.Users.Where(u => u.Id != currentLoggedId).Select(u => u.Username).ToList();

            return users;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var hashedPassword = this.HashPassword(password);
            var userToReturn = this.context.Users
                .SingleOrDefault(u => u.Username == username || u.Email == username && u.Password == hashedPassword);

            return userToReturn;
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

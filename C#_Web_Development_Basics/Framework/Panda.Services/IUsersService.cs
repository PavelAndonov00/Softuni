using Panda.Models;
using System.Collections.Generic;

namespace Panda.Services
{
    public interface IUsersService
    {
        User CreateUser(string username, string email, string password);

        User GetUserByUsernameAndPassword(string username, string password);
        ICollection<string> GetAllUsersNamesWithoutCurrentLogged(string currentLoggedId);
    }
}

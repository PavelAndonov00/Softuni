using Suls.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Service
{
    public interface IUsersService
    {
        User CreateUser(string username, string email, string password);

        User GetUserByUsernameAndPassword(string username, string password);

        User GetUserById(string id);
    }
}

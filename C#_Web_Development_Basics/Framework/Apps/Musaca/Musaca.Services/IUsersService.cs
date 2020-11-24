using Musaca.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musaca.Services
{
    public interface IUsersService
    {
        User CreateUser(string Username, string Email, string Password);

        User GetUserByUsernameAndPassword(string Username, string Password);
    }
}

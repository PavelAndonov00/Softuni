using System;
using System.Collections.Generic;
using System.Text;

namespace Musaca.App.ViewModels.Users
{
    public class UsersRegisterFormDataModel
    {
        public string username { get; set; }

        public string password { get; set; }

        public string confirmPassword { get; set; }

        public string email { get; set; }
    }
}

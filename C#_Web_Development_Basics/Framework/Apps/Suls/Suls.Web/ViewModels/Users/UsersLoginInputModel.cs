using SIS.MvcFramework.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Web.ViewModels.Users
{
    public class UsersLoginInputModel
    {
        private const string invalidUsernameMessage = "Username must be between 5 and 20 characters.";
        private const string invalidPasswordMessage = "Password must be between 6 and 20 characters.";

        [RequiredSis]
        [StringLengthSis(5, 20, invalidUsernameMessage)]
        public string Username { get; set; }


        [RequiredSis]
        [StringLengthSis(6, 20, invalidPasswordMessage)]
        public string Password { get; set; }
    }
}

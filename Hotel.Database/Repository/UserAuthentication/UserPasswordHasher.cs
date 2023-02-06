using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Database.Repository.UserAuthentication
{
    public class UserPasswordHasher : IUserPasswordHasher
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserPasswordHasher(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public void HashPassword(User user, string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            user.Password = hashedPassword;
        }

        public bool VerifyPassword(User user, string password)
        {
            var hashedPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return hashedPassword == PasswordVerificationResult.Success;
        }

    }
}

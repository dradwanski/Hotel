using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Entities;

namespace Hotel.Database.Repository.UserAuthentication
{
    public interface IUserPasswordHasher
    {
        void HashPassword(User user, string password);
        bool VerifyPassword(User user, string password);
    }
}

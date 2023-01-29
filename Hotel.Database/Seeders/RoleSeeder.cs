using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Entities;

namespace Hotel.Database.Seeders
{
    public class RoleSeeder : ISeeder<Role>
    {
        public IEnumerable<Role> GetDefaultValues()
        {
            return new[]
            {
                new Role()
                {
                    RoleName = "User"
                },
                new Role()
                {
                    RoleName = "Manager"
                },
                new Role()
                {
                    RoleName = "Admin"
                }
            };
        }
    }
}

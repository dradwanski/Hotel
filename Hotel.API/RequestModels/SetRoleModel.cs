using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.API.RequestModels
{
    public class SetRoleModel
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}

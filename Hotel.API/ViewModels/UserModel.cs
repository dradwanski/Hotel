using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.API.ViewModels
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

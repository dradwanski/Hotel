using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Services
{
    public interface IUserService
    {
        Task RegisterUser(UserDto dto);
    }
}

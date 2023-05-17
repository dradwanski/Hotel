using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Database.Repository.Helper;

namespace Hotel.Application.Services
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserDto dto);
        Task<Token> LoginUserAsync(UserDto dto);
        Task<List<UserDto>> GetUsers();
        Task SetRoleAsync(int userId, string roleName);
    }
}

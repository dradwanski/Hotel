using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Repository.Helper;

namespace Hotel.Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> RegisterUserAsync(UserDto dto);
        Task<bool> IsEmailExistAsync(UserDto dto);
        Task<bool> IsUserExistAsync(int userId);
        Task<bool> VerifyPasswordAsync(UserDto dto);
        Task<Token> LoginUserAsync(UserDto dto);
        Task<List<UserDto>> GetUsersAsync();
        Task SetRoleAsync(int userId, string roleName);
    }
}

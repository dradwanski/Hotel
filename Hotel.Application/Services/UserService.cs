using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Helper;
using Hotel.Application.Repository;
using Hotel.Database.Repository.Helper;

namespace Hotel.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _role;
        private readonly IUserRepository _user;

        public UserService(IRoleRepository role, IUserRepository user)
        {
            _role = role;
            _user = user;
        }
        public async Task RegisterUserAsync(UserDto dto)
        {
            dto.Role = await _role.GetDefaultRoleAsync();
            var successEmailValidate = EmailValidation.IsEmailValid(dto.Email);
            var successPasswordValidate = PasswordValidation.IsValidPassword(dto.Password);
            if (!successEmailValidate)
                throw new NotEmailValidException(dto.Email);
            if (!successPasswordValidate)
                throw new NotPasswordValidException(dto.Password);
            if (_user.IsEmailExistAsync(dto).Result)
                throw new NotValidException($"Email {dto.Email} exist in database");
            await _user.RegisterUserAsync(dto);
        }

        public async Task<Token> LoginUserAsync(UserDto dto)
        {
            if (!(await _user.IsEmailExistAsync(dto)))
            {
                throw new NotLoginValidException();
            }

            if (!(await _user.VerifyPasswordAsync(dto)))
            {
                throw new NotLoginValidException();
            }

            return await _user.LoginUserAsync(dto);
        }

        public Task<List<UserDto>> GetUsers()
        {
            return _user.GetUsersAsync();
        }

        public async Task SetRoleAsync(int userId, string roleName)
        {
            if (!await _user.IsUserExistAsync(userId))
            {
                throw new NotValidException($"User number {userId} does not exist");
            }
            if (!await _role.IsRoleExistAsync(roleName))
            {
                throw new NotValidException($"The specifed role does not exist");
            }
            await _user.SetRoleAsync(userId, roleName);
        }
    }
}

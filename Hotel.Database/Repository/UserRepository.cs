using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API;
using Hotel.Application.Dto;
using Hotel.Application.Repository;
using Hotel.Database.Entities;
using Hotel.Database.Repository.Helper;
using Hotel.Database.Repository.UserAuthentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hotel.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _dbContext;
        private readonly IUserPasswordHasher _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserRepository(IMapper mapper, HotelDbContext dbContext, IUserPasswordHasher passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public async Task<bool> RegisterUserAsync(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _passwordHasher.HashPassword(user, user.Password);

            var roleFromDatabase = await _dbContext.Set<Role>().FirstOrDefaultAsync(x => x.RoleId == dto.Role.RoleId);
            
            user.Role = roleFromDatabase!;

            await _dbContext.Set<User>().AddAsync(user);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public Task<bool> IsEmailExist(UserDto dto)
        {
            return _dbContext.Set<User>().AnyAsync(x => x.Email.ToUpper() == dto.Email.ToUpper());
        }
        public Task<bool> IsUserExist(int userId)
        {
            return _dbContext.Set<User>().AnyAsync(x => x.UserId == userId);
        }

        public async Task<bool> VerifyPassword(UserDto dto)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == dto.Email);
            return _passwordHasher.VerifyPassword(user, dto.Password);
        }

        public async Task<Token> LoginUserAsync(UserDto dto)
        {
            var user = await _dbContext.Set<User>()
                .Include(r => r.Role)
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.RoleName}")
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var datePattern = expires.ToString("dd-MM-yyyy HH:mm");

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            return new Token()
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpireDate = datePattern
            };
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _dbContext.Set<User>().Include(x => x.Role).ToListAsync();
            var result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public async Task SetRole(int userId, string roleName)
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserId == userId);
            var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
            user.Role = role;
            await _dbContext.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Application.Dto;
using Hotel.Application.Repository;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Database.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoleRepository(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<RoleDto> GetDefaultRoleAsync()
        {
            var defaultRole = await _dbContext.Set<Role>().FirstOrDefaultAsync(x => x.RoleName == "User");
            var dto = _mapper.Map<RoleDto>(defaultRole);
            return dto;
        }

        public Task<bool> IsRoleExistAsync(string roleName)
        {
            return _dbContext.Roles.AnyAsync(x => x.RoleName == roleName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Repository
{
    public interface IRoleRepository
    {
        Task<RoleDto> GetDefaultRoleAsync();
        Task<bool> IsRoleExist(string roleName);
    }
}

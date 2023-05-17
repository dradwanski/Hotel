using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Services
{
    public interface IRoomTypeService
    {
        Task<List<RoomTypeDto>> GetRoomTypesAsync();
        Task<RoomTypeDto> GetRoomTypeByIdAsync(int id);
        Task<int> CreateRoomTypeAsync(RoomTypeDto dto);
        Task DeleteAsync(int id);
        Task ChangeActiveOfRoomTypeAsync(int id);
        Task UpdateAsync(RoomTypeDto dto);
    }
}

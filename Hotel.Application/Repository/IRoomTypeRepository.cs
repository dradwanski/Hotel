using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repository
{
    public interface IRoomTypeRepository
    {
        Task<List<RoomTypeDto>> GetRoomTypesAsync();
        Task<RoomTypeDto> GetRoomTypeByIdAsync(int id);
        Task<int> CreateRoomTypeAsync(RoomTypeDto dto);
        Task<bool> IsRoomTypeExistAsync(int id);
        Task DeleteAsync(int id);
        Task ChangeActiveOfRoomTypeAsync(int id);
        Task UpdateAsync(RoomTypeDto dto);
    }
}

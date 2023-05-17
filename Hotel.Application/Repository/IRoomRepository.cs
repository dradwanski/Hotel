using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repository
{
    public interface IRoomRepository
    {
        Task<int> CreateRoomAsync(RoomDto dto);

        Task<List<RoomDto>> GetRoomsAsync(int pageNumber);
        Task<RoomDto> GetRoomByNumberAsync(int roomNumber);
        Task<RoomDto> GetRoomByIdAsync(int id);
        Task<bool> IsRoomNumberExistAsync(int roomNumber);
        Task<bool> IsRoomExistAsync(int id);
        Task UpdateAsync(RoomDto dto);
        Task<bool> IsRoomReservedAsync(int roomId, DateTime startDate, DateTime endDate);
    }
}

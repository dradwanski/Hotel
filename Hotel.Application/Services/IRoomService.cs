using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Services
{
    public interface IRoomService
    {
        Task<int> CreateRoomAsync(RoomDto dto);
        Task<List<RoomDto>> GetRoomsAsync(int pageNumber);
        Task<RoomDto> GetRoomByNumberAsync(int roomNumber);
        Task<RoomDto> GetRoomByIdAsync(int id);
        Task UpdateAsync(RoomDto dto);
    }
}

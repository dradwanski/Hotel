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
        Task<int> CreateRoom(RoomDto dto);
        Task<List<RoomDto>> GetRooms(int pageNumber);
        Task<RoomDto> GetRoomByNumber(int roomNumber);
        Task<RoomDto> GetRoomById(int id);
        Task Update(RoomDto dto);
    }
}

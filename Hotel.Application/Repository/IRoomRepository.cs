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
        Task<int> CreateRoom(RoomDto dto);

        Task<List<RoomDto>> GetRooms(int pageNumber);
        Task<RoomDto> GetRoomByNumber(int roomNumber);
        Task<RoomDto> GetRoomById(int id);
        Task<bool> IsRoomNumberExist(int roomNumber);
        Task<bool> IsRoomExist(int id);
        Task Update(RoomDto dto);
    }
}

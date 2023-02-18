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
        Task<List<RoomTypeDto>> GetRoomTypes();
        Task<RoomTypeDto> GetRoomTypeById(int id);
        Task<int> CreateRoomType(RoomTypeDto dto);
        Task<bool> IsRoomTypeExist(int id);
        Task Delete(int id);
        Task ChangeActiveOfRoomType(int id);
        Task Update(RoomTypeDto dto);
    }
}

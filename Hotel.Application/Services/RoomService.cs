using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Repository;

namespace Hotel.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomService(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<int> CreateRoomAsync(RoomDto dto)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExistAsync(dto.RoomTypeId);

            var isRoomNumberExist = await _roomRepository.IsRoomNumberExistAsync(dto.Number);

            if (!isRoomTypeExist)
            {
                throw new NotValidException("Type of room does not exist");
            }
            if (isRoomNumberExist)
            {
                throw new NotValidException("Room does exist");
            }

            return await _roomRepository.CreateRoomAsync(dto);
           
        }

        public async Task<List<RoomDto>> GetRoomsAsync(int pageNumber)
        {
            return await _roomRepository.GetRoomsAsync(pageNumber);
        }

        public async Task<RoomDto> GetRoomByNumberAsync(int roomNumber)
        {
            var isRoomNumberExist = await _roomRepository.IsRoomNumberExistAsync(roomNumber);
            if (isRoomNumberExist)
            {
                return await _roomRepository.GetRoomByNumberAsync(roomNumber);
            }
            throw new NotValidException("The room does not exist");
        }

        public async Task<RoomDto> GetRoomByIdAsync(int id)
        {
            var isRoomExist = await _roomRepository.IsRoomExistAsync(id);
            if (isRoomExist)
            {
                return await _roomRepository.GetRoomByIdAsync(id);
            }
            throw new NotValidException("The room does not exist");
        }

        public async Task UpdateAsync(RoomDto dto)
        {
            var isRoomExist = await _roomRepository.IsRoomExistAsync(dto.RoomId);

            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExistAsync(dto.RoomTypeId);

            var isRoomNumberExist = await _roomRepository.IsRoomNumberExistAsync(dto.Number);


            if (isRoomNumberExist)
            {
                throw new NotValidException("Number of room does exist");
            }
            if (!isRoomTypeExist)
            {
                throw new NotValidException("Type of room does not exist");
            }
            if (!isRoomExist)
            {
                throw new NotValidException("Room does not exist");
            }

            await _roomRepository.UpdateAsync(dto);
        }
    }
}

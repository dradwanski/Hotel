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

        public async Task<int> CreateRoom(RoomDto dto)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExist(dto.RoomTypeId);

            var isRoomNumberExist = await _roomRepository.IsRoomNumberExist(dto.Number);

            if (!isRoomTypeExist)
            {
                throw new NotValidException("Type of room does not exist");
            }
            if (isRoomNumberExist)
            {
                throw new NotValidException("Room does exist");
            }

            return await _roomRepository.CreateRoom(dto);
           
        }

        public async Task<List<RoomDto>> GetRooms(int pageNumber)
        {
            return await _roomRepository.GetRooms(pageNumber);
        }

        public async Task<RoomDto> GetRoomByNumber(int roomNumber)
        {
            var isRoomNumberExist = await _roomRepository.IsRoomNumberExist(roomNumber);
            if (isRoomNumberExist)
            {
                return await _roomRepository.GetRoomByNumber(roomNumber);
            }
            throw new NotValidException("The room does not exist");
        }

        public async Task<RoomDto> GetRoomById(int id)
        {
            var isRoomExist = await _roomRepository.IsRoomExist(id);
            if (isRoomExist)
            {
                return await _roomRepository.GetRoomById(id);
            }
            throw new NotValidException("The room does not exist");
        }

        public async Task Update(RoomDto dto)
        {
            var isRoomExist = await _roomRepository.IsRoomExist(dto.RoomId);

            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExist(dto.RoomTypeId);

            var isRoomNumberExist = await _roomRepository.IsRoomNumberExist(dto.Number);


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

            await _roomRepository.Update(dto);
        }
    }
}

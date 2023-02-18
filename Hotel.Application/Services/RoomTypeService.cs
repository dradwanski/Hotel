using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Repository;

namespace Hotel.Application.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }
        public async Task<List<RoomTypeDto>> GetRoomTypes()
        {
            return await _roomTypeRepository.GetRoomTypes();
        }

        public async Task<RoomTypeDto> GetRoomTypeById(int id)
        {
            var roomType = await _roomTypeRepository.GetRoomTypeById(id);

            if (roomType == null)
            {
                throw new NotValidException("the given room type does not exist");
            }

            return roomType;
        }

        public async Task<int> CreateRoomType(RoomTypeDto dto)
        {

            if (!string.IsNullOrWhiteSpace(dto.Type) && !string.IsNullOrWhiteSpace(dto.Standard))
            {
                return await _roomTypeRepository.CreateRoomType(dto);
            }
            throw new NotValidException("All fields must be completed");
        }

        public async Task Delete(int id)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExist(id);

            if (isRoomTypeExist)
            {
                await _roomTypeRepository.Delete(id);
                return;
            }

            throw new NotValidException($"Room Type with Id {id} does not exist");
        }

        public async Task ChangeActiveOfRoomType(int id)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExist(id);

            if (isRoomTypeExist)
            {
                await _roomTypeRepository.ChangeActiveOfRoomType(id);
                return;
            }

            throw new NotValidException($"Room Type with Id {id} does not exist");
        }

        public async Task Update(RoomTypeDto dto)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExist(dto.Id);

            if (isRoomTypeExist && !string.IsNullOrWhiteSpace(dto.Type) && !string.IsNullOrWhiteSpace(dto.Standard))
            {
                await _roomTypeRepository.Update(dto);
                return;
            }

            throw new NotValidException($"Room Type with Id {dto.Id} does not exist");
        }
    }
}

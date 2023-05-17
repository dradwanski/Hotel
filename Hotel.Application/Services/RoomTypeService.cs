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
        public async Task<List<RoomTypeDto>> GetRoomTypesAsync()
        {
            return await _roomTypeRepository.GetRoomTypesAsync();
        }

        public async Task<RoomTypeDto> GetRoomTypeByIdAsync(int id)
        {
            var roomType = await _roomTypeRepository.GetRoomTypeByIdAsync(id);

            if (roomType == null)
            {
                throw new NotValidException("the given room type does not exist");
            }

            return roomType;
        }

        public async Task<int> CreateRoomTypeAsync(RoomTypeDto dto)
        {

            if (!string.IsNullOrWhiteSpace(dto.Type) && !string.IsNullOrWhiteSpace(dto.Standard))
            {
                return await _roomTypeRepository.CreateRoomTypeAsync(dto);
            }
            throw new NotValidException("All fields must be completed");
        }

        public async Task DeleteAsync(int id)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExistAsync(id);

            if (isRoomTypeExist)
            {
                await _roomTypeRepository.DeleteAsync(id);
                return;
            }

            throw new NotValidException($"Room Type with Id {id} does not exist");
        }

        public async Task ChangeActiveOfRoomTypeAsync(int id)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExistAsync(id);

            if (isRoomTypeExist)
            {
                await _roomTypeRepository.ChangeActiveOfRoomTypeAsync(id);
                return;
            }

            throw new NotValidException($"Room Type with Id {id} does not exist");
        }

        public async Task UpdateAsync(RoomTypeDto dto)
        {
            var isRoomTypeExist = await _roomTypeRepository.IsRoomTypeExistAsync(dto.Id);

            if (isRoomTypeExist && !string.IsNullOrWhiteSpace(dto.Type) && !string.IsNullOrWhiteSpace(dto.Standard))
            {
                await _roomTypeRepository.UpdateAsync(dto);
                return;
            }

            throw new NotValidException($"Room Type with Id {dto.Id} does not exist");
        }
    }
}

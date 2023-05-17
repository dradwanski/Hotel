using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Application.Dto;
using Hotel.Application.Repository;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Database.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoomTypeRepository(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<RoomTypeDto>> GetRoomTypesAsync()
        {
            var roomTypes = await _dbContext.Set<RoomType>().Where(x => x.IsActive == true).ToListAsync();

            var result = _mapper.Map<List<RoomTypeDto>>(roomTypes);

            return result;
        }

        public async Task<RoomTypeDto> GetRoomTypeByIdAsync(int id)
        {
            var roomType = await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == id);

            var result = _mapper.Map<RoomTypeDto>(roomType);

            return result;
        }

        public async Task<int> CreateRoomTypeAsync(RoomTypeDto dto)
        {
            var roomType = _mapper.Map<RoomType>(dto);

            await _dbContext.AddAsync(roomType);

            await _dbContext.SaveChangesAsync();

            return roomType.RoomTypeId;
        }

        public async Task<bool> IsRoomTypeExistAsync(int id)
        {
            return await _dbContext.Set<RoomType>().AnyAsync(x => x.RoomTypeId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var roomType = await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == id);

            _dbContext.RoomTypes.Remove(roomType!);

            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeActiveOfRoomTypeAsync(int id)
        {
            var roomType = await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == id);
            
            roomType!.IsActive ^= true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomTypeDto dto)
        {
            var roomType = await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == dto.Id);
            roomType!.Price = dto.Price;
            roomType.Standard = dto.Standard;
            roomType.Type = dto.Type;
            await _dbContext.SaveChangesAsync();
        }
    }
}

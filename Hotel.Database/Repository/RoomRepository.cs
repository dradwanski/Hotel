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
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;

        public RoomRepository(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> CreateRoom(RoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);

            var roomTypeFromDatabase = await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == dto.RoomTypeId);

            room.Type = roomTypeFromDatabase!;

            await _dbContext.AddAsync(room);

            await _dbContext.SaveChangesAsync();
            
            return room.RoomId;
        }

        public async Task<List<RoomDto>> GetRooms(int pageNumber)
        {
            var pageSize = 10;

            var rooms = await _dbContext.Set<Room>()
                .Include(x => x.Type)
                .Skip(pageSize * (pageNumber -1))
                .Take(pageSize)
                .ToListAsync();

            var roomDtos = _mapper.Map<List<RoomDto>>(rooms);
            return roomDtos;
        }

        public async Task<RoomDto> GetRoomByNumber(int roomNumber)
        {
            var roomByNumber = await _dbContext.Set<Room>().Include(x => x.Type).FirstOrDefaultAsync(x => x.Number == roomNumber);
            var roomByNumberDto = _mapper.Map<RoomDto>(roomByNumber);
            return roomByNumberDto;
        }

        public async Task<RoomDto> GetRoomById(int id)
        {
            var roomById = await _dbContext.Set<Room>().Include(x => x.Type).FirstOrDefaultAsync(x => x.RoomId == id);
            var roomByIdDto = _mapper.Map<RoomDto>(roomById);
            return roomByIdDto;
        }

        public async Task<bool> IsRoomNumberExist(int roomNumber)
        {
            return await _dbContext.Set<Room>().AnyAsync(x => x.Number == roomNumber);
        }
        public async Task<bool> IsRoomExist(int id)
        {
            return await _dbContext.Set<Room>().AnyAsync(x => x.RoomId == id);
        }

        public async Task Update(RoomDto dto)
        {
            var roomFromDatabase = await _dbContext.Set<Room>().FirstOrDefaultAsync(x => x.RoomId == dto.RoomId);
            var roomTypeFromDatabase =
                await _dbContext.Set<RoomType>().FirstOrDefaultAsync(x => x.RoomTypeId == dto.RoomTypeId);

            roomFromDatabase!.Number = dto.Number;

            roomFromDatabase.Type = roomTypeFromDatabase!;

            await _dbContext.SaveChangesAsync();
        }
    }
}

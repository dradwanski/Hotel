using Hotel.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Application.Dto;
using Hotel.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;

namespace Hotel.Database.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationRepository(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task CreateReservation(ReservationDto reservationDto)
        {
            var client = await _dbContext
                .Set<Client>()
                .FirstOrDefaultAsync(x => x.Id == reservationDto.ClientId);

            var methodOfPayment = await _dbContext
                .Set<MethodOfPayment>()
                .FirstOrDefaultAsync(x => x.MethodOfPaymentId == reservationDto.MethodOfPaymentId);

            var user = await _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(x => x.UserId == reservationDto.CreatedUserId);

            var reservation = _mapper.Map<Reservation>(reservationDto);
            reservation.Client = client;
            reservation.MethodOfPayment = methodOfPayment;
            reservation.CreatedUser = user;
            reservation.LastEditedUser = user;

            var room = await _dbContext
                .Set<Room>()
                .Include(x => x.ListOfReservation)
                .FirstOrDefaultAsync(x => x.RoomId == reservationDto.RoomId);

            room.ListOfReservation.Add(reservation);


            await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> IsReservationExist(int reservationId)
        {
            var reservation = await _dbContext
                .Set<Reservation>()
                .FirstOrDefaultAsync(j => j.ReservationId == reservationId);

            return reservation is not null;
        }

        public async Task PutMethodOfPayment(int reservationId, int methodOfPaymentId)
        {
            var methodOfPayment = await _dbContext
                .Set<MethodOfPayment>()
                .FirstOrDefaultAsync(x => x.MethodOfPaymentId == methodOfPaymentId);

            var reservation = await _dbContext
                .Set<Reservation>()
                .Include(m => m.MethodOfPayment)
                .FirstOrDefaultAsync(j => j.ReservationId == reservationId);

            reservation.MethodOfPayment = methodOfPayment;
            reservation.DateOfPayment = DateTime.Now;
            reservation.ReservationStatus.StatusId = 2;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _dbContext
                .Set<Reservation>()
                .FirstOrDefaultAsync(j => j.ReservationId == id);

            _dbContext.Remove(reservation!);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<ReservationDto>> GetReservationsByClientid(int clientid, int pageSize, int pageNumber)
        {
            var reservations= await _dbContext
                .Set<Reservation>()
                .Include(z => z.Client)
                .Where(x => x.Client.Id == clientid)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);
            return reservationsDto;
        }

        public async Task<List<ReservationDto>> GetReservationsByRoomId(int roomId, int pageSize, int pageNumber)
        {

            var reservations = await _dbContext
                .Set<Room>()
                .Include(x => x.ListOfReservation)
                .ThenInclude(z => z.Client)
                .Include(z => z.ListOfReservation)
                .ThenInclude(z => z.CreatedUser)
                .Include(z => z.ListOfReservation)
                .ThenInclude(z => z.LastEditedUser)
                .Include(z => z.ListOfReservation)
                .ThenInclude(z => z.MethodOfPayment)
                .Include(z => z.ListOfReservation)
                .ThenInclude(z => z.ReservationStatus)
                .Include(z => z.ListOfReservation)
                .ThenInclude(z => z.Room)
                .Include(z => z.ListOfReservation)
                .Where(x => x.RoomId == roomId)
                .Select(x => x.ListOfReservation)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .FirstOrDefaultAsync();

            var reservationsDtos = _mapper.Map<List<ReservationDto>>(reservations);

            return reservationsDtos;
        }

        public async Task<List<ReservationDto>> GetReservationsByDate(DateTime startDate, DateTime endDate, int pageSize, int pageNumber)
        {
            var reservations = await _dbContext
                .Set<Reservation>()
                .Include(z => z.Client)
                .Include(z => z.CreatedUser)
                .Include(t => t.LastEditedUser)
                .Include(m => m.MethodOfPayment)
                .Include(m => m.ReservationStatus)
                .Include(m => m.Room)
                .Where(z => (z.ReservationStart > startDate && z.ReservationStart < endDate && z.ReservationEnd > startDate && z.ReservationEnd < endDate)
                            || (z.ReservationStart < startDate && z.ReservationEnd > startDate && z.ReservationEnd < endDate)
                            || (z.ReservationStart > startDate && z.ReservationStart < endDate && z.ReservationEnd > endDate)
                            || (z.ReservationStart < startDate && z.ReservationEnd > endDate)
                            || (z.ReservationStart == startDate)
                            || (z.ReservationEnd == endDate))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            

            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);


            return reservationsDto;
        }

        public async Task UpdateReservation(ReservationDto reservationDto)
        {
            var list = new List<Task>(3);

            var client = _dbContext
                .Set<Client>()
                .FirstOrDefaultAsync(x => x.Id == reservationDto.ClientId);

            var methodOfPayment = _dbContext
                .Set<MethodOfPayment>()
                .FirstOrDefaultAsync(x => x.MethodOfPaymentId == reservationDto.MethodOfPaymentId);

            var user = _dbContext
                .Set<User>()
                .FirstOrDefaultAsync(x => x.UserId == reservationDto.LastEditedUserId);

            list.Add(client);
            list.Add(methodOfPayment);
            list.Add(user);

            await Task.WhenAll(list);


            var reservation = await _dbContext
                .Set<Reservation>()
                .FirstOrDefaultAsync(x => x.ReservationId == reservationDto.ReservationId);


            reservation.Client = client.Result;
            reservation.MethodOfPayment = methodOfPayment.Result;
            reservation.LastEditedUser = user.Result;

            await _dbContext.SaveChangesAsync();

        }

        public async Task ConfirmReservation(int reservationId)
        {
            var reservation = await _dbContext
                .Set<Reservation>()
                .Include(m => m.ReservationStatus)
                .FirstOrDefaultAsync(x => x.ReservationId == reservationId);

            var status = await _dbContext.Set<Status>().FirstOrDefaultAsync(x => x.StatusName == "confirmed");

            reservation.ReservationStatus = status;
            await _dbContext.SaveChangesAsync();

        }

        public async Task CancelReservation(int reservationId)
        {
            var reservation = await _dbContext
                .Set<Reservation>()
                .Include(m => m.ReservationStatus)
                .FirstOrDefaultAsync(x => x.ReservationId == reservationId);

            var status = await _dbContext.Set<Status>().FirstOrDefaultAsync(x => x.StatusName == "cancelled");

            reservation.ReservationStatus = status;
            await _dbContext.SaveChangesAsync();
        }
    }
}



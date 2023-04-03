using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Helper;
using Hotel.Application.Repository;

namespace Hotel.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMethodOfPaymentRepository _methodOfPayment;

        public ReservationService(IReservationRepository reservationRepository, IClientRepository clientRepository, IRoomRepository roomRepository, IMethodOfPaymentRepository methodOfPayment)
        {
            _reservationRepository = reservationRepository;
            _clientRepository = clientRepository;
            _roomRepository = roomRepository;
            _methodOfPayment = methodOfPayment;
        }

        public async Task CreateReservation(ReservationDto reservationDto)
        {
            if(!await _clientRepository.IsClientExist(reservationDto.ClientId))
            {
                throw new NotValidException("Client does not exist");
            }

            if (!await _roomRepository.IsRoomExist(reservationDto.RoomId))
            {
                throw new NotValidException("Room does not exist");
            }

            if (reservationDto.MethodOfPaymentId is not null && !await _methodOfPayment.IsMethodOfPaymentsExistById(reservationDto.MethodOfPaymentId.Value))
            { 
                throw new NotValidException("Method of payment does not exist");
            }

            if (!ReservationDateValidation.IsReservationDateValid(reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }

            if (await _roomRepository.IsRoomReserved(reservationDto.RoomId, reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("The room is unavailable");
            }

            await _reservationRepository.CreateReservation(reservationDto);
        }

        public async Task PutMethodOfPayment(int reservationId, int methodOfPaymentId)
        {
            if (!await _methodOfPayment.IsMethodOfPaymentsExistById(methodOfPaymentId))
            {
                throw new NotValidException("Method of payment does not exist");
            }
            if (!await _reservationRepository.IsReservationExist(reservationId))
            {
                throw new NotValidException("Reservation does not exist");
            }
            await _reservationRepository.PutMethodOfPayment(reservationId, methodOfPaymentId);
        }

        public async Task DeleteReservation(int id)
        {
            if (!await _reservationRepository.IsReservationExist(id))
            {
                throw new NotValidException("Reservation does not exist");
            }

            await _reservationRepository.DeleteReservation(id);
        }

        public async Task<List<ReservationDto>> GetReservationsByClientid(int clientid, int pageSize, int pageNumber)
        {
            return await _reservationRepository.GetReservationsByClientid(clientid, pageSize, pageNumber);
        }

        public async Task<List<ReservationDto>> GetReservationsByRoomId(int roomId, int pageSize, int pageNumber)
        {
            return await _reservationRepository.GetReservationsByRoomId(roomId, pageSize, pageNumber);
        }

        public async Task<List<ReservationDto>> GetReservationsByDate(string startDate, string? endDate, int pageSize, int pageNumber)
        {

            var ReservationStart = DateTime.Parse(startDate).AddHours(12);
            var ReservationEnd = DateTime.Parse(endDate).AddHours(10);

            if (!ReservationDateValidation.IsReservationDateValid(ReservationStart, ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }
            return await _reservationRepository.GetReservationsByDate(ReservationStart, ReservationEnd, pageSize, pageNumber);
        }

        public async Task UpdateReservation(int reservationId, ReservationDto reservationDto)
        {
            if (!await _clientRepository.IsClientExist(reservationDto.ClientId))
            {
                throw new NotValidException("Client does not exist");
            }

            if (!await _roomRepository.IsRoomExist(reservationDto.RoomId))
            {
                throw new NotValidException("Room does not exist");
            }

            if (reservationDto.MethodOfPaymentId is not null && !await _methodOfPayment.IsMethodOfPaymentsExistById(reservationDto.MethodOfPaymentId.Value))
            {
                throw new NotValidException("Method of payment does not exist");
            }

            if (!ReservationDateValidation.IsReservationDateValid(reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }

            if (await _roomRepository.IsRoomReserved(reservationDto.RoomId, reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("The room is unavailable");
            }

            await _reservationRepository.UpdateReservation(reservationDto);
        }

        public async Task ConfirmReservation(int reservationId)
        {
            await _reservationRepository.ConfirmReservation(reservationId);
        }

        public async Task CancelReservation(int reservationId)
        {
            await _reservationRepository.CancelReservation(reservationId);
        }
    }
}

﻿using System;
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

        public async Task CreateReservationAsync(ReservationDto reservationDto)
        {
            if(!await _clientRepository.IsClientExistAsync(reservationDto.ClientId))
            {
                throw new NotValidException("Client does not exist");
            }

            if (!await _roomRepository.IsRoomExistAsync(reservationDto.RoomId))
            {
                throw new NotValidException("Room does not exist");
            }

            if (reservationDto.MethodOfPaymentId is not null && !await _methodOfPayment.IsMethodOfPaymentsExistByIdAsync(reservationDto.MethodOfPaymentId.Value))
            { 
                throw new NotValidException("Method of payment does not exist");
            }

            if (!ReservationDateValidation.IsReservationDateValid(reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }

            if (await _roomRepository.IsRoomReservedAsync(reservationDto.RoomId, reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("The room is unavailable");
            }

            await _reservationRepository.CreateReservationAsync(reservationDto);
        }

        public async Task PutMethodOfPaymentAsync(int reservationId, int methodOfPaymentId)
        {
            if (!await _methodOfPayment.IsMethodOfPaymentsExistByIdAsync(methodOfPaymentId))
            {
                throw new NotValidException("Method of payment does not exist");
            }
            if (!await _reservationRepository.IsReservationExistAsync(reservationId))
            {
                throw new NotValidException("Reservation does not exist");
            }
            await _reservationRepository.PutMethodOfPaymentAsync(reservationId, methodOfPaymentId);
        }

        public async Task DeleteReservationAsync(int id)
        {
            if (!await _reservationRepository.IsReservationExistAsync(id))
            {
                throw new NotValidException("Reservation does not exist");
            }

            await _reservationRepository.DeleteReservationAsync(id);
        }

        public async Task<List<ReservationDto>> GetReservationsByClientidAsync(int clientid, int pageSize, int pageNumber)
        {
            return await _reservationRepository.GetReservationsByClientidAsync(clientid, pageSize, pageNumber);
        }

        public async Task<List<ReservationDto>> GetReservationsByRoomIdAsync(int roomId, int pageSize, int pageNumber)
        {
            return await _reservationRepository.GetReservationsByRoomIdAsync(roomId, pageSize, pageNumber);
        }

        public async Task<List<ReservationDto>> GetReservationsByDateAsync(string startDate, string? endDate, int pageSize, int pageNumber)
        {

            var ReservationStart = DateTime.Parse(startDate).AddHours(12);
            var ReservationEnd = DateTime.Parse(endDate).AddHours(10);

            if (!ReservationDateValidation.IsReservationDateValid(ReservationStart, ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }
            return await _reservationRepository.GetReservationsByDateAsync(ReservationStart, ReservationEnd, pageSize, pageNumber);
        }

        public async Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto)
        {
            if (!await _clientRepository.IsClientExistAsync(reservationDto.ClientId))
            {
                throw new NotValidException("Client does not exist");
            }

            if (!await _roomRepository.IsRoomExistAsync(reservationDto.RoomId))
            {
                throw new NotValidException("Room does not exist");
            }

            if (reservationDto.MethodOfPaymentId is not null && !await _methodOfPayment.IsMethodOfPaymentsExistByIdAsync(reservationDto.MethodOfPaymentId.Value))
            {
                throw new NotValidException("Method of payment does not exist");
            }

            if (!ReservationDateValidation.IsReservationDateValid(reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("Invalid date");
            }

            if (await _roomRepository.IsRoomReservedAsync(reservationDto.RoomId, reservationDto.ReservationStart,
                    reservationDto.ReservationEnd))
            {
                throw new NotValidException("The room is unavailable");
            }

            await _reservationRepository.UpdateReservationAsync(reservationDto);
        }

        public async Task ConfirmReservationAsync(int reservationId)
        {
            await _reservationRepository.ConfirmReservationAsync(reservationId);
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            await _reservationRepository.CancelReservationAsync(reservationId);
        }
    }
}

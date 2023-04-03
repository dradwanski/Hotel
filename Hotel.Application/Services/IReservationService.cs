using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Services
{
    public interface IReservationService
    {
        Task CreateReservation(ReservationDto reservationDto);
        Task PutMethodOfPayment(int reservationId, int methodOfPaymentId);
        Task DeleteReservation(int id);
        Task<List<ReservationDto>> GetReservationsByClientid(int clientid, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByRoomId(int roomId, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByDate(string startDate, string? endDate, int pageSize, int pageNumber);
        Task UpdateReservation(int reservationId, ReservationDto reservationDto);
        Task ConfirmReservation(int reservationId);
        Task CancelReservation(int reservationId);
    }
}

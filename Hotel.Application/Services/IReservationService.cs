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
        Task CreateReservationAsync(ReservationDto reservationDto);
        Task PutMethodOfPaymentAsync(int reservationId, int methodOfPaymentId);
        Task DeleteReservationAsync(int id);
        Task<List<ReservationDto>> GetReservationsByClientidAsync(int clientid, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByRoomIdAsync(int roomId, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByDateAsync(string startDate, string? endDate, int pageSize, int pageNumber);
        Task UpdateReservationAsync(int reservationId, ReservationDto reservationDto);
        Task ConfirmReservationAsync(int reservationId);
        Task CancelReservationAsync(int reservationId);
    }
}

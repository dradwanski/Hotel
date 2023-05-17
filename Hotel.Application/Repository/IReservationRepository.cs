using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Repository
{
    public interface IReservationRepository
    {
        Task CreateReservationAsync(ReservationDto reservationDto);
        Task<bool> IsReservationExistAsync(int reservationId);
        Task PutMethodOfPaymentAsync(int reservationId, int methodOfPaymentId);
        Task DeleteReservationAsync(int id);
        Task<List<ReservationDto>> GetReservationsByClientidAsync(int clientid, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByRoomIdAsync(int roomId, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByDateAsync(DateTime startDate, DateTime endDate, int pageSize, int pageNumber);
        Task UpdateReservationAsync(ReservationDto reservationDto);
        Task ConfirmReservationAsync(int reservationId);
        Task CancelReservationAsync(int reservationId);
    }
}

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
        Task CreateReservation(ReservationDto reservationDto);
        Task<bool> IsReservationExist(int reservationId);
        Task PutMethodOfPayment(int reservationId, int methodOfPaymentId);
        Task DeleteReservation(int id);
        Task<List<ReservationDto>> GetReservationsByClientid(int clientid, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByRoomId(int roomId, int pageSize, int pageNumber);
        Task<List<ReservationDto>> GetReservationsByDate(DateTime startDate, DateTime endDate, int pageSize, int pageNumber);
        Task UpdateReservation(ReservationDto reservationDto);
        Task ConfirmReservation(int reservationId);
        Task CancelReservation(int reservationId);
    }
}

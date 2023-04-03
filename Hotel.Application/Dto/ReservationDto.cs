using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public int? MethodOfPaymentId { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int CreatedUserId { get; set; }
        public int LastEditedUserId { get; set; }
        public DateTime ModificationDate { get; init; }
        public int ReservationStatusId { get; set; }
    }
}

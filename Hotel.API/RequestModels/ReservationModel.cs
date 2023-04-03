using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.API.RequestModels
{
    public class ReservationModel
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public string ReservationStart { get; set; }
        public string ReservationEnd { get; set; }
        public int? MethodOfPaymentId { get; set; }
    }
}

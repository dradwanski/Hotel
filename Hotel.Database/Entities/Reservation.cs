using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Database.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Room Room { get; set; }
        public virtual MethodOfPayment? MethodOfPayment { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public virtual User? CreatedUser { get; set; }
        public virtual User LastEditedUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public virtual Status ReservationStatus { get; set; }
    }
}

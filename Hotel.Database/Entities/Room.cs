using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Database.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public int Number { get; set; }
        public virtual RoomType Type { get; set; }
        public virtual List<Reservation> ListOfReservation { get; set; }
    }
}

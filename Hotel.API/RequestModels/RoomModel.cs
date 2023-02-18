using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.API.RequestModels
{
    public class RoomModel
    {
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
    }
}

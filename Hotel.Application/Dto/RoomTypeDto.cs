using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Dto
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Standard { get; set; }
        public int Price { get; set; }
        public bool? IsActive { get; set; }
    }
}

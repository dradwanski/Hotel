using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.API.RequestModels
{
    public class RoomTypeModel
    {
        public string Type { get; set; }
        public string Standard { get; set; }
        public int Price { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Database.Entities
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Type { get; set; }
        public string Standard { get; set; }
        public int Price { get; set; }
        public bool? IsActive { get; set; }
    }
}

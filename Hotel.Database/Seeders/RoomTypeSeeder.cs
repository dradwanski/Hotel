using Hotel.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Database.Seeders
{
    public class RoomTypeSeeder : ISeeder<RoomType>
    {
        public IEnumerable<RoomType> GetDefaultValues()
        {
            return new[]
            {
                new RoomType()
                {
                     Type = "Single",
                     Standard = "Economic",
                     Price = 100
                },
                new RoomType()
                {
                    Type = "Double",
                    Standard = "Economic",
                    Price = 190
                },
                new RoomType()
                {
                    Type = "Triple",
                    Standard = "Economic",
                    Price = 270
                },
                new RoomType()
                {
                    Type = "Quad",
                    Standard = "Economic",
                    Price = 340
                },
                new RoomType()
                {
                    Type = "Single",
                    Standard = "Standard",
                    Price = 120
                },
                new RoomType()
                {
                    Type = "Double",
                    Standard = "Standard",
                    Price = 230
                },
                new RoomType()
                {
                    Type = "Triple",
                    Standard = "Standard",
                    Price = 330
                },
                new RoomType()
                {
                    Type = "Quad",
                    Standard = "Standard",
                    Price = 420
                },
                new RoomType()
                {
                    Type = "Double",
                    Standard = "Apartament",
                    Price = 320
                },
                new RoomType()
                {
                    Type = "Quad",
                    Standard = "Apartament",
                    Price = 600
                },
            };
        }
    }
}

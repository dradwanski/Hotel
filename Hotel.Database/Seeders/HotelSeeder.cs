using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Hotel.Database.Seeders
{
    public class HotelSeeder
    {
        private readonly HotelDbContext _dbContext;

        public HotelSeeder(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed<T, TEntity>() where T : class, ISeeder<TEntity>, new() where TEntity : class
        {

            if (!_dbContext.Set<TEntity>().Any())
            {
                var valueToAdd = new T().GetDefaultValues();
                _dbContext.Set<TEntity>().AddRange(valueToAdd);
                _dbContext.SaveChanges();
            }
        }
    }
}

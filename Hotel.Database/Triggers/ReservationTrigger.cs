using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Database.Triggers
{
    public class ReservationTrigger : IBeforeSaveTrigger<Reservation>
    {
        private readonly HotelDbContext _dbContext;

        public ReservationTrigger(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task BeforeSave(ITriggerContext<Reservation> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                return Task.CompletedTask;
            }
            if (context.ChangeType == ChangeType.Added)
            {
                var status = _dbContext.Set<Status>().FirstOrDefault();
                context.Entity.ReservationStatus = status;
            }
            context.Entity.ModificationDate = DateTime.Now;
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Helper
{
    public static class ReservationDateValidation
    {
        public static bool IsReservationDateValid(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return false;
            }

            return true;
        }
    }
}

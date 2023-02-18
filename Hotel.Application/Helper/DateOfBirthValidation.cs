using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Helper
{
    public static class DateOfBirthValidation
    {
        public static bool IsAdult(DateTime dateOfBirth)
        {
            return DateTime.Now.AddYears(-18) >= dateOfBirth;
        }
    }
}

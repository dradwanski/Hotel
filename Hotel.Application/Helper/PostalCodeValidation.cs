using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Helper
{
    public static class PostalCodeValidation
    {
        public static bool IsPostalCodeValid(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                return false;
            }
            int count = 0;
            foreach (var item in postalCode)
            {
                if (item is >= '0' or <= '9')
                {
                    count++;
                }
            }
            return count >= 4 && count + 2 <= postalCode.Length && postalCode.Length<10;
        }
    }
}

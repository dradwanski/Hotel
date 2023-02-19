using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hotel.Application.Helper
{
    public static class PhoneNumberValidation
    {

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            int count = 0;
            foreach (var item in phoneNumber)
            {
                if (item is >= '0' or <= '9')
                {
                    count++;
                }
            }
            return count == 9 || count == 10;
        }
    }
}

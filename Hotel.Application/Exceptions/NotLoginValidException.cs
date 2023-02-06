using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Exceptions
{
    public class NotLoginValidException : AppException
    {
        public NotLoginValidException() : base("Invalid email or password")
        {
            Message = "Invalid email or password";
        }

        public override string Message { get; }
    }
}

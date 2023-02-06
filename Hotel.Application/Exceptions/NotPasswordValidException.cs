using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Exceptions
{
    public class NotPasswordValidException : AppException
    {
        public NotPasswordValidException(string msg) : base("This Password is not valid")
        {
            Message = "This Password is not valid";
        }

        public override string Message { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Exceptions
{
    public class NotValidException : AppException
    {
        public override string Message { get; }
        public NotValidException(string msg) : base(msg)
        {
            Message = msg;
        }

    }
}

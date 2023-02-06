using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Exceptions
{
    public class NotEmailValidException : AppException
    {
        public NotEmailValidException(string msg) : base($"This email is not valid : {msg}")
        {
            Message = $"This email is not valid : {msg}";
        }

        public override string Message { get; }
    }
}

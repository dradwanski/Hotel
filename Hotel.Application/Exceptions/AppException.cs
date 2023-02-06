using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string Message { get; }
        public AppException(string msg) : base(msg)
        {
            
        }
    }
}

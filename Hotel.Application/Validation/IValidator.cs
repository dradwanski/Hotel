using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Hotel.Application.Validation
{
    public interface IValidator<T>
    {
         bool Validate(T obj);
         Task<bool> IsExistAsync(T obj);
    }
}

using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repository
{
    public interface IMethodOfPaymentRepository
    {
        Task<List<MethodOfPaymentDto>> GetMethodOfPaymentsAsync();
        Task<bool> IsMethodOfPaymentsExistByNameAsync(string methodOfPaymentName);
        Task<bool> IsMethodOfPaymentsExistByIdAsync(int methodOfPaymentId);
        Task CreateMethodOfPaymentAsync(MethodOfPaymentDto methodOfPayment);
        Task DeleteMethodOfPaymentAsync(int methodOfPaymentId);
    }
}

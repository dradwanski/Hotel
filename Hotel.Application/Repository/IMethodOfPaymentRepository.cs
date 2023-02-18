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
        Task<List<MethodOfPaymentDto>> GetMethodOfPayments();
        Task<bool> IsMethodOfPaymentsExistByName(string methodOfPaymentName);
        Task<bool> IsMethodOfPaymentsExistById(int methodOfPaymentId);
        Task CreateMethodOfPayment(MethodOfPaymentDto methodOfPayment);
        Task DeleteMethodOfPayment(int methodOfPaymentId);
    }
}

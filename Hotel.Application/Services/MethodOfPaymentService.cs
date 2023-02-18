using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Repository;

namespace Hotel.Application.Services
{
    public class MethodOfPaymentService : IMethodOfPaymentService
    {
        private readonly IMethodOfPaymentRepository _methodOfPaymentRepository;

        public MethodOfPaymentService(IMethodOfPaymentRepository methodOfPaymentRepository)
        {
            _methodOfPaymentRepository = methodOfPaymentRepository;
        }
        public async Task<List<MethodOfPaymentDto>> GetMethodOfPayments()
        {
            return await _methodOfPaymentRepository.GetMethodOfPayments();
        }

        public async Task CreateMethodOfPayment(MethodOfPaymentDto methodOfPaymentDto)
        {
            if (await _methodOfPaymentRepository.IsMethodOfPaymentsExistByName(methodOfPaymentDto.MethodOfPaymentName))
            {
                throw new NotValidException("Method of payment is exist");
            }

            await _methodOfPaymentRepository.CreateMethodOfPayment(methodOfPaymentDto);
        }

        public async Task DeleteMethodOfPayment(int methodOfPaymentId)
        {

            if (await _methodOfPaymentRepository.IsMethodOfPaymentsExistById(methodOfPaymentId))
            {
                await _methodOfPaymentRepository.DeleteMethodOfPayment(methodOfPaymentId);
                return;
            }

            throw new NotValidException("The specified payment method does not exist");
        }
    }
}

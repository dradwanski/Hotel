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
        public async Task<List<MethodOfPaymentDto>> GetMethodOfPaymentsAsync()
        {
            return await _methodOfPaymentRepository.GetMethodOfPaymentsAsync();
        }

        public async Task CreateMethodOfPaymentAsync(MethodOfPaymentDto methodOfPaymentDto)
        {
            if (await _methodOfPaymentRepository.IsMethodOfPaymentsExistByNameAsync(methodOfPaymentDto.MethodOfPaymentName))
            {
                throw new NotValidException("Method of payment is exist");
            }

            await _methodOfPaymentRepository.CreateMethodOfPaymentAsync(methodOfPaymentDto);
        }

        public async Task DeleteMethodOfPaymentAsync(int methodOfPaymentId)
        {

            if (await _methodOfPaymentRepository.IsMethodOfPaymentsExistByIdAsync(methodOfPaymentId))
            {
                await _methodOfPaymentRepository.DeleteMethodOfPaymentAsync(methodOfPaymentId);
                return;
            }

            throw new NotValidException("The specified payment method does not exist");
        }
    }
}

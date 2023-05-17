using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Application.Dto;
using Hotel.Application.Repository;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Database.Repository
{
    public class MethodOfPaymentRepository : IMethodOfPaymentRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;

        public MethodOfPaymentRepository(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<MethodOfPaymentDto>> GetMethodOfPaymentsAsync()
        {
            var listOfMethodOfPayments = await _dbContext.Set<MethodOfPayment>().ToListAsync(); 
            var listOfPaymentDtos = _mapper.Map<List<MethodOfPaymentDto>>(listOfMethodOfPayments);
            return listOfPaymentDtos;
        }

        public async Task<bool> IsMethodOfPaymentsExistByNameAsync(string methodOfPaymentName)
        {
            return await _dbContext.Set<MethodOfPayment>().AnyAsync(x => x.MethodOfPaymentName == methodOfPaymentName);
        }
        public async Task<bool> IsMethodOfPaymentsExistByIdAsync(int methodOfPaymentId)
        {
            return await _dbContext.Set<MethodOfPayment>().AnyAsync(x => x.MethodOfPaymentId == methodOfPaymentId);
        }

        public async Task CreateMethodOfPaymentAsync(MethodOfPaymentDto methodOfPaymentDto)
        {
            var methodOfPayment = _mapper.Map<MethodOfPayment>(methodOfPaymentDto);
            await _dbContext.AddAsync(methodOfPayment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMethodOfPaymentAsync(int methodOfPaymentId)
        {
            var methodOfPayment = await _dbContext.Set<MethodOfPayment>()
                .FirstOrDefaultAsync(x => x.MethodOfPaymentId == methodOfPaymentId);
             _dbContext.Remove(methodOfPayment);
             await _dbContext.SaveChangesAsync();
        }
    }
}

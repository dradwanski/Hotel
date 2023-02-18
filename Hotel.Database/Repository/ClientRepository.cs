using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Application.Dto;
using Hotel.Application.Repository;
using Hotel.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Database.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _dbContext;

        public ClientRepository(IMapper mapper, HotelDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<bool> IsEmailExist(string email)
        {
            return await _dbContext.Set<Client>().AnyAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<bool> IsPhoneNumberExist(string phoneNumber)
        {
            return await _dbContext.Set<Client>().AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task CreateClient(ClientDto clientDto)
        {
            var client =_mapper.Map<Client>(clientDto);
            await _dbContext.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}

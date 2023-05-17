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
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _dbContext.Set<Client>().AnyAsync(x => x.Email.ToUpper() == email.ToUpper());
        }

        public async Task<bool> IsPhoneNumberExistAsync(string phoneNumber)
        {
            return await _dbContext.Set<Client>().AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<bool> IsClientExistAsync(int id)
        {
            return await _dbContext.Set<Client>().AnyAsync(x => x.Id == id);
        }

        public async Task CreateClientAsync(ClientDto clientDto)
        {
            var client =_mapper.Map<Client>(clientDto);
            await _dbContext.AddAsync(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClientDto>> GetClientsAsync(int pageSize, int pageNumber)
        {
            var listOfClients = await _dbContext.Set<Client>()
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var listOfClientsDto = _mapper.Map<List<ClientDto>>(listOfClients);
            return listOfClientsDto;
        }

        public async Task<List<ClientDto>> GetClientsByNameAndLastNameAsync(string name, string lastName, int pageSize, int pageNumber)
        {
            var listOfClients = await _dbContext.Set<Client>()
                .Where(x => x.Name.StartsWith(name) && x.LastName.StartsWith(lastName))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var listOfClientsDto = _mapper.Map<List<ClientDto>>(listOfClients);
            return listOfClientsDto;
        }

        public async Task<ClientDto> GetClientByMailAsync(string mail)
        {
            var client = await _dbContext.Set<Client>().FirstOrDefaultAsync(x => x.Email == mail);
            var clientDto = _mapper.Map<ClientDto>(client);
            return clientDto;
        }

        public async Task<ClientDto> GetByClientByPhoneNumberAsync(string phoneNumber)
        {
            var client = await _dbContext.Set<Client>().FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
            var clientDto = _mapper.Map<ClientDto>(client);
            return clientDto;
        }

        public async Task<List<ClientDto>> GetClientsByNameAsync(string name, int pageSize, int pageNumber)
        {
            var listOfClients = await _dbContext.Set<Client>()
                .Where(x => x.Name.StartsWith(name))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var listOfClientsDto = _mapper.Map<List<ClientDto>>(listOfClients);
            return listOfClientsDto;
        }

        public async Task<List<ClientDto>> GetClientsByLastNameAsync(string lastName, int pageSize, int pageNumber)
        {
            var listOfClients = await _dbContext.Set<Client>()
                .Where(x => x.LastName.StartsWith(lastName))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            var listOfClientsDto = _mapper.Map<List<ClientDto>>(listOfClients);
            return listOfClientsDto;
        }

        public async Task UpdateAsync(ClientDto dto)
        {
            var mappedDto = _mapper.Map<Client>(dto);
            var client = await _dbContext.Set<Client>().FirstOrDefaultAsync(x => x.Id == mappedDto.Id);
            client.Email = mappedDto.Email;
            client.Name = mappedDto.Name;
            client.LastName = mappedDto.LastName;
            client.City = mappedDto.City;
            client.Country = mappedDto.Country;
            client.Street = mappedDto.Street;
            client.PostalCode = mappedDto.PostalCode;
            client.DateOfBirth = mappedDto.DateOfBirth;
            client.PhoneNumber = mappedDto.PhoneNumber;
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var client = await _dbContext.Set<Client>().FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}

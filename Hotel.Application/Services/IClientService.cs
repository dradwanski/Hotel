using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;

namespace Hotel.Application.Services
{
    public interface IClientService
    {
        Task CreateClientAsync(ClientDto clientDto);
        Task<List<ClientDto>> GetClientsAsync(int pageSize, int number);
        Task<List<ClientDto>> GetClientsByNameAndLastNameAsync(string name, string lastName, int pageSize, int pageNumber);
        Task<ClientDto> GetClientByMailAsync(string mail);
        Task<ClientDto> GetByClientByPhoneNumberAsync(string phoneNumber);
        Task<List<ClientDto>> GetClientsByNameAsync(string name, int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByLastNameAsync(string lastName, int pageSize, int pageNumber);
        Task UpdateAsync(ClientDto dto);
        Task DeleteAsync(int id);
    }
}

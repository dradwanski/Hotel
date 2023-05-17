using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.Repository
{
    public interface IClientRepository
    {
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> IsPhoneNumberExistAsync(string phoneNumber);
        Task<bool> IsClientExistAsync(int id);
        Task CreateClientAsync(ClientDto clientDto);
        Task<List<ClientDto>> GetClientsAsync(int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByNameAndLastNameAsync(string name, string lastName, int pageSize, int pageNumber);
        Task<ClientDto> GetClientByMailAsync(string mail);
        Task<ClientDto> GetByClientByPhoneNumberAsync(string phoneNumber);
        Task<List<ClientDto>> GetClientsByNameAsync(string name, int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByLastNameAsync(string lastName, int pageSize, int pageNumber);
        Task UpdateAsync(ClientDto dto);
        Task DeleteAsync(int id);
    }
}

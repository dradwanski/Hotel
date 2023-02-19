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
        Task CreateClient(ClientDto clientDto);
        Task<List<ClientDto>> GetClients(int pageSize, int number);
        Task<List<ClientDto>> GetClientsByNameAndLastName(string name, string lastName, int pageSize, int pageNumber);
        Task<ClientDto> GetClientByMail(string mail);
        Task<ClientDto> GetByClientByPhoneNumber(string phoneNumber);
        Task<List<ClientDto>> GetClientsByName(string name, int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByLastName(string lastName, int pageSize, int pageNumber);
        Task Update(ClientDto dto);
        Task Delete(int id);
    }
}

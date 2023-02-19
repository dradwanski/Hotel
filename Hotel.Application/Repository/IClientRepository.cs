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
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneNumberExist(string phoneNumber);
        Task<bool> IsClientExist(int id);
        Task CreateClient(ClientDto clientDto);
        Task<List<ClientDto>> GetClients(int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByNameAndLastName(string name, string lastName, int pageSize, int pageNumber);
        Task<ClientDto> GetClientByMail(string mail);
        Task<ClientDto> GetByClientByPhoneNumber(string phoneNumber);
        Task<List<ClientDto>> GetClientsByName(string name, int pageSize, int pageNumber);
        Task<List<ClientDto>> GetClientsByLastName(string lastName, int pageSize, int pageNumber);
        Task Update(ClientDto dto);
        Task Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Helper;
using Hotel.Application.Repository;
using Hotel.Application.Validation;

namespace Hotel.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<ClientDto> _validator;

        public ClientService(IClientRepository clientRepository, IValidator<ClientDto> validator)
        {
            _clientRepository = clientRepository;
            _validator = validator;
        }
        public async Task CreateClientAsync(ClientDto clientDto)
        {
            _validator.Validate(clientDto);

            await _validator.IsExistAsync(clientDto);

            await _clientRepository.CreateClientAsync(clientDto);
        }

        public async Task<List<ClientDto>> GetClientsAsync(int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClientsAsync(pageSize, pageNumber);
        }

        public async Task<List<ClientDto>> GetClientsByNameAndLastNameAsync(string name, string lastName, int pageSize, int pageNumber)
        {

            return await _clientRepository.GetClientsByNameAndLastNameAsync(name, lastName, pageSize, pageNumber);

        }

        public async Task<ClientDto> GetClientByMailAsync(string mail)
        {
            if (!await _clientRepository.IsEmailExistAsync(mail))
            {
                throw new NotValidException("Email does not exist");
            }

            return await _clientRepository.GetClientByMailAsync(mail);
        }

        public async Task<ClientDto> GetByClientByPhoneNumberAsync(string phoneNumber)
        {
            if (!await _clientRepository.IsPhoneNumberExistAsync(phoneNumber))
            {
                throw new NotValidException("Phone number does not exist");
            }

            return await _clientRepository.GetByClientByPhoneNumberAsync(phoneNumber);
        }

        public async Task<List<ClientDto>> GetClientsByNameAsync(string name, int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClientsByNameAsync(name, pageSize, pageNumber);
        }

        public async Task<List<ClientDto>> GetClientsByLastNameAsync(string lastName, int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClientsByLastNameAsync(lastName, pageSize, pageNumber);
        }

        public async Task UpdateAsync(ClientDto dto)
        {
            _validator.Validate(dto);

            await _validator.IsExistAsync(dto);

            await _clientRepository.UpdateAsync(dto);

        }

        public async Task DeleteAsync(int id)
        {
            if (!await _clientRepository.IsClientExistAsync(id))
            {

                throw new NotValidException($"Client with id {id} does not exist");

            }

            await _clientRepository.DeleteAsync(id);
            
        }
    }
}

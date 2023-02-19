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
        public async Task CreateClient(ClientDto clientDto)
        {
            _validator.Validate(clientDto);

            await _validator.IsExistAsync(clientDto);

            await _clientRepository.CreateClient(clientDto);
        }

        public async Task<List<ClientDto>> GetClients(int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClients(pageSize, pageNumber);
        }

        public async Task<List<ClientDto>> GetClientsByNameAndLastName(string name, string lastName, int pageSize, int pageNumber)
        {

            return await _clientRepository.GetClientsByNameAndLastName(name, lastName, pageSize, pageNumber);

        }

        public async Task<ClientDto> GetClientByMail(string mail)
        {
            if (!await _clientRepository.IsEmailExist(mail))
            {
                throw new NotValidException("Email does not exist");
            }

            return await _clientRepository.GetClientByMail(mail);
        }

        public async Task<ClientDto> GetByClientByPhoneNumber(string phoneNumber)
        {
            if (!await _clientRepository.IsPhoneNumberExist(phoneNumber))
            {
                throw new NotValidException("Phone number does not exist");
            }

            return await _clientRepository.GetByClientByPhoneNumber(phoneNumber);
        }

        public async Task<List<ClientDto>> GetClientsByName(string name, int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClientsByName(name, pageSize, pageNumber);
        }

        public async Task<List<ClientDto>> GetClientsByLastName(string lastName, int pageSize, int pageNumber)
        {
            return await _clientRepository.GetClientsByLastName(lastName, pageSize, pageNumber);
        }

        public async Task Update(ClientDto dto)
        {
            _validator.Validate(dto);

            await _validator.IsExistAsync(dto);

            await _clientRepository.Update(dto);

        }

        public async Task Delete(int id)
        {
            if (!await _clientRepository.IsClientExist(id))
            {

                throw new NotValidException($"Client with id {id} does not exist");

            }

            await _clientRepository.Delete(id);
            
        }
    }
}

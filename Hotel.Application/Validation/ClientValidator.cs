using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Application.Dto;
using Hotel.Application.Exceptions;
using Hotel.Application.Helper;
using Hotel.Application.Repository;

namespace Hotel.Application.Validation
{
    public class ClientValidator : IValidator<ClientDto>
    {
        private readonly IClientRepository _clientRepository;

        public ClientValidator(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<bool> IsExistAsync(ClientDto obj)
        {
            if (obj.Id != 0 && !await _clientRepository.IsClientExist(obj.Id))
            {
                throw new NotValidException("Client does not exist");
            }

            if (await _clientRepository.IsPhoneNumberExist(obj.PhoneNumber))
            {
                throw new NotValidException("The phone number is already taken");
            }

            if (await _clientRepository.IsEmailExist(obj.Email))
            {
                throw new NotValidException("Email is already taken");
            }

            return true;

        }

        public bool Validate(ClientDto clientDto)
        {
            if (string.IsNullOrWhiteSpace(clientDto.Name))
            {
                throw new NotValidException("Name is not valid");
            }
            if (string.IsNullOrWhiteSpace(clientDto.LastName))
            {
                throw new NotValidException("Last Name is not valid");
            }
            if (!EmailValidation.IsEmailValid(clientDto.Email))
            {
                throw new NotValidException("Email is not valid");
            }
            if (string.IsNullOrWhiteSpace(clientDto.City))
            {
                throw new NotValidException("City is not valid");
            }
            if (string.IsNullOrWhiteSpace(clientDto.Street))
            {
                throw new NotValidException("Street is not valid");
            }
            if (string.IsNullOrWhiteSpace(clientDto.Country))
            {
                throw new NotValidException("Country is not valid");
            }
            if (!PhoneNumberValidation.IsPhoneNumberValid(clientDto.PhoneNumber))
            {
                throw new NotValidException("Phone number is not valid");
            }
            if (!PostalCodeValidation.IsPostalCodeValid(clientDto.PostalCode))
            {
                throw new NotValidException("Postal code is not valid");
            }
            if (!DateOfBirthValidation.IsAdult(clientDto.DateOfBirth))
            {
                throw new NotValidException("The client is too young");
            }

            return true;
        }
    }
}

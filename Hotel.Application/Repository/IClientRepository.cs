﻿using Hotel.Application.Dto;
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
        Task CreateClient(ClientDto clientDto);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.Application.Dto;

namespace Hotel.API
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<RegisterUser, UserDto>();
        }
    }
}

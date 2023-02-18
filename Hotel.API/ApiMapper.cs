using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.API.ViewModels;
using Hotel.Application.Dto;

namespace Hotel.API
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<RegisterUser, UserDto>();
            CreateMap<LoginUser, UserDto>();
            CreateMap<RoomTypeModel, RoomTypeDto>();
            CreateMap<RoomModel, RoomDto>();
            CreateMap<MethodOfPaymentModel, MethodOfPaymentDto>();
            CreateMap<UserDto, UserModel>()
                .ForMember(x => x.Role, z => z.MapFrom(c => c.Role.RoleName))
                .ForMember(x => x.Name, z => z.MapFrom(c => $"{c.Name} {c.LastName}"));
        }
    }
}

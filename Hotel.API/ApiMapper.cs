using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.API.RequestModels;
using Hotel.API.ViewModels;
using Hotel.Application.Dto;
using Hotel.Application.Validation;

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
            CreateMap<ReservationModel, ReservationDto>()
                .ForMember(x => x.ReservationStart,
                    z => z.MapFrom(c => DateTime.Parse(c.ReservationStart).AddHours(12)))
                .ForMember(x => x.ReservationEnd, z => z.MapFrom(c => DateTime.Parse(c.ReservationEnd).AddHours(10)));
            CreateMap<ClientModel, ClientDto>()
                .ForMember(x=>x.DateOfBirth, z=>z.MapFrom(c => DateOnly.Parse(c.DateOfBirth)))
                .ReverseMap();
            CreateMap<MethodOfPaymentModel, MethodOfPaymentDto>();
            CreateMap<UserDto, UserModel>()
                .ForMember(x => x.Role, z => z.MapFrom(c => c.Role.RoleName))
                .ForMember(x => x.Name, z => z.MapFrom(c => $"{c.Name} {c.LastName}"));
        }
    }
}

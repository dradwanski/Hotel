using Hotel.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hotel.Database.Entities;

namespace Hotel.Database
{
    public class DatabaseMapper : Profile
    {
        public DatabaseMapper()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<MethodOfPayment, MethodOfPaymentDto>().ReverseMap();
            CreateMap<ClientDto, Client>()
                .ForMember(x => x.DateOfBirth, t => t.MapFrom(z => DateTime.Parse(z.DateOfBirth.ToString())));
            CreateMap<Client, ClientDto> ()
                .ForMember(x => x.DateOfBirth, t => t.MapFrom(z => DateOnly.Parse(z.DateOfBirth.ToString("dd-MM-yyyy"))));
            CreateMap<Room, RoomDto>()
                .ForMember(x => x.RoomTypeId, s => s.MapFrom(z => z.Type.RoomTypeId))
                .ReverseMap();
            CreateMap<RoomType, RoomTypeDto>()
                .ForMember(x => x.Id, t => t.MapFrom(z => z.RoomTypeId))
                .ReverseMap();
        }
    }
}

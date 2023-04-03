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
                .ForMember(x => x.Reservations, z => z.MapFrom(d => d.ListOfReservation))
                .ReverseMap();
            CreateMap<RoomType, RoomTypeDto>()
                .ForMember(x => x.Id, t => t.MapFrom(z => z.RoomTypeId))
                .ReverseMap();
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(z => z.ReservationStart,
                    x => x.MapFrom(y => DateTime.Parse(y.ReservationStart.ToString("dd-MM-yyyy HH:mm"))))
                .ForMember(z => z.ReservationEnd,
                    x => x.MapFrom(y => DateTime.Parse(y.ReservationEnd.ToString("dd-MM-yyyy HH:mm"))))
                .ForMember(z => z.ModificationDate,
                    x => x.MapFrom(y => DateTime.Parse(y.ModificationDate.ToString("dd-MM-yyyy HH:mm"))))
                .ForMember(z => z.DateOfPayment, x =>
                    x.MapFrom(y =>
                        y.DateOfPayment.HasValue
                            ? DateTime.Parse(y.DateOfPayment.Value.ToString("dd-MM-yyyy HH:mm"))
                            : y.DateOfPayment))
                .ForMember(z => z.ClientId, t => t.MapFrom(s => s.Client.Id))
                .ForMember(l => l.CreatedUserId, t => t.MapFrom(s => s.CreatedUser.UserId))
                .ForMember(z => z.LastEditedUserId, t => t.MapFrom(s => s.LastEditedUser.UserId))
                .ForMember(z => z.MethodOfPaymentId, t => t.MapFrom(s => s.MethodOfPayment.MethodOfPaymentId))
                .ForMember(z => z.RoomId, t => t.MapFrom(s => s.Room.RoomId))
                .ForMember(z => z.ReservationStatusId, t => t.MapFrom(s => s.ReservationStatus.StatusId));
        }
    }
}

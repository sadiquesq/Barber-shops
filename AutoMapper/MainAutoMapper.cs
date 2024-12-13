using AutoMapper;
using Barber_shops.DTOs.User;
using Barber_shops.Models;

namespace Barber_shops.AutoMapper
{
    public class MainAutoMapper : Profile
    {


        public MainAutoMapper()
        {
            CreateMap<UserDto,User>().ReverseMap();
        }

    }
}

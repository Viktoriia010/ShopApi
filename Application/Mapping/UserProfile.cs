using AutoMapper;
using Shop.Application.DTOs.UserDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateDTO, User>();
        CreateMap<User, UserReadDTO>();
        CreateMap<User, UserLoginDTO>();
    }
}
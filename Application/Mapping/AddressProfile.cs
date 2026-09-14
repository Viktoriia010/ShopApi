using AutoMapper;
using Shop.Application.DTOs.AddressDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.DTOs.ProductImageDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Mapping;

public class AddressProfile : Profile
{

    public AddressProfile()
    {
        CreateMap<AddressCreateDTO, Address>();

        CreateMap<Address, AddressReadDTO>();
    }
}
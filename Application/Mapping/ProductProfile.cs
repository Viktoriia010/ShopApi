using AutoMapper;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.DTOs.ProductImageDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Mapping;

public class ProductProfile:Profile
{

    public ProductProfile()
    {
        CreateMap<ProductCreateDTO, Product>();
        //CategoryCreateDto -> Category
        CreateMap<Product, ProductReadDTO>();
        CreateMap<ProductImage, ProductImageDTO>();
        //CreateMap<ProductCreateRequest, Product>
    }
}

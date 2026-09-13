using AutoMapper;
using MediatR;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Repository;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.Product;

public class CreateProductHandler(IMapper _mapper, IProductRepository _repository) : IRequestHandler<CreateProductCommand, int?>
{
    public async Task<int?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var product = _mapper.Map<ShopDomain.Models.Product>(request.dto);

        product.Images = request.dto.ImagesUrl?
            .Select((x, index) => new ProductImage
            {
                Url = x,
                IsPrimary = index == 0
            })
            .ToList() ?? new List<ProductImage>();
        return await _repository.AddProductAsync(product, cancellationToken);

    }
}


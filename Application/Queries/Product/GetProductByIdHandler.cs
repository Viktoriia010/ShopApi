using AutoMapper;
using MediatR;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.Product;

public class GetProductByIdHandler(IMapper _mapper, IProductRepository _repository) : IRequestHandler<GetProductByIdQuery, ProductReadDTO?>
{
    public async Task<ProductReadDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetProductByIdAsync(request.id, cancellationToken);
        if (entity == null)
            return null;
        return _mapper.Map<ProductReadDTO?>(entity);

    }
}
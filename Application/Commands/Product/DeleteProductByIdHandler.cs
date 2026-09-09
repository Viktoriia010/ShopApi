using AutoMapper;
using MediatR;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Queries.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.Product;

public class DeleteProductByIdHandler(IMapper _mapper, IProductRepository _repository) : IRequestHandler<DeleteProductByIdCommand, ProductReadDTO?>
{
    public async Task<ProductReadDTO?> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.DeleteProductByIdAsync(request.id, cancellationToken);
        if (entity == null)
            return null;
        return _mapper.Map<ProductReadDTO?>(entity);

    }
}
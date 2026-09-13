using AutoMapper;
using MediatR;
using Shop.Application.Commands.Product;
using Shop.Application.Interfaces.Repository;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.Category;

public class CreateCategoryHandler(IMapper _mapper, ICategoryRepository _repository) : IRequestHandler<CreateCategoryCommand, int?>
{
    public async Task<int?> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {

        var category = _mapper.Map<ShopDomain.Models.Category>(request.dto);
        return await _repository.AddCategoryAsync(category, cancellationToken);

    }
}

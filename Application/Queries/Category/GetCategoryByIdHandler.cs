using AutoMapper;
using MediatR;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.Category;

public class GetCategoryByIdHandler(IMapper _mapper, ICategoryRepository _repository, ICachingService _cacheService) : IRequestHandler<GetCategoryByIdQuery, CategoryReadDTO?>
{
    public async Task<CategoryReadDTO?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"Category_{request.id}";
        var cache = await _cacheService.GetAsync<CategoryReadDTO>(cacheKey);
        if (cache == null)
        {
            var res = await _repository.GetCategoryByIdAsync(request.id, cancellationToken);
            cache = _mapper.Map<CategoryReadDTO>(res);
            await _cacheService.SetAsync(cacheKey, cache, null);
        }
        return cache;

    }
}
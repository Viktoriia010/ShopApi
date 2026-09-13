using AutoMapper;
using MediatR;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Configurations;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Queries.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.Category;

public class GetCategoryBySlugHandler(IMapper _mapper, ICategoryRepository _repository, IFilePathProvider _filePathProvider) : IRequestHandler<GetCategoryBySlugQuery, CategoryReadDTO?>
{
    public async Task<CategoryReadDTO?> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetCategoryBySlugAsync(
            request.slug,
            cancellationToken
        );

        if (category == null)
            return null;

        var dto = _mapper.Map<CategoryReadDTO>(category);

        FixedImageForCategory(dto);

        return dto;

    }

    private void FixedImageForCategory(CategoryReadDTO dto)
    {
        dto.Url = $"{_filePathProvider.Categories}/{dto.Url}";
    }
}
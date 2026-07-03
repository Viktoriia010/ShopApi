using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class CategoryService(ICategoryRepository _repository) : ICategoryService
{
    //додати автомапер
    public async Task<int?> CreateCategoryAsync(CategoryCreateDTO dto)
    {
        return await _repository.AddCategoryAsync(new Category()
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Url = dto.Url,
            ParentId = dto.ParentId,
        });
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        return await _repository.DeleteCategoryAsync(id);
    }

    public async Task<List<CategoryReadDTO>?> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllCategoriesAsync();
        List<CategoryReadDTO> dtos = new List<CategoryReadDTO>();
        if (categories != null)
        {
            foreach (var item in categories)
            {
                dtos.Add(new CategoryReadDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Slug = item.Slug,
                    Url = item.Url,
                    ParentId = item.ParentId,
                });
            }
        }

        return dtos;
    }

    public async Task<CategoryReadDTO?> GetCategoryByIdAsync(int id)
    {
        var res = await _repository.GetCategoryByIdAsync(id);
        if (res != null)
        {
            return new CategoryReadDTO()
            {
                Id = res.Id,
                Name = res.Name,
                Slug = res.Slug,
                Url = res.Url,
                ParentId = res.ParentId,

            };
        }
        return null;
    }

    public async Task<CategoryUpdateDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO updated)
    {
        var category = await _repository.GetCategoryByIdAsync(id);

        if (category == null)
            return null;

        category.Name = updated.Name;
        category.Url = updated.Url;
        category.ParentId = updated.ParentId;
        category.Slug = updated.Slug;
        category.IsActive = updated.IsActive;

        var result = await _repository.UpdateCategoryAsync(category);
        if (result == null)
            return null;
        return new CategoryUpdateDTO
        {
            Name = result.Name,
            Slug = result.Slug,
            Url = result.Url,
            IsActive = result.IsActive,
            ParentId = result.ParentId
        };
    }
}

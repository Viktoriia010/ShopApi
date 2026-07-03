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
    public async Task<List<CategoryReadDTO>?> GetAllCategoriesAsync()
    {
        var categories =  await _repository.GetAllCategoriesAsync();
        List<CategoryReadDTO> dtos = new List<CategoryReadDTO>();
        if(categories != null)
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
        var res =   await _repository.GetCategoryByIdAsync(id);
        if (res != null)
        {
            return new CategoryReadDTO()
            {
                Id= res.Id,
                Name = res.Name,
                Slug = res.Slug,
                Url= res.Url,
                ParentId = res.ParentId,

            };
        }
        return null;
    }
}

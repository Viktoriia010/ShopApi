using Shop.Application.DTOs.CategoryDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<int?> CreateCategoryAsync(CategoryCreateDTO dto);
    Task<List<CategoryReadDTO>?> GetAllCategoriesAsync();
    Task<CategoryReadDTO?> GetCategoryByIdAsync(int id);
    Task<bool> DeleteCategoryAsync(int id);
    Task<CategoryUpdateDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO updated);
    Task<List<CategoryReadDTO>> GetParentsCategoryByIdAsync(CategoryReadDTO dto);
    Task<List<CategoryReadDTO>> GetChildrensCategoryByIdAsync(int id);
    Task<List<CategoryTreeDTO>> GetTreeCategoryByIdAsync();

}

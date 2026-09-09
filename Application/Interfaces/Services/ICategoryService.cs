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
    Task<int?> CreateCategoryAsync(CategoryCreateDTO dto, CancellationToken cancellationToken);
    Task<List<CategoryReadDTO>?> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<CategoryReadDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
    Task<CategoryUpdateDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO updated, CancellationToken cancellationToken);
    Task<List<CategoryReadDTO>> GetParentsCategoryByIdAsync(CategoryReadDTO dto, CancellationToken cancellationToken);
    Task<List<CategoryReadDTO>> GetChildrensCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<CategoryTreeDTO>> GetTreeCategoryByIdAsync(CancellationToken cancellationToken);

}

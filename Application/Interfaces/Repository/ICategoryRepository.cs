using Shop.Application.DTOs.CategoryDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface ICategoryRepository
{
    Task<int?> AddCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<List<Category>?> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken);
    Task<Category?> UpdateCategoryAsync(Category updated, CancellationToken cancellationToken);

}

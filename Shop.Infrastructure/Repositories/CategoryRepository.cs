using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repository;
using Shop.Infrastructure.Data;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class CategoryRepository(ShopDbContext _context) : ICategoryRepository
{
    public async Task<int?> AddCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return category.Id;
    }

    public async Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(id, cancellationToken);

        if (category == null)
            return false;
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<Category>?> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        return await _context.Categories.Include(x => x.Products).ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Categories
        .Include(x => x.Products)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Category?> UpdateCategoryAsync(Category updated, CancellationToken cancellationToken)
    {
        _context.Categories.Update(updated);
        await _context.SaveChangesAsync(cancellationToken);
        return updated;

    }
}

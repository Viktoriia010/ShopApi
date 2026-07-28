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

public class ProductRepository(ShopDbContext _context) : IProductRepository
{
    public async Task<int> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task<List<Product>?> GetAllProductsAsync()
    {
        return await _context.Products.Include(x => x.Images).ToListAsync();
    }

    public async Task<Product>? GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(x => x.Images)
        .FirstOrDefaultAsync(x => x.Id == id); 
    }

    public async Task<List<Product>?> GetProductsByCategoryAsync(int categoryId)
    {
        return await _context.Products
        .Where(p => p.CategoryId == categoryId)
        .Include(p => p.Images)
        .ToListAsync();
    }
}

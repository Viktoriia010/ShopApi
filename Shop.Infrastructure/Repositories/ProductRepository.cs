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
    public async Task<int> AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return product.Id;
    }

    public async Task<List<Product>?> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.Include(x => x.Images).ToListAsync(cancellationToken);
    }

    public async Task<Product>? GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products.Include(x => x.Images)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken); 
    }

    public async Task<List<Product>?> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.Products
        .Where(p => p.CategoryId == categoryId)
        .Include(p => p.Images)
        .ToListAsync(cancellationToken);
    }

    public async Task<Product?> DeleteProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        var product = await _context.Products.Include(x => x.Images)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (product == null)
        {
            return null;
        }
        product.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }


}

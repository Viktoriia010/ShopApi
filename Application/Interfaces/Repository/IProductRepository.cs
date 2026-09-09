using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IProductRepository
{
    Task<List<Product>?> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<int> AddProductAsync(Product product, CancellationToken cancellationToken);
    Task<Product>? GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Product>?> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    Task<Product?> DeleteProductByIdAsync(int id, CancellationToken cancellationToken);
}

using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IProductRepository
{
    Task<List<Product>?> GetAllProductsAsync();
    Task<int> AddProductAsync(Product product);
    Task<Product>? GetProductByIdAsync(int id);
}

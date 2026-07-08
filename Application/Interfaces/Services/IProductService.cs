using Shop.Application.DTOs.ProductDTOs;
using ShopDomain.Models;

namespace Shop.Api.Interfaces;

public interface IProductService
{
    Task<List<ProductReadDTO>> GetAllProductsAsync();
    Task<int> CreateProductAsync(ProductCreateDTO product);
    Task<ProductReadDTO?> GetProductByIdAsync(int id);
    //Product UpdateProductById(Product product, int id);
    //bool DeleteProductById(int id);
    //List<Product> SearchProductByName(string name);
}

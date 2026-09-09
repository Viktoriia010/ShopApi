using Shop.Application.DTOs.ProductDTOs;
using ShopDomain.Models;

namespace Shop.Api.Interfaces;

public interface IProductService
{
    Task<List<ProductReadDTO>> GetAllProductsAsync(CancellationToken cancellationToken);
    Task<int> CreateProductAsync(ProductCreateDTO product, CancellationToken cancellationToken);
    Task<ProductReadDTO?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ProductReadDTO>?> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken);
    //Product UpdateProductById(Product product, int id);
    //bool DeleteProductById(int id);
    //List<Product> SearchProductByName(string name);
}

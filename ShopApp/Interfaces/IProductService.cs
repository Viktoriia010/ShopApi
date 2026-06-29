using ShopDomain.Models;

namespace ShopApp.Interfaces;

public interface IProductService
{
    List<Product> GetAllProducts();
    void AddProduct(Product product);
    Product GetProductById(int id);
    Product UpdateProductById(Product product, int id);
    bool DeleteProductById(int id);
    List<Product> SearchProductByName(string name);
}

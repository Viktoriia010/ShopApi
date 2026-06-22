using ShopApp.Interfaces;
using ShopDomain.Models;

namespace ShopApp.Services;

public class ProductService : IProductService
{
    private List<Product> _products = new();
    public ProductService()
    {
        _products.Add(new Product()
        {
            Title = "milk",
            Price = 40.9f
        });
        _products.Add(new Product()
        {
            Title = "bread",
            Price = 46.5f
        });
    }
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> GetAllProducts()
    {
 
        return _products;
    }
    
}

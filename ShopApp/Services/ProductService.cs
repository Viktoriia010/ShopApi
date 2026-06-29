using ShopApp.Interfaces;
using ShopDomain.Models;

namespace ShopApp.Services;

public class ProductService : IProductService
{
    private List<Product> _products = new();
    public ProductService()
    {
        _products.Add(new Product("milk", 40.9f));
        _products.Add(new Product("bread", 46.5f));
        _products.Add(new Product("soup", 56.5f));
    }
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }
    public Product GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product UpdateProductById(Product updatedProduct, int id)
    {
        var product = GetProductById(id);
        if(product == null)
        {
            return null;
        }
        product.Title = updatedProduct.Title;
        product.Price = updatedProduct.Price;
        return product;
    }
    public bool DeleteProductById(int id)
    {
        var product = GetProductById(id);
        if (product == null)
        {
            return false;
        }
        _products.Remove(product);
        return true;
    }
    public List<Product> SearchProductByName(string name)
    {
        var products = _products.Where(p => p.Title.ToLower() == name.ToLower()).ToList();
        if (products == null)
        {
            return [];
        }
        return products;
    }
}

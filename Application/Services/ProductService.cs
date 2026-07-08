using Shop.Api.Interfaces;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Repository;
using ShopDomain.Models;

namespace Shop.Api.Services;

public class ProductService(IProductRepository _repository) : IProductService
{
    public async Task<int> CreateProductAsync(ProductCreateDTO dto)
    {
        return await _repository.AddProductAsync(new Product()
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQty = dto.StockQty,
            CategoryId = dto.CategoryId,
        });
    }

    public async Task<List<ProductReadDTO>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllProductsAsync();
        List<ProductReadDTO> dtos = new List<ProductReadDTO>();
        if (products != null)
        {
            foreach (var item in products)
            {
                dtos.Add(new ProductReadDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    StockQty = item.StockQty,
                    CategoryId = item.CategoryId,
                    IsActive = item.IsActive,
                });
            }
        }

        return dtos;
    }

    public async Task<ProductReadDTO?> GetProductByIdAsync(int id)
    {
        var res = await _repository.GetProductByIdAsync(id);
        if (res != null)
        {
            return new ProductReadDTO()
            {
                Id = res.Id,
                Name = res.Name,
                Description = res.Description,
                Price = res.Price,
                StockQty = res.StockQty,
                CategoryId = res.CategoryId,
                IsActive = res.IsActive
            };
        }
        return null;
    }
    //public ProductService()
    //{
    //    _products.Add(new Product("milk", 40.9f));
    //    _products.Add(new Product("bread", 46.5f));
    //    _products.Add(new Product("soup", 56.5f));
    //}
    //public void AddProduct(Product product)
    //{
    //    _products.Add(product);
    //}

    //public List<Product> GetAllProducts()
    //{
    //    return _products;
    //}
    //public Product GetProductById(int id)
    //{
    //    return _products.FirstOrDefault(p => p.Id == id);
    //}

    //public Product UpdateProductById(Product updatedProduct, int id)
    //{
    //    var product = GetProductById(id);
    //    if (product == null)
    //    {
    //        return null;
    //    }
    //    product.Title = updatedProduct.Title;
    //    product.Price = updatedProduct.Price;
    //    return product;
    //}
    //public bool DeleteProductById(int id)
    //{
    //    var product = GetProductById(id);
    //    if (product == null)
    //    {
    //        return false;
    //    }
    //    _products.Remove(product);
    //    return true;
    //}
    //public List<Product> SearchProductByName(string name)
    //{
    //    var products = _products.Where(p => p.Title.ToLower() == name.ToLower()).ToList();
    //    if (products == null)
    //    {
    //        return [];
    //    }
    //    return products;
    //}
}

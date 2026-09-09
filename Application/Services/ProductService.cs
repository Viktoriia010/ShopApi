using AutoMapper;
using Shop.Api.Interfaces;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;

namespace Shop.Api.Services;

public class ProductService(IProductRepository _repository, IMapper _mapper, ICachingService _cacheService) : IProductService
{
    public async Task<int> CreateProductAsync(ProductCreateDTO dto, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(dto);

        product.Images = dto.ImagesUrl?
            .Select(x => new ProductImage
            {
                Url = x,
                IsPrimary = false
            })
            .ToList() ?? new List<ProductImage>();
        return await _repository.AddProductAsync(product, cancellationToken);
    }

    public async Task<List<ProductReadDTO>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var cache = await _cacheService.GetAsync<List<ProductReadDTO>>("Products");
        if (cache == null)
        {
            var products = await _repository.GetAllProductsAsync(cancellationToken);
            cache = _mapper.Map<List<ProductReadDTO>>(products);
            await _cacheService.SetAsync("Products", cache, null);

        }
        return cache;

        //var products = await _repository.GetAllProductsAsync();

        //if (products == null)
        //{
        //    return new List<ProductReadDTO>();
        //}

        //return _mapper.Map<List<ProductReadDTO>>(products);
    }


    public async Task<ProductReadDTO?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"Product_{id}";
        var cache = await _cacheService.GetAsync<ProductReadDTO>(cacheKey);
        if (cache == null)
        {
            var res = await _repository.GetProductByIdAsync(id, cancellationToken);
            cache = _mapper.Map<ProductReadDTO>(res);
            await _cacheService.SetAsync(cacheKey, cache, null);
        }
        return cache;
        //var res = await _repository.GetProductByIdAsync(id);
        //if (res != null)
        //{
        //    return _mapper.Map<ProductReadDTO>(res);
        //}
        //return null;
    }

    public async Task<List<ProductReadDTO>?> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken)
    {
        var res = await _repository.GetProductsByCategoryAsync(categoryId, cancellationToken);
        if (res != null)
        {
            return _mapper.Map<List<ProductReadDTO>>(res);
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

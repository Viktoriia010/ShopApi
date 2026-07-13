
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Api.Interfaces;
using Shop.Api.Requests.Categories;
using Shop.Api.Requests.Products;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.DTOs.ProductImageDTOs;
using ShopDomain.Models;

namespace Shop.Api.Controllers;
//
[ApiController]
[Route("api/[controller]")]
[LogActionFilter]
public class ProductsController(IProductService _productService, IImageService _imageService, IConfiguration _configuration, IMapper _mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductCreateRequest dto)
    {

        var imageUrls = new List<string>(); 
        if (dto.Images != null && dto.Images.Any())
        {
            foreach (var image in dto.Images)
            {
                var fileName = await _imageService.SaveFileAsync(
                    image,
                    _configuration["DirnameForFiles:Products"]!
                );

                imageUrls.Add(fileName);
            }
        }
        var createDto = new ProductCreateDTO
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQty = dto.StockQty,
                CategoryId = dto.CategoryId,
                ImagesUrl = imageUrls

            };
        var id = await _productService.CreateProductAsync(createDto);
        return CreatedAtAction(
                    nameof(GetProductById), // назва методу
                    new { id },              // параметри маршруту
                    new { id });             // тіло відповіді
    
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    //private readonly IProductService _productService;

    //public ProductController(IProductService productService)
    //{
    //    _productService = productService;
    //}
    //[HttpGet]
    //public List<Product> GetProducts()
    //{
    //    return _productService.GetAllProducts();
    //}
    //[HttpGet("{id}")]
    //public IActionResult GetProductById([FromRoute] int id)
    //{
    //    var product = new Product()
    //    {
    //        Name = $"Test Product {id}",
    //        Price = 100
    //    };
    //    return Ok(product);
    //}

    //[HttpGet("{id:int}")]
    //public IActionResult GetProductById([FromRoute] int id)
    //{
    //    var product = _productService.GetProductById(id);
    //    if (product == null) {
    //        return NotFound("Product not found");
    //    }
    //    return Ok(product);
    //}

    //[HttpPost]
    //public IActionResult AddNewProduct([FromBody] Product product)
    //{
    //    if(product == null)
    //    {
    //        return BadRequest();
    //    }
    //    _productService.AddProduct(product);
    //    return CreatedAtAction(nameof(GetProductById),  new{ id = product.Id }, product);
    //}
    //[HttpPut("{id:int}")]
    //public IActionResult UpdateProductById([FromRoute] int id, [FromBody] Product updatedProduct)
    //{
    //    if (updatedProduct == null)
    //    {
    //        return BadRequest();
    //    }
    //    var product = _productService.UpdateProductById(updatedProduct, id);
    //    if (product == null) {
    //        return NotFound();
    //            }
    //    return Ok(product);
    //}
    //[HttpDelete("{id:int}")]
    //public IActionResult DeleteProductById([FromRoute] int id)
    //{
    //    bool res =_productService.DeleteProductById(id);
    //    if (res)
    //    {
    //        return NoContent();
    //    }
    //    return NotFound();
        
    //}
    //[HttpGet("search")]
    //public IActionResult SearchProductByName([FromQuery] string title)
    //{
    //    var res =_productService.SearchProductByName(title);
    //    if (res == null)
    //    {
    //        return NotFound();
    //    }
    //    if (title == null)
    //    {
    //        return BadRequest();
    //    }
    //    return Ok(res);
        
    //}
}

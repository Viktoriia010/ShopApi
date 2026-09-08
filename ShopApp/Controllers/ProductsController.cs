
using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Filters;
using Shop.Api.Interfaces;
using Shop.Api.Requests.Categories;
using Shop.Api.Requests.Products;
using Shop.Application.Commands.Product;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;
using Shop.Application.DTOs.ProductFeedbackDTOs;
using Shop.Application.DTOs.ProductImageDTOs;
using Shop.Application.Interfaces.Services;
using Shop.Application.Queries.Product;
using Shop.Infrastructure.Services;
using ShopDomain.Models;

namespace Shop.Api.Controllers;
//
[ApiController]
[Route("api/v1/[controller]")]
[LogActionFilter]
public class ProductsController(IProductService _productService, IImageService _imageService, IConfiguration _configuration, IMongoDbService _mongoDbService, IMediator _mediator) : ControllerBase
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
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        //var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpPost("{productId}/feedback")]
    public async Task<IActionResult> AddProductFeedback([FromBody] ProductFeedbackDTO feedback,int productId)
    {
        feedback.ProductId = productId;

        await _mongoDbService.AddFeedbackAsync(feedback);

        return Ok(feedback);
    }

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
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] int id)
    {
        var res = await _mediator.Send(new DeleteProductByIdCommand(id));
        //bool res = _productService.DeleteProductById(id);
        if (res == null)
        {
            return NotFound();
        }
        return Ok(res);

    }
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

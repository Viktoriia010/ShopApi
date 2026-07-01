
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Filters;
using Shop.Api.Interfaces;
using ShopDomain.Models;

namespace Shop.Api.Controllers;
//
[ApiController]
[Route("api/[controller]")]
[LogActionFilter]
public class ProductsController(IProductService _productService) : ControllerBase
{
    //private readonly IProductService _productService;
    
    //public ProductController(IProductService productService)
    //{
    //    _productService = productService;
    //}
    [HttpGet]
    public List<Product> GetProducts()
    {
        return _productService.GetAllProducts();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProductById([FromRoute] int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null) {
            return NotFound("Product not found");
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult AddNewProduct([FromBody] Product product)
    {
        if(product == null)
        {
            return BadRequest();
        }
        _productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProductById),  new{ id = product.Id }, product);
    }
    [HttpPut("{id:int}")]
    public IActionResult UpdateProductById([FromRoute] int id, [FromBody] Product updatedProduct)
    {
        if (updatedProduct == null)
        {
            return BadRequest();
        }
        var product = _productService.UpdateProductById(updatedProduct, id);
        if (product == null) {
            return NotFound();
                }
        return Ok(product);
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteProductById([FromRoute] int id)
    {
        bool res =_productService.DeleteProductById(id);
        if (res)
        {
            return NoContent();
        }
        return NotFound();
        
    }
    [HttpGet("search")]
    public IActionResult SearchProductByName([FromQuery] string title)
    {
        var res =_productService.SearchProductByName(title);
        if (res == null)
        {
            return NotFound();
        }
        if (title == null)
        {
            return BadRequest();
        }
        return Ok(res);
        
    }
}

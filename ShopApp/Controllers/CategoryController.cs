using Microsoft.AspNetCore.Mvc;
using Shop.Api.Interfaces;
using Shop.Api.Services;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class CategoryController(ICategoryService _categoryService) : ControllerBase
{

    //[HttpGet]
    //public List<Category> GetCategories()
    //{
    //    return _categoryService.GetAllCategories();
    //}


    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDTO dto)
    {
        int? id = await _categoryService.CreateCategoryAsync(dto);
        return Ok($"Category created {id}");
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if(category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }


}

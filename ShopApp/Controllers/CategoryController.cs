using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Interfaces;
using Shop.Api.Requests.Categories;
using Shop.Api.Services;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

public class CategoryController(ICategoryService _categoryService, IImageService _imageService, IConfiguration _configuration) : ControllerBase
{

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CategoryCreateRequest dto)
    {
        if (dto.Image != null)
        {
            dto.Url = (await _imageService.SaveFileAsync(dto.Image, _configuration["DirnameForFiles:Categories"])) ?? string.Empty;
        }
        var createDto = new CategoryCreateDTO
        {
            Name = dto.Name,
            Url = dto.Url,
            Slug = dto.Slug,
            ParentId = dto.ParentId,
        };
        var id = await _categoryService.CreateCategoryAsync(createDto);

        if (id == null)
        {
            return BadRequest("Category was not created.");
        }


        var category = await _categoryService.GetCategoryByIdAsync(id.Value);
        //return Ok($"Category created {id}");
        return CreatedAtAction(
                    nameof(GetCategoryById), // назва методу
                    new { id },              // параметри маршруту
                    category);             // тіло відповіді
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


    [HttpGet("{id:int}/parents")]
    public async Task<IActionResult> GetParentsCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        var res = await _categoryService.GetParentsCategoryByIdAsync(category);

        return Ok(res);
    }

    [HttpGet("{id:int}/childrens")]
    public async Task<IActionResult> GetChildrensCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        var res = await _categoryService.GetChildrensCategoryByIdAsync(category.Id);

        return Ok(res);
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetTreeCategoryById()
    {
        var category = await _categoryService.GetTreeCategoryByIdAsync();

        return Ok(category);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var res = await _categoryService.DeleteCategoryAsync(id);
        if (!res)
        {
            return NotFound();
        }
        return NoContent(); 
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryUpdateDTO updateDTO)
    {
        var res = await _categoryService.UpdateCategoryAsync(id, updateDTO);
        if (res == null)
        {
            return NotFound();
        }
        return Ok(res); 
    }


}

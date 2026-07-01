using Microsoft.AspNetCore.Mvc;
using Shop.Api.Interfaces;
using Shop.Api.Services;
using ShopDomain.Models;
namespace Shop.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

//public class CategoryCreateDto
//{

//}
public class CategoryController(ICategoryService _categoryService) : ControllerBase
{

    [HttpGet]
    public List<Category> GetCategories()
    {
        return _categoryService.GetAllCategories();
    }

    //[HttpPost]
    //public IActionResult CreateCategory(CategoryCreateDto dto)
    //{

    //}
    

}

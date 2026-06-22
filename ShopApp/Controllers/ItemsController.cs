using Microsoft.AspNetCore.Mvc;
using ShopDomain.Models;

namespace ShopApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private List<Item> _items = new()
    {
        new Item
        {
            Id = 1,
            Title = "milk"
        },
        new Item
        {
            Id = 2,
            Title = "bread"
        }
    };
    [HttpGet]

    public IActionResult GetItems()
    {

        return Ok(_items);
    }
    [HttpGet("{id}")]
    public IActionResult GetItemById([FromRoute] int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}

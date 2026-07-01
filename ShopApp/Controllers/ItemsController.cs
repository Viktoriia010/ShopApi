using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;
using ShopDomain.Models;

namespace Shop.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private static List<Item> _items = new()
    {
        new Item("milk"),
        new Item("bread"),
        new Item("soup"),
        new Item("milkslice"),
        new Item("slicemilk")
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
    [HttpGet("search")]
    public IActionResult GetItemByName([FromQuery] string name)
    {
        var items = _items.Where(x => x.Title.ToLower().Contains(name.ToLower()));

        return Ok(items);
    }
    [HttpPost]
    public IActionResult AddItem(Item item)
    {
        if(item.Title == null)
        {
            return BadRequest();
        }

        _items.Add(item);

        return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateItem([FromRoute] int id, [FromBody] Item updatedItem)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);


        if (string.IsNullOrWhiteSpace(updatedItem.Title))
        {
            return BadRequest();
        }
        if (item == null)
        {
            return NotFound();
        }
        item.Title = updatedItem.Title;


        return Ok(item);
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteItem([FromRoute] int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            return NotFound();
        }
       
        _items.Remove(item);


        return NoContent();
    }
}

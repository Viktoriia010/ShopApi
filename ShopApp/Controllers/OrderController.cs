using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Requests.Categories;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.OrderDTOs;
using Shop.Application.Interfaces.Services;
using System.Collections;
using System.Security.Claims;

namespace Shop.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController(IOrderService _orderService, IQueueService _queue) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO dto, CancellationToken cancellationToken)
    {
        if (dto.Items == null || dto.Items.Count == 0)
        {
            return BadRequest("Замовлення повинно містити хоча б один продукт");
        }
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return BadRequest("Email не найдено");
        }
        var resp = await _orderService.CreateOrderAsync(email, dto, cancellationToken);
  
        await _queue.PublishAsync("Orders",  resp, cancellationToken);

        return Ok(new
        {
            Order = resp,
            //Message = "Order added to queue"
        });
    }

}

//Створити контроллер для замовлень OrderController
//Додати метод створення замовлення, який приймає інформацію про замовлення,
//масив продуктів і всю необхідну інформацію
//Записує цю інформацію у rabbitmq чергу Orders
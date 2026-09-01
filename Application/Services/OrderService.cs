using Shop.Application.DTOs.OrderDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class OrderService(IAuthRepository _authRepository, IOrderRepository _orderRepository, IProductRepository _productRepository) : IOrderService
{
    public async Task<OrderResponseDTO> CreateOrderAsync(string email, OrderCreateDTO dto)
    {
        var user = await _authRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("Користувача не найдено");
        }
        if (dto.Items == null || dto.Items.Count == 0)
        {
            throw new Exception("Замовлення повинно містити хоча б один продукт");
        }
        var order = new Orders
        {
            UserId = user.Id,
            Status = OrderStatus.Pending,
            Paid = dto.Paid
        };
        List<OrderDetails> orders = new List<OrderDetails>();
        foreach (var item in dto.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new Exception("Продукт не найдено");
            }
            var orderDetails = new OrderDetails
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Price = product.Price,
                Count = item.Count
            };
            orders.Add(orderDetails);
        }
        await _orderRepository.AddOrderAsync(order, orders);
        var response = new OrderResponseDTO
        {
            Id = order.Id,
            Email = email,
            UserId = user.Id,
            Paid = order.Paid,
            Status = order.Status,
            Items = orders.Select(x => new OrderItemResponseDTO
            {
                ProductId = x.ProductId,
                Price = x.Price,
                Count = x.Count
            }).ToList()
        };
        return response;
    }
}

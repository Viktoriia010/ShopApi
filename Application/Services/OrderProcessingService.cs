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

public class OrderProcessingService(IProductRepository _productRepository, IOrderRepository _orderRepository, IEmailService _emailService) : IOrderProcessingService
{
    public async Task ProcessAsync(OrderResponseDTO message, CancellationToken cancellationToken)
    {
        var products = new List<Product>();

        foreach (var item in message.Items)
        {
            var product = await _productRepository.GetProductByIdAsync(item.ProductId, cancellationToken);

            if (product == null)
            {
                throw new Exception("Товару не існує");
            }

            if (product.StockQty < item.Count)
            {
                var res = await _orderRepository.GetOrderByIdAsync(message.Id, cancellationToken);
                if (res == null)
                {
                    throw new Exception("Замовлення не знайдено");
                }
                res.Status = OrderStatus.Waiting;
                await _orderRepository.UpdateOrder(res, cancellationToken);
                await _emailService.SendEmailAsync(message.Email, "Замовлення", $"На жаль, наразі товару недостатньо на складі.\r\nВаше замовлення очікує поповнення складу.", cancellationToken);

                return;
            }

            products.Add(product);
        }
        string mess = "";
        decimal price = 0;
        foreach (var item in message.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);

            product.StockQty -= item.Count;
            mess += $"{product.Name} — {item.Count} шт × {item.Price} грн\r\n";
            price += (item.Count * item.Price);
        }

        var order = await _orderRepository.GetOrderByIdAsync(message.Id, cancellationToken);

        if (order == null)
        {
            throw new Exception("Замовлення не знайдено");
        }
        order.Status = OrderStatus.Processing;
        await _orderRepository.UpdateOrder(order, cancellationToken);

        await _emailService.SendEmailAsync(message.Email, "Замовлення", $"{mess}\r\n\r\nЗагальна сума: {price}", cancellationToken);
    }
}

using Shop.Application.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderResponseDTO> CreateOrderAsync(string email, OrderCreateDTO dto);
}

using ShopDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderDTOs;

public class OrderResponseDTO
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool Paid { get; set; } = false;

    public OrderStatus Status { get; set; }
    public List<OrderItemResponseDTO> Items {  get; set; } = new();
}

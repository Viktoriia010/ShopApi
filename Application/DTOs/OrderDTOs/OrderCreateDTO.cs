using Shop.Api.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderDTOs;

public class OrderCreateDTO
{
    public bool Paid { get; set; }  = false;
    public List<OrderItemDTO> Items { get; set; } = null!;
}

using Shop.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderDTOs;

public class OrderItemDTO
{
    public int ProductId { get; set; }
    public int Count { get; set; }

}

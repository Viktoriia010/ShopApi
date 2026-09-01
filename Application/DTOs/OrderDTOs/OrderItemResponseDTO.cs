using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderDTOs;

public class OrderItemResponseDTO
{
   public int ProductId { get; set; }
   public decimal Price { get; set; }
   public int Count { get; set; }
}

using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductDTOs;

public class ProductCreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; } = 0;
    public int CategoryId { get; set; }
    public List<string> ImageUrls { get; set; } = [];
}

using Shop.Application.DTOs.ProductImageDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductDTOs;

public class ProductReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; } = 0;
    //public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public List<ProductImageDTO> Images { get; set; } = [];
}

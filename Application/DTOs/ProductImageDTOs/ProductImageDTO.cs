using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductImageDTOs;

public class ProductImageDTO
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}

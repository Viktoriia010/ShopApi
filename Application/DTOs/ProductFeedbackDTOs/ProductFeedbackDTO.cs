using ShopDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductFeedbackDTOs;

public class ProductFeedbackDTO
{
    public int ProductId { get; set; }
    public FeedbackType Type { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
}
using Shop.Application.DTOs.ProductDTOs;

namespace Shop.Api.Requests.Products;

public class ProductCreateRequest 
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQty { get; set; } = 0;
    public int CategoryId { get; set; }
    public List<IFormFile> Images { get; set; } = [];

}

using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductDTOs;

namespace Shop.Api.Requests.Products;

public class ProductCreateRequest : ProductCreateDTO
{
    public List<IFormFile>? Images { get; set; }
}
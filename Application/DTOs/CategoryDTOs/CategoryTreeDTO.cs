using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.CategoryDTOs;

public class CategoryTreeDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Url { get; set; }
    public bool IsActive { get; set; }
    public int? ParentId { get; set; }

    public ICollection<int>? Products { get; set; }

    public List<CategoryTreeDTO> Children { get; set; } = [];
}
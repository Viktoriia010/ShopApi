using MediatR;
using Shop.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.Category;

public record GetCategoryBySlugQuery(string slug) : IRequest<CategoryReadDTO?>;

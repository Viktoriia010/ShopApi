using MediatR;
using Shop.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.Category;

public record CreateCategoryCommand(CategoryCreateDTO dto) : IRequest<int?>;

using MediatR;
using Shop.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.Product;

public record DeleteProductByIdCommand(int id) : IRequest<ProductReadDTO?>;


using MediatR;
using Shop.Application.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.Product;

public record GetProductByIdQuery(int id) : IRequest<ProductReadDTO?>;

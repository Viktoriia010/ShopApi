using Shop.Application.DTOs.ProductFeedbackDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IMongoDbService
{
    Task AddFeedbackAsync(ProductFeedbackDTO feedback);
}

using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IOrderRepository
{
    public Task AddOrderAsync(Orders order, List<OrderDetails> orders);
    public Task UpdateOrder(Orders updated);
    Task<Orders?> GetOrderByIdAsync(Guid id);
}

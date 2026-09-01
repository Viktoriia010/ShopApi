using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces.Repository;
using Shop.Infrastructure.Data;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class OrderRepository(ShopDbContext _context) : IOrderRepository
{
    public async Task AddOrderAsync(Orders order, List<OrderDetails> orders)
    {
        await _context.Orders.AddAsync(order);
        await _context.OrderDetails.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrder(Orders updated)
    {
        _context.Orders.Update(updated);
        await _context.SaveChangesAsync();
    }
    public async Task<Orders?> GetOrderByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }
}

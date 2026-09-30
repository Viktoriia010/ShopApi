using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Data;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories;

public class AddressRepository(ShopDbContext _context) : IAddressRepository
{
    public async Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken)
    {
         await _context.Addresses.AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return address;
    }
    public async Task<List<Address>?> GetAllAddressAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Addresses
        .Where(a => a.UserId == userId)
        .ToListAsync(cancellationToken);
    }
}

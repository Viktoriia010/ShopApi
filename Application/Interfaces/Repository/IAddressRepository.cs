using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Repository;

public interface IAddressRepository
{
    Task<Address> AddAddressAsync(Address address, CancellationToken cancellationToken);
    Task<List<Address>?> GetAllAddressAsync(Guid userId, CancellationToken cancellationToken);
}

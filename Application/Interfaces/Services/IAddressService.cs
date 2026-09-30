using Shop.Application.DTOs.AddressDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Interfaces.Services;

public interface IAddressService
{
    Task<AddressReadDTO> AddAddressAsync(string email, AddressCreateDTO address, CancellationToken cancellationToken);
    Task<List<AddressReadDTO>?> GetAllAddressAsync(string email, CancellationToken cancellationToken);
}

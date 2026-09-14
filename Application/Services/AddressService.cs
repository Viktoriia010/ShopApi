using AutoMapper;
using Shop.Application.DTOs.AddressDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class AddressService(IAddressRepository _addressRepository, IAuthRepository _authRepository, IMapper _mapper) : IAddressService
{
    public async Task<AddressReadDTO> AddAddressAsync(string email, AddressCreateDTO dto, CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetUserByEmailAsync(email, cancellationToken);
        if (user == null)
        {
            throw new Exception("Користувача не найдено");
        }
        var address = _mapper.Map<Address>(dto);
        address.UserId = user.Id;
        var resp = await _addressRepository.AddAddressAsync(address, cancellationToken);
        return _mapper.Map<AddressReadDTO>(resp);
    }
}

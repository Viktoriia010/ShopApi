using FluentValidation;
using Shop.Application.DTOs.AddressDTOs;


namespace Shop.Application.Validators.Address;

public class AddressCreateValidator : AbstractValidator<AddressCreateDTO>
{
    public AddressCreateValidator()
    {
        RuleFor(x => x.HouseNumber)
             .NotEmpty()
             .WithMessage("Номер будинку обов'язковий")
             .MaximumLength(20)
             .WithMessage("Номер будинку не може бути довшим за 20 символів");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Вулиця обов'язкова")
            .MaximumLength(100)
            .WithMessage("Назва вулиці не може бути довшою за 100 символів");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Місто обов'язкове")
            .MaximumLength(100)
            .WithMessage("Назва міста не може бути довшою за 100 символів");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Країна обов'язкова")
            .MaximumLength(100)
            .WithMessage("Назва країни не може бути довшою за 100 символів");

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("Поштовий індекс обов'язковий")
            .MaximumLength(20)
            .WithMessage("Поштовий індекс не може бути довшим за 20 символів");

    }
}
using FluentValidation;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.DTOs.ProductFeedbackDTOs;
using ShopDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Validators.Product;


public class ProductFeedbackValidator : AbstractValidator<ProductFeedbackDTO>
{
    public ProductFeedbackValidator()
    {


        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .WithMessage("Email обов'язковий")
            .EmailAddress()
            .WithMessage("Некоректний формат email")
            .MaximumLength(100)
            .WithMessage("Email не може бути довшим за 100 символів");

        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Повідомлення обов'язкове")
            .MaximumLength(1000)
            .WithMessage("Повідомлення не може бути довшим за 1000 символів");


    }
}
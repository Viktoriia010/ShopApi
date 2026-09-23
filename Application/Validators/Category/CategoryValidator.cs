using FluentValidation;
using Shop.Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Validators.Category;

public class CategoryValidator : AbstractValidator<CategoryCreateDTO>
{
    public CategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("Назва обов'язкова")
            .MaximumLength(100).WithMessage("Назва не може бути довшою за 100 символів");

        RuleFor(category => category.Slug)
            .NotEmpty().WithMessage("Slug обов'язковий")
            .MaximumLength(100).WithMessage("Slug не може бути довшим за 100 символів")
            .Matches(@"^[a-zA-Z0-9_-]+$")
            .WithMessage("Slug повинен містити лише латинськіб цифри та символи _ -");
    }
}
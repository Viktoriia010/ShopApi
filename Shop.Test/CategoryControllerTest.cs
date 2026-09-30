using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Shop.Api.Controllers;
using Shop.Api.Interfaces;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;


namespace MyCalculator;

public class CategoryControllerTest
{
    [Fact]
    public async Task GetCategory_ReturnsOk_WhenCategoryExists()
    {
        //Arraange
        var mockService = new Mock<ICategoryService>();
        var mockImageService = new Mock<IImageService>();
        var mockConfiguration = new Mock<IConfiguration>();
        var mockMediator = new Mock<IMediator>();
        var mockCreateValidator = new Mock<IValidator<CategoryCreateDTO>>();
        var mockUpdateValidator = new Mock<IValidator<CategoryUpdateDTO>>();

        var token = new CancellationToken();

        var testCategory = new CategoryReadDTO
        {
            Id = 1,
            Name = "testCategory",
            Slug = "test-category",
            IsActive = true
        };

         mockService
            .Setup(s => s.GetCategoryByIdAsync(1, token))
            .ReturnsAsync(testCategory);

        var controller = new CategoryController(
          mockService.Object,
          mockImageService.Object,
          mockConfiguration.Object,
          mockMediator.Object,
          mockCreateValidator.Object,
          mockUpdateValidator.Object
      );

        //Act 

        var res = await controller.GetCategoryById(1, token);

        //Assert

        var okResult = Assert.IsType<OkObjectResult>(res);

        var returnedCategory = Assert.IsType<CategoryReadDTO>(okResult.Value);

        Assert.Equal(testCategory.Id, returnedCategory.Id);

    }
}

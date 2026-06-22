using ShopApp.Interfaces;
using ShopDomain.Models;

namespace ShopApp.Services;

public class CategoryService : ICategoryService
{
    private List<Category> _categories = new();
    public CategoryService()
    {
        _categories.Add(new Category()
        {
            Id = 1,
            Title = "Milk Products",
            Description = "Dairy products category",
            Image = "milk.jpg",
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            IsShow = true
        });
        _categories.Add(new Category()
        {
            Id = 2,
            Title = "Bakery",
            Description = "Bread and bakery products",
            Image = "bread.jpg",
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            IsShow = true
        });
    }
    public List<Category> GetAllCategories()
    {

        return _categories;
    }

}

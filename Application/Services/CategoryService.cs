using AutoMapper;
using Shop.Application.DTOs.CategoryDTOs;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services;

public class CategoryService(ICategoryRepository _repository, IMapper _mapper, ICachingService _cacheService) : ICategoryService
{
    //додати автомапер
    public async Task<int?> CreateCategoryAsync(CategoryCreateDTO dto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.AddCategoryAsync(category, cancellationToken);
        //return await _repository.AddCategoryAsync(new Category()
        //{
        //    Name = dto.Name,
        //    Slug = dto.Slug,
        //    Url = dto.Url,
        //    ParentId = dto.ParentId,
        //});
    }

    public async Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        return await _repository.DeleteCategoryAsync(id, cancellationToken);
    }

    public async Task<List<CategoryReadDTO>?> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);
        var cache = await _cacheService.GetAsync<List<CategoryReadDTO>>("Categories");
        if (cache == null)
        {
            var categories = await _repository.GetAllCategoriesAsync(cancellationToken);
            cache = _mapper.Map<List<CategoryReadDTO>>(categories);
            await _cacheService.SetAsync("Categories", cache, null);

        }
        return cache;
    }

    public async Task<CategoryReadDTO?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"Category_{id}";
        var cache = await _cacheService.GetAsync<CategoryReadDTO>(cacheKey);
        if(cache == null)
        {
            var res = await _repository.GetCategoryByIdAsync(id, cancellationToken);
            cache = _mapper.Map<CategoryReadDTO>(res);
            await _cacheService.SetAsync(cacheKey, cache, null);
        }
        return cache;
    }

    public async Task<List<CategoryReadDTO>> GetParentsCategoryByIdAsync(CategoryReadDTO dto, CancellationToken cancellationToken)
    {
        List<CategoryReadDTO> categories = new List<CategoryReadDTO>();

        while (dto.ParentId != null)
        {
            var parent = await GetCategoryByIdAsync(dto.ParentId.Value, cancellationToken);
            categories.Add(parent);
            dto = parent;

        }

        return categories;
    }
    public async Task<List<CategoryReadDTO>> GetChildrensCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        var categories = await GetAllCategoriesAsync(cancellationToken);

        if (categories == null)
        {
            return [];
        }
        var res = categories.Where(c => c.ParentId == id).ToList();
        List<CategoryReadDTO> childrens = new List<CategoryReadDTO>();
        foreach (var item in res)
        {
            childrens.Add(item);

            var descendants = await GetChildrensCategoryByIdAsync(item.Id, cancellationToken);

            childrens.AddRange(descendants);
        }

        return childrens;
    }
    private CategoryTreeDTO BuildTree(CategoryReadDTO category,List<CategoryReadDTO> allCategories)
    {
      
        var node = _mapper.Map<CategoryTreeDTO>(category);

        var children = allCategories
            .Where(c => c.ParentId == category.Id)
            .ToList();

        foreach (var child in children)
        {
            node.Children.Add(BuildTree(child, allCategories));
        }

        return node;
    }
    public async Task<List<CategoryTreeDTO>> GetTreeCategoryByIdAsync(CancellationToken cancellationToken)
    {
        var categories = await GetAllCategoriesAsync(cancellationToken);

        if (categories == null || !categories.Any())
        {
            return new List<CategoryTreeDTO>();
        }

        var rootCategories = categories
            .Where(c => c.ParentId == null)
            .ToList();

        var tree = new List<CategoryTreeDTO>();

        foreach (var category in rootCategories)
        {
            tree.Add(BuildTree(category, categories));
        }

        return tree;
    }

    public async Task<CategoryUpdateDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO updated, CancellationToken cancellationToken)
    {
       
        var category = await _repository.GetCategoryByIdAsync(id, cancellationToken);

        if (category == null)
            return null;
        _mapper.Map(updated, category);
   

        var result = await _repository.UpdateCategoryAsync(category, cancellationToken);
        if (result == null)
            return null;
        return _mapper.Map<CategoryUpdateDTO>(result);

    }


}

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

public class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
{
    //додати автомапер
    public async Task<int?> CreateCategoryAsync(CategoryCreateDTO dto)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.AddCategoryAsync(category);
        //return await _repository.AddCategoryAsync(new Category()
        //{
        //    Name = dto.Name,
        //    Slug = dto.Slug,
        //    Url = dto.Url,
        //    ParentId = dto.ParentId,
        //});
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        return await _repository.DeleteCategoryAsync(id);
    }

    public async Task<List<CategoryReadDTO>?> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllCategoriesAsync();
        List<CategoryReadDTO> dtos = new List<CategoryReadDTO>();
        if (categories != null && categories.Count > 0)
        {
            dtos = _mapper.Map<List<CategoryReadDTO>>(categories);
            //foreach (var item in categories)
            //{
            //    dtos.Add(new CategoryReadDTO()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Slug = item.Slug,
            //        Url = item.Url,
            //        ParentId = item.ParentId,
            //    });
            //}
        }

        return dtos;
    }

    public async Task<CategoryReadDTO?> GetCategoryByIdAsync(int id)
    {
        CategoryReadDTO dto = null;
        var res = await _repository.GetCategoryByIdAsync(id);
        if (res != null)
        {
            dto = _mapper.Map<CategoryReadDTO>(res);
        }
        return dto;
    }

    public async Task<List<CategoryReadDTO>> GetParentsCategoryByIdAsync(CategoryReadDTO dto)
    {
        List<CategoryReadDTO> categories = new List<CategoryReadDTO>();

        while (dto.ParentId != null)
        {
            var parent = await GetCategoryByIdAsync(dto.ParentId.Value);
            categories.Add(parent);
            dto = parent;

        }

        return categories;
    }
    public async Task<List<CategoryReadDTO>> GetChildrensCategoryByIdAsync(int id)
    {
        var categories = await GetAllCategoriesAsync();

        if (categories == null)
        {
            return [];
        }
        var res = categories.Where(c => c.ParentId == id).ToList();
        List<CategoryReadDTO> childrens = new List<CategoryReadDTO>();
        foreach (var item in res)
        {
            childrens.Add(item);

            var descendants = await GetChildrensCategoryByIdAsync(item.Id);

            childrens.AddRange(descendants);
        }

        return childrens;
    }
    private CategoryTreeDTO BuildTree(CategoryReadDTO category,List<CategoryReadDTO> allCategories)
    {
        var node = new CategoryTreeDTO
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId
        };

        var children = allCategories
            .Where(c => c.ParentId == category.Id)
            .ToList();

        foreach (var child in children)
        {
            node.Children.Add(BuildTree(child, allCategories));
        }

        return node;
    }
    public async Task<List<CategoryTreeDTO>> GetTreeCategoryByIdAsync()
    {
        var categories = await GetAllCategoriesAsync();

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

    public async Task<CategoryUpdateDTO?> UpdateCategoryAsync(int id, CategoryUpdateDTO updated)
    {
       
        var category = await _repository.GetCategoryByIdAsync(id);

        if (category == null)
            return null;
        _mapper.Map(updated, category);
        //category.Name = updated.Name;
        //category.Url = updated.Url;
        //category.ParentId = updated.ParentId;
        //category.Slug = updated.Slug;
        //category.IsActive = updated.IsActive;

        var result = await _repository.UpdateCategoryAsync(category);
        if (result == null)
            return null;
        return _mapper.Map<CategoryUpdateDTO>(result);
        //return new CategoryUpdateDTO
        //{
        //    Name = result.Name,
        //    Slug = result.Slug,
        //    Url = result.Url,
        //    IsActive = result.IsActive,
        //    ParentId = result.ParentId
        //};
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Category;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDto>> GetAllCategoryDtoAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return categoryDtos;
        }

        public async Task<CategoryDto?> GetCategoryDtoByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) 
                throw new NotFoundException($"Category not found.");
            
            return category.ToCategoryDto();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var isExistCategory = await _categoryRepository.IsCategoryNameExistAsync(categoryDto.Name);
            if (isExistCategory) 
                throw new BadRequestException($"Category with name {categoryDto.Name} already exists.");
            
            var newCategory = await _categoryRepository.CreateAsync(categoryDto.ToCategoryModel());
            return newCategory.ToCategoryDto();
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto)
        {
            var isExistCategory = await _categoryRepository.IsCategoryExistAsync(id);
            if (isExistCategory == false) 
                throw new NotFoundException($"Category was not found.");
            
            var isExistCategoryName = await _categoryRepository.IsCategoryNameExistAsync(categoryDto.Name);
            if (isExistCategoryName) 
                throw new BadRequestException($"Category with name {categoryDto.Name} already exists.");

            var newCategory = await _categoryRepository.UpdateAsync(id, categoryDto);
            return newCategory.ToCategoryDto();
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var isExistCategory = await _categoryRepository.IsCategoryExistAsync(id);
            if (isExistCategory == false) 
                throw new NotFoundException($"Category was not found.");

            await _categoryRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> IsCategoryExistAsync(Guid id)
        {
            return await _categoryRepository.IsCategoryExistAsync(id);
        }

        public async Task<bool> IsCategoryNameExistAsync(string name)
        {
            return await _categoryRepository.IsCategoryNameExistAsync(name);
        }

        public async Task<bool> IsCategoryExist(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return category != null;
        }
    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Category;

namespace BookStore.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> GetAllCategoryDtoAsync();
        public Task<CategoryDto?> GetCategoryDtoByIdAsync(Guid id);
        public Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);
        public Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto);
        public Task<bool> DeleteCategoryAsync(Guid id);
        public Task<bool> IsCategoryExistAsync(Guid id);
        public Task<bool> IsCategoryNameExistAsync(string name);
    }
}
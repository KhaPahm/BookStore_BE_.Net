using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Category;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();
        public Task<Category> GetByIdAsync(Guid id);
        public Task<Category> CreateAsync(Category category);
        public Task<Category> UpdateAsync(Guid id, UpdateCategoryDto category);
        public Task<Category> DeleteAsync(Guid id);
    }
}
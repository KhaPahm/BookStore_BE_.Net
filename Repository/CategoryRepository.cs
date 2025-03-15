using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Category;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(Guid id, UpdateCategoryDto category)
        {
            var exCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (exCategory == null) 
                return null;

            exCategory.Name = category.Name;
            exCategory.Description = category.Description;

            await _context.SaveChangesAsync();

            return exCategory;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(category == null) return null;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Category;
using BookStore.Interfaces;
using BookStore.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var categories = await _categoryRepo.GetAllAsync();
            var categoriesDto = categories.Select(c => c.ToCategoryDto());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) {
            var category = await _categoryRepo.GetByIdAsync(id);

            if(category == null) {
                return NotFound();
            }

            return Ok(category.ToCategoryDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto) {
            var categoryModel = createCategoryDto.ToCategoryFromCreateCategoryDto();
            await _categoryRepo.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new {id = categoryModel.Id}, categoryModel.ToCategoryDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto) {
            var category = await _categoryRepo.UpdateAsync(id, updateCategoryDto);

            if(category == null) {
                return NotFound();
            }

            return Ok(category.ToCategoryDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id) {
            var category = await _categoryRepo.DeleteAsync(id);
            
            if(category == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
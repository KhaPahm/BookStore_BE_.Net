using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Category;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var categoriesDto = await _categoryService.GetAllCategoryDtoAsync();
            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) {
            var categoryDto = await _categoryService.GetCategoryDtoByIdAsync(id);
            return Ok(categoryDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto) {
            var categoriesDto = await _categoryService.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new {id = categoriesDto.Id}, categoriesDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto) {
            var categoriesDto = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            return Ok(categoriesDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id) {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
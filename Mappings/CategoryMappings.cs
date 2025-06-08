using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Category;
using BookStore.Models;

namespace BookStore.Mappings
{
    public static class CategoryMappings
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel) {
            return new CategoryDto {
                Id = categoryModel.Id,
                Name = categoryModel.Name
            };
        }

        public static Category ToCategoryModel(this CreateCategoryDto createCategoryDto) {
            return new Category {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };
        }
    }
}
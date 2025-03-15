using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
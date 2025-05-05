using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Review
{
    public class CreateReviewDto
    {
        public Guid BookId { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        [StringLength(500, ErrorMessage = "Content must be less than 500 characters.")]
        public string Content { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
        public List<IFormFile>? Images { get; set; } // Accept image files
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Review
{
    public class CreateReviewReplyDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ReviewId is required.")]
        public Guid ReviewId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required")]
        public string Content { get; set; }
    }
}
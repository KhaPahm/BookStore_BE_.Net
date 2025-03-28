using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos.Review
{
    public class UpdateReviewDto
    {
        public string Content { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;
using BookStore.Dtos.User;

namespace BookStore.Dtos.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
        public UserReviewDto User { get; set; }
        public ICollection<ReviewImageDto> Images { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.User;

namespace BookStore.Dtos.Review
{
    public class ReviewReplyDto
    {
        public Guid Id { get; set; }
        public Guid ReviewId { get; set; }
        public string Content { get; set; }
        public UserReviewDto User { get; set; }
    }
}
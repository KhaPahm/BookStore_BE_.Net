using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;

namespace BookStore.Dtos.User
{
    public class UserReviewDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; } // Store Google/GitHub profile image

    }
}
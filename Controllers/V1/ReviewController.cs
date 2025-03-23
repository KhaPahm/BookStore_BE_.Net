using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.ResponeApi;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [ApiController]
    [Route("api/v1/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet("by-book-id/{bookId}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId) {
            var reviews = await _reviewRepo.GetByBookIdAsync(bookId);

            var reviewsDto = reviews.Select(rv => rv.ToReviewDto()).ToList();

            return Ok(new ApiResponse<List<ReviewDto>>(200, reviewsDto));
        }
    }
}
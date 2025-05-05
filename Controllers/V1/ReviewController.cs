using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Dtos.Review;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;
using BookStore.Models.Entities;
using BookStore.Models.ResponeApi;
using BookStore.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.Controllers.V1
{
    [ApiController]
    [Route("api/v1/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("by-book-id/{bookId}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId)
        {
            var reviewsDto = await _reviewService.GetByBookIdAsync(bookId);
            return Ok(new ApiResponse<List<ReviewDto>>(200, reviewsDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromForm] CreateReviewDto createReviewDto)
        {
            var reviewDto = await _reviewService.CreateReviewAsync(createReviewDto, User.GetUserId());
            return Ok(new ApiResponse<ReviewDto>(200, reviewDto));
        }

        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid reviewId, [FromBody] UpdateReviewDto updateReviewDto)
        {
          
            var reviewDto = await _reviewService.UpdateReviewAsync(reviewId, updateReviewDto, User.GetUserId());
            return Ok(new ApiResponse<ReviewDto>(200, reviewDto));
        }

        [HttpPost("like")]
        [Authorize]
        public async Task<IActionResult> LikeReview([FromBody] LikeReviewDto likeReviewDto)
        {
            var userId = User.GetUserId();
            await _reviewService.LikeReviewAsync(likeReviewDto.ReviewId, userId);

            return Ok(new ApiResponse<string>(200, null));
        }

        [HttpPost("reviewReply")]
        [Authorize]
        public async Task<IActionResult> ReviewReply([FromBody] CreateReviewReplyDto createReviewReplyDto)
        {
            var userId = User.GetUserId();
            await _reviewService.ReplyReviewAsync(userId, createReviewReplyDto);
            return Ok(new ApiResponse<string>(200, null));
        }

        [HttpGet("reviewReply/{reviewId}")]
        public async Task<IActionResult> GetReviewReplyByReviewId([FromRoute] Guid reviewId) {

            var reviewRepliesDto = await _reviewService.GetReviewRepliesAsync(reviewId);

            return Ok(new ApiResponse<List<ReviewReplyDto>>(200, reviewRepliesDto));
        }
    }
}
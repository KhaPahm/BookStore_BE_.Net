using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Book;
using BookStore.Dtos.Review;
using BookStore.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
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
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IReviewImageRepository _reviewImageRepo;
        private readonly IReviewLikeRepository _reviewLikeRepo;

        public ReviewController(IReviewRepository reviewRepo, IBookRepository bookRepo, IReviewImageRepository reviewImageRepo, IReviewLikeRepository reviewLikeRepo)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _reviewImageRepo = reviewImageRepo;
            _reviewLikeRepo = reviewLikeRepo;
        }

        [HttpGet("by-book-id/{bookId}")]
        public async Task<IActionResult> GetByBookId([FromRoute] Guid bookId) {
            var reviews = await _reviewRepo.GetByBookIdAsync(bookId);

            var reviewsDto = reviews.Select(rv => rv.ToReviewDto()).ToList();

            return Ok(new ApiResponse<List<ReviewDto>>(200, reviewsDto));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromForm] CreateReviewDto createReviewDto) {
            if(!ModelState.IsValid) 
                return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));

            //Check book is exist
            var book = await _bookRepo.GetByIdAsync(createReviewDto.BookId);
            if(book == null)
                return NotFound(new ApiResponse<string>(404, null, "Couldn't find the book.", false));

            var userId = User.GetUserId();
            //Convert to review model
            var review = createReviewDto.ToReviewModel(userId);

            //Add to database
            await _reviewRepo.CreateAsync(review);
            await _reviewImageRepo.CreateAsync(review.Id, createReviewDto.Images);

            var newReview = await _reviewRepo.GetByIdAsync(review.Id);

            return Ok(new ApiResponse<ReviewDto>(200, newReview.ToReviewDto()));
        }

        [HttpPut("{reviewId}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid reviewId, [FromBody] UpdateReviewDto updateReviewDto) {
            if(!ModelState.IsValid) 
                return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));

            var review = await _reviewRepo.GetByIdAsync(reviewId);
            if(review == null) 
                return NotFound(new ApiResponse<string>(404, null, "Couldn't find the review.", false));

            var userId = User.GetUserId();

            if(review.UserId != userId) 
                return BadRequest(new ApiResponse<string>(400, null, "You couldn't edit other people's review.", false));

            var updatedReview = await _reviewRepo.UpdateAsync(reviewId, updateReviewDto);

            return Ok(new ApiResponse<ReviewDto>(200, updatedReview.ToReviewDto()));
        }
    
        [HttpPost("like")]
        [Authorize]
        public async Task<IActionResult> LikeReview([FromBody] LikeReviewDto likeReviewDto) {
            if(!ModelState.IsValid) 
                return BadRequest(new ApiResponse<string>(400, null, "Request data is wrong structure.", false));

            var review = await _reviewRepo.GetByIdAsync(likeReviewDto.ReviewId);
            if(review == null) 
                return NotFound(new ApiResponse<string>(404, null, "Couldn't find the review.", false));

            
            var userId = User.GetUserId();
            var checkLiked = await _reviewLikeRepo.CheckLikeReviewAsync(userId, likeReviewDto.ReviewId);
            if (checkLiked == null)
                await _reviewLikeRepo.LikeReviewAsync(userId, likeReviewDto.ReviewId);

            return Ok(new ApiResponse<string>(200, null));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Review;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewReplyRepository _reviewReplyRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewImageService _reviewImageService;
        private readonly IBookService _bookService;
        private readonly IReviewLikeRepository _reviewLikeRepository;

        public ReviewService(IReviewReplyRepository reviewReplyRepository, 
            IReviewRepository reviewRepository, 
            IReviewImageService reviewImageService, 
            IBookService bookService,
            IReviewLikeRepository reviewLikeRepository)
        {
            _reviewReplyRepository = reviewReplyRepository;
            _reviewRepository = reviewRepository;
            _reviewImageService = reviewImageService;
            _bookService = bookService;
            _reviewLikeRepository = reviewLikeRepository;
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReviewDto, Guid userId)
        {
            //Check book is exist
            var isBookExist = _bookService.IsBookExistAsync(createReviewDto.BookId);
            if (!isBookExist.Result)
                throw new NotFoundException("Book not found.");

            //Convert to review model
            var review = createReviewDto.ToReviewModel(userId);
            await _reviewRepository.CreateAsync(review);
            
            return review.ToReviewDto();
        }

        public async Task<List<ReviewDto>> GetByBookIdAsync(Guid bookId)
        {
            var reviews = await _reviewRepository.GetByBookIdAsync(bookId);
            var reviewsDto = reviews.Select(rv => rv.ToReviewDto()).ToList();
            return reviewsDto;
        }

        public Task<List<ReviewReplyDto>> GetReviewRepliesAsync(Guid reviewId)
        {
            var reviewReplies = _reviewReplyRepository.GetByReviewIdAsync(reviewId);
            var reviewRepliesDto = reviewReplies.Result.Select(r => r.ToReviewReplyDto()).ToList();
            return Task.FromResult(reviewRepliesDto);
        }

        public async Task<bool> LikeReviewAsync(Guid reviewId, Guid userId)
        {
            var isReviewExist = _reviewRepository.IsExistAsync(reviewId);
            if (!isReviewExist.Result)
                throw new NotFoundException("Review not found.");

            var isLikeExist = await _reviewLikeRepository.CheckLikeReviewAsync(userId, reviewId);
            
            if (isLikeExist == null)
                await _reviewLikeRepository.LikeReviewAsync(userId, reviewId);
            
            return true;
        }

        public async Task<ReviewReplyDto> ReplyReviewAsync(Guid reviewId, CreateReviewReplyDto createReviewReplyDto)
        {
            var reviewReply = createReviewReplyDto.ToReviewReplyModel(reviewId);
            await _reviewReplyRepository.CreateAsync(reviewReply);
            return reviewReply.ToReviewReplyDto(); 
        }

        public async Task<ReviewDto> UpdateReviewAsync(Guid reviewId, UpdateReviewDto updateReviewDto, Guid userId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                throw new NotFoundException("Review not found.");

            if(userId != review.UserId)
                throw new BadRequestException("You couldn't edit other people's review.");

            var updatedReview = await _reviewRepository.UpdateAsync(reviewId, updateReviewDto);

            return updatedReview.ToReviewDto();
        }
    }
}
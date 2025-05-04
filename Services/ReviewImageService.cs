using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;
using BookStore.Models;

namespace BookStore.Services
{
    public class ReviewImageService : IReviewImageService
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IReviewImageRepository _reviewImageRepository;

        public ReviewImageService(ICloudinaryService cloudinaryService, IReviewImageRepository reviewImageRepository)
        {
            _cloudinaryService = cloudinaryService;
            _reviewImageRepository = reviewImageRepository;
        }
        public async Task<List<ReviewImageDto>> CreateReviewImagesAsync(Guid reviewId, ICollection<IFormFile> images)
        {
            List<ReviewImage> lstReviewImage = new();

            foreach(var image in images) {
                var reviewImage = new ReviewImage {
                    ReviewId = reviewId
                };
                var url = _cloudinaryService.UploadImageAsync(image, "reviews").Result;
                reviewImage.ImageUrl = url;
                await _reviewImageRepository.CreateAsync(reviewImage);
                lstReviewImage.Add(reviewImage);
            } 

            return lstReviewImage.Select(reviewImage => reviewImage.ToReviewImageDto()).ToList();
        }
    }
}
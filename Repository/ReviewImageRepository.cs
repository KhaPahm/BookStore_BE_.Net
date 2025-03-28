using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;

namespace BookStore.Repository
{
    public class ReviewImageRepository : IReviewImageRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ICloudinaryService _cloudinaryService;

        public ReviewImageRepository(ApplicationDBContext context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<List<ReviewImage>> CreateAsync(Guid reviewId, ICollection<IFormFile>? images)
        {   
            if(images == null)
                return new();
            if(images.Count == 0)
                return new();

            List<ReviewImage> reviewImages = new();

            foreach(var image in images) {
                var reviewImage = new ReviewImage {
                    ReviewId = reviewId
                };

                var url = await _cloudinaryService.UploadImageAsync(image, "reviews");
                reviewImage.ImageUrl = url;
                await _context.ReviewImages.AddAsync(reviewImage);
                await _context.SaveChangesAsync();
                reviewImages.Add(reviewImage);
            }

            return reviewImages;
        }
    }
}
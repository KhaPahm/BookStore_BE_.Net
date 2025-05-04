using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.ReviewImage;

namespace BookStore.Interfaces.Services
{
    public interface IReviewImageService
    {
        public Task<List<ReviewImageDto>> CreateReviewImagesAsync(Guid reviewId, ICollection<IFormFile> images);
    }
}
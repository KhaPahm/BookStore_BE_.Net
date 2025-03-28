using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace BookStore.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloundinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloundinary = new Cloudinary(account);
        }

        public async Task<string?> UploadImageAsync(IFormFile file, string folderName = "books")
        {
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = $"bookstore/{folderName}" // Organize images in Cloudinary
                };
                var uploadResult = await _cloundinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString(); // Return image URL
            }
            return null;
        }
    }
}
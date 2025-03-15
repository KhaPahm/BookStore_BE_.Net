using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImageAsync(IFormFile file);
    }
}
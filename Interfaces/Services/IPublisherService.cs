using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Publisher;

namespace BookStore.Interfaces.Services
{
    public interface IPublisherService
    {
        public Task<PublisherDto?> GetPublisherDtoByIdAsync(Guid id);
        public Task<List<PublisherDto>> GetAllPublisherDtoAsync();
        public Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto publisherDto);
        public Task<PublisherDto> UpdatePublisherAsync(Guid id, UpdatePublisherDto publisherDto);
        public Task<bool> DeletePublisherAsync(Guid id);
    }
}
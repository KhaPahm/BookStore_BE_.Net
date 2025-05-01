using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Publisher;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;

namespace BookStore.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepo;

        public PublisherService(IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo; 
        }

        public async Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto publisherDto)
        {
            var publisher = publisherDto.ToPublisherFromCreateDto();
            var createdPublisher = await _publisherRepo.CreateAsync(publisher);
            return createdPublisher.ToPublisherDto();
        }

        public async Task<bool> DeletePublisherAsync(Guid id)
        {
            var publisher = await _publisherRepo.GetByIdAsync(id);
            if (publisher == null) 
                throw new NotFoundException("Publisher not found.");
            
            await _publisherRepo.DeleteAsync(publisher);
            return true;
        }

        public async Task<List<PublisherDto>> GetAllPublisherDtoAsync()
        {
            var publishers = await _publisherRepo.GetAllAsync();
            return publishers.Select(p => p.ToPublisherDto()).ToList();
        }

        public async Task<PublisherDto?> GetPublisherDtoByIdAsync(Guid id)
        {
            var publisher = await _publisherRepo.GetByIdAsync(id);
            
            if (publisher == null) 
                throw new NotFoundException("Publisher not found.");

            return publisher.ToPublisherDto();
        }

        public async Task<bool> IsPublisherExist(Guid id)
        {
            var publisher = await _publisherRepo.GetByIdAsync(id);
            return publisher != null;
        }

        public async Task<PublisherDto> UpdatePublisherAsync(Guid id, UpdatePublisherDto publisherDto)
        {
            var publisher = await _publisherRepo.GetByIdAsync(id);
            if (publisher == null) 
                throw new NotFoundException("Publisher not found.");
            
            publisher.Name = publisherDto.Name;
            publisher.Address = publisherDto.Address;
            publisher.PhoneNumber = publisherDto.PhoneNumber;

            var updatedPublisher = await _publisherRepo.UpdateAsync(publisher);
            return updatedPublisher.ToPublisherDto(); 
        }
    }
}
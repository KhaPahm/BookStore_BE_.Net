using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Publisher;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private ApplicationDBContext _context;
        public PublisherRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher?> DeleteAsync(Guid id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

            if(publisher == null)
                return null;

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(Guid id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

            if(publisher == null)
                return null;
            
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(Guid id, UpdatePublisherDto publisherDto)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);

            if(publisher == null)
                return null;

            publisher.Name = publisherDto.Name;
            publisher.Address = publisherDto.Address;
            publisher.PhoneNumber = publisherDto.PhoneNumber;

            await _context.SaveChangesAsync();

            return publisher;
        }
    }
}
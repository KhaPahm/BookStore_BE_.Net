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

        public async Task<Publisher?> DeleteAsync(Publisher publisher)
        {
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
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }
    }
}
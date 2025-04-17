using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Publisher;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IPublisherRepository
    {
        public Task<List<Publisher>> GetAllAsync();
        public Task<Publisher> GetByIdAsync(Guid id);
        public Task<Publisher> CreateAsync(Publisher publisher);
        public Task<Publisher> UpdateAsync(Publisher publisher);
        public Task<Publisher> DeleteAsync(Publisher publisher);
    }
}
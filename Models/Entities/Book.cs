using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }  
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<BookImage> Images { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}
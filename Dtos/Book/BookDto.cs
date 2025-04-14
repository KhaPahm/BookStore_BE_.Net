
using BookStore.Dtos.BookImage;
using BookStore.Dtos.Category;
using BookStore.Dtos.Publisher;
using BookStore.Models;

namespace BookStore.Dtos.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }  
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public CategoryDto Category {get; set;}
        public PublisherDto Publisher {get;set;}
        public ICollection<BookImageDto> Images { get; set; }
        
    }
}
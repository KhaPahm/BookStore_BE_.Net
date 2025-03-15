using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class ReviewImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
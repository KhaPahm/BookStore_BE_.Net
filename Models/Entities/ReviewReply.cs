using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Entities
{
    public class ReviewReply
    {
        public Guid Id { get; set; }
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Review Review { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
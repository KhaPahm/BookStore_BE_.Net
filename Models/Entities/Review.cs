using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;

namespace BookStore.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        [Range(1, 5)]
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public ICollection<ReviewImage> Images { get; set; }
        public ICollection<ReviewReply> ReviewReplies { get; set; }
        public ICollection<ReviewLike> ReviewLikes { get; set; }
    }
}
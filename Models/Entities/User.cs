using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Entities;

namespace BookStore.Models
{

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [NotNull]   
        [MaxLength(50)]
        public string FullName { get; set; }  
        [NotNull]   
        [EmailAddress]
        public string Email { get; set; }  
        public string? PasswordHash { get; set; }  // Null if using external login
        public string? ProfilePictureUrl { get; set; } = "https://res.cloudinary.com/dpsux2vzu/image/upload/v1742357469/bookstore/defaultImage/kkiwzpnmgw3xxfvjgurd.jpg";
        public string? AuthProvider { get; set; }  // e.g., "Google", "GitHub"
        public string? ProviderId { get; set; }  // Unique ID from Google/GitHub
        public string Role { get; set; } //"Admin", "Staff", "Customer"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ShoppingCart> ShoppingCarts { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<Order> Orders { get; set; }
        public List<ReviewReply> ReviewReplies { get; set; } = new();
        public List<ReviewLike> ReviewLikes { get; set; } = new();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Dtos.Auth
{
    public class UserDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string? ProfilePictureUrl { get; set; } = ""; // Store Google/GitHub profile image
        public string? AuthProvider { get; set; } = ""; // e.g., "Google", "GitHub"
        public string Role { get; set; } = ""; // Default role is Customer
        public string Token { get; set; } = "";
    }
}
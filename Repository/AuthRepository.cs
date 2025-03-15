using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Auth;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> RegisterUser(CustomerRegisterDto newUserDto)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUserDto.Email);
            
            if(checkUser != null)
                return null;
                
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUserDto.Password, 10);
            var user = new User {
                FullName = newUserDto.FullName,
                Email = newUserDto.Email,
                PasswordHash = hashedPassword,
                Role = "CUSTOMER"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
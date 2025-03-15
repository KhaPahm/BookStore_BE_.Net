using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IAuthRepository
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> RegisterUser(CustomerRegisterDto newUserDto);
    }
}
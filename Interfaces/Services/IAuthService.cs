using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;

namespace BookStore.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<UserDto> Login(string email, string password);
        public Task<UserDto> CustomerRegister(CustomerRegisterDto customerRegisterDto);
        public Task<UserDto> GoogleLogin(string providerId, string email, string fullName);
    }
}
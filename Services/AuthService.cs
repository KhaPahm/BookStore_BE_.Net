using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappings;
using BookStore.Models;
using BookStore.Static;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthRepository authRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _jwtService = jwtService;
        }

        public async Task<UserDto> CustomerRegister(CustomerRegisterDto customerRegisterDto)
        {
            var checkUser = await _authRepository.GetUserByEmail(customerRegisterDto.Email);
            if (checkUser != null)
                throw new BadRequestException("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(customerRegisterDto.Password, 10);
            var newUser = new User
            {
                FullName = customerRegisterDto.FullName,
                Email = customerRegisterDto.Email,
                PasswordHash = hashedPassword,
                Role = UserRole.Customer,
                AuthProvider = AuthProvider.Local,
            };

            await _authRepository.RegisterUser(newUser);

            var accessToken = _jwtService.GenerateToken(newUser);

            return newUser.ToUserDto(accessToken);
        }

        public async Task<UserDto> GoogleLogin(string providerId, string email, string fullName)
        {
            var user = await _authRepository.GetUserByProviderIdAsync(providerId, AuthProvider.Google);

            if (user == null) {
                user = new User {
                    Id = new(),
                    FullName = fullName,
                    Email = email,
                    AuthProvider = AuthProvider.Google,
                    Role = UserRole.Customer,
                    ProviderId = providerId
                };

                await _authRepository.RegisterUser(user);
            }
            
            var accessToken = _jwtService.GenerateToken(user);
            return user.ToUserDto(accessToken);
        }

        public async Task<UserDto> Login(string email, string password)
        {
            var user = await _authRepository.GetUserByEmail(email);
            if (user == null)
                throw new UnauthorizedException("Invalid username or password!");

            var checkPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!checkPassword)
                throw new UnauthorizedException("Invalid username or password!");

            var accessToken = _jwtService.GenerateToken(user);

            return user.ToUserDto(accessToken);
        }
    }
}
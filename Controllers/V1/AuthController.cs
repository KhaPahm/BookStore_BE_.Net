using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthRepository _authRepo;

        public AuthController(IJwtService jwtService, IAuthRepository authRepo)
        {
            _jwtService = jwtService;
            _authRepo = authRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            var user = await _authRepo.GetUserByEmail(loginDto.Email);

            if(user == null) {
                return Unauthorized("Invalid username or password!");
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if(!checkPassword) {
                return Unauthorized("Invalid username or password!");
            }

            var accessToken = _jwtService.GenerateToken(user);

            return Ok(
                new UserDto {
                    FullName = user.FullName,
                    Email = user.Email,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    AuthProvider = user.AuthProvider,
                    Role = user.Role,
                    Token = accessToken
                }
            );
        }

        [HttpPost("customer-register")]
        public async Task<IActionResult> CustomerRegister([FromBody] CustomerRegisterDto customerRegisterDto) {
            var newUser = await _authRepo.RegisterUser(customerRegisterDto);
            
            if(newUser == null) {
                return BadRequest("Email already exist.");
            }

            var accessToken = _jwtService.GenerateToken(newUser);

            return Ok(
                new UserDto {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    ProfilePictureUrl = newUser.ProfilePictureUrl,
                    AuthProvider = newUser.AuthProvider,
                    Role = newUser.Role,
                    Token = accessToken
                }
            );
        }
    }
}
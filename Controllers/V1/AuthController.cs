using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models;
using BookStore.Models.ResponeApi;
using BookStore.Static;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
    
        [HttpGet("login/google")]
        public IActionResult GoogleLogin() {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse() {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            if(!authenticateResult.Succeeded) {
                return Unauthorized();
            }

            var claims = authenticateResult.Principal.Identities
                .FirstOrDefault()?.Claims.Select(c => new {c.Type, c.Value});

            var claimId = claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.NameIdentifier);

            var user = await _authRepo.GetUserByProviderIdAsync(claimId.Value, AuthProvider.Google);

            if (user == null) {
                user = new User {
                    Id = new(),
                    FullName = claims.First(c => c.Type.ToString() == ClaimTypes.Name).Value,
                    Email = claims.First(c => c.Type.ToString() == ClaimTypes.Email).Value,
                    AuthProvider = AuthProvider.Google,
                    Role = UserRole.Customer,
                    ProviderId = claimId.Value
                };

                await _authRepo.RegisterUser(user);
            }
            
            var accessToken = _jwtService.GenerateToken(user);

            return Ok(new ApiResponse<UserDto>(200, user.ToUserDto(accessToken), "Login successful"));
        }
    }
}
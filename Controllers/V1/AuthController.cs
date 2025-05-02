using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Dtos.Auth;
using BookStore.Exceptions;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            return Ok(await _authService.Login(loginDto.Email, loginDto.Password));
        }

        [HttpPost("customer-register")]
        public async Task<IActionResult> CustomerRegister([FromBody] CustomerRegisterDto customerRegisterDto) {
           return Ok(await _authService.CustomerRegister(customerRegisterDto));
        }
    
        [HttpGet("login/google")]
        public IActionResult GoogleLogin() {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse() {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            if(!authenticateResult.Succeeded) 
                throw new UnauthorizedException("Google login failed.");

            var claims = authenticateResult.Principal.Identities
                .FirstOrDefault()?.Claims.Select(c => new {c.Type, c.Value});

            var claimId = claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.NameIdentifier).Value;
            var email = claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Email).Value;
            var fullName = claims.FirstOrDefault(c => c.Type.ToString() == ClaimTypes.Name).Value;

            return Ok(await _authService.GoogleLogin(claimId, email, fullName));
        }
    }
}
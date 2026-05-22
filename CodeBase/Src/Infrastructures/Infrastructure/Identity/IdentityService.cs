using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtService;
        private readonly IConfiguration _config;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenService jwtService,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _config = config;
        }

        // REGISTER
        public async Task<(bool Succeeded, string UserId)> RegisterAsync(string email, string password, string fullName)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault()?.Description ?? "Registration failed";
                return (false, error);
            }

            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return (true, user.Id);
        }

        // LOGIN
        public async Task<AuthResponseDto?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = await _jwtService.GenerateToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken(user);

            var accessExpiry = int.Parse(_config["Jwt:AccessTokenExpiryMinutes"]);
            var refreshExpiry = int.Parse(_config["Jwt:RefreshTokenExpiryDays"]);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshExpiry);


            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(accessExpiry),
                RefreshTokenExpiry = user.RefreshTokenExpiryTime
            };
        }

        // REFRESH TOKEN
        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            if (user == null)
                throw new Exception("Invalid refresh token");

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Refresh token expired");

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = await _jwtService.GenerateToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken(user);

            var accessExpiry = int.Parse(_config["Jwt:AccessTokenExpiryMinutes"]);
            var refreshExpiry = int.Parse(_config["Jwt:RefreshTokenExpiryDays"]);


            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshExpiry);

            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(accessExpiry),
                RefreshTokenExpiry = user.RefreshTokenExpiryTime
            };
        }

        
    }
}
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, string UserId)> RegisterAsync(string email, string password, string fullName);
        Task<AuthResponseDto?> LoginAsync(string email, string password);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

    }
}

using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GenerateToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken(ApplicationUser user);
    }
}

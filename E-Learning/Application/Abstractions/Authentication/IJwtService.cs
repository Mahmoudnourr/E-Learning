using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions.Authentication
{
    public interface IJwtService
    {
        string GenerateAccessToken(
        Guid userId,
        string email,
        IEnumerable<string> roles);

        string GenerateRefreshToken();

        string HashRefreshToken(
            string refreshToken);
    }
}
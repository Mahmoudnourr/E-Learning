using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Authentication;

namespace Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        public string GenerateAccessToken(Guid userId, string email, IEnumerable<string> roles)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public string HashRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.RefreshToken;
using Domain.Enums;
namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }
        private readonly List<RefreshToken.RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken.RefreshToken> RefreshTokens
            => _refreshTokens.AsReadOnly();
        private User() { }

        public static User Create(string name, string email, UserRole role)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Role = role
            };
        }

        public void AddRefreshToken(RefreshToken.RefreshToken token)
        {
            var oldTokens = _refreshTokens
                .Where(t => !t.IsActive)
                .ToList();

            foreach (var old in oldTokens)
                _refreshTokens.Remove(old);

            _refreshTokens.Add(token);
        }

        public RefreshToken.RefreshToken? GetActiveRefreshToken(string tokenHash)
        {
            return _refreshTokens
                .FirstOrDefault(t => t.TokenHash == tokenHash && t.IsActive);
        }


       public void UpdateProfile(
       string name,
       string email)
        {
            Name = name;
            Email = email;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
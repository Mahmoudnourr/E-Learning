using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.RefreshToken
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string TokenHash { get; private set; }

        public DateTime ExpiresAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool IsActive =>
            RevokedAt == null &&
            ExpiresAt > DateTime.UtcNow;

        private RefreshToken() { }

        public static RefreshToken Create(
            Guid userId,
            string tokenHash,
            DateTime expiresAt)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TokenHash = tokenHash,
                ExpiresAt = expiresAt
            };
        }

        public void Revoke()
        {
            RevokedAt = DateTime.UtcNow;
        }
    }
}
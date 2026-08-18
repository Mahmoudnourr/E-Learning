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

    private readonly List<RefreshToken.RefreshToken> _refreshTokens = new();

    public IReadOnlyCollection<RefreshToken.RefreshToken> RefreshTokens
        => _refreshTokens.AsReadOnly();

    private User() { }

    public static User Create(
        Guid id,
        string name,
        string email)
    {
        return new User
        {
            Id = id,
            Name = name,
            Email = email
        };
    }

    public void AddRefreshToken(RefreshToken.RefreshToken token)
    {
        _refreshTokens.Add(token);
    }

    public RefreshToken.RefreshToken? GetActiveRefreshToken(
        string tokenHash)
    {
        return _refreshTokens.FirstOrDefault(
            x => x.TokenHash == tokenHash &&
                 x.IsActive);
    }
}
}
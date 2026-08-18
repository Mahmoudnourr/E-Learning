using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions.Authentication
{
    public interface IIdentityService
    {
        Task<Guid> CreateUserAsync(Guid userId,
        string email,
        string password,
        Guid DomainUserId,
        CancellationToken cancellationToken);

    Task<bool> CheckPasswordAsync(
        Guid userId,
        string password,
        CancellationToken cancellationToken);
    }
}
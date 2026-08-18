using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationDbContext _context;

    public UserRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(
            user,
            cancellationToken);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
    }
}
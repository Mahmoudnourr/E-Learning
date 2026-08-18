using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
   public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync(
        CancellationToken cancellationToken)
    {
        if (_transaction is not null)
            return;

        _transaction =
            await _context.Database.BeginTransactionAsync(
                cancellationToken);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task CommitTransactionAsync(
        CancellationToken cancellationToken)
    {
        if (_transaction is null)
            return;

        try
        {
            await _transaction.CommitAsync(
                cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(
        CancellationToken cancellationToken)
    {
        if (_transaction is null)
            return;

        try
        {
            await _transaction.RollbackAsync(
                cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
}
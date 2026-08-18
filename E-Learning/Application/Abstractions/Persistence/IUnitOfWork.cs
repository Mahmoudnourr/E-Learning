using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(
       CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);

        Task CommitTransactionAsync(
            CancellationToken cancellationToken);

        Task RollbackTransactionAsync(
            CancellationToken cancellationToken);
    }
}
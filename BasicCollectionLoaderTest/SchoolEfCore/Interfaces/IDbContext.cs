using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SchoolEfCore.Interfaces
{
    /// <summary>
    /// Not an official IDbContext. For some reason, EF Core team have decided not to implement one. Extracted from
    /// EF Core source code.
    /// </summary>
    public interface IDbContext
    {
        // Disabled generated code
#pragma warning disable SA1600 // Elements should be documented
        ChangeTracker ChangeTracker { get; }

        DbContextId ContextId { get; }

        DatabaseFacade Database { get; }

        IModel Model { get; }

        EntityEntry Add([NotNull] object entity);

        EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        ValueTask<EntityEntry> AddAsync([NotNull] object entity, CancellationToken cancellationToken = default);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>([NotNull] TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;

        void AddRange([NotNull] IEnumerable<object> entities);

        void AddRange([NotNull] params object[] entities);

        Task AddRangeAsync([NotNull] IEnumerable<object> entities, CancellationToken cancellationToken = default);

        Task AddRangeAsync([NotNull] params object[] entities);

        EntityEntry Attach([NotNull] object entity);

        EntityEntry<TEntity> Attach<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        void AttachRange([NotNull] IEnumerable<object> entities);

        void AttachRange([NotNull] params object[] entities);

        void Dispose();

        ValueTask DisposeAsync();

        EntityEntry Entry([NotNull] object entity);

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        bool Equals(object obj);

        object Find([NotNull] Type entityType, [MaybeNull] params object[] keyValues);

        TEntity Find<TEntity>([MaybeNull] params object[] keyValues)
            where TEntity : class;

        ValueTask<object> FindAsync([NotNull] Type entityType, [MaybeNull] object[] keyValues, CancellationToken cancellationToken);

        ValueTask<object> FindAsync([NotNull] Type entityType, [MaybeNull] params object[] keyValues);

        ValueTask<TEntity> FindAsync<TEntity>([MaybeNull] object[] keyValues, CancellationToken cancellationToken)
            where TEntity : class;

        ValueTask<TEntity> FindAsync<TEntity>([MaybeNull] params object[] keyValues)
            where TEntity : class;

        int GetHashCode();

        EntityEntry Remove([NotNull] object entity);

        EntityEntry<TEntity> Remove<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        void RemoveRange([NotNull] IEnumerable<object> entities);

        void RemoveRange([NotNull] params object[] entities);

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        string ToString();

        EntityEntry Update([NotNull] object entity);

        EntityEntry<TEntity> Update<TEntity>([NotNull] TEntity entity)
            where TEntity : class;

        void UpdateRange([NotNull] IEnumerable<object> entities);

        void UpdateRange([NotNull] params object[] entities);
#pragma warning restore SA1600 // Elements should be documented
    }
}

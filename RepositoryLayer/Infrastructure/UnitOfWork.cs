using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YungchingDemo.RepositoryLayer.Infrastructure
{
    public class UnitOfWork<TContext>: IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private Dictionary<Type, object> _generalRepositories;
        private bool _disposed = false;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the db context.
        /// </summary>
        /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
        protected TContext Context => _context;

        /// <summary>
        /// Saves all changes made in this context to the source.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if sayve changes ensure auto record the change history.</param>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the source.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous save operation.</returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets the general repository for the <typeparamref name="TEntity"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IEFRepository{TEntity}"/> interface.</returns>
        public IRepository<TEntity> GetGeneralRepository<TEntity>() where TEntity : class
        {
            if (_generalRepositories == null)
            {
                _generalRepositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);

            if (!_generalRepositories.ContainsKey(type))
            {
                _generalRepositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)_generalRepositories[type];
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (_generalRepositories != null)
                    {
                        _generalRepositories.Clear();
                    }

                    // dispose the db context.
                    if (_context != null)
                    {
                        _context.Dispose();
                    }
                }
            }

            _disposed = true;
        }

        /// <summary>
        /// Recover all uncommitted changes.
        /// </summary>
        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}

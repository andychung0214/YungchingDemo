using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YungchingDemo.RepositoryLayer.Infrastructure
{
    public interface IUnitOfWork<TContext>
    {
        /// <summary>
        /// Saves all changes made in this context to the source.
        /// </summary>
        /// <param name="ensureAutoHistory"><c>True</c> if sayve changes ensure auto record the change history.</param>
        void Commit();

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the source.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous save operation.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

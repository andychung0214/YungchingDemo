using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace YungchingDemo.RepositoryLayer.Infrastructure
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets all <see cref="TEntity"/>s.
        /// </summary>
        /// <returns>The items.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Asynchronously gets all <see cref="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous save operation. That contains all <see cref="TEntity"/>s.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Finds the <see cref="TEntity"/>s based on a filter, order by delegate and page information.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="orderBy">A function to order <see cref="TEntity"/>s.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <param name="pageIndex">The number of current paged index. If the value is 0, it will not be paged.</param>
        /// <param name="pageSize">The number of the <see cref="TEntity"/>s of every page.</param>
        /// <returns>The results of <see cref="TEntity"/>.</returns>
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageIndex = 0, int pageSize = 10, string includeProperties = "");

        /// <summary>
        /// Asynchronously finds the <see cref="TEntity"/>s based on a filter, order by delegate and page information.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="orderBy">A function to order <see cref="TEntity"/>s.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <param name="pageIndex">The number of current paged index. If the value is 0, it will not be paged.</param>
        /// <param name="pageSize">The number of the <see cref="TEntity"/>s of every page.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous save operation. That contains all <see cref="TEntity"/>s.</returns>
        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageIndex = 0, int pageSize = 10, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets single <see cref="TEntity"/> based on a filter.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <returns>The <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        TEntity Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> based on a filter.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to insert.</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Asynchronously inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to insert.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">The <see cref="TEntity"/>s to insert.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes the specified <see cref="TEntity"/> by primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to delete.</param>
        void Delete(TEntity entity);
    }
}

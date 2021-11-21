using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace YungchingDemo.RepositoryLayer.Infrastructure
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected DbContext Context
        {
            get
            {
                return _context;
            }
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _dbSet;
            }
        }

        public Repository(DbContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets all <see cref="TEntity"/>s.
        /// </summary>
        /// <returns>The items.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Asynchronously gets all <see cref="TEntity"/>.
        /// </summary>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous save operation. That contains all <see cref="TEntity"/>s.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Finds the <see cref="TEntity"/>s based on a filter, order by delegate and page information.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="orderBy">A function to order <see cref="TEntity"/>s.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <param name="pageIndex">The number of current paged index. If the value is 0, it will not be paged.</param>
        /// <param name="pageSize">The number of the <see cref="TEntity"/>s of every page.</param>
        /// <returns>The results of <see cref="TEntity"/>.</returns>
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageIndex = 0, int pageSize = 10, string includeProperties = "")
        {
            IQueryable<TEntity> query = FindByImplementor(filter, orderBy, includeProperties, pageIndex, pageSize);

            return query;
        }

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
        public async Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int pageIndex = 0, int pageSize = 10, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> queryAsync = await Task.Run<IQueryable<TEntity>>(() =>
            {
                IQueryable<TEntity> query = FindByImplementor(filter, orderBy, includeProperties, pageIndex, pageSize);

                return query;
            });

            return queryAsync;
        }

        /// <summary>
        /// Gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The <see cref="TEntity"/> or null.</returns>
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Gets single <see cref="TEntity"/> based on a filter.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <returns>The <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = FindByImplementor(filter, null, includeProperties, 0, 0);

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Asynchronously gets single <see cref="TEntity"/> based on a filter.
        /// </summary>
        /// <param name="filter">A functin to test each <see cref="TEntity"/> for a condition.</param>
        /// <param name="includeProperties">The string contains the navigation property names to be included in query results, a string of '.' separated navigation property names to be included. Every navigation property name is concated by comma.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>A <see cref="Task"/> that contains the <see cref="TEntity"/> if the entity is existing, otherwise null.</returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = FindByImplementor(filter, null, includeProperties, 0, 0);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to insert.</param>
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">The <see cref="TEntity"/>s to insert.</param>
        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Asynchronously inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to insert.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Inserts a new <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">The <see cref="TEntity"/>s to insert.</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Updates the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to update.</param>
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">The <see cref="TEntity"/> to update.</param>
        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Deletes the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entity">The <see cref="TEntity"/> to delete.</param>
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes the specified <see cref="TEntity"/>.
        /// </summary>
        /// <param name="entities">The <see cref="TEntity"/> to delete.</param>
        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Deletes the specified <see cref="TEntity"/> by primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        public void Delete(object id)
        {
            TEntity entity = GetById(id);
            Delete(entity);
        }

        /// <summary>
        /// 取得-Entity串列資料
        /// </summary>
        /// <param name="filter">查詢條件資料</param>
        /// <param name="subSetNames">子查詢名稱串列資料</param>
        /// <returns>Entity串列資料</returns>
        public async Task<List<TEntity>> GetsByFilterAsync(Expression<Func<TEntity, bool>> filter, List<string> subSetNames = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (subSetNames != null)
            {
                subSetNames.ForEach(x => query = query.Include(x));
            }

            return await query.Where(filter).ToListAsync();
        }

        #region Private Methods

        private IQueryable<TEntity> FindByImplementor(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, int pageIndex, int pageSize)
        {
            IQueryable<TEntity> query = _dbSet;

            // Filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Related properties
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // Order
            if (orderBy != null)
            {
                query = orderBy(query).AsQueryable();
            }

            // Paging
            if (pageIndex > 0 && pageSize > 0)
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }

            return query;
        }

        #endregion
    }
}

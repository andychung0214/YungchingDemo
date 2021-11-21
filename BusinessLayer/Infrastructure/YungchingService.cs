using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using YungchingDemo.RepositoryLayer.Infrastructure;

namespace YungchingDemo.BusinessLayer.Infrastructure
{
    public class YungchingService<TContext, TUnitOfWork> : IService<TContext, TUnitOfWork> where TContext : DbContext where TUnitOfWork : UnitOfWork<TContext>
    {
        private bool _disposed = false;

        protected TContext _context;
        protected TContext _readOnlyContext;
        protected TUnitOfWork _unitOfWork;
        protected TUnitOfWork _readOnlyUnitOfWork;

        public string ConnectionString { get; set; }

        protected TContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = CreateContext();
                }

                return _context;
            }
        }

        public TUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = (TUnitOfWork)Activator.CreateInstance(typeof(TUnitOfWork), Context);
                }

                return _unitOfWork;
            }
        }

        private TContext CreateContext()
        {
            TContext context = null;
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(ConnectionString);

            context = (TContext)Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);

            return context;
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
                    // dispose the db context.
                    if (_context != null)
                    {
                        _context.Dispose();
                    }

                    if (_readOnlyContext != null)
                    {
                        _readOnlyContext.Dispose();
                    }
                }
            }

            _disposed = true;
        }

        public TUnitOfWork GetOnceUnitOfWork()
        {
            TContext context = CreateContext();
            TUnitOfWork unitOfWork = (TUnitOfWork)Activator.CreateInstance(typeof(TUnitOfWork), context);

            return unitOfWork;
        }

    }
}

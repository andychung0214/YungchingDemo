using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using YungchingDemo.RepositoryLayer.Infrastructure;

namespace YungchingDemo.BusinessLayer.Infrastructure
{
    public interface IService<TContext, TUnitOfWork> where TContext : DbContext where TUnitOfWork : UnitOfWork<TContext>
    {
        string ConnectionString { get; set; }
        TUnitOfWork UnitOfWork { get; }
    }
}

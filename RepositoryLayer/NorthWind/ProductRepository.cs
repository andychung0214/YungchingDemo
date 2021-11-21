using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.RepositoryLayer.Infrastructure;

namespace YungchingDemo.RepositoryLayer.NorthWind
{
    public class ProductRepository: Repository<Product>
    {
        public ProductRepository(DbContext dbContext): base(dbContext)
        {

        }

        public List<Product> FindAllProduct(string keyword)
        {
            IQueryable<Product> query = FindAllProductImplement(keyword);
            return query.ToList();
        }

        public async Task<List<Product>> FindAllProductAsync(string keyword)
        {
            IQueryable<Product> query = FindAllProductImplement(keyword);
            return await query.ToListAsync();
        }


        public IQueryable<Product> FindAllProductImplement(string keyword)
        {
            IQueryable<Product> query = DbSet.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.ProductName.Contains(keyword));
            }

            return query;
        }
    }
}

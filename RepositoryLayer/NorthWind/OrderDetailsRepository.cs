using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.RepositoryLayer.Infrastructure;

namespace YungchingDemo.RepositoryLayer.NorthWind
{
    public class OrderDetailsRepository: Repository<OrderDetail>
    {
        public OrderDetailsRepository(DbContext dbContext): base(dbContext)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.BusinessLayer.Infrastructure;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;
using YungchingDemo.RepositoryLayer.NorthWind;

namespace YungchingDemo.BusinessLayer.NorthWind
{
    public interface INorthWindService: IService<NorthwindContext, NorthWindUnitOfWork>
    {
        List<ProductModel> GetAllProducts(string keyword);

        IQueryable<Product> GetIqueryableAllProducts(string keyword);
    }
}

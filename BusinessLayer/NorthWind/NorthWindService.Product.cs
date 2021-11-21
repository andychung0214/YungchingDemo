using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;

namespace YungchingDemo.BusinessLayer.NorthWind
{
    public partial class NorthWindService
    {
        public List<ProductModel> GetAllProducts(string keyword)
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            var products = ProductRepository.FindAllProduct(keyword);

            foreach (var item in products)
            {
                var productItem = Mapper.Map<ProductModel>(item);
                listProduct.Add(productItem);
            }

            return listProduct;
        }

        public IQueryable<Product> GetIqueryableAllProducts(string keyword)
        {
            IQueryable<Product> dbProduct  = ProductRepository.FindAllProductImplement(keyword);

            return dbProduct;
        }
    }
}

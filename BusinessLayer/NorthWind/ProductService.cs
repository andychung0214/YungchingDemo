using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;

namespace YungchingDemo.BusinessLayer.NorthWind
{
    public class ProductService: BaseService
    {
        /// <summary>
        /// Northwind Context
        /// </summary>
        NorthwindContext NorthwindContext { get; set; }

        /// <summary>
        /// 設定檔
        /// </summary>
        IConfiguration Config { get; }

        /// <summary>
        /// Auto Mapper
        /// </summary>
        IMapper Mapper { get; }

        public ProductService(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper)
        {
            NorthwindContext = northwindContext;
            Config = configuration;
            Mapper = mapper;
        }

        public IQueryable<Product> GetAllProducts(string keyword)
        {
            IQueryable<Product> products;

            if (!string.IsNullOrEmpty(keyword))
            {
                products = NorthwindContext.Products.Where(o => o.ProductName.Contains(keyword));
            }
            else
            {
                products = NorthwindContext.Products;
            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = NorthwindContext.Products.Where(o => o.ProductId == productId).FirstOrDefault();
            return product;
        }
    }
}

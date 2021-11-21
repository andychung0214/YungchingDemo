using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;

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

        public HttpResponseMessage CreateProduct(HttpResponseMessage response, ProductModel productInfo)
        {
            var product = Mapper.Map<Product>(productInfo);

            try
            {
                NorthwindContext.Products.Add(product);
                NorthwindContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return response;
        }


        public async Task UpdateProduct(int productID, ProductModel productInfo)
        {
            var productEntitiy = NorthwindContext.Products.Where(o => o.ProductId == productID).FirstOrDefault();

            if (productEntitiy == null)
            {
                throw new InvalidOperationException($"Connot find the article with product id: {productID}");
            }
            else
            {
                try
                {
                    productEntitiy.ProductName = productInfo.ProductName;
                    productEntitiy.Discontinued = productInfo.Discontinued;
                    productEntitiy.QuantityPerUnit = productInfo.QuantityPerUnit;
                    productEntitiy.UnitPrice = productInfo.UnitPrice;
                    productEntitiy.UnitsInStock = productInfo.UnitsInStock;
                    productEntitiy.UnitsOnOrder = productInfo.UnitsOnOrder;
                    productEntitiy.ReorderLevel = productInfo.ReorderLevel;
                    productEntitiy.Discontinued = productInfo.Discontinued;

                    NorthwindContext.Products.Update(productEntitiy);
                    await NorthwindContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task DeleteProductBy(int productId)
        {
            var productEntitiy = NorthwindContext.Products.Where(a => a.ProductId == productId).FirstOrDefault();

            try
            {
                if (productEntitiy == null)
                {
                    throw new InvalidOperationException($"Connot find the article with article id: {productId}");
                }
                else
                {
                    NorthwindContext.Products.Remove(productEntitiy);
                    await NorthwindContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

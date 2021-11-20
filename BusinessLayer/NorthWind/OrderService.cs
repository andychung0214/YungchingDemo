using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;

namespace YungchingDemo.BusinessLayer.NorthWind
{
    public class OrderService: BaseService
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

        public OrderService(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper)
        {
            NorthwindContext = northwindContext;
            Config = configuration;
            Mapper = mapper;
        }

        public IQueryable<Order> GetAllOrders(string keyword)
        {
            IQueryable<Order> orders;
            if (!string.IsNullOrEmpty(keyword))
            {
                orders = NorthwindContext.Orders.Where(o => o.CustomerId.Contains(keyword));
            }
            else
            {
                orders = NorthwindContext.Orders;
            }
            return orders;
        }

        public OrderDetail GetOrderById(int orderId)
        {
            OrderDetail order = NorthwindContext.OrderDetails.Where(o => o.OrderId == orderId).FirstOrDefault();
            return order;
        }

        public async Task UpdateOrder(int orderID, OrderDetailModel orderInfo)
        {
            var orderEntitiy = NorthwindContext.OrderDetails.Where(o => o.OrderId == orderID).FirstOrDefault();

            if (orderEntitiy == null)
            {
                throw new InvalidOperationException($"Connot find the article with order id: {orderID}");
            }
            else
            {
                try
                {
                    orderEntitiy.UnitPrice = orderInfo.UnitPrice;
                    orderEntitiy.Quantity = orderInfo.Quantity;
                    orderEntitiy.Discount = orderInfo.Discount;

                    NorthwindContext.OrderDetails.Update(orderEntitiy);
                    await NorthwindContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

    }
}

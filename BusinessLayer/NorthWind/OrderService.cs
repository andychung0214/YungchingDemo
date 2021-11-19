using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;

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

        public IQueryable<Order> GetAllOrders()
        {
            IQueryable<Order> orders = NorthwindContext.Orders;
            return orders;
        }

        public OrderDetail GetOrderById(int orderId)
        {
            OrderDetail order = NorthwindContext.OrderDetails.Where(o => o.OrderId == orderId).FirstOrDefault();
            return order;
        }

    }
}

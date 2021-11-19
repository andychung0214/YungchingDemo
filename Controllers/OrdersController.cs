using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.BusinessLayer.NorthWind;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;

namespace YungchingDemo.Controllers
{
    public class OrdersController : Controller
    {
        OrderService OrderService { get; }

        /// <summary>
        /// Northwind Context
        /// </summary>
        private readonly NorthwindContext _context;

        /// <summary>
        /// 設定檔
        /// </summary>
        IConfiguration Config { get; }

        /// <summary>
        /// Auto Mapper
        /// </summary>
        IMapper Mapper { get; }
        public OrdersController(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper)
        {
            OrderService = new OrderService(northwindContext, configuration, mapper);
            _context = northwindContext;
            Config = configuration;
            Mapper = mapper;
        }

        // GET: OrdersController
        public ActionResult Index()
        {
            OrderModel vm = new OrderModel();
            List<OrderModel> orders = new List<OrderModel>();
            var dbProducts = OrderService.GetAllOrders();

            foreach (var item in dbProducts)
            {
                var orderItem = Mapper.Map<OrderModel>(item);
                orders.Add(orderItem);
            }

            vm.Orders = orders;

            return View(vm);
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

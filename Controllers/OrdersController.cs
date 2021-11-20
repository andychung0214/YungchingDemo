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
        public async Task<IActionResult> Index(string keyword, int? pageNumber, int pageSize = 0)
        {
            OrderModel vm = new OrderModel();
            List<OrderModel> orders = new List<OrderModel>();
            IQueryable<Order> dbOrders = OrderService.GetAllOrders(keyword);
            var tmpTotalCount = dbOrders.ToList().Count;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            if (pageSize == 0)
            {
                pageSize = 8;
            }
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    
            //}
            //else
            //{
            //    vm = GetOrders(1);
            //}

            PaginatedList<Order> paginatedList = await PaginatedList<Order>.CreateAsync(dbOrders, pageNumber ?? 1, pageSize);
            List<OrderModel> orderModels = new List<OrderModel>();


            foreach (var item in paginatedList)
            {
                var orderItem = Mapper.Map<OrderModel>(item);
                orders.Add(orderItem);
            }

            vm.Orders = orders;
            ViewBag.TotalPageCount = (tmpTotalCount / pageSize) + (tmpTotalCount % pageSize == 0 ? 0 : 1);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            return View(this.GetOrders(currentPageIndex));
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            OrderDetailModel vm = new OrderDetailModel();
            OrderDetail orderDetail = OrderService.GetOrderById(id);

            vm = Mapper.Map<OrderDetailModel>(orderDetail);

            return View(vm);
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            OrderDetailModel vm = new OrderDetailModel();

            return View(vm);
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
            OrderDetailModel vm = new OrderDetailModel();
            var orderDetail = OrderService.GetOrderById(id);

            vm = Mapper.Map<OrderDetailModel>(orderDetail);
            return View(vm);
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

        private OrderModel GetOrders(int currentPage)
        {
            int maxRows = 10;
            OrderModel OrderModel = new OrderModel();

            var dbOrders = (from order in _context.Orders
                              select order)
                        .OrderBy(order => order.OrderId)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            List<OrderModel> listOrder = new List<OrderModel>();

            foreach (var item in dbOrders)
            {
                var orderItem = Mapper.Map<OrderModel>(item);
                listOrder.Add(orderItem);
            }

            OrderModel.Orders = listOrder;

            double pageCount = (double)((decimal)_context.Orders.Count() / Convert.ToDecimal(maxRows));
            OrderModel.PageCount = (int)Math.Ceiling(pageCount);

            OrderModel.CurrentPageIndex = currentPage;

            return OrderModel;
        }
    }
}

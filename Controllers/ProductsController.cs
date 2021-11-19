using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ProductsController : Controller
    {
        ProductService ProductService { get; }

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

        public ProductsController(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper)
        {
            ProductService = new ProductService(northwindContext, configuration, mapper);

            _context = northwindContext;
            Config = configuration;
            Mapper = mapper;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            ProductModel vm = new ProductModel();
            List<ProductModel> products = new List<ProductModel>();
            var dbProducts = ProductService.GetAllProducts();

            foreach (var item in dbProducts)
            {
                var productItem = Mapper.Map<ProductModel>(item);
                products.Add(productItem);
            }

            vm.Products = products;

            return View(vm);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
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

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController/Delete/5
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

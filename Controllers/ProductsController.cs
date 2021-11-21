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
        private INorthWindService NorthWindService { get; set; }

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

        public ProductsController(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper, INorthWindService northWindService)
        {
            ProductService = new ProductService(northwindContext, configuration, mapper);
            
            _context = northwindContext;
            Config = configuration;
            Mapper = mapper;

            NorthWindService = northWindService;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index(string keyword, int? pageNumber, int pageSize = 0)
        {
            ViewBag.CurrentPageIndex = pageNumber <= 1 ? 1 : pageNumber;

            ProductModel vm = new ProductModel();
            List<ProductModel> products = new List<ProductModel>();

            var dbProducts = NorthWindService.GetIqueryableAllProducts(keyword);
            
            
            var tmpTotalCount = dbProducts.ToList().Count;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            if (pageSize == 0)
            {
                pageSize = 8;
            }


            PaginatedList<Product> paginatedList = await PaginatedList<Product>.CreateAsync(dbProducts, pageNumber ?? 1, pageSize);
            List<ProductModel> productModels = new List<ProductModel>();


            foreach (var item in paginatedList)
            {
                var orderItem = Mapper.Map<ProductModel>(item);
                products.Add(orderItem);
            }

            vm.Products = products;
            ViewBag.TotalPageCount = (tmpTotalCount / pageSize) + (tmpTotalCount % pageSize == 0 ? 0 : 1);


            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            return View(this.GetProducts(currentPageIndex));
        }



        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            ProductModel vm = new ProductModel();
            var dbProduct = ProductService.GetProductById(id);
            ProductModel product = Mapper.Map<ProductModel>(dbProduct);
            vm = product;

            return View(vm);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ProductModel vm = new ProductModel();
            string keyword = string.Empty;
            var lateastProductID = _context.Products.Select(o => o.ProductId).Max();
            ViewBag.addProductID = lateastProductID + 1 ;


            return View(vm);
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
            ProductModel vm = new ProductModel();
            var dbProduct = ProductService.GetProductById(id);
            vm = Mapper.Map<ProductModel>(dbProduct);

            return View(vm);
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
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            try
            {
                await ProductService.DeleteProductBy(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
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

        private ProductModel GetProducts(int currentPage)
        {
            int maxRows = 10;
            ProductModel productModel = new ProductModel();

            var dbProducts = (from product in _context.Products
                              select product)
                        .OrderBy(product => product.ProductId)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

            List<ProductModel> listProduct = new List<ProductModel>();

            foreach (var item in dbProducts)
            {
                var productItem = Mapper.Map<ProductModel>(item);
                listProduct.Add(productItem);
            }

            productModel.Products = listProduct;

            double pageCount = (double)((decimal)_context.Products.Count() / Convert.ToDecimal(maxRows));
            productModel.PageCount = (int)Math.Ceiling(pageCount);

            productModel.CurrentPageIndex = currentPage;

            return productModel;
        }
    }
}

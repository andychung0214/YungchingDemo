using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YungchingDemo.BusinessLayer.NorthWind;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.Models.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YungchingDemo.Controllers
{
    [Route("api/web")]
    public class WebsiteApiController : ControllerBase
    {

        ProductService ProductService { get; }
        OrderService OrderService { get; }

        IMapper _mapper { get; }


        public WebsiteApiController(NorthwindContext northwindContext, IConfiguration configuration, IMapper mapper)
        {
            ProductService = new ProductService(northwindContext, configuration, mapper);
            OrderService = new OrderService(northwindContext, configuration, mapper);
            _mapper = mapper;
        }

        #region Product API
        // GET: api/web
        [HttpGet]
        [Route("product")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/web/5
        [HttpGet]
        [Route("product/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/web
        [HttpPost]
        [Route("product")]
        public async Task<ActionResult> Post(ProductModel requestBody)
        {
            if (requestBody == null || !ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status404NotFound, requestBody);
            }

            try
            {
                HttpResponseMessage response = new HttpResponseMessage();
                HttpResponseMessage product = ProductService.CreateProduct(response, requestBody);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT api/web/5
        [HttpPut]
        [Route("product/{id}")]
        public async Task<ActionResult> Put(int id, ProductModel requestBody)
        {
            if (requestBody == null || !ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status404NotFound, requestBody);
            }

            try
            {
                await ProductService.UpdateProduct(id, requestBody);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // DELETE api/web/5
        //[HttpDelete]
        //[Route("product/{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        await ProductService.DeleteProductBy(id);
        //        return StatusCode(StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex);
        //    }
        //}

        #endregion

        // PUT api/web/5
        [HttpPut]
        [Route("order/{id}")]
        public async Task<ActionResult> PutOrder(int id, OrderDetailModel requestBody)
        {
            if (requestBody == null || !ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status404NotFound, requestBody);
            }

            try
            {
                await OrderService.UpdateOrder(id, requestBody);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

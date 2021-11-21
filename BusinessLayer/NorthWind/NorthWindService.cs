using YungchingDemo.BusinessLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using YungchingDemo.RepositoryLayer.NorthWind;
using YungchingDemo.DataLayer.NorthWind;
using AutoMapper;

namespace YungchingDemo.BusinessLayer.NorthWind
{
    public partial class NorthWindService : YungchingService<NorthwindContext, NorthWindUnitOfWork>, INorthWindService
    {
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        IMapper Mapper { get; }

        private OrderDetailsRepository OrderDetailsRepository
        {
            get
            {
                return UnitOfWork.OrderDetailsRepository;
            }
        }

        private OrderRepository OrderRepository
        {
            get
            {
                return UnitOfWork.OrderRepository;
            }
        }

        private ProductRepository ProductRepository
        {
            get
            {
                return UnitOfWork.ProductRepository;
            }
        }

        public NorthWindService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            ConnectionString = configuration.GetConnectionString("NorthWind");
        }
    }
}

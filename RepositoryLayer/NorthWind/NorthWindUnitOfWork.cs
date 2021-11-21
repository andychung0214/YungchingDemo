using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungchingDemo.DataLayer.NorthWind;
using YungchingDemo.RepositoryLayer.Infrastructure;

namespace YungchingDemo.RepositoryLayer.NorthWind
{
    public class NorthWindUnitOfWork: UnitOfWork<NorthwindContext>
    {
        private NorthwindContext _northwindContext;
        private OrderDetailsRepository _orderDetailsRepository;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;

        public NorthWindUnitOfWork(NorthwindContext dbContext) : base(dbContext)
        {
            _northwindContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public OrderDetailsRepository OrderDetailsRepository
        {
            get
            {
                if (_orderDetailsRepository == null)
                {
                    _orderDetailsRepository = new OrderDetailsRepository(_northwindContext);
                }

                return _orderDetailsRepository;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_northwindContext);
                }

                return _orderRepository;
            }
        }

        public ProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_northwindContext);
                }

                return _productRepository;
            }
        }


    }
}

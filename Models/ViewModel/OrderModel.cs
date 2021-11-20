using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YungchingDemo.Models.ViewModel
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        //public virtual Customer Customer { get; set; }
        //public virtual Employee Employee { get; set; }
        //public virtual Shipper ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetailModel> OrderDetails { get; set; }

        public List<OrderModel> Orders { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }

    public class OrderDetailModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public virtual OrderModel Order { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}

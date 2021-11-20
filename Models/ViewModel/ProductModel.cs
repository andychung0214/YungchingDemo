using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YungchingDemo.Models.ViewModel
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        //public virtual Category Category { get; set; }
        //public virtual Supplier Supplier { get; set; }
        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}

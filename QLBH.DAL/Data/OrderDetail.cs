using System;
using System.Collections.Generic;

namespace QLBH.DAL.Data
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Food Food { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}

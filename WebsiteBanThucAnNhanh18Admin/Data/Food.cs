using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18Admin.Data
{
    public partial class Food
    {
        public Food()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Discount { get; set; }
        public string? Picture { get; set; }
        public short? Idx { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

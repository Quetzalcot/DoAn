using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18Admin.Data
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
        public string? Status { get; set; }
        public string? Payment { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public int? CouponId { get; set; }

        public virtual Branch Branch { get; set; } = null!;
        public virtual Coupon? Coupon { get; set; }
        public virtual CustomerAccount User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

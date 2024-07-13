using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18.Data
{
    public partial class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }

        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxUsage { get; set; }
        public decimal? DiscountAmount { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

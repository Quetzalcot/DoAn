namespace WebsiteBanThucAnNhanh18Admin.Models
{
    public class CouponModel
    {
        public int CouponId { get; set; }
        public string? CouponCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxUsage { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}

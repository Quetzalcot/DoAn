namespace WebsiteBanThucAnNhanh18Admin.Models
{
    public class OrderDetailsViewModel
    {
        public OrderModel Order { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public List<FoodsModel> Foods { get; set; }
        public List<CouponModel> Coupon { get; set; }

        public decimal? DiscountAmount { get; set; }
        public string? FoodName { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}

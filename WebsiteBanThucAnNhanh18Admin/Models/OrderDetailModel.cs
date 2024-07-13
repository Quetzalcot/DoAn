namespace WebsiteBanThucAnNhanh18Admin.Models
{
    public class OrderDetailModel
    {

        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}

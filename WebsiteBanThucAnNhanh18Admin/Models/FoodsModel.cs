using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanThucAnNhanh18Admin.Models
{
	public class FoodsModel
	{
		[DisplayName("Mã sản phẩm")]
		public int FoodId { get; set; }
		[DisplayName("Tên sản phẩm")]
		public string? FoodName { get; set; }
		public string? Description { get; set; }
		public decimal? Price { get; set; }
		public int? CategoryId { get; set; }
		public decimal? Discount { get; set; }
		[Required]
		public string? Picture { get; set; }
		public short? Idx { get; set; }

	}
}

using WebsiteBanThucAnNhanh18.Data;

namespace WebsiteBanThucAnNhanh18.ViewModels
{
	public class CartVM
	{
		
		public int? MaDa { get; set; }
		public string TenDa { get; set; }
		public string? HinhAnh { get; set; }
		public decimal? Gia { get; set; }
		public int SoLuong { get; set; }
		public decimal? ThanhTien => SoLuong * Gia;
		
	}
}

using System.ComponentModel.DataAnnotations;

namespace WebsiteBanThucAnNhanh18.ViewModels
{
    public class CheckoutVM
    {
        [Required(ErrorMessage = "Chưa chọn chi nhánh muốn đặt hàng")]
        public int? MaCN { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên")]
        public string? HoTen { get; set; }

        [Required(ErrorMessage = "Chưa nhập địa chỉ")]
        public string? DiaChi { get; set; }

        [Required(ErrorMessage = "Chưa nhập số điện thoại")]
        public string? DienThoai { get; set; }
        public decimal? ThanhTien { get; set; }
        public string? GhiChu { get; set; }
        public string ? ThanhToan { get; set; }
        public bool? CapNhat { get; set; }
    }
}

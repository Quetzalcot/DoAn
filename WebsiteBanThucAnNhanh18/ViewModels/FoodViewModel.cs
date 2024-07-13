using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebsiteBanThucAnNhanh18.ViewModels
{
    public class FoodViewModel
    {
        public int MaDA { get; set; }
        public string TenDA { get; set; }
       
        public decimal Gia { get; set; }
        public decimal GiamGia { get; set; }
        public string MoTa{ get; set; }
        public short ThuTu { get; set; }
        public string HinhAnh { get; set; }

        public int MaLoai { get; set; }
    }
}

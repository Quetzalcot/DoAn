using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.Helper;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.Controllers
{
    public class DoAnController : Controller
    {
		private readonly QLBHContext da;
        const string CART_KEY = "MYCART";
        public List<CartVM> Cart => HttpContext.Session.Get<List<CartVM>>(CART_KEY)
            ?? new List<CartVM>();
        public DoAnController(QLBHContext context)
        {
            da = context;
        }
		
        public IActionResult Index(int? loai, int ?sort_type)
        {

            if (Cart == null)
            {
                ViewBag.SL = 0;
            }
            ViewBag.SL = Cart.Count();
            //Kiểm tra loại hàng hóa có tồn lại ?
            var ThucDon = da.Foods.AsQueryable();
            if (loai.HasValue)
            {
				if (loai.Value == 0)
					return RedirectToAction("Index");
				ThucDon = ThucDon.Where(a => a.CategoryId ==  loai.Value);
			

			}
			
			

			IQueryable<FoodViewModel> kq;

			switch (sort_type)
            {
				case 1:
                    {
						kq = ThucDon.Select(p => new FoodViewModel
						{
							MaDA = p.FoodId,
							TenDA = p.FoodName ?? "",
							Gia = p.Price ?? 0,
							GiamGia = p.Discount ?? 0,
							HinhAnh = p.Picture ?? "",
							MoTa = p.Description ?? "",
							ThuTu = p.Idx ?? 0,
							MaLoai = p.CategoryId?? 0
						}).OrderBy(p=>(p.Gia*(1-p.GiamGia)));
						break;
                    }
                case 2:
                    {
						kq = ThucDon.Select(p => new FoodViewModel
						{
							MaDA = p.FoodId,
							TenDA = p.FoodName ?? "",
							Gia = p.Price ?? 0,
							GiamGia = p.Discount ?? 0,
							HinhAnh = p.Picture ?? "",
							MoTa = p.Description ?? "",
							ThuTu = p.Idx ?? 0,
                            MaLoai = p.CategoryId ?? 0
                        }).OrderByDescending(p => (p.Gia * (1 - p.GiamGia)));
						break;
					}
                default:
                    {
						kq = ThucDon.Select(p => new FoodViewModel
						{
							MaDA = p.FoodId,
							TenDA = p.FoodName ?? "",
							Gia = p.Price ?? 0,
							GiamGia = p.Discount ?? 0,
							HinhAnh = p.Picture ?? "",
							MoTa = p.Description ?? "",
							ThuTu = p.Idx ?? 0,
                            MaLoai = p.CategoryId ?? 0
                        }).OrderBy(p => p.MaLoai).ThenBy(p=>p.ThuTu);
						break;
					}
            }
           
            
            return View(kq);
        }
        public IActionResult Search(string? ten)
        {
			var ThucDon = da.Foods.AsQueryable();
			if (ten != "")
			{
				ThucDon = ThucDon.Where(a => a.FoodName.Contains(ten));

			}
			var kq = ThucDon.Select(p => new FoodViewModel
			{
				MaDA = p.FoodId,
				TenDA = p.FoodName ?? "",
				Gia = p.Price ?? 0,
				GiamGia = p.Discount ?? 0,
				HinhAnh = p.Picture ?? "",
				MoTa = p.Description ?? "",
				ThuTu = p.Idx ?? 0
			}).OrderBy(p=> p.ThuTu);

			return View(kq);
		}
    }
}

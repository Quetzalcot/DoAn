using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.Helper;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.Controllers
{
	public class CartController : Controller
	{
		private readonly QLBHContext da;

		public CartController(QLBHContext context)
		{
			da = context;
		}
		const string CART_KEY = "MYCART";
		public List<CartVM> Cart => HttpContext.Session.Get<List<CartVM>>(CART_KEY)
			?? new List<CartVM>();

		public IActionResult ListCart()
		{
			if (Cart == null)
			{
				ViewBag.SL = 0;
			}
			ViewBag.SL = Cart.Count();
			
			return View(Cart);
		}

		public IActionResult AddToCart(int id, int quantity = 1)
		{
			var gioHang = Cart;
			//Kiểm tra giỏ hàng có gì hay chưa
			var item = gioHang.SingleOrDefault(p => p.MaDa == id);
			if (item == null)
			{
				var monAn = da.Foods.SingleOrDefault(p => p.FoodId == id);
				if (monAn == null)
				{
					TempData["Message"] = $"Không tìm thấy món ăn có mã {id}";
					return Redirect("/404");
				}
				item = new CartVM
				{
					MaDa = monAn.FoodId,
					TenDa = monAn.FoodName,
					HinhAnh = monAn.Picture ?? "",
					Gia = monAn.Price * (1 - monAn.Discount) ?? 0,
					SoLuong = quantity
				};

				gioHang.Add(item);
			}
			else
			{
				item.SoLuong += quantity;
			}

			HttpContext.Session.Set(CART_KEY, gioHang);
			return RedirectToAction("ListCart");
		}

		public IActionResult RemoveCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaDa == id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(CART_KEY, gioHang);
			}
			return RedirectToAction("ListCart");
		}
		[HttpPost]
		public IActionResult UpdateQuantity(int quantity, string op, int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.MaDa == id);
			try
			{
				// Thực hiện xử lý dựa trên 'operation' cập nhật số lượng
				if (op == "plus")
				{
					// Cộng thêm 1 vào số lượng
					item.SoLuong++;

				}
				else if (op == "minus")
				{
					// Đảm bảo số lượng không nhỏ hơn 0
					if (item.SoLuong > 0)
					{
						item.SoLuong--;
					}
				}
				HttpContext.Session.Set(CART_KEY, gioHang);
			}
			catch (Exception ex)
			{
				return RedirectToAction("ListCart");
			}
			// Sau khi cập nhật render lại trang hiện tại
			return RedirectToAction("ListCart");
		}
     
        public IActionResult GetVoucher(string code)
		{
			
			var voucher = da.Coupons.SingleOrDefault(p => p.CouponCode == code);
			var today = DateTime.Now;
			
			if (voucher == null || voucher.StartDate > today || voucher.EndDate < today || voucher.MaxUsage == 0)
                TempData["VoucherState"] = " Mã giảm không hợp lệ!";
			else
			{
                TempData["VoucherState"] = " Áp mã thành công!";
				//Không xài ViewBag vì sau khi RedirectToAction sẽ mất
                TempData["Voucher"] = voucher.DiscountAmount.ToString();
				TempData["VoucherID"] = voucher.CouponId;


			}
            return RedirectToAction("ListCart");

        }
        

        [HttpGet]
		public IActionResult Checkout(int vid)
		{

            var voucher = da.Coupons.SingleOrDefault(p => p.CouponId == vid);
			if (voucher != null)
			{
                TempData["VID"] = vid;
                TempData["Voucher"] = voucher.DiscountAmount;
            }
            
            ViewData["CN"] = da.Branches;
		
			if (Cart.Count == 0)
			{
				return Redirect("/");
			}
			//lấy mã đơn hàng gần đây nhất + 1
			var latestOrder = da.Orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();
			ViewBag.LatestOrder = latestOrder != null ? (latestOrder.OrderId + 1).ToString() : "1";
			//
			var username = HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Username").Value;
			CustomerAccount cus = da.CustomerAccounts.FirstOrDefault(p => p.Username == username);
			TempData["FullName"] = cus.Fullname ?? "";
			TempData["AddRess"] = cus.Address ?? "";
			TempData["Phone"] = cus.Phone ?? "";
			return View(Cart);
		}
		[HttpPost]
		public IActionResult Checkout(CheckoutVM vm)
		{

			if (ModelState.IsValid)
			{

				var username = HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Username").Value;

				var order = new Order()
				{
					UserId = da.CustomerAccounts.FirstOrDefault(p => p.Username == username).UserId,
					BranchId = (int)vm.MaCN,
					Address = vm.DiaChi,
					Phone = vm.DienThoai,
					OrderDate = DateTime.Now,
					Payment = vm.ThanhToan,
					Status = "pending",
					Note = vm.GhiChu,
					Total = vm.ThanhTien,
					CouponId = (int?)TempData["VID"]
                };
				using (TransactionScope transcope = new TransactionScope())
				{
					try
					{
						
						da.Add(order);
						da.SaveChanges();
						var order_details = new List<OrderDetail>();
						foreach(var item in Cart)
						{
							order_details.Add(new OrderDetail()
							{
								OrderId = order.OrderId,
								Quantity = item.SoLuong,
								FoodId = item.MaDa?? 0,
								Price = item.ThanhTien
							}) ;
						}
						da.AddRange(order_details);
                        //cập nhất thông tin ?
                        if (vm.CapNhat == true)
                        {
                            da.CustomerAccounts.FirstOrDefault(p => p.Username == username).Fullname = vm.HoTen;
                            da.CustomerAccounts.FirstOrDefault(p => p.Username == username).Address = vm.DiaChi;
                            da.CustomerAccounts.FirstOrDefault(p => p.Username == username).Phone = vm.DienThoai;

                        }
                        //mã giảm giá
                        var voucherId = TempData["VoucherID"];
                        if (voucherId != null)
                            da.Coupons.FirstOrDefault(p => p.CouponId == (int)TempData["VID"]).MaxUsage--;
                        da.SaveChanges();
                        transcope.Complete();
						HttpContext.Session.Set<List<CartVM>>(CART_KEY, new List<CartVM>());
						return RedirectToAction("Success");
					}
						
						
					catch
					{
						return RedirectToAction("ListCart");
					}
				}



			}

			return View();
		}
		public IActionResult Success() 
		{
			return View();
		}


	}



}




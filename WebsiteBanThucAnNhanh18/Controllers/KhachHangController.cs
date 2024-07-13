using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.Helper;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly QLBHContext da;
        private readonly IMapper _mapper;

        public KhachHangController(QLBHContext context, IMapper mapper)
        {
            da = context;
            _mapper = mapper;
        }
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(CustomerRegisterVM model)
        {
			if (ModelState.IsValid)
			{
				try
				{

					var khachHang = _mapper.Map<CustomerAccount>(model);
					if (da.CustomerAccounts.SingleOrDefault(p => p.Username == khachHang.Username) != null)
						throw new Exception("Tên đăng nhập đã tồn tại");
					if (da.CustomerAccounts.SingleOrDefault(p => p.Email == khachHang.Email) != null)
						throw new Exception("Email đã tồn tại");
					khachHang.Randomkey = MaHoa.GenerateRandomKey();
					khachHang.Password = model.Password.ToMd5Hash(khachHang.Randomkey);
					da.Add(khachHang);
					da.SaveChanges();
					TempData["RegisterSuccess"] = "Đăng ký thành công!";
					return RedirectToAction("DangNhap");
				}
				catch (Exception ex)
				{
					var mess = $"{ex.Message} ";
					ViewBag.RegisterFail = mess;
					return View();
				}
			}
            ViewBag.RegisterFail = "Đăng ký thất bại! Kiểm tra lại thông tin vừa nhập!";
            return View();

        }
        #endregion
        #region Login
     

        public IActionResult DangNhap(string? ReturnUrl) //lưu URL để khi đăng nhập thì công thì biết chuyển sang URL nào
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(CustomerLoginVM vm, string? ReturnUrl) //lưu URL để khi đăng nhập thì công thì biết chuyển sang URL nào
        {

            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid) //Tránh báo lỗi chưa nhập gì khi vừa vào trang đăng nhập
            {
                var khachHang = da.CustomerAccounts.SingleOrDefault(kh => kh.Username == vm.UserName);
                if (khachHang == null || khachHang.Password != vm.Password.ToMd5Hash(khachHang.Randomkey))
                {
                    ModelState.AddModelError("Lỗi", "Tài khoản hoặc mật khẩu không hợp lệ");

                }
                else //Đăng nhập thành công
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, khachHang.Email),
                        new Claim(ClaimTypes.Name, khachHang.Fullname),
                        new Claim("Username", khachHang.Username),
						new Claim("Fullname", khachHang.Fullname),
						new Claim("Address", khachHang.Address),
						new Claim("Phone", khachHang.Phone)

                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }

                }


            }
            return View();
        }
        #endregion
     
        public IActionResult Profile()
        {
			var username = HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Username").Value;
            CustomerAccount cus = da.CustomerAccounts.FirstOrDefault(p=> p.Username ==  username);
            CustomerProfileVM customerProfileVM = new CustomerProfileVM();
            customerProfileVM.UserId = cus.UserId;
            customerProfileVM.Username = cus.Username;
            customerProfileVM.Fullname = cus.Fullname;
            customerProfileVM.Address = cus.Address;
            customerProfileVM.Email = cus.Email;
            customerProfileVM.Phone = cus.Phone;
            customerProfileVM.Gender = (bool)cus.Gender;
            customerProfileVM.Bod = cus.Bod;
            

           
			return View(customerProfileVM);

        }
		
		public async Task<IActionResult> DangXuat()
		{
			await HttpContext.SignOutAsync();
			return Redirect("/");
		}
        public IActionResult LichSu()
        {
            var username = HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Username").Value;           
            List<Order> orders = da.Orders.Where(p=>p.UserId == da.CustomerAccounts.FirstOrDefault(p => p.Username == username).UserId).ToList();
            // Dictionary lưu tên chi nhánh dựa trên mã chi nhánh
            Dictionary<int, string> branchNames = new Dictionary<int, string>();
            foreach (Order or in orders) {
                Coupon  discount = da.Coupons.FirstOrDefault(p => p.CouponId == or.CouponId);
                if(discount != null)
                    or.Total = Math.Max(0, (decimal)(or.Total - discount.DiscountAmount));
                //Lấy tên chi nhánh
                string branchName = da.Branches.FirstOrDefault(b => b.BranchId == or.BranchId)?.BranchName;
                if (branchName != null)
                {
                    // Thêm tên chi nhánh vào dictionary
                    branchNames.Add(or.OrderId, branchName);
                }

            }
           
            ViewBag.BranchNames = branchNames;
            return View(orders);

        }
        public IActionResult Detail(int id)
        {
            Order or = da.Orders.FirstOrDefault(p => p.OrderId == id);
			List<OrderDetail> od = da.OrderDetails.Where(p => p.OrderId == id).ToList();
            List<CartVM> cart = new List<CartVM>();
            foreach (var item in od)
            {
                cart.Add(new CartVM {
                    TenDa = da.Foods.FirstOrDefault(p => p.FoodId == item.FoodId).FoodName,
                    SoLuong = item.Quantity.Value,
                    Gia = item.Price.Value              
                });
            }
            


            ViewBag.OrderId = id;
            ViewBag.UserName = da.CustomerAccounts.FirstOrDefault(p => p.UserId == or.UserId).Username ?? "";
            ViewBag.FullName = da.CustomerAccounts.FirstOrDefault(p => p.UserId == or.UserId).Fullname ?? "";   
            ViewBag.Branch = da.Branches.FirstOrDefault(p => p.BranchId == or.BranchId).BranchName ?? "";
            ViewBag.Address = or.Address ?? "";
            ViewBag.Phone = or.Phone ?? "";
            ViewBag.OrderDate = or.OrderDate;
            ViewBag.Payment = or.Payment ?? "";
            ViewBag.Status = or.Status ?? "";
            ViewBag.Note = or.Note ?? "";
            Coupon discount = da.Coupons.FirstOrDefault(p => p.CouponId == or.CouponId);
            if (discount != null)
            {
                ViewBag.Total = Math.Max(0, (decimal)(or.Total - discount.DiscountAmount));
                ViewBag.Discount = discount.DiscountAmount;
            }
               
            else
                ViewBag.Total = or.Total;
			
			
            
             
            return View(cart);

        }

    }
}

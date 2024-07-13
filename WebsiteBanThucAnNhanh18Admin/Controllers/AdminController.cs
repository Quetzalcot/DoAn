using Microsoft.AspNetCore.Mvc;
using WebsiteBanThucAnNhanh18Admin.Data;
using WebsiteBanThucAnNhanh18Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using WebsiteBanThucAnNhanh18Admin.Helper;
using WebsiteBanThucAnNhanhAdmin18.Helper;
using Microsoft.EntityFrameworkCore;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
	public class AdminController : Controller
	{
		private QLBHContext db = new QLBHContext();
        public IActionResult Index()
        {
            var admin = db.ManagerAccounts.OrderBy(u => u.UserId);
            return View(admin);
        }
		#region DangNhap
		public IActionResult DangNhap(string? ReturnUrl) //lưu URL để khi đăng nhập thì công thì biết chuyển sang URL nào
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM vm, string? ReturnUrl) //lưu URL để khi đăng nhập thì công thì biết chuyển sang URL nào
        {

            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid) //Tránh báo lỗi chưa nhập gì khi vừa vào trang đăng nhập
            {
                var admin = db.ManagerAccounts.SingleOrDefault(ad => ad.Username == vm.UserName);
                if (admin == null || admin.Password != vm.Password.ToMd5Hash(admin.Randomkey))
                {
                    ModelState.AddModelError("Lỗi", "Tài khoản hoặc mật khẩu không hợp lệ");

                }
                else //Đăng nhập thành công
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.Username),
                        new Claim("Role", admin.RoleId.ToString()),
                        new Claim("FullName", admin.Fullname ?? ""),
                        new Claim("Email", admin.Email ?? ""),
                        new Claim("Phone", admin.Phone ?? "")
                            
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
        #region Register
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]

        public IActionResult DangKy(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var admin = new ManagerAccount();

                        admin.Randomkey = MaHoa.GenerateRandomKey();
                        admin.Username = model.Username;
                        admin.Password = model.Password.ToMd5Hash(admin.Randomkey);
                        admin.Fullname = model.Fullname;
                        admin.Email = model.Email;
                        admin.Phone = model.Phone;
                        admin.RoleId = model.RoleId;
                        
                  
                    if (db.ManagerAccounts.SingleOrDefault(p => p.Username == admin.Username) != null)
                        throw new Exception("Tên đăng nhập đã tồn tại");
                    if (db.ManagerAccounts.SingleOrDefault(p => p.Email == admin.Email) != null)
                        throw new Exception("Email đã tồn tại");
                    if (model.RoleId == 0)
                    {
                        throw new Exception("Đây là quyền Admin");
                    }

                    db.Add(admin);
                    db.SaveChanges();
                    TempData["RegisterSuccess"] = "Đăng ký thành công!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    // Xử lý lỗi cập nhật database
                    ViewBag.RegisterFail = "Đã xảy ra lỗi khi lưu dữ liệu vào database. Vui lòng thử lại.";
                    return View();
                }
                catch (Exception ex)
                {
                    // Xử lý các ngoại lệ khác
                    ViewBag.RegisterFail = ex.Message;
                    return View();
                }
            }
            ViewBag.RegisterFail = "Đăng ký thất bại! Kiểm tra lại thông tin vừa nhập!";
            return View();
        }
        #endregion
        #region Delete
        public IActionResult Delete(int id)
        {

            ManagerAccount account = db.ManagerAccounts.FirstOrDefault(p => p.UserId == id);
            return View(account);
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection col)
        {
            ManagerAccount account = db.ManagerAccounts.FirstOrDefault(p => p.UserId == id);
            try
            {
                db.ManagerAccounts.Remove(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

            }

            return RedirectToAction("Index");
        }
        #endregion
        #region edit
        public IActionResult Edit(int id)
        {
            ManagerAccount account = db.ManagerAccounts.FirstOrDefault(p => p.UserId == id);
            return View(account);
        }
        [HttpPost]
        public IActionResult Edit(int id, IFormCollection collection, string newPassword)
        {
            try
            {
                
                ManagerAccount account = db.ManagerAccounts.FirstOrDefault(p => p.UserId == id);
                account.Randomkey = MaHoa.GenerateRandomKey();
                account.Fullname = collection["Fullname"].ToString();
                account.Email = collection["Email"].ToString();
                account.RoleId = Convert.ToInt32(collection["RoleId"].ToString());
                account.Phone = collection["Phone"].ToString();                              
                account.Password = newPassword.ToMd5Hash(account.Randomkey);
                db.ManagerAccounts.Update(account);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }
        #endregion

        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

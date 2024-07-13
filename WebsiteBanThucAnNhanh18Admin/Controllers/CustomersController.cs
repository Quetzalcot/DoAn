using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualBasic;
using PageList;
using System.Linq.Expressions;
using WebsiteBanThucAnNhanh18Admin.Data;
using WebsiteBanThucAnNhanh18Admin.Helper;
using WebsiteBanThucAnNhanh18Admin.Models;
using WebsiteBanThucAnNhanhAdmin18.Helper;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
	public class CustomersController : Controller
	{
		private QLBHContext da = new QLBHContext();
		
		public IActionResult Index(int page = 1, int pageSize = 20)
		{
            var accounts = da.CustomerAccounts.OrderByDescending(p=>p.UserId)
				.Skip((page -1) * pageSize).Take(pageSize).ToList();
			int total = da.CustomerAccounts.Count();
			PagesModel model = new PagesModel
			{
				Accounts = accounts,
				CurrentPage = page,
				PageSize = pageSize,
				TotalUsers = total
			};
			return View(model);
		}
        public IActionResult Search(string username)
		{           
            try
			{
                CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.Username == username);
				if (account == null)
				{
					throw new Exception("User không tồn tại");
				}
                return RedirectToAction("Edit", new { id = account.UserId });
            }

			catch(Exception ex)
			{
				ViewBag.Search =ex.Message;
                return RedirectToAction("Index");

            }			        
        }
        public IActionResult SearchID(int id)
        {
            try
            {
                CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.UserId == id);
                if (account == null)
                {
                    throw new Exception("User không tồn tại");
                }
                return RedirectToAction("Edit", new { id = account.UserId });
            }

            catch (Exception ex)
            {
                ViewBag.Search = ex.Message;
                return RedirectToAction("Index");

            }
        }
        public IActionResult Create()
		{

			return View();
		}
		[HttpPost]
        public IActionResult Create(IFormCollection collection, string Password)
        {
			CustomerAccount account = new CustomerAccount();
			try
			{
				string username = collection["Username"].ToString();
				string email = collection["Email"].ToString();
				if (da.CustomerAccounts.FirstOrDefault(p=>p.Username == username) != null)
				{
					throw new Exception("Tên đăng nhập đã tồn tại!");
				}
				if (da.CustomerAccounts.FirstOrDefault(p => p.Email == email) != null)
				{
					throw new Exception("Tên đăng nhập đã tồn tại!");
				}
				account.Username = username;
				account.Email = email;
				account.Fullname = collection["Fullname"].ToString();				
                account.Address = collection["Address"].ToString();
                account.Phone = collection["Phone"].ToString();
                account.Bod = Convert.ToDateTime(collection["Bod"].ToString());
                account.Gender = Convert.ToBoolean(collection["Gender"].ToString());
                account.Randomkey = MaHoa.GenerateRandomKey();
                account.Password = Password.ToMd5Hash(account.Randomkey);
                da.CustomerAccounts.Add(account);
                da.SaveChanges();
                return RedirectToAction("Index");
            }
			catch(Exception ex)
			{
				TempData["Error"] = ex.Message;
                return View();
            }
           
        }
        public IActionResult Edit(int id)
		{
			CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.UserId == id);
			return View(account);
		}
		[HttpPost]
		public IActionResult Edit(int id, IFormCollection collection,string newPassword)
		{
			try
			{
                CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.UserId == id);
                account.Fullname = collection["Fullname"].ToString();
                account.Email = collection["Email"].ToString();
                account.Address = collection["Address"].ToString();
                account.Phone = collection["Phone"].ToString();
                account.Bod = Convert.ToDateTime(collection["Bod"].ToString());
                account.Gender = Convert.ToBoolean(collection["Gender"].ToString());
				account.Randomkey = MaHoa.GenerateRandomKey();
				account.Password = newPassword.ToMd5Hash(account.Randomkey);
				da.CustomerAccounts.Update(account);
				da.SaveChanges();
            }
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			
			return RedirectToAction("Index");	
		}
        public IActionResult Delete(int id)
        {

            CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.UserId == id);
			return View(account);
        }
        [HttpPost]
		public IActionResult Delete(int id, IFormCollection col)
		{
            CustomerAccount account = da.CustomerAccounts.FirstOrDefault(p => p.UserId == id);
            try
			{
				da.CustomerAccounts.Remove(account);
				da.SaveChanges();
                return RedirectToAction("Index");
            }
			catch
			{

			}

            return RedirectToAction("Index");
        }
	}
}

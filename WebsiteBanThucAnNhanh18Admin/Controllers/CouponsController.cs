using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using WebsiteBanThucAnNhanh18Admin.Models;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
    public class CouponsController : Controller
    {
        string domain = "https://localhost:7231/";
        HttpClient client = new HttpClient();
        public async Task<IActionResult> ListCoupon()
        {
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Coupons");
            List<CouponModel> coupons = JsonConvert.DeserializeObject<List<CouponModel>>(datajson);
            return View(coupons);
        }
        public async Task<IActionResult> Create()
        {                                
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> CreateAsync(CouponModel model)
        {
            client.BaseAddress = new Uri(domain);
            var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
            formData.Add(new StringContent(model.CouponCode), "couponCode");
            formData.Add(new StringContent(model.DiscountAmount.ToString()), "discountAmount");
            formData.Add(new StringContent(model.MaxUsage.ToString()), "maxUsage");
            formData.Add(new StringContent(model.StartDate.ToString()), "startDate");
            formData.Add(new StringContent(model.EndDate.ToString()), "endDate");
            var kq = await client.PostAsync("/api/Coupons/create-coupon", formData);          
            return RedirectToAction("ListCoupon");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Coupons/" + id);
                CouponModel coupon = JsonConvert.DeserializeObject<CouponModel>(datajson);
                return View(coupon);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListCoupon");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> Edit(CouponModel model)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
            var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
            formData.Add(new StringContent(model.CouponCode), "couponCode");
            formData.Add(new StringContent(model.DiscountAmount.ToString()), "discountAmount");
            formData.Add(new StringContent(model.MaxUsage.ToString()), "maxUsage");
            formData.Add(new StringContent(model.StartDate.ToString()), "startDate");
            formData.Add(new StringContent(model.EndDate.ToString()), "endDate");
            int id = model.CouponId;
            var kq = await client.PutAsync("/api/Coupons/update-coupon/" + id, formData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListCoupon");
          
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Coupons/" + id);
                CouponModel coupon = JsonConvert.DeserializeObject<CouponModel>(datajson);
                return View(coupon);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListCoupon");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmAsync(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var response = await client.DeleteAsync("/api/Coupons/" + id);
                return RedirectToAction("ListCoupon");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListCoupon");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Debugger.Contracts;
using Newtonsoft.Json;
using System.Security.Claims;
using WebsiteBanThucAnNhanh18Admin.Models;


namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
    public class OrderController : Controller
    {
        string domain = "https://localhost:7231/";
        HttpClient client = new HttpClient();
     
        public async Task<IActionResult> ListOrder(int page= 1, int pageSize = 5)
        {
            int bId = Convert.ToInt32( HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Role").Value);
            if(bId != 0)
            {
                return RedirectToAction("Search", new { BranchID = bId });
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(domain);
                 string datajson = await client.GetStringAsync("/api/Order");
                List<OrderModel> orders = JsonConvert.DeserializeObject<List<OrderModel>>(datajson);

                // Calculate pagination data
                int totalPages = (int)Math.Ceiling((double)orders.Count / pageSize);

                // Skip and take items for current page
                orders = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Pass pagination data and orders to view
                ViewBag.PageIndex = page;
                ViewBag.TotalPages = totalPages;
                return View(orders);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Order/search/" + id);
                OrderModel orde = JsonConvert.DeserializeObject<OrderModel>(datajson);
                ViewBag.Status = new List<string> { "pending", "processing", "delivering", "paid", "canceled" };
                return View(orde);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListCoupon");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> Edit(OrderModel model)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
                formData.Add(new StringContent(model.Status), "Status");
                formData.Add(new StringContent(model.OrderDate.ToString()), "OrderDate");
                int id = model.OrderId;
                var kq = await client.PutAsync("/api/Order/update-order/" + id, formData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListOrder");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Order/search/" + id);
            OrderModel orde = JsonConvert.DeserializeObject<OrderModel>(datajson);            
            return View(orde);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmAsync(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var response = await client.DeleteAsync("/api/Order/" + id);
                return RedirectToAction("ListOrder");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListOrder");
        }
        [HttpGet]
        public async Task<IActionResult> SearchDate(DateTime start, DateTime end)
        {
            if (start == null && end == null)
                return RedirectToAction("ListOrder");
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync($"/api/Order/Bydate?start={start}&end={end}");
            List<OrderModel> bra = JsonConvert.DeserializeObject<List<OrderModel>>(datajson);
            return View(bra);
        }
        [HttpGet]
        public async Task<IActionResult> Search(int BranchID)
        {
            if (BranchID <=0)
                return RedirectToAction("ListOrder");
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync($"/api/Order/BranchId/"+ BranchID);
            List<OrderModel> bra = JsonConvert.DeserializeObject<List<OrderModel>>(datajson);
            return View(bra);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Lấy thông tin order
                client.BaseAddress = new Uri(domain);
                string orderDataJson = await client.GetStringAsync("/api/Order/search/" + id);
                OrderModel order = JsonConvert.DeserializeObject<OrderModel>(orderDataJson);

                // Lấy thông tin coupon nếu có
                if (order.CouponId != null)
                {
                    string couponDataJson = await client.GetStringAsync("/api/Coupons/" + order.CouponId.Value);
                    CouponModel coupon = JsonConvert.DeserializeObject<CouponModel>(couponDataJson);
                    order.Coupon = coupon;
                }

                // Lấy chi tiết đơn hàng
                string orderDetailDataJson = await client.GetStringAsync("/api/OrderDetail/" + id);
                List<OrderDetailModel> orderDetails = JsonConvert.DeserializeObject<List<OrderDetailModel>>(orderDetailDataJson);

                string datajson = await client.GetStringAsync("/api/Foods");
                List<FoodsModel> foods = JsonConvert.DeserializeObject<List<FoodsModel>>(datajson);

                // Gắn dữ liệu vào ViewModel
                var viewModel = new OrderDetailsViewModel
                {
                    Order = order,
                    OrderDetails = orderDetails,
                    Foods = foods
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListOrder");
            }
        }
    }
}

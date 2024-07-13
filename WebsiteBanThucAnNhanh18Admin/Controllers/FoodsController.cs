using Microsoft.AspNetCore.Mvc;
using WebsiteBanThucAnNhanh18Admin.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Template;
using System.Text;
using Microsoft.Data.SqlClient.Server;
using System.Drawing;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
    public class FoodsController : Controller
    {
        string domain = "https://localhost:7231/";
        HttpClient client = new HttpClient();
        public async Task<IActionResult> ListFood()
        {
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Foods");
            List<FoodsModel> foods = JsonConvert.DeserializeObject<List<FoodsModel>>(datajson);
            return View(foods);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string foodName)
        {
            if (foodName == "")
                return RedirectToAction("ListFood");
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Foods/search/" + foodName);
            List<FoodsModel> foods = JsonConvert.DeserializeObject<List<FoodsModel>>(datajson);
            return View(foods);
        }

        [HttpGet]
        public async Task<IActionResult> SearchID(int foodId)
        {
            if (foodId <= 0)
                return RedirectToAction("ListFood");
            return RedirectToAction("Edit", new { id = foodId });
        }
        public async Task<IActionResult> Create()
        {
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Categories");
            List<CategoryModel> categories = JsonConvert.DeserializeObject<List<CategoryModel>>(datajson);
            ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> CreateAsync(FoodsModel f, IFormFile fileimage)
        {
            client.BaseAddress = new Uri(domain);
            var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
            formData.Add(new StringContent(f.FoodName), "foodName");
            formData.Add(new StringContent(f.Description), "description");
            formData.Add(new StringContent(f.Price.ToString()), "price");
            formData.Add(new StringContent(f.CategoryId.ToString()), "categoryId");
            formData.Add(new StringContent(f.Discount.ToString()), "discount");
            formData.Add(new StringContent(f.Idx.ToString()), "idx");
            //phòng khi file ảnh là null
            if (fileimage == null)
            {
                using (var stream = System.IO.File.OpenRead("wwwroot/image/default.jpg"))
                {
                    formData.Add(new StreamContent(stream), "image", "default.webp");
                }

            }
            else
            {
                formData.Add(new StreamContent(fileimage.OpenReadStream()), "image", fileimage.FileName);
            }

            var kq = await client.PostAsync("/api/Foods/create-food", formData);
            //TempData["kq"] = await kq.Result.Content.ReadAsStringAsync();
            return RedirectToAction("ListFood");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {

                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Foods/" + id);
                FoodsModel food = JsonConvert.DeserializeObject<FoodsModel>(datajson);

                string datajson2 = await client.GetStringAsync("/api/Categories");
                List<CategoryModel> categories = JsonConvert.DeserializeObject<List<CategoryModel>>(datajson2);
                ViewBag.Category = new SelectList(categories, "CategoryId", "CategoryName");
                return View(food);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListFood");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoodsModel model, IFormFile fileimage)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(model.FoodName), "foodName");
                formData.Add(new StringContent(model.Description), "description");
                formData.Add(new StringContent(model.Price.ToString()), "price");
                formData.Add(new StringContent(model.CategoryId.ToString()), "categoryId");
                formData.Add(new StringContent(model.Discount.ToString()), "discount");
                formData.Add(new StringContent(model.Idx.ToString()), "idx");
                //phòng khi file ảnh là null , chủ yếu để bắt lỗi
                if (fileimage == null)
                {
                    using (var stream = System.IO.File.OpenRead("wwwroot/image/default.jpg"))
                    {
                        formData.Add(new StreamContent(stream), "image", "default.webp");
                    }
               
                }
                else
                {
                   
                    formData.Add(new StreamContent(fileimage.OpenReadStream()), "image", fileimage.FileName);
                }
                int id = model.FoodId;
                var response = await client.PutAsync("/api/Foods/update-food/" + id, formData);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListFood");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = responseContent;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListFood");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Foods/" + id);
                FoodsModel food = JsonConvert.DeserializeObject<FoodsModel>(datajson);
                return View(food);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListFood");
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmAsync(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var response = await client.DeleteAsync("/api/Foods/" + id);
                return RedirectToAction("ListFood");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListFood");
        }
        
    }
}

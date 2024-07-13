using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using WebsiteBanThucAnNhanh18Admin.Models;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
    public class BranchController : Controller
    {
        string domain = "https://localhost:7231/";
        HttpClient client = new HttpClient();
        public async Task<IActionResult> ListBranch()
        {
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Branch");
            List<BranchModel> branches = JsonConvert.DeserializeObject<List<BranchModel>>(datajson);
            return View(branches);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                string datajson = await client.GetStringAsync("/api/Branch/" + id);
                BranchModel brachh = JsonConvert.DeserializeObject<BranchModel>(datajson);
                return View(brachh);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListBranch");
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> Edit(BranchModel model)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
                formData.Add(new StringContent(model.BranchName), "BranchName");
                formData.Add(new StringContent(model.Address), "Address");
                formData.Add(new StringContent(model.Phone), "Phone");
                formData.Add(new StringContent(model.OpenHour), "OpenHour");
                formData.Add(new StringContent(model.CloseHour), "CloseHour");
                int id = model.BranchId;
                var kq = await client.PutAsync("/api/Branch/update-branch/" + id, formData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListBranch");

        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //tránh giả mạo request
        public async Task<IActionResult> CreateAsync(BranchModel model)
        {
            client.BaseAddress = new Uri(domain);
            var formData = new MultipartFormDataContent();//đối tượng để add các dữ liệu từ form gửi đi API
            formData.Add(new StringContent(model.BranchName), "BranchName");
            formData.Add(new StringContent(model.Address), "Address");
            formData.Add(new StringContent(model.Phone), "Phone");
            formData.Add(new StringContent(model.OpenHour), "OpenHour");
            formData.Add(new StringContent(model.CloseHour), "CloseHour");
            var kq = await client.PostAsync("/api/Branch/create-branch/", formData);
            return RedirectToAction("ListBranch");

        }
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        client.BaseAddress = new Uri(domain);
        //        string datajson = await client.GetStringAsync("/api/Branch/" + id);
        //        BranchModel bra = JsonConvert.DeserializeObject<BranchModel>(datajson);
        //        return View(bra);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message;

        //    }
        //    return RedirectToAction("ListBranch");
        //}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // No need to call the API here, as it's just for confirmation view
                return View(); // Return the empty view for confirmation
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListBranch");
            }
        }
        // POST: Branch/Delete/5 (Handles actual deletion)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                client.BaseAddress = new Uri(domain);
                await client.DeleteAsync("/api/Branch/" + id); // Use DeleteAsync for deleting
                return RedirectToAction("ListBranch");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListBranch");
        }
        [HttpGet]
        public async Task<IActionResult> Search(string BranchName)
        {
            if (BranchName == "")
                return RedirectToAction("ListBranch");
            client.BaseAddress = new Uri(domain);
            string datajson = await client.GetStringAsync("/api/Branch/searche/" + BranchName);
            List<BranchModel> bra = JsonConvert.DeserializeObject<List<BranchModel>>(datajson);
            return View(bra);
        }

        //[HttpGet]
        //public async Task<IActionResult> Search(int BranchId)
        //{
        //    if (BranchId <= 0)
        //        return RedirectToAction("ListBranch");
        //    client.BaseAddress = new Uri(domain);
        //    string datajson = await client.GetStringAsync("/api/Branch/" + BranchId);
        //    List<BranchModel> bra = JsonConvert.DeserializeObject<List<BranchModel>>(datajson);
        //    return View(bra);
        //}

    }
}

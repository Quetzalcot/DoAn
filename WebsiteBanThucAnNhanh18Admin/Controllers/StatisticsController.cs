using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebsiteBanThucAnNhanh18Admin.Data;

namespace WebsiteBanThucAnNhanh18Admin.Controllers
{
    public class StatisticsController : Controller
    {
        //chưa hình dung được chức năng này nếu làm API
        private QLBHContext da = new QLBHContext();

        public ActionResult Index()
        {
            ViewBag.Month = new SelectList(Enumerable.Range(1, 12));
            ViewBag.Year = new SelectList(Enumerable.Range(DateTime.Now.Year - 9, 10).OrderByDescending(y => y), DateTime.Now.Year);
            return View();
        }

        [HttpPost]
        public ActionResult Index(int month, int year)
        {          
            // Lấy đơn hàng đã được thanh toán
            var orders = da.Orders
                .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Month == month && o.OrderDate.Value.Year == year && o.Status == "paid")
                .ToList();

            // Tính doanh thu của những chi nhánh có doanh thu trong tháng
            var revenueDataBrach = orders
                .GroupBy(o => o.BranchId)
                .Select(g => new
                {
                    BranchId = g.Key,
                    BranchName = da.Branches.Where(b => b.BranchId == g.Key).Select(b => b.BranchName).FirstOrDefault(),
                    TotalRevenue = g.Sum(o =>
                        o.Total.HasValue ?
                            o.Total.Value - (o.CouponId.HasValue ? da.Coupons.Where(c => c.CouponId == o.CouponId).Select(c => c.DiscountAmount).FirstOrDefault() : 0)
                            : 0)
                })
                .OrderByDescending(r => r.TotalRevenue)
                .ToList();

            // Lấy danh sách tất cả các chi nhánh ,cho doanh thu  = 0
            var noRevenueDataBranch= da.Branches.Select(b => new
            {
                BranchId = b.BranchId,
                BranchName = b.BranchName,
                TotalRevenue = 0
            }).ToList();

            // Join dữ liệu từ các chi nhánh đã có doanh thu và các chi nhánh chưa có doanh thu
            var revenueData = noRevenueDataBranch.GroupJoin(revenueDataBrach, b => b.BranchId, r => r.BranchId, (b, r) => new
            {
                BranchId = b.BranchId,
                BranchName = b.BranchName,
                TotalRevenue = r.Any() ? r.First().TotalRevenue : 0
            }).OrderByDescending(r => r.TotalRevenue).ToList();
            //nếu chỉ lấy revenueDataBrach gán cho ViewBag thì chi nhánh chưa có doanh thu không hiển thị
            ViewBag.RevenueData = revenueData;
            ViewBag.Month = new SelectList(Enumerable.Range(1, 12), month);
            ViewBag.Year = new SelectList(Enumerable.Range(DateTime.Now.Year - 9, 10).OrderByDescending(y => y), year);
            
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.Controllers
{
    public class BranchController : Controller
    {
        private readonly QLBHContext da;

        public BranchController(QLBHContext context)
        {
            da = context;
        }

        public ActionResult Index()
        {
            List<Branch> branches = da.Branches.ToList();
            return View(branches);
        }

        // Xử lý khi chọn chi nhánh
        [HttpPost]
        public ActionResult Branch(int branchId)
        {

            var branch = da.Branches.FirstOrDefault(b => b.BranchId == branchId);
           
            ViewBag.SelectedBranch = branch;
             List<Branch> branches = da.Branches.ToList();
            return View("Index",branches);
        }
    }
}

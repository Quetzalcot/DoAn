using Microsoft.AspNetCore.Mvc;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.ViewComponents
{
    public class MenuCategoryViewComponent: ViewComponent
    {
        private readonly QLBHContext da;

        public MenuCategoryViewComponent(QLBHContext context) => da = context;

        public IViewComponentResult Invoke()
        {
            var data = da.Categories.Select(loai => new MenuCategoryVM
            {
                MaL =loai.CategoryId,
                TenL = loai.CategoryName,
                SoLuong = loai.Foods.Count
            }).OrderBy(li => li.TenL);
            return View(data); // Default.cshtml
        }
    }
}

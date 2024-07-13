using WebsiteBanThucAnNhanh18Admin.Data;

namespace WebsiteBanThucAnNhanh18Admin.Models
{
    public class PagesModel
    {
        public List<CustomerAccount> Accounts {  get; set; }   
        public List<Order> Orders { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalUsers { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalUsers / PageSize);
    }
}

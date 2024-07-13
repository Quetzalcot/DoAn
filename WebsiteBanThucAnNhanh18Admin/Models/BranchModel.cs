using System.ComponentModel.DataAnnotations;

namespace WebsiteBanThucAnNhanh18Admin.Models
{
    public class BranchModel
    {
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? OpenHour { get; set; }
        public string? CloseHour { get; set; }
        public string? Ggmap { get; set; }

    }
}

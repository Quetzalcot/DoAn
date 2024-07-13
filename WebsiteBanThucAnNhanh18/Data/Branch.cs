using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18.Data
{
    public partial class Branch
    {
        public Branch()
        {
            Orders = new HashSet<Order>();
        }

        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? OpenHour { get; set; }
        public string? CloseHour { get; set; }
        public string? Ggmap { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

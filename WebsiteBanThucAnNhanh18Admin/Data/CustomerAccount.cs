using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18Admin.Data
{
    public partial class CustomerAccount
    {
        public CustomerAccount()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Bod { get; set; }
        public string? Randomkey { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

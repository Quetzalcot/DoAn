using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18Admin.Data
{
    public partial class ManagerAccount
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public string? Randomkey { get; set; }

        public virtual Role Role { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebsiteBanThucAnNhanh18Admin.Models
{
	public class ManagerModel
	{
		
		public int UserId { get; set; }
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string? Fullname { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public int RoleId { get; set; }
	}
}

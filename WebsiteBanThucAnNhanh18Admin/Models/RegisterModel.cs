using System.ComponentModel.DataAnnotations;

namespace WebsiteBanThucAnNhanh18Admin.Models
{
	public class RegisterModel
	{
		
		public int UserId { get; set; }
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
		public string Username { get; set; } = null!;
		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]	
		public string Password { get; set; } = null!;
		[Display(Name = "Họ tên")]
		[Required(ErrorMessage = "*")]
		[MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
		public string? Fullname { get; set; }
		[EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
		public string? Email { get; set; }
		
		[Display(Name = "Điện thoại")]
		[MaxLength(15, ErrorMessage = "Tối đa 15 kí tự")]
		[RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động Việt Nam")]
		public string? Phone { get; set; }
		[Required(ErrorMessage = "*")]
		public int RoleId { get; set; }
	}
}

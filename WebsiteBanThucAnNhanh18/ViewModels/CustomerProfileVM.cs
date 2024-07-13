namespace WebsiteBanThucAnNhanh18.ViewModels
{
    public class CustomerProfileVM
    {
       
        public int? UserId { get; set; }
       
        public string? Username { get; set; } = null!;
       
       
     
        public string? Fullname { get; set; }
       
        public string? Email { get; set; }
       
        public string? Phone { get; set; }
       
        public string? Address { get; set; }
        
        public DateTime? Bod { get; set; }
        public bool Gender { get; set; } = true;
    }
}

using AutoMapper;
using WebsiteBanThucAnNhanh18.Data;
using WebsiteBanThucAnNhanh18.ViewModels;

namespace WebsiteBanThucAnNhanh18.Helper
{
	public class AutoMapperProfile: Profile
	{
		public AutoMapperProfile() 
		{
			//2 cột cùng tên tự động match
			CreateMap<CustomerRegisterVM, CustomerAccount>();

		}
	}
}

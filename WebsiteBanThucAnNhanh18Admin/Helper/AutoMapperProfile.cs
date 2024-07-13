using AutoMapper;
using WebsiteBanThucAnNhanh18Admin.Data;
using WebsiteBanThucAnNhanh18Admin.Models;

namespace WebsiteBanThucAnNhanh18Admin.Helper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, ManagerAccount>();
                
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;

namespace WebsiteBanThucAnNhanhAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private CategoriesSvc categoriesSvc;
		public CategoriesController() 
		{
			categoriesSvc = new CategoriesSvc();
		}
        //[HttpPost("get-by-id")]
        //public IActionResult GetCategoriesByID([FromBody] SimpleReq simpleReq)
        //{
        //	var res = new SingleRsp();
        //	res = categoriesSvc.Read(simpleReq.Id);
        //	return Ok(res);
        //}
        //[HttpPost("get-all")]
        //public IActionResult getAllCategories()
        //{
        //	var res = new SingleRsp();
        //	res.Data = categoriesSvc.All;
        //	return Ok(res.Data);
        //}
        [HttpGet]
        public IActionResult geCategories()
        {
            var res = new SingleRsp();
            res.Data = categoriesSvc.All;
            return Ok(res.Data);
        }
        [HttpPost("create-category")]
		public IActionResult CreateCategory([FromBody] CategoryReq categoryReq)
		{
			var res = new SingleRsp();
			res = categoriesSvc.CreateCategory(categoryReq);
			return Ok(res);
		}
	}
}

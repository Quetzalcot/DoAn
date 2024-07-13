using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using QLBH.DAL;


namespace WebsiteBanThucAnNhanhAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodsController : ControllerBase
	{
		private FoodsSvc foodsSvc;
		public FoodsController()
		{
			foodsSvc = new FoodsSvc();
		}
	
		//[HttpPost("get-by-id")]
		//public IActionResult GetFoodsByID([FromBody] SimpleReq simpleReq)
		//{
		//	var res = new SingleRsp();
		//	res = foodsSvc.Read(simpleReq.Id);
		//	return Ok(res);
		//}
		//[HttpPost("get-all")]
		//public IActionResult getAllFoods()
		//{
		//	var res = new SingleRsp();
		//	res.Data = foodsSvc.All;
		//	return Ok(res);
		//}
		[HttpGet]
		public IActionResult getFoods()
		{
			var res = new SingleRsp();
			res.Data = foodsSvc.All.OrderByDescending(p=>p.FoodId);			
			return Ok(res.Data);
		}
		[HttpGet]
		[Route("{id}")]
		public IActionResult getFoodsByID(int id)
		{
			var res = new SingleRsp();
			res = foodsSvc.Read(id);
			return Ok(res.Data);
		}
		[HttpGet]
		[Route("search/{name}")]
		public IActionResult searchFoodByName(string name)
		{
			var res = new SingleRsp();
			res = foodsSvc.Search(name);
			return Ok(res.Data);

		}
		[HttpPost("create-food")]
		public IActionResult CreateFoods([FromForm] FoodReq foodReq)
		{
			var res = new SingleRsp();
			res = foodsSvc.CreateFood(foodReq);
			return Ok(res);
		}
		[HttpPut]
		[Route("update-food/{id}")]
		public IActionResult UpdateFood( [FromForm] FoodReq foodReq, int id)
		{
			var res = new SingleRsp();
			try
			{
				foodReq.FoodId = id; // Gán ID cho Food
				res = foodsSvc.UpdateFood(foodReq);
				if (res.Success)
				{
					return Ok(res.Data);
				}
				else
				{
					return BadRequest(res.Message);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpDelete]
		[Route("{id}")]
		public IActionResult DeleteFoods(int id)
		{
			try
			{
				var res = foodsSvc.DeleteFood(id);
				if (res.Success)
				{
					return Ok();
				}
				else
				{
					return BadRequest(res.Message);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
	
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;

namespace WebsiteBanThucAnNhanhAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CouponsController : ControllerBase
	{
		private CouponSvc couponSvc;
		
		public CouponsController()
		{
			couponSvc = new CouponSvc();
		}
		//[HttpPost("get-by-id")]
		//public IActionResult GetCouponsByID([FromBody] SimpleReq simpleReq)
		//{
		//	var res = new SingleRsp();
		//	res = couponSvc.Read(simpleReq.Id);
		//	return Ok(res);
		//}
		//[HttpPost("get-all")]
		//public IActionResult getAllCategories()
		//{
		//	var res = new SingleRsp();
		//	res.Data = couponSvc.All;
		//	return Ok(res);
		//}
		[HttpGet]
		public IActionResult getCoupons()
		{
			var res = new SingleRsp();
			res.Data = couponSvc.All.OrderByDescending(p => p.CouponId);
			return Ok(res.Data);
		}
		[HttpGet]
		[Route("{id}")]
		public IActionResult getCouponsByID(int id)
		{
			var res = new SingleRsp();
			res = couponSvc.Read(id);
			return Ok(res.Data);
		}
		[HttpPost("create-coupon")]
		public IActionResult CreateCoupons([FromForm] CouponReq couponReq)
		{
			var res = new SingleRsp();
			res = couponSvc.CreateCoupon(couponReq);
			return Ok(res);
		}
		[HttpPut]
        [Route("update-coupon/{id}")]
        public IActionResult UpdateCoupons([FromForm] CouponReq couponReq, int id)
		{
			var res = new SingleRsp();
			try
			{
				couponReq.CouponId = id; 
				res = couponSvc.UpdateCoupon(couponReq);
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
		public IActionResult DeleteCoupons(int id)
		{
			try
			{
				var res = couponSvc.DeleteCoupon(id);
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

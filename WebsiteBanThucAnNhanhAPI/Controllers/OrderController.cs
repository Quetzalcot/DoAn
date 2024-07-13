using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QLBH.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using QLBH.DAL;

namespace WebsiteBanThucAnNhanhAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{

		private OrdersD orderD;
		public OrderController()
		{
			orderD = new OrdersD();
		}
		[HttpGet]
        [Route("search/{id}")]
        public IActionResult SearchOrderByID(int id)
		{
			var res = new SingleRsp();
			res = orderD.Read(id);
			return Ok(res.Data);
		}

        //[HttpGet]
        //public IActionResult OrderList()
        //{
        //	var res = new SingleRsp();
        //	res.Data = orderD.All.OrderBy(o => o.OrderId);
        //	return Ok(res.Data);
        //}
        [HttpGet]
        public IActionResult OrderList(int page = 1, int pageSize = 1000)
        {
            var res = new SingleRsp();
            res.Data = orderD.All.OrderBy(o => o.OrderId)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
								.OrderByDescending(p=>p.OrderId);
            return Ok(res.Data);
        }
        [HttpGet]
        [Route("Bydate")]
        public IActionResult SearchDate(DateTime start, DateTime end)
        {
            var res = new SingleRsp();
            res.Data = orderD.All.Where(o => o.OrderDate >= start && o.OrderDate <= end);
            return Ok(res.Data);
        }
        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //	var res = new SingleRsp();
        //	return Ok(res.Data);

        //}
        [HttpGet]
		[Route("Branchid/{id}")]
		public IActionResult SearchBranch(int id)
		{
			var res = new SingleRsp();
			res.Data = orderD.All.Where(o => o.BranchId == id);
			return Ok(res.Data);
		}
		[HttpPut]
		[Route("update-order/{id}")]
		public IActionResult UpdateOrder([FromForm] OrderReq orderReq, int id)
		{
			var res = new SingleRsp();
			try
			{
				orderReq.OrderId = id;
				res = orderD.UpdateOrder(orderReq);
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
		public IActionResult Delete(int id)
		{
			try
			{
				var res = orderD.DeleteOrder(id);
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

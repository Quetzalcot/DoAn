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
	public class BranchController : ControllerBase
	{
		private BranchSvc branchSvc;
		public BranchController()
		{
			branchSvc = new BranchSvc();
		}
		[HttpGet]
		public IActionResult getBranch()
		{
			var res = new SingleRsp();
			res.Data = branchSvc.All;
			return Ok(res.Data);
		}
		[HttpGet]
        [Route("{id}")]
        public IActionResult FindBranchByID(int id)
		{
			var res = new SingleRsp();
			res = branchSvc.Read(id);
			return Ok(res.Data);
		}

        [HttpGet]
        [Route("searche/{name}")]
        public IActionResult FindByName(string name)
        {
            var res = new SingleRsp();
            res = branchSvc.SearchName(name);
            return Ok(res.Data);
        }

        [HttpPost("create-branch")]
		public IActionResult CreateBranch([FromForm] BranchReq branchReq)
		{
			var res = new SingleRsp();
			res = branchSvc.CreateBranch(branchReq);
			return Ok(res);
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public IActionResult DeleteBranch(int id)
		{
			try
			{
				var res = branchSvc.DeleteBranch(id);
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
        [HttpPut]
        [Route("update-branch/{id}")]
        public IActionResult UpdateOrder([FromForm] BranchReq branchReq, int id)
        {
            var res = new SingleRsp();
            try
            {
				branchReq.BranchId = id;
                res = branchSvc.UpdateBranch(branchReq);
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

    }
}

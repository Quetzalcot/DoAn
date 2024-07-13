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
    public class OrderDetailController : ControllerBase
    {
        private OrderDetailsSvc od;
        public OrderDetailController()
        {
            od = new OrderDetailsSvc();
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult OrderDetailList(int id)
        {
            var res = new SingleRsp();
            res.Data = od.All.Where(o => o.OrderId == id);
            return Ok(res.Data);
        }
    }
}

using QLBH.DAL.Data;
using QLBH.Common.BLL;
using QLBH.Common.Req;
using QLBH.Common.Rsp;
using QLBH.DAL;
using QLBH.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QLBH.BLL
{
    public class OrderDetailsSvc:GenericSvc<OrderDetailRep, OrderDetail>
    {
        private OrderDetailRep odrep;
        public OrderDetailsSvc()
        {
            odrep = new OrderDetailRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            if (!_rep.All.Any(p => p.OrderId == id))
            {
                res.SetError("EZ103", "Không tìm thấy mã đơn hàng");
                return res;
            }

            res.Data = _rep.Read(id);
            return res;
        }
    }
}

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
	public class OrdersD:GenericSvc<OrderRep,Order>
	{
		private OrderRep orderRep;
		public OrdersD() 
		{
			orderRep = new OrderRep();
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
        

        public SingleRsp GetOrder()
		{
			var res = new SingleRsp();
			res.Data = _rep.GetOrders();
			return res;
		}
		public SingleRsp UpdateOrder(OrderReq orderReq)
		{
			var res = new SingleRsp();
			if (!_rep.All.Any(p => p.OrderId == orderReq.OrderId))
			{
				res.SetError("EZ103", "Không tìm được đơn hàng");
				return res;
			}
			var m = _rep.Read(orderReq.OrderId);
			//m.UserId = orderReq.userid;
			//m.BranchId = orderReq.BranchId;
			m.OrderDate = orderReq.OrderDate;
			//m.Total = orderReq.Total;
			m.Status = orderReq.Status;
			//m.Payment = orderReq.Payment;
			
			res = base.Update(m);
			res.Data = m;
			return res;
		}
		public SingleRsp DeleteOrder(int id)
		{
			var res = new SingleRsp();

			var exst = _rep.Read(id);
			if (exst == null)
			{
				res.SetError("EZ103", "Không tìm thấy đơn hàng");
				return res;
			}
			_rep.DeleteOrder(exst);
			return res;
		}

       
    }
}

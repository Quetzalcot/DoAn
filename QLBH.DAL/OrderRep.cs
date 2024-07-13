using Microsoft.EntityFrameworkCore;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using QLBH.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.DAL
{
	public class OrderRep : GenericRep<QLBHContext,Order>
	{
		public OrderRep()
		{

		}
		public override Order Read(int id)
		{
			var res = All.FirstOrDefault(o=>o.OrderId==id);
			return res;
		}
      
        public List<Order> GetOrders()
		{
			return All.ToList();
        }
		public int Remove(int id)
		{
            
			var m = base.All.FirstOrDefault(o => o.OrderId == id);
			m = base.Delete(m);
			return m.OrderId;
		}
		public SingleRsp Details(OrderDetail detail)
		{
			var res = new SingleRsp();
			return res;
		}
		public SingleRsp DeleteOrder(Order order)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					
					try
					{
                    //xoa details
                    var orderDetails = context.OrderDetails.Where(d => d.OrderId == order.OrderId).ToList();
                    context.OrderDetails.RemoveRange(orderDetails);
					//sang order
                    var p = context.Orders.Remove(order);
						context.SaveChanges();
						tran.Commit();
					}
					catch (Exception ex)
					{
						tran.Rollback();
						res.SetError(ex.StackTrace);
					}
				}
			}
			return res;
		}
	}
}



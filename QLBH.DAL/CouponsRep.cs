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
	public class CouponsRep:GenericRep<QLBHContext, Coupon>
	{
		public CouponsRep()
		{

		}
		public override Coupon Read(int id)
		{
			var res = All.FirstOrDefault(p => p.CouponId == id);
			return res;
		}
		
		public int Remove(int id)
		{
			var m = base.All.FirstOrDefault(p => p.CouponId == id);
			m = base.Delete(m);
			return m.CouponId;
		}
       
		
            #region ---Methods---
            public SingleRsp CreateCoupon(Coupon coupon)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						var p = context.Coupons.Add(coupon);
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
        public SingleRsp UpdateCoupon(Coupon coupon)
        {
            var res = new SingleRsp();
            using (var context = new QLBHContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
						var p = context.Coupons.Update(coupon);
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
        public SingleRsp DeleteCoupon(Coupon coupon)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						context.Coupons.Remove(coupon);
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
		#endregion
	}
}

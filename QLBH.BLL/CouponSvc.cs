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
	public class CouponSvc:GenericSvc<CouponsRep,Coupon>
	{
		private CouponsRep couponsRep;
		public CouponSvc()
		{
			couponsRep = new CouponsRep();
		}
		#region --Override--
		public override SingleRsp Read(int id)
		{
			
			var res = new SingleRsp();
			if (!_rep.All.Any(p => p.CouponId == id))
			{
				res.SetError("EZ103", "Không tìm thấy mã giảm giá");
				return res;
			}
			res.Data = _rep.Read(id);
			return res;
		}

		#endregion
		public SingleRsp UpdateCoupon(CouponReq couponReq)
		{
			//kiểm tra id có tồn tại?
			var res = new SingleRsp();
			if (!_rep.All.Any(p => p.CouponId == couponReq.CouponId))
			{
				res.SetError("EZ103", "Không tìm thấy mã giảm giá");
				return res;
			}
			var m = _rep.Read(couponReq.CouponId);
			//mã coupon tự tăng
			m.CouponCode = couponReq.CouponCode; 
			m.MaxUsage = couponReq.MaxUsage;
			m.StartDate = couponReq.StartDate;
			m.EndDate = couponReq.EndDate;
			m.DiscountAmount = couponReq.DiscountAmount;
			res = couponsRep.UpdateCoupon(m);
			res.Data = m;
			return res;
		}
		public SingleRsp CreateCoupon(CouponReq couponReq)
		{
			var res = new SingleRsp();
			Coupon coupon = new Coupon();

			//couponid tự tăng
			coupon.CouponCode = couponReq.CouponCode;
			coupon.MaxUsage = couponReq.MaxUsage;
			coupon.StartDate = couponReq.StartDate;
			coupon.EndDate = couponReq.EndDate;
			coupon.DiscountAmount = couponReq.DiscountAmount;
			res = couponsRep.CreateCoupon(coupon);
			return res;

		}
		public SingleRsp DeleteCoupon(int id)
		{
			var res = new SingleRsp();

			// Kiểm tra ID có tồn tại hay không
			var existCoupon = _rep.Read(id);
			if (existCoupon == null)
			{
				res.SetError("EZ103", "Không tìm thấy mã giảm giá");
				return res;
			}

			// Xóa Food
			_rep.DeleteCoupon(existCoupon);

			//res.Success = true;
			return res;
		}
	}
}

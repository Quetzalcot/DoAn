using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.Common.Req
{
	public  class CouponReq
	{
		public int CouponId { get; set; }
		public string? CouponCode { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public int? MaxUsage { get; set; }
		public decimal? DiscountAmount { get; set; }
	}
}

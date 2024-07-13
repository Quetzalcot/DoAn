using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.Common.Req
{
	public class OrderReq
	{
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
        public string? Status { get; set; }
        public string? Payment { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public int? CouponId { get; set; }


    }
}

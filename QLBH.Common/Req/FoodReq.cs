using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.Common.Req
{
	public class FoodReq
	{
		public int FoodId { get; set; }
		public string? FoodName { get; set; }
		public string? Description { get; set; }
		public decimal? Price { get; set; }
		public int? CategoryId { get; set; }
		public decimal? Discount { get; set; }
		public string? Picture { get; set; }
		public short? Idx { get; set; }

		public IFormFile Image { get; set; }
	}
}

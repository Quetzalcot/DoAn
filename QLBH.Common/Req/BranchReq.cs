using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.Common.Req
{
	public class BranchReq
	{
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? OpenHour { get; set; }
        public string? CloseHour { get; set; }
        public string? Ggmap { get; set; }

    }
}

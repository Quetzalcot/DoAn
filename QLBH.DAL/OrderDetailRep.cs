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
    public class OrderDetailRep : GenericRep<QLBHContext, OrderDetail>
    {
        public override OrderDetail Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }
    }
}

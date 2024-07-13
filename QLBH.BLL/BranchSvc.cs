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
	public class BranchSvc:GenericSvc<BranchRep, Branch>
	{
		private BranchRep branchRep;
		public BranchSvc()
		{
			branchRep = new BranchRep();
		}
		public override SingleRsp Read(int id)
		{
            var res = new SingleRsp();
            if (!_rep.All.Any(p => p.BranchId == id))
            {
                res.SetError("EZ103", "Không tìm thấy sản phẩm");
                return res;
            }
            res.Data = _rep.Read(id);
            return res;
        }
		public SingleRsp CreateBranch(BranchReq branchreq)
		{
			var res = new SingleRsp();
			Branch bra= new Branch();
			//branchreq.BranchId = bra.BranchId;
			bra.BranchName = branchreq.BranchName;
			bra.Address = branchreq.Address;
			bra.Phone = branchreq.Phone;
			bra.OpenHour = branchreq.OpenHour;
			bra.CloseHour = branchreq.CloseHour;
			res = branchRep.CreateBranch(bra);
			return res;

		}
		public SingleRsp DeleteBranch(int id)
		{
			var res = new SingleRsp();

			var branchexst = _rep.Read(id);
			if (branchexst == null)
			{
				res.SetError("EZ103", "Không tìm thấy chi nhánh");
				return res;
			}
			
			_rep.DeleteBranch(branchexst);

			return res;
		}
		public SingleRsp SearchName(string name)
		{
			var res = new SingleRsp();
			res.Data = _rep.SearchName(name);
			return res;
		}
        public SingleRsp UpdateBranch(BranchReq branchReq)
        {
            var res = new SingleRsp();
            if (!_rep.All.Any(p => p.BranchId == branchReq.BranchId))
            {
                res.SetError("EZ103", "Không tìm được chi nhánh");
                return res;
            }
            var m = _rep.Read(branchReq.BranchId);
           
			m.BranchName = branchReq.BranchName;
			m.Address = branchReq.Address;
			m.Phone = branchReq.Phone;
			m.OpenHour = branchReq.OpenHour;
			m.CloseHour = branchReq.CloseHour;
			

            res = base.Update(m);
            res.Data = m;
            return res;
        }
    }
}

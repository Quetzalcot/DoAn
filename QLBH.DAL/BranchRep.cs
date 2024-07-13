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
	public class BranchRep : GenericRep<QLBHContext, Branch>
	{
		public BranchRep() 
		{

		}
		public override Branch Read(int id)
		{
			var res = All.FirstOrDefault(p => p.BranchId == id);
			return res;
		}
		public List<Branch> SearchName(string name)
		{
			return All.Where(n => n.BranchName.Contains(name)).ToList();
		}
		public override Branch Read(string code)
		{
			var res = All.FirstOrDefault(p=>p.BranchName.Contains(code));
			return res;
		}
		public int Remove(int id)
		{
			var m = base.All.FirstOrDefault(p => p.BranchId == id);
			m = base.Delete(m);
			return m.BranchId;
		}
		public SingleRsp CreateBranch(Branch bra)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						var p = context.Branches.Add(bra);
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
		public SingleRsp DeleteBranch(Branch bra)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						var p = context.Branches.Remove(bra);
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

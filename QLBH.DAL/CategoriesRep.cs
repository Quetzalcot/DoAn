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
	public class CategoriesRep: GenericRep<QLBHContext, Category>
	{
		
		public CategoriesRep()
		{ 

		}
		public override Category Read(int id)
		{
			var res = All.FirstOrDefault(p=>p.CategoryId == id);
			return res;
		}
		public int Remove(int id)
		{
			var m = base.All.FirstOrDefault(p => p.CategoryId == id);
			m = base.Delete(m);
			return m.CategoryId;
		}
		#region ---Methods---
		public SingleRsp CreateCategory(Category category)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						var p = context.Categories.Add(category);
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

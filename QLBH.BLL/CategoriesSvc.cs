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
	public class CategoriesSvc:GenericSvc<CategoriesRep,Category>
	{
		private CategoriesRep categoriesRep;
		public CategoriesSvc()
		{
			categoriesRep = new CategoriesRep();
		}
		#region --Override--
		public override SingleRsp Read(int id)
		{
			var res = new SingleRsp ();
			res.Data = _rep.Read (id);
			return res;
		}
		public override SingleRsp Update(Category m)
		{
			var res = new SingleRsp();

			var m1 = m.CategoryId > 0 ? _rep.Read(m.CategoryId) : _rep.Read(m.CategoryName);
			if (m1 == null)
			{
				res.SetError("EZ103", "No data.");
			}
			else
			{
				res = base.Update(m);
				res.Data = m;
			}

			return res;
		}
		#endregion
		public SingleRsp CreateCategory(CategoryReq categoryReq)
		{
			var res = new SingleRsp();
			Category category = new Category();
			category.CategoryId = categoryReq.CategoryId;
			category.CategoryName = categoryReq.CategoryName;
			res = categoriesRep.CreateCategory(category);
			return res;

		}
	}
}

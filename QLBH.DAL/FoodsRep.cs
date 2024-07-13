using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using QLBH.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.DAL
{
	public class FoodsRep: GenericRep<QLBHContext, Food>
	{
		#region --Overrides--
		public override Food Read(int id)
		{
			var res  = All.FirstOrDefault(p=>p.FoodId == id);
			return res;
		}
		
		public int Remove(int id) 
		{ 
			var m = base.All.FirstOrDefault(p=> p.FoodId == id);
			m = base.Delete(m);
			return m.FoodId;
		}
		
		#endregion
		#region ---Methods---
		public List<Food> SearchByName(string name)
		{
			return All.Where(p => p.FoodName.Contains(name)).ToList();
		}
		public SingleRsp CreateFood(Food food)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using(var tran = context.Database.BeginTransaction())
				{
					try
					{
						var p = context.Foods.Add(food);
						context.SaveChanges();
						tran.Commit();
					}
					catch(Exception ex)
					{
						tran.Rollback();
						res.SetError(ex.StackTrace);
					}
				}
			}
			return res;
		}
		public SingleRsp DeleteFood(Food food)
		{
			var res = new SingleRsp();
			using (var context = new QLBHContext())
			{
				using (var tran = context.Database.BeginTransaction())
				{
					try
					{
						context.Foods.Remove(food);
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

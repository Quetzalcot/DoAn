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
	
	public class FoodsSvc:GenericSvc<FoodsRep,Food>
	{
		private FoodsRep foodsRep;
		public FoodsSvc()
		{
			foodsRep = new FoodsRep();

		}
		#region --Override--
		public override SingleRsp Read(int id)
		{
			var res = new SingleRsp();
			if (!_rep.All.Any(p => p.FoodId == id))
			{
				res.SetError("EZ103", "Không tìm thấy sản phẩm");
				return res;
			}
			res.Data = _rep.Read(id);
			return res;
		}


		#endregion
		public SingleRsp UpdateFood(FoodReq foodReq)
		{
			//kiểm tra id có tồn tại
			var res = new SingleRsp();
			if (!_rep.All.Any(p => p.FoodId == foodReq.FoodId))
			{
				res.SetError("EZ103", "Không tìm thấy sản phẩm");
				return res;
			}
			var m = _rep.Read(foodReq.FoodId);
            //tồn tại đường dẫn  ảnh ? xử lí ảnh
            if (foodReq.Image.Length > 0)
            {
                var path = Path.Combine(
            "D:/1OU-20230530T133709Z-001/1OU/3.HKII/LTCSDL/CK/DoAn/WebsiteBanThucAnNhanh18/WebsiteBanThucAnNhanh18/wwwroot/image/food/"
            , foodReq.Image.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    foodReq.Image.CopyTo(stream);
                }
               m.Picture = "/image/food/" + foodReq.Image.FileName;
            }
            else
                m.Picture = "";
            //mã Food tự tăng
            m.FoodName = foodReq.FoodName;
			m.Price = foodReq.Price;
			//m.Picture = foodReq.Picture;
			m.Description = foodReq.Description;
			m.Discount = foodReq.Discount;
			m.CategoryId = foodReq.CategoryId;
			m.Idx = foodReq.Idx;
			//var m1 = m.FoodId > 0 ? _rep.Read(m.FoodId) : _rep.Read(m.FoodName);
								
			res = base.Update(m);
			res.Data = m;
			
			return res;
		}
		public SingleRsp CreateFood(FoodReq foodReq)
		{
			var res = new SingleRsp();
			Food food = new Food();
			//tồn tại đường dẫn  ảnh ? xử lí ảnh
			if(foodReq.Image.Length >0)
			{
				var path = Path.Combine(
			"D:/1OU-20230530T133709Z-001/1OU/3.HKII/LTCSDL/CK/DoAn/WebsiteBanThucAnNhanh18/WebsiteBanThucAnNhanh18/wwwroot/image/food/"
			, foodReq.Image.FileName);
				using(var stream = System.IO.File.Create(path))
				{
					foodReq.Image.CopyTo(stream);
				}
				food.Picture = "/image/food/" + foodReq.Image.FileName;
			}
			else
				food.Picture = "";

			//mã Food tự tăng
			food.FoodName = foodReq.FoodName;
			food.Price = foodReq.Price;			
			food.Description = foodReq.Description;
			food.Discount = foodReq.Discount;
			food.CategoryId = foodReq.CategoryId;
			food.Idx = foodReq.Idx;
			res = foodsRep.CreateFood(food);
			return res;

		}
		public SingleRsp Search(string name)
		{
			var res = new SingleRsp();
			res.Data = _rep.SearchByName(name);
			return res;
		}
		public SingleRsp DeleteFood(int id)
		{
			var res = new SingleRsp();

			// Kiểm tra ID có tồn tại hay không
			var existFood = _rep.Read(id);
			if (existFood == null)
			{
				res.SetError("EZ103", "Không tìm thấy sản phẩm");
				return res;
			}

			// Xóa Food
			_rep.DeleteFood(existFood);

			
			return res;
		}
	}
}

using System;
using System.Collections.Generic;

namespace WebsiteBanThucAnNhanh18.Data
{
    public partial class Category
    {
        public Category()
        {
            Foods = new HashSet<Food>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}

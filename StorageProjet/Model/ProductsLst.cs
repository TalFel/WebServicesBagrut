using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProductsLst : List<Product>
    {
        public ProductsLst() { }
        public ProductsLst(IEnumerable<Product> lst) : base(lst){ }
        public ProductsLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Product>().ToList()) { }
        public ProductsLst getSameCategory(Category cat)
        {
            ProductsLst pLst = new ProductsLst();
            foreach (Product p in this)
            {
                if (p.TheCategory.CategoryId == cat.CategoryId)
                    pLst.Add(p);
            }
            return pLst;
        }
    }
}

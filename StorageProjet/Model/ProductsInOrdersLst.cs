using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProductsInOrdersLst : List<ProductInOrder>
    {
        public ProductsInOrdersLst() { }
        public ProductsInOrdersLst(IEnumerable<ProductInOrder> lst) : base(lst) { }
        public ProductsInOrdersLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<ProductInOrder>().ToList()) { }
        public void AddToList(ProductInOrder p)
        {
            foreach (ProductInOrder PIO in this)
            {
                if (PIO.TheProduct.Equals(p.TheProduct))
                {
                    PIO.Amount += p.Amount;
                    return;
                }
            }
            this.Add(p);
        }
    }
}

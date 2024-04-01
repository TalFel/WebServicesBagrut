using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BestProductsLst : List<BestProduct>
    {
        public BestProductsLst() { }
        public BestProductsLst(IEnumerable<BestProduct> lst) : base(lst) { }
        public BestProductsLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<BestProduct>().ToList()) { }
    }
}

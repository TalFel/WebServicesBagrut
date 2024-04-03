using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ProductsAllowedRolesLst : List<ProductAllowedRole>
    {
        public ProductsAllowedRolesLst() { }
        public ProductsAllowedRolesLst(IEnumerable<ProductAllowedRole> lst) : base(lst) { }
        public ProductsAllowedRolesLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<ProductAllowedRole>().ToList()) { }
    }
}

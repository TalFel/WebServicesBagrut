using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class CategoriesLst : List<Category>
    {
        public CategoriesLst() { }
        public CategoriesLst(IEnumerable<Category> lst) : base(lst) { }
        public CategoriesLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Category>().ToList()) { }
    }
}

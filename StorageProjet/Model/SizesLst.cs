using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SizesLst : List<Size>
    {
        public SizesLst() { }
        public SizesLst(IEnumerable<Size> lst) : base(lst) { }
        public SizesLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Size>().ToList()) { }
    }
}

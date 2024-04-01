using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ColorLst : List<Color>
    {
        public ColorLst() { }
        public ColorLst(IEnumerable<Color> lst) : base(lst) { }
        public ColorLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Color>().ToList()) { }
    }
}

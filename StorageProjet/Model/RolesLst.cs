using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RolesLst : List<Role>
    {
        public RolesLst() { }
        public RolesLst(IEnumerable<Role> lst) : base(lst) { }
        public RolesLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Role>().ToList()) { }
    }
}
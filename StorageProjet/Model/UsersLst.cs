using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class UsersLst : List<User>
    {
        public UsersLst() { }
        public UsersLst(IEnumerable<User> lst) : base(lst) { }
        public UsersLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<User>().ToList()) { }
    }
}

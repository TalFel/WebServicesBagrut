using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OrdersLst : List<Order>
    {
        public OrdersLst() { }
        public OrdersLst(IEnumerable<Order> lst) : base(lst) { }
        public OrdersLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Order>().ToList()) { }
    }
}
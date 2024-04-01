using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RequestsLst : List<Request>
    {
        public RequestsLst() { }
        public RequestsLst(IEnumerable<Request> lst) : base(lst) { }
        public RequestsLst(IEnumerable<BaseEntity> lst) : base(lst.Cast<Request>().ToList()) { }
        public RequestsLst GetByDate()
        {
            RequestsLst sorted = new RequestsLst();
            RequestsLst temp = new RequestsLst(this);
            for (int i = 0; i < this.Count(); i++)
            {
                Request min = new Request();
                min.RequestDate = DateTime.MaxValue;
                foreach(Request req in temp)
                {
                    if (req.RequestDate < min.RequestDate)
                        min = req;
                }
                temp.Remove(min);
                sorted.Add(min);
            }
            return sorted;
        }
    }
}

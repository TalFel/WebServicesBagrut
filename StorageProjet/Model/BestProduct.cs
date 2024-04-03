using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BestProduct : BaseEntity
    {
        public Product product { get; set; }
        public int count {  get; set; }
        public BestProduct(Product product, int count)
        {
            this.product = product;
            this.count = count;
        }
        public BestProduct() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class ProductInOrder : BaseEntity
    {
        public Product TheProduct { get; set; }
        public Order TheOrder { get; set; }
        public int Amount { get; set; }
        public Status TheStatus { get; set; }

        //empty constructor.
        public ProductInOrder() { return; }
        //copy constructor.
        public ProductInOrder(ProductInOrder productInOrder)
        {
            this.TheProduct = new Product(productInOrder.TheProduct);
            this.TheOrder = new Order(productInOrder.TheOrder);
            this.Amount = productInOrder.Amount;
            this.TheStatus = productInOrder.TheStatus;
        }
        //new Category with all properties.
        public ProductInOrder(Product theProduct, Order theOrder,
            int amount, Status theStatus)
        {
            this.TheProduct = theProduct;
            this.TheOrder = theOrder;
            this.Amount = amount;
            this.TheStatus = theStatus;
        }
        //temporary for new order
        public ProductInOrder(Product theProduct,
            int amount)
        {
            this.TheProduct = theProduct;
            this.Amount = amount;
            this.TheStatus = Status.Ordered;
        }

    }
}

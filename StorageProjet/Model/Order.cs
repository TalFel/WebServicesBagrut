using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Order : BaseEntity
    {
        public int OrderID { get; set; }
        public User OrderingUser { get; set; }
        public DateTime OrderTime { get; set; }
        public Status OrderStatus { get; set; }
        public string OrderTimeShortString
        {
            get
            {
                return OrderTime.Day + "/" + OrderTime.Month + "/" + OrderTime.Year;
            }
        }
        public string OrderDescription { get; set; } = string.Empty;
        //empty constructor.
        public Order() { return; }
        //copy constructor.
        public Order(Order order) {
            this.OrderID = order.OrderID;
            this.OrderingUser = order.OrderingUser;
            this.OrderTime = order.OrderTime;
            this.OrderDescription = order.OrderDescription;
            this.OrderStatus = order.OrderStatus;
        }
        //new Order with all properties.
        public Order(int orderID, User orderingUser, DateTime orderTime, string orderDescription, Status orderStatus)
        {
            this.OrderID = orderID;
            this.OrderingUser = orderingUser;
            this.OrderTime = orderTime;
            this.OrderDescription = orderDescription;
            this.OrderStatus = orderStatus;
        }
        //new Order with Status 'Ordered'.
        public Order(User orderingUser, DateTime orderTime, string orderDescription)
        {
            this.OrderingUser = orderingUser;
            this.OrderTime = orderTime;
            this.OrderDescription = orderDescription;
            OrderStatus = Status.Ordered;
        }
        public bool Insert()
        {
            OrderDB ODB = new OrderDB();
            if (ODB.Exists(this))
                return false;
            ODB.Insert(this);
            return true;
        }
        public void InsertProducts(ProductsInOrdersLst PIOL)
        {
            ProductInOrderDB PIODB = new ProductInOrderDB();
            PIODB.InsertNewOrder(PIOL, this);
        }
        public ProductsInOrdersLst GetOrderProducts()
        {
            ProductInOrderDB PIODB = new ProductInOrderDB();
            return PIODB.SelectProductsByOrderID(this.OrderID);
        }
        public void Update(ProductsInOrdersLst products, DateTime date, string Description)
        {
            this.OrderTime = date;
            this.OrderDescription = Description;
            this.OrderStatus = Status.Ordered;
            OrderDB ODB = new OrderDB();
            ODB.Update(this);
            //update all products
            ProductInOrderDB PIODB = new ProductInOrderDB();
            foreach (ProductInOrder p in products)
            {
                p.TheOrder = this;
                //product exists, needs to update.
                if (PIODB.SelectProductsByOrderANDProduct(OrderID, p.TheProduct.ProductID) != null)
                {
                    //reset the product amount to before and adds the new amount needed.
                    p.TheProduct.UpdateAmount(p.TheProduct.Amount - p.Amount + PIODB.SelectProductsByOrderANDProduct(this.OrderID, p.TheProduct.ProductID)[0].Amount);
                    //update in DB
                    PIODB.Update(p);
                }
                else
                {
                    //remove the amount from the stock
                    p.TheProduct.UpdateAmount(p.TheProduct.Amount - p.Amount);
                    //new product in order, insert it.
                    PIODB.Insert(p);
                }

                    
            }
        }
        public void Update(ProductsInOrdersLst products, Status newStatus)
        {
            this.OrderStatus = newStatus;
            OrderDB ODB = new OrderDB();
            ODB.Update(this);
            //update all products
            ProductInOrderDB PIODB = new ProductInOrderDB();
            foreach (ProductInOrder p in products)
            {
                p.TheOrder = this;
                //product exists, needs to update.
                if (PIODB.SelectProductsByOrderANDProduct(OrderID, p.TheProduct.ProductID) != null)
                {
                    //reset the product amount to before and adds the new amount needed.
                    p.TheProduct.UpdateAmount(p.TheProduct.Amount - p.Amount + PIODB.SelectProductsByOrderANDProduct(this.OrderID, p.TheProduct.ProductID)[0].Amount);
                    //update in DB
                    PIODB.Update(p);
                }
                else //new product in order, insert it.
                    PIODB.Insert(p);
            }
        }
        public void DeleteOrder()
        {
            OrderDB ODB = new OrderDB();
            ODB.delete(this);
        }
    }
}

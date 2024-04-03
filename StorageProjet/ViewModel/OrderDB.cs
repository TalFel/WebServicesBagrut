using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class OrderDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Order order = entity as Order;
            order.OrderID = int.Parse(reader["OrderID"].ToString());
            order.OrderDescription = reader["OrderDescription"].ToString();
            order.OrderTime = (DateTime)reader["OrderTime"];
            order.OrderStatus = (Status)(int.Parse(reader["OrderStatus"].ToString()));
            UserDB userDB = new UserDB();
            order.OrderingUser = userDB.SelectByUserID(int.Parse(reader["UserID"].ToString()));

            return order;
        }

        protected override BaseEntity NewEntity()
        {
            return new Order();
        }
        public OrdersLst SelectAll()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Orders WHERE OrderDeleted=False AND NOT OrderStatus=0 ORDER BY OrderTime DESC";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new OrdersLst(list);
        }
        public Order SelectByOrderID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Orders WHERE OrderID=@ID AND OrderDeleted=False";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Order;
            return null;
        }
        public OrdersLst SelectByUserID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Orders WHERE UserID=@ID AND OrderDeleted=False";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new OrdersLst(list);
        }
        public OrdersLst SelectOrdersByNameAndOrdered(string name, string byOrder, bool pastOrders)
        {
            if (byOrder.Length == 0)
                byOrder = "UserID";
            command.Parameters.Clear();
            List<BaseEntity> list;
            if (pastOrders)
            {
                command.CommandText = $"SELECT * FROM Orders WHERE OrderDeleted=False AND NOT OrderStatus=0 ORDER BY {byOrder}";
                list = base.Select();
            }
            else
            {
                command.CommandText = $"SELECT * FROM Orders WHERE OrderDeleted=False AND NOT OrderStatus=5 AND NOT OrderStatus=0 ORDER BY {byOrder}";
                list = base.Select();
                /*List<BaseEntity> newList = new List<BaseEntity>();
                foreach(BaseEntity ent in newList)
                {
                    if(((Order)ent).OrderTime.Date >= DateTime.Today.Date)
                        newList.Add(ent);
                }
                list = newList;*/
            }
            
            if (list.Count == 0) return null;

            UserDB userDB = new UserDB();
            OrdersLst orders = new OrdersLst();
            foreach(BaseEntity ent in list)
            {
                if (((Order)ent).OrderingUser.UserFirstName.Contains(name) || ((Order)ent).OrderingUser.UserFamilyName.Contains(name))
                    orders.Add((Order)ent);
            }
            return orders;
        }
        public int Insert(Order ord)
        {
            string sql = $"INSERT INTO Orders(UserID, OrderTime, OrderDescription, OrderDeleted, OrderStatus)" +
                $"VALUES({ord.OrderingUser.UserID}, #{General.toDateConverter(ord.OrderTime)}#, '{ord.OrderDescription}', False, 1)";
            int records = base.SaveChanges(sql);
            ord.OrderID = base.lastId;
            return records;
        }
        public int delete(Order ord)
        {
            string sql = $"UPDATE Orders SET OrderDeleted=True " +
                $"WHERE OrderID={ord.OrderID}";
            return base.SaveChanges(sql);
        }
        public int Update(Order ord)
        {
            string sql = $"UPDATE Orders SET UserID={ord.OrderingUser.UserID}, OrderTime=#{General.toDateConverter(ord.OrderTime)}#, OrderDescription='{ord.OrderDescription}', OrderStatus = {(int)ord.OrderStatus} " +
                $"WHERE OrderID={ord.OrderID}";
            return base.SaveChanges(sql);
        }
        //checks if there is an order for the same day from the same user
        public bool Exists(Order order)
        {
            command.Parameters.Clear();
            command.CommandText = $"SELECT * FROM Orders WHERE UserID=@ID AND OrderTime=#{General.toDateConverter(order.OrderTime)}# AND OrderDeleted=False";
            command.Parameters.Add(new OleDbParameter("@ID", order.OrderingUser.UserID));
            List<BaseEntity> list = base.Select();
            if (list.Count != 0) return true;
            return false;
        }
        public int NuberOfOrdersForToday()
        {
            OrdersLst orders = SelectAll();
            OrdersLst forToday = new OrdersLst(orders.FindAll(ord => { return ord.OrderTime.Date == DateTime.Now.Date; }));
            return forToday.Count;
        }

        public int NumberOfOrdersOrderedForToday()
        {
            OrdersLst orders = SelectAll();
            OrdersLst forTodayOrdered = new OrdersLst(orders.FindAll(ord => { return ord.OrderTime.Date == DateTime.Today.Date && ord.OrderStatus == Status.Ordered; }));
            return forTodayOrdered.Count;
        }
        public int NumberOfOrdersInProcessForToday()
        {
            OrdersLst orders = SelectAll();
            OrdersLst forTodayInProc = new OrdersLst(orders.FindAll(ord => { return ord.OrderTime.Date == DateTime.Today.Date && ord.OrderStatus == Status.InProcess; }));
            return forTodayInProc.Count;
        }
        public int NumberOfOrdersCompletedForToday()
        {
            OrdersLst orders = SelectAll();
            OrdersLst forTodayInProc = new OrdersLst(orders.FindAll(ord => { return ord.OrderTime.Date == DateTime.Today.Date && !(ord.OrderStatus == Status.Ordered) && !(ord.OrderStatus == Status.InProcess); }));
            return forTodayInProc.Count;
        }
        //public enum Status
        //    {
        //        NotOrdered,
        //        Ordered,
        //        InProcess,
        //        Ready,
        //        Given,
        //        Returned
        //    }
    }
}

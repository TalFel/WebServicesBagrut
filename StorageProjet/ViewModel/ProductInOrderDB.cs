using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductInOrderDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            ProductInOrder PIO = entity as ProductInOrder;
            PIO.Amount = (int.Parse(reader["Amount"].ToString()));

            ProductDB PDB = new ProductDB();
            PIO.TheProduct = PDB.SelectByProductID(int.Parse(reader["ProductID"].ToString()));

            PIO.TheStatus = (Status)(int.Parse(reader["StatusDealtWith"].ToString()));

            OrderDB ODB = new OrderDB();
            PIO.TheOrder = ODB.SelectByOrderID(int.Parse(reader["OrderID"].ToString()));

            return PIO;
        }

        protected override BaseEntity NewEntity()
        {
            return new ProductInOrder();
        }
        public ProductsInOrdersLst SelectProductsByOrderID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM ProductsInOrders WHERE OrderID=@ID AND NOT StatusDealtWith=" + (int)Status.NotOrdered;
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            return new ProductsInOrdersLst(list);
        }
        public ProductsInOrdersLst SelectProductsByOrderANDProduct(int OID, int PID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM ProductsInOrders WHERE OrderID=@OID AND ProductID=@PID AND NOT StatusDealtWith=" + (int)Status.NotOrdered;
            command.Parameters.Add(new OleDbParameter("@ID", OID));
            command.Parameters.Add(new OleDbParameter("@ID", PID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new ProductsInOrdersLst(list);
        }
        public int Insert(ProductInOrder PIO)
        {
            
            string sql = $"INSERT INTO ProductsInOrders(ProductID, OrderID, Amount, StatusDealtWith)" +
                $"VALUES({PIO.TheProduct.ProductID},{PIO.TheOrder.OrderID}, {PIO.Amount}, {((int)Status.Ordered)})";
            int records = base.SaveChanges(sql);
            return records;
        }
        public void InsertNewOrder(ProductsInOrdersLst PIOL, Order ord)
        {
            if (PIOL == null)
                return;
            foreach(ProductInOrder PIO in PIOL)
            {
                PIO.TheOrder = ord;
                Insert(PIO);
                PIO.TheProduct.UpdateAmount(PIO.TheProduct.Amount - PIO.Amount);
            }
        }
        //status=deleted? amount=-1?
        public int deleteByProd(Product prod, Order ord)
        {
            string sql = $"Delete From ProductsInOrders WHERE ProductID={prod.ProductID} AND OrderID={ord.OrderID}";
            return base.SaveChanges(sql);
        }
        public int deleteAll(Order ord)
        {
            string sql = $"Delete From ProductsInOrders WHERE OrderID={ord.OrderID}";
            return base.SaveChanges(sql);
        }
        public int Update(ProductInOrder PIO)
        {
            string sql = $"UPDATE ProductsInOrders SET Amount = {PIO.Amount}, StatusDealtWith = {((int)PIO.TheStatus)} " +
                $"WHERE ProductID={PIO.TheProduct.ProductID} AND OrderID={PIO.TheOrder.OrderID}";
            return base.SaveChanges(sql);
        }
        public int UpdateStatus(Product prod, Order ord, Status stat)
        {
            string sql = $"UPDATE ProductsInOrders SET StatusDealtWith = {stat}" +
                $"WHERE ProductID={prod.ProductID} AND Order={ord.OrderID}";
            return base.SaveChanges(sql);
        }
    }
}

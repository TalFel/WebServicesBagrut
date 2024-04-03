using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class BestProductsDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            BestProduct bestProduct = entity as BestProduct;
            ProductDB pdb = new ProductDB();
            bestProduct.product = pdb.SelectByProductID(int.Parse(reader["ProductID"].ToString()));
            bestProduct.count = int.Parse(reader["AmountCount"].ToString());
            return bestProduct;
        }

        protected override BaseEntity NewEntity()
        {
            return new BestProduct();
        }
        public BestProductsLst GetBestProducts()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT ProductID, Count(Amount) as AmountCount FROM ProductsInOrders GROUP BY ProductID ORDER BY COUNT(Amount) DESC";
            List<BaseEntity> list = base.Select();
            return new BestProductsLst(list);
        }
    }
}

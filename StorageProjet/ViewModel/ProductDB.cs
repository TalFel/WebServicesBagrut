using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace ViewModel
{
    public class ProductDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Product product = entity as Product;
            product.ProductID = int.Parse(reader["ProductID"].ToString());
            product.Amount = int.Parse(reader["ProductInStock"].ToString());

            Category cat = new Category();
            cat.CategoryId = int.Parse(reader["Products.CategoryID"].ToString());
            cat.CategoryName = reader["CategoryName"].ToString();
            cat.CategoryDescription = reader["CategoryDescription"].ToString();
            cat.CategoryImageLink = reader["CategoryImageLink"].ToString();
            product.TheCategory = cat;

            Color col = new Color();
            col.ColorID = int.Parse(reader["Products.ColorID"].ToString());
            col.ColorName = reader["ColorName"].ToString();
            product.TheColor = col;

            Size sze = new Size();
            sze.SizeID = int.Parse(reader["Products.SizeID"].ToString());
            sze.SizeName = reader["SizeName"].ToString();
            product.TheSize = sze;

            return product;
        }

        protected override BaseEntity NewEntity()
        {
            return new Product();
        }
        public ProductsLst SelectByCategoryID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON (Products.CategoryID=Categories.CategoryID AND Products.CategoryID=@ID))" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null; 
            return new ProductsLst(list);
        }
        public Product SelectByProductID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON (Products.CategoryID=Categories.CategoryID AND Products.ProductID=@ID))" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count != 1) return null;
            return list[0] as Product;
        }
        public ProductsLst SelectProductsDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON Products.CategoryID=Categories.CategoryID)" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new ProductsLst(list);
        }
        public int Insert(Product prod)
        {
            string sql = $"INSERT INTO Products(CategoryID, ColorID, SizeID, ProductInStock)" +
                $"VALUES({prod.TheCategory.CategoryId}, {prod.TheColor.ColorID}, {prod.TheSize.SizeID}, {prod.Amount})";
            int records = base.SaveChanges(sql);
            prod.ProductID = base.lastId;
            return records;
        }
        public int delete(Product prod)
        {
            string sql = $"Delete From Products WHERE ProductID={prod.ProductID}";
            return base.SaveChanges(sql);
        }
        public int Update(Product prod)
        {
            string sql = $"UPDATE Products SET CategoryID={prod.TheCategory.CategoryId}, ColorID={prod.TheColor.ColorID}, SizeID={prod.TheSize.SizeID}, ProductInStock={prod.Amount}" +
                $" WHERE ProductID={prod.ProductID}";
            return base.SaveChanges(sql);
        }
        public bool exists(Product prod)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON Products.CategoryID=Categories.CategoryID)" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID WHERE Products.CategoryID=@CATID AND Products.SizeID=@SIZEID AND Products.ColorID=@COLORID";
            command.Parameters.Add(new OleDbParameter("@CATID", prod.TheCategory.CategoryId));
            command.Parameters.Add(new OleDbParameter("@SIZEID", prod.TheSize.SizeID));
            command.Parameters.Add(new OleDbParameter("@COLORID", prod.TheColor.ColorID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public ProductsLst SelectByCategoryColorSize(int CategoryID, int ColorID, int SizeID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON (Products.CategoryID=Categories.CategoryID))" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID" +
                " WHERE Products.CategoryID=@CATID AND Products.ProductInStock>0";
            command.Parameters.Add(new OleDbParameter("@CATID", CategoryID));
            if (ColorID != -1)
            {
                command.CommandText += " AND Products.ColorID=COLID";
                command.Parameters.Add(new OleDbParameter("@COLID", ColorID));
            }
            if (SizeID != -1)
            {
                command.CommandText += " AND Products.SizeID=SZEID";
                command.Parameters.Add(new OleDbParameter("@SZEID", SizeID));
            }
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new ProductsLst(list);
        }
        public ProductsLst SelectProductsConditioned(string name, bool empty)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM (([Products] INNER JOIN [Categories] ON (Products.CategoryID=Categories.CategoryID))" +
                " INNER JOIN [Colors] ON Colors.ColorID=Products.ColorID)" +
                " INNER JOIN [Sizes] ON Sizes.SizeID=Products.SizeID" +
                " WHERE (CategoryName LIKE @CAT OR ColorName LIKE @COL OR SizeName LIKE @SZE)";
            if (empty)
            {
                command.CommandText += " AND Products.ProductInStock=0";
            }
            command.Parameters.Add(new OleDbParameter("@CAT", "%" + name + "%"));
            command.Parameters.Add(new OleDbParameter("@COL", "%" + name + "%"));
            command.Parameters.Add(new OleDbParameter("@SZE", "%" + name + "%"));
            List<BaseEntity> list = base.Select();
            return new ProductsLst(list);
        }
    }
}

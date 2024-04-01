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
    public class CategoryDB : BaseDB
    {
        protected override BaseEntity NewEntity()
        {
            return new Category();
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Category category = entity as Category;
            category.CategoryId = int.Parse(reader["CategoryID"].ToString());
            category.CategoryName = reader["CategoryName"].ToString();
            category.CategoryDescription = reader["CategoryDescription"].ToString();
            category.CategoryImageLink = reader["CategoryImageLink"].ToString();
            category.CategoryActive = (bool)reader["CategoryActive"];

            return category;
        }
        public CategoriesLst SelectCategoriesDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new CategoriesLst(list);
        }
        public CategoriesLst SelectCategoriesConditioned(string name, bool cond)
        {
            command.Parameters.Clear();
            if (cond)
                command.CommandText = "SELECT * FROM Categories WHERE CategoryName LIKE @CNAME";
            else
                command.CommandText = "SELECT * FROM Categories WHERE CategoryName LIKE @CNAME AND CategoryActive=True";
            command.Parameters.Add(new OleDbParameter("@CNAME", "%" + name + "%"));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new CategoriesLst(list);
        }
        public Category SelectByCategoryID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories WHERE CategoryID=@ID AND CategoryActive=True";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Category;
            return null;
        }
        public int Update(Category category)
        {
            string sql = $"UPDATE Categories SET CategoryName = '{category.CategoryName}', CategoryImageLink='{category.CategoryImageLink}', CategoryDescription='{category.CategoryDescription}', CategoryActive={category.CategoryActive} " +
                $"WHERE CategoryID={category.CategoryId}";
            return base.SaveChanges(sql);
        }
        public bool NameExists(string CategoryName)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories WHERE CategoryName=@CN";
            command.Parameters.Add(new OleDbParameter("@CN", CategoryName));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool NameExistsOtherCategory(string CategoryName, int CategoryID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories WHERE CategoryName=@CN AND CategoryID!=@CID";
            command.Parameters.Add(new OleDbParameter("@CN", CategoryName));
            command.Parameters.Add(new OleDbParameter("@CID", CategoryID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool ImageNameExistsOtherCategory(string ImageName, int CategoryID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories WHERE CategoryImageName=@IN AND NOT CategoryID=@CID";
            command.Parameters.Add(new OleDbParameter("@IN", ImageName));
            command.Parameters.Add(new OleDbParameter("@CID", CategoryID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool ImageNameExists(string ImageName)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Categories WHERE CategoryImageName=@IN";
            command.Parameters.Add(new OleDbParameter("@IN", ImageName));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public int Insert(Category category)
        {
            string sql = $"INSERT INTO Categories(CategoryName, CategoryDescription, CategoryImageLink, CategoryActive)" +
                $"VALUES('{category.CategoryName}','{category.CategoryDescription}','{category.CategoryImageLink}', {category.CategoryActive})";
            int records = base.SaveChanges(sql);
            category.CategoryId = base.lastId;
            return records;
        }
        public int delete(Category category)
        {
            category.CategoryActive = false;
            return Update(category);
        }
    }
}

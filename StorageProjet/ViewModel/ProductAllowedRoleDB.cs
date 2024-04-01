using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Model;

namespace ViewModel
{
    public class ProductAllowedRoleDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            ProductAllowedRole productAllowedRole = entity as ProductAllowedRole;
            Category cat = new Category();
            cat.CategoryId = int.Parse(reader["Categories.CategoryID"].ToString());
            cat.CategoryName = reader["CategoryName"].ToString();
            cat.CategoryDescription = reader["CategoryDescription"].ToString();
            cat.CategoryImageLink = reader["CategoryImageLink"].ToString();
            cat.CategoryActive = (bool)reader["CategoryActive"];

            Role rol = new Role();
            rol.RoleID = int.Parse(reader["Roles.RoleID"].ToString());
            rol.RoleName = reader["RoleName"].ToString();

            productAllowedRole.TheCategory = cat;
            productAllowedRole.TheRole = rol;

            return productAllowedRole;
        }

        protected override BaseEntity NewEntity()
        {
            return new ProductAllowedRole();
        }
        public ProductsAllowedRolesLst SelectByCategoryID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM ([ProductsAllowedRoles] INNER JOIN [Roles] ON (ProductsAllowedRoles.RoleID=Roles.RoleID))" +
                " INNER JOIN [Categories] ON (Categories.CategoryID=ProductsAllowedRoles.CategoryID)" +
                " WHERE Categories.CategoryID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return null;
            return new ProductsAllowedRolesLst(list);
        }
        public void deleteAllRolesForCateID(int ID)
        {
            command.Parameters.Clear();
            string sql = $"Delete From ProductsAllowedRoles WHERE CategoryID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            base.SaveChanges(sql);
        }
        public void addRolesToCategoryID(RolesLst RL, Category cat)
        {
            if (RL == null)
                return;
            foreach(Role r in RL)
            {
                Insert(new ProductAllowedRole(cat, r));
            }
        }
        public int Insert(ProductAllowedRole PAR)
        {
            string sql = $"INSERT INTO ProductsAllowedRoles(CategoryID, RoleID)" +
                $"VALUES({PAR.TheCategory.CategoryId}, {PAR.TheRole.RoleID})";
            int records = base.SaveChanges(sql);
            return records;
        }

    }
}

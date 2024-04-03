using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel
{
    public class RoleDB : BaseDB
    {
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Role r = entity as Role;
            r.RoleID = int.Parse(reader["RoleID"].ToString());
            r.RoleName = reader["RoleName"].ToString();
            return r;
        }

        protected override BaseEntity NewEntity()
        {
            return new Role();
        }
        public Role SelectByRoleID(int ID)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Roles WHERE RoleID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", ID));
            List<BaseEntity> list = base.Select();
            if (list.Count == 1) return list[0] as Role;
            return null;
        }
        public RolesLst SelectRolesConditioned(string Rolename)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Roles WHERE RoleName LIKE @RName";
            command.Parameters.Add(new OleDbParameter("@RName", "%" + Rolename + "%"));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new RolesLst(list);
        }
        public bool NameExists(string name)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Roles WHERE RoleName=@NAME";
            command.Parameters.Add(new OleDbParameter("@NAME", name));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public bool NameExistsOtherRole(Role role, string name)
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Roles WHERE RoleName=@NAME AND NOT RoleID=@ID";
            command.Parameters.Add(new OleDbParameter("@ID", role.RoleID));
            command.Parameters.Add(new OleDbParameter("@NAME", name));
            List<BaseEntity> list = base.Select();
            if (list.Count == 0) return false;
            return true;
        }
        public RolesLst SelectRolesDB()
        {
            command.Parameters.Clear();
            command.CommandText = "SELECT * FROM Roles";
            List<BaseEntity> list = base.Select();
            if (list.Count == 0)
                return null;
            return new RolesLst(list);
        }
        public int Insert(Role role)
        {
            string sql = $"INSERT INTO Roles(RoleName)" +
                $" VALUES('{role.RoleName}')";
            int records = base.SaveChanges(sql);
            role.RoleID = base.lastId;
            return records;
        }
        public int delete(Role role)
        {
            string sql = $"Delete From Roles WHERE RoleID={role.RoleID}";
            return base.SaveChanges(sql);
        }
        public int Update(Role role)
        {
            string sql = $"UPDATE Roles SET RoleName = '{role.RoleName}'"+
                $"WHERE RoleID={role.RoleID}";
            return base.SaveChanges(sql);
        }
    }
}

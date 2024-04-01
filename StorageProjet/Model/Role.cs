using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Role : BaseEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ProductsAllowedRolesLst TheProductAllowedRoles { get; set; }
        public bool IsAdminRole
        {
            get
            {
                return RoleID == 1;
            }
        }

        //empty constructor.
        public Role() { return; }
        //copy constructor.
        public Role(Role role)
        {
            this.RoleID = role.RoleID;
            this.RoleName = role.RoleName;
            this.TheProductAllowedRoles = new ProductsAllowedRolesLst(role.TheProductAllowedRoles);
        }
        public Role(int roleID, string roleName)
        {
            this.RoleID = roleID;
            this.RoleName = roleName;
        }
        //new Category with all properties.
        public Role(int roleID, string roleName, ProductsAllowedRolesLst theProductAllowedRoles)
        {
            this.RoleID = roleID;
            this.RoleName = roleName;
            this.TheProductAllowedRoles = theProductAllowedRoles;
        }
        public bool NameExistsOtherRole(string name)
        {
            RoleDB rolDB = new RoleDB();
            return rolDB.NameExistsOtherRole(this, name);
        }
        public void UpdateRole(string name)
        {
            RoleDB rolDB = new RoleDB();
            this.RoleName = name;
            rolDB.Update(this);
        }
        public void DeleteRole()
        {
            RoleDB rolDB = new RoleDB();
            rolDB.delete(this);
        }
        public void AddRole()
        {
            RoleDB rolDB = new RoleDB();
            this.RoleID = rolDB.Insert(this);
        }
    }
}
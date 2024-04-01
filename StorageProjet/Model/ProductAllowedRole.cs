using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ProductAllowedRole : BaseEntity
    {
        public Category TheCategory { get; set; }
        public Role TheRole { get; set; }

        //empty constructor.
        public ProductAllowedRole() { return; }
        //copy constructor.
        public ProductAllowedRole(ProductAllowedRole productAllowedRole)
        {
            this.TheCategory = new Category(productAllowedRole.TheCategory);
            this.TheRole = new Role(productAllowedRole.TheRole);
        }
        //new Category with all properties.
        public ProductAllowedRole(Category theCategory, Role theRole)
        {
            this.TheCategory = theCategory;
            this.TheRole = theRole;
        }
    }
}

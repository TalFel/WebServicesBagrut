using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public string CategoryImageLink { get; set; } = string.Empty;
        public bool CategoryActive { get; set; }
        public string productsString
        {
            get
            {
                ProductsLst products = getProductsLst();
                if(products == null) { 
                    return string.Empty;
                }
                string str = "";
                foreach(Product p in products)
                {
                    str += p.productString + "\n";

                }
                return str;
            }
        }
        public string roleString
        {
            get
            {
                RolesLst roles = getProductsAllowedRolesLst();
                if (roles == null)
                {
                    return "לא מאושר";
                }
                string str = "";
                foreach (Role r in roles)
                {
                    str += r.RoleName + ", \n";
                }
                return str;
            }
        }

        //empty constructor.
        public Category() { return; }
        //copy constructor.
        public Category(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.CategoryName = category.CategoryName;
            this.CategoryDescription = category.CategoryDescription;
            this.CategoryImageLink = category.CategoryImageLink;
            this.CategoryActive = category.CategoryActive;
        }
        //new Category with all properties.
        public Category(int categoryId, string categoryName, string categoryDescription,
            string categoryImageLink)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;
            this.CategoryImageLink = categoryImageLink;
            this.CategoryActive = false;
        }
        public Category(string categoryName, string categoryDescription, string categoryImageLink)
        {
            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;
            this.CategoryImageLink = categoryImageLink;
            this.CategoryActive = true;
        }
        public void AddCategory()
        {
            CategoryDB categoryDB = new CategoryDB();
            this.CategoryId = categoryDB.Insert(this);
        }
        public bool NameExistsOtherCategory(string name)
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.NameExistsOtherCategory(name, this.CategoryId);
        }
        public bool ImageNameExistsOtherCategory(string name)
        {
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.ImageNameExistsOtherCategory(name, this.CategoryId);
        }
        public int UpdateCategory(string name, string desc, string Imagelink, bool active)
        {
            this.CategoryActive = active;
            this.CategoryName = name;
            this.CategoryDescription = desc;
            if(!Imagelink.Equals(""))
                this.CategoryImageLink = Imagelink;
            CategoryDB categoryDB = new CategoryDB();
            return categoryDB.Update(this);
        }
        public void Update()
        {
            CategoryDB categoryDB = new CategoryDB();
            categoryDB.Update(this);
        }
        public void DeleteCategory()
        {
            CategoryDB categoryDB = new CategoryDB();
            categoryDB.delete(this);
        }
        public void updateProductAllowedRoles(RolesLst RL)
        {
            ProductAllowedRoleDB PARDB = new ProductAllowedRoleDB();
            PARDB.deleteAllRolesForCateID(this.CategoryId);
            PARDB.addRolesToCategoryID(RL, this);
        }
        public ColorLst SelectColorLstOfCategory()
        {
            ProductDB PDB = new ProductDB();
            ProductsLst PL =  PDB.SelectByCategoryID(this.CategoryId);
            if (PL == null)
                return null;
            ColorLst CL = new ColorLst();
            foreach(Product product in PL)
            {
                bool exists = false;
                foreach(Color tempColor in CL)
                {
                    if (tempColor.ColorID == product.TheColor.ColorID)
                        exists = true;
                }
                if (!exists)
                    CL.Add(product.TheColor);
            }
            return CL;
        }
        public SizesLst SelectSizesLstOfCategory()
        {
            ProductDB PDB = new ProductDB();
            ProductsLst PL = PDB.SelectByCategoryID(this.CategoryId);
            if (PL == null)
                return null;
            SizesLst SL = new SizesLst();
            foreach (Product product in PL)
            {
                bool exists = false;
                foreach (Size tempSize in SL)
                {
                    if (tempSize.SizeID == product.TheSize.SizeID)
                        exists = true;
                }
                if (!exists)
                    SL.Add(product.TheSize);
            }
            return SL;
        }
        public ProductsLst SelectProductsConditioned(int ColorID, int SizeID)
        {
            ProductDB productDB = new ProductDB();
            return productDB.SelectByCategoryColorSize(this.CategoryId, ColorID, SizeID);
        }
        public bool RoleAllowedForCategory(Role role)
        {
            RolesLst RL = getProductsAllowedRolesLst();
            bool exists = false;
            if (RL == null)
                return false;
            foreach (Role r in RL)
            {
                if (r.RoleID == role.RoleID)
                    exists = true;
            }
            return exists;
        }
        public RolesLst getProductsAllowedRolesLst()
        {
            ProductAllowedRoleDB PARDB = new ProductAllowedRoleDB();
            RolesLst RL = new RolesLst();
            ProductsAllowedRolesLst PARL = PARDB.SelectByCategoryID(CategoryId);
            if (PARL == null)
                return null;
            foreach (ProductAllowedRole PAR in PARL)
            {
                RL.Add(PAR.TheRole);
            }
            return RL;
        }
        public ProductsLst getProductsLst()
        {
            ProductDB PDB = new ProductDB();
            return PDB.SelectByCategoryID(this.CategoryId);
        }
    }
}

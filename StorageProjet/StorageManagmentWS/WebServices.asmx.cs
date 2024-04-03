using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ViewModel;
using Model;

namespace StorageManagmentWS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebServices : System.Web.Services.WebService
    {
        //consts
        private const string SRC_DIR = "https://localhost:44337/";

        [WebMethod]
        public void UploadFile(string fileName, string contentType, byte[] bytes)
        {
            string filePath = Server.MapPath(string.Format("~/PhotosWS/{0}", fileName));
            File.WriteAllBytes(filePath, bytes);
            string path = @"D:\StorageProjet\StorageManagment\CategoriesImages\" + fileName;
            File.Move(filePath, path);
        }

        [WebMethod]
        public void UpdateImage(CategoryWS categoryWS)
        {
            Category category = categoryWS.GetCategory();
            category.Update();
        }

        //get all products
        [WebMethod]
        public List<ProductWS> GetAllProducts()
        {
            ProductDB PDB = new ProductDB();
            ProductsLst plst = PDB.SelectProductsDB();
            List<ProductWS> ret = new List<ProductWS>();
            foreach (Product p in plst)
                ret.Add(new ProductWS(p));
            return ret;
        }

        //get products that needs to be resupplied
        [WebMethod]
        public List<ProductWS> GetProductsConditioned(string name, bool empty)
        {
            ProductDB PDB = new ProductDB();
            ProductsLst plst = PDB.SelectProductsConditioned(name, empty);
            List<ProductWS> ret = new List<ProductWS>();
            if (plst == null || plst.Count == 0)
                return new List<ProductWS>();
            foreach (Product p in plst)
                ret.Add(new ProductWS(p));
            return ret;
        }

        //update amount of a spesific product
        [WebMethod]
        public void AddAmount(ProductWS product, int amount)
        {
            ProductDB PDB = new ProductDB();
            product.Amount += amount;
            PDB.Update(product.GetProduct());
        }

        //get the new URL of an image
        [WebMethod]
        public string GetNewUrl(CategoryWS category)
        {
            return SRC_DIR + category.CategoryImageLink.Substring(2);
        }

        //get all categories
        [WebMethod]
        public List<CategoryWS> GetAllCategories()
        {
            CategoryDB CDB = new CategoryDB();
            CategoriesLst clst = CDB.SelectCategoriesDB();
            if (clst == null || clst.Count == 0)
                return new List<CategoryWS>();
            List<CategoryWS> ret = new List<CategoryWS>();
            foreach (Category c in clst)
                ret.Add(new CategoryWS(c));
            return ret;
        }

        //get all categories by name search
        [WebMethod]
        public List<CategoryWS> GetAllCategoriesConditioned(string name)
        {
            CategoryDB CDB = new CategoryDB();
            CategoriesLst clst = CDB.SelectCategoriesConditioned(name, false);
            if (clst == null || clst.Count == 0)
                return new List<CategoryWS>();
            List<CategoryWS> ret = new List<CategoryWS>();
            foreach (Category c in clst)
                ret.Add(new CategoryWS(c));
            return ret;
        }
    }
}

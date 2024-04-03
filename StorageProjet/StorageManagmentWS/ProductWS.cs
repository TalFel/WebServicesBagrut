using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace StorageManagmentWS
{
    public class ProductWS
    {
        public int ProductID { get; set; }
        public CategoryWS TheCategory { get; set; }
        public ColorWS TheColor { get; set; }
        public SizeWS TheSize { get; set; }
        public int Amount { get; set; }
        public string ProductCatColSzeString
        {
            get
            {
                return $"{TheCategory.CategoryName} {TheColor.ColorName} {TheSize.SizeName}";
            }
            set { }
        }
        //empty constructor.
        public ProductWS() { return; }
        //copy constructor.
        public ProductWS(ProductWS product)
        {
            this.ProductID = product.ProductID;
            this.TheCategory = product.TheCategory;
            this.TheColor = product.TheColor;
            this.TheSize = product.TheSize;
            this.Amount = product.Amount;
        }
        public ProductWS(Product product)
        {
            this.ProductID = product.ProductID;
            this.TheCategory = new CategoryWS(product.TheCategory);
            this.TheColor = new ColorWS(product.TheColor);
            this.TheSize = new SizeWS(product.TheSize);
            this.Amount = product.Amount;
        }
        //new Category with all properties.
        public ProductWS(int productID, CategoryWS category, ColorWS color, SizeWS size)
        {
            this.ProductID = productID;
            this.TheCategory = category;
            this.TheColor = color;
            this.TheSize = size;
            Amount = 1;
        }
        public ProductWS(int productID, CategoryWS category, ColorWS color, SizeWS size, int amount)
        {
            this.ProductID = productID;
            this.TheCategory = category;
            this.TheColor = color;
            this.TheSize = size;
            this.Amount = amount;
        }
        public Product GetProduct()
        {
            Product p = new Product();
            p.ProductID = this.ProductID;
            p.TheCategory = this.TheCategory.GetCategory();
            p.TheColor = this.TheColor.GetColor();
            p.TheSize = this.TheSize.GetSize();
            p.Amount = this.Amount;
            return p;
        }
    }
}
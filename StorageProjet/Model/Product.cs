using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Model
{
    public class Product : BaseEntity
    {
        public int ProductID { get; set; }
        public Category TheCategory { get; set; }
        public Color TheColor { get; set; }
        public Size TheSize { get; set; }
        public int Amount { get; set; }
        public string productString
        {
            get
            {
                return "(" + TheColor.ColorName + "," + TheSize.SizeName + ")\n";
            }
        }
        public string ProductStringToUser
        {
            get
            {
                return $"צבע: {TheColor.ColorName}\n, גודל: {TheSize.SizeName}";
            }
        }
        public string ProductCatColSzeString
        {
            get
            {
                return $"{TheCategory.CategoryName} {TheColor.ColorName} {TheSize.SizeName}";
            }
        }
        public string ProductStatisticString
        {
            get
            {
                return $"קטגוריה: {TheCategory.CategoryName}\nצבע: {TheColor.ColorName}\nגודל:{TheSize.SizeName}\n";
            }
        }
        //empty constructor.
        public Product() { return; }
        //copy constructor.
        public Product(Product product)
        {
            this.ProductID = product.ProductID;
            this.TheCategory = product.TheCategory;
            this.TheColor = product.TheColor;
            this.TheSize = product.TheSize;
            this.Amount = product.Amount;
        }
        //new Category with all properties.
        public Product(int productID, Category category, Color color, Size size)
        {
            this.ProductID = productID;
            this.TheCategory = category;
            this.TheColor = color;
            this.TheSize = size;
            Amount = 1;
        }
        public Product(int productID, Category category, Color color, Size size, int amount)
        {
            this.ProductID = productID;
            this.TheCategory = category;
            this.TheColor = color;
            this.TheSize = size;
            this.Amount = amount;
        }
        public bool exists()
        {
            ProductDB PDB = new ProductDB();
            return PDB.exists(this);
        }
        public void InsertToDB()
        {
            ProductDB PDB = new ProductDB();
            PDB.Insert(this);
        }
        public override bool Equals(object obj)
        {
            Product PObj = (Product)obj;
            return this.TheCategory.CategoryId == PObj.TheCategory.CategoryId && this.TheColor.ColorID == PObj.TheColor.ColorID && this.TheSize.SizeID == PObj.TheSize.SizeID;
        }
        public void UpdateAmount(int NewAmount)
        {
            this.Amount = NewAmount;
            ProductDB PDB = new ProductDB();
            PDB.Update(this);
        }
    }
}

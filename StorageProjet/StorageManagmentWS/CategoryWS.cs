using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace StorageManagmentWS
{
    public class CategoryWS
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public string CategoryImageLink { get; set; } = string.Empty;
        public bool CategoryActive { get; set; }

        //empty constructor.
        public CategoryWS() { return; }
        //copy constructor.
        public CategoryWS(CategoryWS category)
        {
            this.CategoryId = category.CategoryId;
            this.CategoryName = category.CategoryName;
            this.CategoryDescription = category.CategoryDescription;
            this.CategoryImageLink = category.CategoryImageLink;
            this.CategoryActive = category.CategoryActive;
        }
        public CategoryWS(Category category)
        {
            this.CategoryId = category.CategoryId;
            this.CategoryName = category.CategoryName;
            this.CategoryDescription = category.CategoryDescription;
            this.CategoryImageLink = category.CategoryImageLink;
            this.CategoryActive = category.CategoryActive;
        }
        //new Category with all properties.
        public CategoryWS(int categoryId, string categoryName, string categoryDescription,
            string categoryImageLink)
        {
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;
            this.CategoryImageLink = categoryImageLink;
            this.CategoryActive = false;
        }
        public CategoryWS(string categoryName, string categoryDescription, string categoryImageLink)
        {
            this.CategoryName = categoryName;
            this.CategoryDescription = categoryDescription;
            this.CategoryImageLink = categoryImageLink;
            this.CategoryActive = true;
        }
        public Category GetCategory()
        {
            Category cat = new Category();
            cat.CategoryId = this.CategoryId;
            cat.CategoryName = this.CategoryName;
            cat.CategoryDescription = this.CategoryDescription;
            cat.CategoryActive = this.CategoryActive;
            cat.CategoryImageLink = this.CategoryImageLink;
            return cat;
        }
    }
}
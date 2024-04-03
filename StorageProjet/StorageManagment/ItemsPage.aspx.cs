using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StorageManagment
{
    public partial class ItemsPage : System.Web.UI.Page
    {
        public int page;
        public static int rowsPerPage = 2;
        public static int columnsPerRow = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/OrdersAdminPage.aspx");
            if (!IsPostBack)
            {
                HyperGoBackToOrder.Visible = false;
                page = 0;
                Session["DataCategories"] = null;
                Session["ConditionCategories"] = null;
                loadCategories();
                err1.Visible = false;
                err2.Visible = false;
                err3.Visible = false;
            }
            if (Request.QueryString["PastOrder"].Equals("True"))
            {
                HyperGoBackToOrder.Visible = true;
            }
        }
        protected void loadCategories()
        {
            p1.Visible = true;
            p2.Visible = false;
            p3.Visible = false;
            if (Session["ConditionCategories"] == null)
            {
                CategoriesLst CL = General.SelectCategoriesDB();
                Session["DataCategories"] = new CategoriesLst(CL.FindAll(category => category.RoleAllowedForCategory(((User)Session["CurrentUser"]).UserRole)));
            }
            else
            {
                if (Session["ConditionCategories"] == null)
                    Session["ConditionCategories"] = "";
                CategoriesLst CL = General.SelectCategoriesConditioned(Session["ConditionCategories"].ToString(), true);
                Session["DataCategories"] = new CategoriesLst(CL.FindAll(category => category.RoleAllowedForCategory(((User)Session["User"]).UserRole)));
            }

            if ((CategoriesLst)Session["DataCategories"] == null)
            {
                err1.Text = "לא נמצאו קטגוריות העונות על התנאים";
                err1.Visible = true;
                return;
            }
            err1.Text = "";
            err1.Visible = false;
            CategoriesLst data = (CategoriesLst)Session["DataCategories"];
            CategoriesLst pageData = new CategoriesLst();
            for(int i = 0; i < data.Count; i++)
            {
                if(i >= page*columnsPerRow*rowsPerPage && i < (page + 1) * columnsPerRow * rowsPerPage)
                {
                    pageData.Add(data[i]);
                }
            }
            IdagidogDataList.DataSource = pageData;
            IdagidogDataList.DataBind();
        }

        protected void SearchItems_Click(object sender, EventArgs e)
        {
            if (ByCategoryName.Text.Length == 0)
            {
                Session["ConditionCategories"] = null;
            }
            else
            {
                page = 0;
                Session["ConditionCategories"] = ByCategoryName.Text;
            }
            loadCategories();
        }

        protected void prev_Click(object sender, EventArgs e)
        {
            page = Math.Max(page--, 0);
            loadCategories();
        }

        protected void next_Click(object sender, EventArgs e)
        {
            double tempceil = ((CategoriesLst)Session["DataCategories"]).Count / (columnsPerRow * rowsPerPage);
            page = Math.Min(page++, (int)Math.Ceiling(tempceil)-1);
            loadCategories();
        }

        protected void IdagidogDataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Category cat = ((CategoriesLst)Session["DataCategories"])[e.Item.ItemIndex];
            Session["CategoryToOrder"] = cat;

            //DLLs
            ColorLst CL = cat.SelectColorLstOfCategory();
            if (CL == null)
                CL = new ColorLst();
            CL.Insert(0, new Color(-1, "בחר צבע"));

            ColorsDDL.DataSource = CL;
            ColorsDDL.DataTextField = "ColorName";
            ColorsDDL.DataValueField = "ColorID";
            ColorsDDL.DataBind();

            SizesLst SL = cat.SelectSizesLstOfCategory();
            if (SL == null)
                SL = new SizesLst();
            SL.Insert(0, new Size(-1, "בחר צורה/גודל"));

            SizesDDL.DataSource = SL;
            SizesDDL.DataTextField = "SizeName";
            SizesDDL.DataValueField = "SizeID";
            SizesDDL.DataBind();

            loadProducts();

        }

        public void loadProducts()
        {
            p1.Visible = false;
            p2.Visible = true;
            p3.Visible = false;
            Category cat = (Category)Session["CategoryToOrder"];
            CatLabel.Text = cat.CategoryName;

            if (Session["ConditionProductsItemsPage"] == null)
                Session["DataProductsItemsPage"] = cat.SelectProductsConditioned(-1, -1);
            else
            {
                if (Session["ConditionProductsItemsPage"] == null)
                    Session["ConditionProductsItemsPage"] = "";
                Session["DataProductsItemsPage"] = cat.SelectProductsConditioned(int.Parse(Session["COLID"].ToString()), int.Parse(Session["SZEID"].ToString()));
            }

            if ((ProductsLst)Session["DataProductsItemsPage"] == null)
            {
                err2.Text = "לא נמצאו מוצרים העונים על התנאים";
                err2.Visible = true;
                return;
            }
            else
            {
                err2.Text = "";
                err2.Visible = false;
                GridView1.DataSource = (ProductsLst)Session["DataProductsItemsPage"];
                GridView1.DataBind();
            }
        }
        protected void SearchSpecifcProduct_Click(object sender, EventArgs e)
        {
            Session["ConditionProductsItemsPage"] = true;
            Session["COLID"] = ColorsDDL.SelectedValue;
            Session["SZEID"] = SizesDDL.SelectedValue;
            loadProducts();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            Label lbl = (Label)e.Row.FindControl("ProductText");
            lbl.Text = ((Product)e.Row.DataItem).ProductStringToUser;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            p3.Visible = true;
            p2.Visible = false;

            CatLabel2.Text = ((ProductsLst)Session["DataProductsItemsPage"])[GridView1.SelectedIndex].ProductCatColSzeString;
            AmountInputTB.Attributes.Add("placeHolder", "כרגע במלאי: " + ((ProductsLst)Session["DataProductsItemsPage"])[GridView1.SelectedIndex].Amount);
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PastOrder"].Equals("True"))
                Response.Redirect("/ItemsPage.aspx?PastOrder=True");
            Response.Redirect("/ItemsPage.aspx?PastOrder=False");
        }

        protected void AddToOrder_Click(object sender, EventArgs e)
        {
            bool isHistory = Request.QueryString["PastOrder"].Equals("True");

            if (!isHistory && Session["CurrentOrder"] == null)
            {
                Session["CurrentOrder"] = new ProductsInOrdersLst();
            }
            ProductInOrder newProduct;
            try
            {
                newProduct = new ProductInOrder(((ProductsLst)Session["DataProductsItemsPage"])[GridView1.SelectedIndex], int.Parse(AmountInputTB.Text));
            }
            catch(Exception excp)
            {
                err3.Text = "הכנס מספר שלם בלבד";
                err3.Visible = true;
                return;
            }
            //not enough left in stock
            if((((ProductsLst)Session["DataProductsItemsPage"])[GridView1.SelectedIndex]).Amount < int.Parse(AmountInputTB.Text))
            {
                err3.Text = "אין מספיק במחסן! נסו להזמין פחות או לבחור פריט אחר.";
                err3.Visible = true;
                return;
            }

            if(isHistory)
                ((ProductsInOrdersLst)Session["OrderData"]).AddToList(newProduct);
            else
                ((ProductsInOrdersLst)Session["CurrentOrder"]).AddToList(newProduct);
            p1.Visible = true;
            p2.Visible = false;
            p3.Visible = false;
            //check if order containes current product
            //if so, add amount to the product
            //redirect to items
        }
    }
}
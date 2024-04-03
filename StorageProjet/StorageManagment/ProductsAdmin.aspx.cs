using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Model;
using Panel = System.Web.UI.WebControls.Panel;
using Label = System.Web.UI.WebControls.Label;
using System.EnterpriseServices;
using CheckBox = System.Web.UI.WebControls.CheckBox;

namespace StorageManagment
{
    public partial class ProductsAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (((User)Session["CurrentUser"]).UserID != 1)
                    Response.Redirect("/ProfilePage.aspx");

                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;


                #region base
                hide(Panel31, Panel32, Panel33, err31, err32, err33);
                LoadSizesGV();

                hide(Panel21, Panel22, Panel23, err21, err22, err23);
                LoadColorsGV();

                hide(Panel11, Panel12, Panel13, err11, err12, err13);
                Panel121.Visible = false;
                Panel122.Visible = false;
                LoadCategoriesGV();
                #endregion
            }
        }
        protected void LoadColorsGV()
        {
            if (Session["ConditionColors"] == null)
                Session["DataColors"] = General.SelectColorsDB();
            else
                Session["DataColors"] = General.SelectColorsConditioned(Session["ConditionColors"].ToString());
            if ((ColorLst)Session["DataColors"] == null)
            {
                err21.Text = "לא נמצאו צבעים העונים על התנאים";
                err21.Visible = true;
            }
            if (Session["ConditionColors"] != null)
                ByColorName.Text = Session["ConditionColors"].ToString();
            ColorsGV.DataSource = (ColorLst)Session["DataColors"];
            ColorsGV.DataBind();
        }
        protected void LoadSizesGV()
        {
            if (Session["ConditionSizes"] == null)
                Session["DataSizes"] = General.SelectSizesDB();
            else
                Session["DataSizes"] = General.SelectSizesConditioned(Session["ConditionSizes"].ToString());
            if ((SizesLst)Session["DataSizes"] == null)
            {
                err31.Text = "לא נמצאו צורות או גדלים העונים על התנאים";
                err31.Visible = true;
            }
            if (Session["ConditionSizes"] != null)
                BySizeName.Text = Session["ConditionSizes"].ToString();
            SizesGV.DataSource = (SizesLst)Session["DataSizes"];
            SizesGV.DataBind();
        }
        protected void LoadCategoriesGV()
        {
            if (Session["ConditionCategories"] == null && Session["ConditionCategoriesActive"] == null)
                Session["DataCategories"] = General.SelectCategoriesDB();
            else
            {
                if (Session["ConditionCategoriesActive"] != null)
                    NotActiveCat.Checked = (bool)Session["ConditionCategoriesActive"];
                if (Session["ConditionCategories"] != null)
                    ByCategoryName.Text = Session["ConditionCategories"].ToString();
                else
                    Session["ConditionCategories"] = "";
                Session["DataCategories"] = General.SelectCategoriesConditioned(Session["ConditionCategories"].ToString(), (bool)Session["ConditionCategoriesActive"]);
            }

            if ((CategoriesLst)Session["DataCategories"] == null)
            {
                err11.Text = "לא נמצאו קטגוריות העונות על התנאים";
                err11.Visible = true;
            }
            CategoriesGV.DataSource = (CategoriesLst)Session["DataCategories"];
            CategoriesGV.DataBind();
        }
        protected ProductsLst getSameCategory(ProductsLst proLst, Category cat)
        {
            ProductsLst pLst = new ProductsLst();
            foreach (Product p in proLst)
            {
                if (p.TheCategory.CategoryId == cat.CategoryId)
                    pLst.Add(p);
            }
            return pLst;
        }
        protected void hide(Panel p1, Panel p2, Panel p3, Label tb1, Label tb2, Label tb3)
        {
            p1.Visible = true;
            p2.Visible = false;
            p3.Visible = false;
            tb1.Visible = false;
            tb2.Visible = false;
            tb3.Visible = false;
        }
        protected void CancelBTN_Click(object sender, EventArgs e)
        {
            if(Panel22.Visible == true || Panel23.Visible == true)
            {
                LoadColorsGV();
                Panel21.Visible = true;
                Panel22.Visible = false;
                Panel23.Visible = false;
            }
            else if (Panel32.Visible == true || Panel33.Visible == true)
            {
                LoadSizesGV();
                Panel31.Visible = true;
                Panel32.Visible = false;
                Panel33.Visible = false;
            }
            else if(Panel121.Visible == true)
            {
                if ((bool)Session["EditingCategory"])
                {
                    Panel122.Visible = false;
                    Panel1211.Visible = false;
                    Panel1212.Visible = true;
                    CategoriesGV_SelectedIndexChanged(null, null);
                }
                else
                {
                    Panel121.Visible = false;
                    Panel12.Visible = false;
                    Panel11.Visible = true;
                }
            }
            else if(Panel13.Visible == true)
            {
                Panel13.Visible = false;
                Panel11.Visible = true;
            }
        }


        #region Size
        protected void DeleteSizeBTN_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((SizesLst)Session["DataSizes"])[SizesGV.SelectedIndex].DeleteSize();
                Response.Redirect("/ProductsAdmin.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        protected void SubmitChngSize_Click(object sender, EventArgs e)
        {
            Size size = ((SizesLst)Session["DataSizes"])[SizesGV.SelectedIndex];
            if (size.NameExistsOtherSize(SName.Text))
            {
                err32.Text = "השם כבר קיים במערכת";
                err32.Visible = true;
            }
            else
            {
                size.UpdateSize(SName.Text);
                err32.Text = "";
                err32.Visible = false;
                LoadSizesGV();
                Panel32.Visible = false;
                Panel31.Visible = true;
            }
        }

        protected void AddSizeToDB_Click(object sender, EventArgs e)
        {
            if (General.SizeNameExists(NewSizeName.Text))
            {
                err32.Text = "השם כבר קיים במערכת";
                err32.Visible = true;
            }
            else
            {
                Size size = new Size(NewSizeName.Text);
                size.AddSize();
                LoadSizesGV();
                Panel32.Visible = false;
                Panel33.Visible = false;
                Panel31.Visible = true;
            }
        }

        protected void SearchSizes_Click(object sender, EventArgs e)
        {
            if (BySizeName.Text.Length == 0)
                Session["ConditionSizes"] = null;
            else
                Session["ConditionSizes"] = BySizeName.Text;

            LoadSizesGV();
        }

        protected void SizesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel31.Visible = false;
            Panel32.Visible = true;

            Size sizeToChng = ((SizesLst)Session["DataSizes"])[SizesGV.SelectedIndex];
            SName.Text = sizeToChng.SizeName;
        }

        protected void AddSize_Click(object sender, EventArgs e)
        {
            Panel31.Visible = false;
            Panel33.Visible = true;
        }
        #endregion
        /*-------------------------------------------*/
        #region Color
        protected void DeleteColorBTN_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((ColorLst)Session["DataColors"])[ColorsGV.SelectedIndex].DeleteColor();
                Response.Redirect("/ProductsAdmin.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        protected void SubmitChngColor_Click(object sender, EventArgs e)
        {
            Color color = ((ColorLst)Session["DataColors"])[ColorsGV.SelectedIndex];
            if (color.NameExistsOtherColor(ColName.Text))
            {
                err22.Text = "השם כבר קיים במערכת";
                err22.Visible = true;
            }
            else
            {
                color.UpdateColor(ColName.Text);
                err22.Text = "";
                err22.Visible = false;
                LoadColorsGV();
                Panel22.Visible = false;
                Panel21.Visible = true;
            }
        }

        protected void AddColorToDB_Click(object sender, EventArgs e)
        {
            if (General.ColorNameExists(NewColorName.Text))
            {
                err22.Text = "השם כבר קיים במערכת";
                err22.Visible = true;
            }
            else
            {
                Color color = new Color(NewColorName.Text);
                color.AddColor();
                LoadColorsGV();
                Panel22.Visible = false;
                Panel23.Visible = false;
                Panel21.Visible = true;
            }
        }

        protected void SearchColors_Click(object sender, EventArgs e)
        {
            if (ByColorName.Text.Length == 0)
                Session["ConditionColors"] = null;
            else
                Session["ConditionColors"] = ByColorName.Text;

            LoadColorsGV();
        }

        protected void ColorsGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel21.Visible = false;
            Panel22.Visible = true;

            Color colorToChng = ((ColorLst)Session["DataColors"])[ColorsGV.SelectedIndex];
            ColName.Text = colorToChng.ColorName;
        }

        protected void AddColor_Click(object sender, EventArgs e)
        {
            Panel21.Visible = false;
            Panel23.Visible = true;
        }
        #endregion
        /*-------------------------------------------*/
        #region Category
        protected void CategoriesGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CategoriesGV.PageIndex = e.NewPageIndex;
            CategoriesGV.DataSource = (CategoriesLst)Session["DataCategories"];
            CategoriesGV.DataBind();
        }
        protected void DeleteCategoryBTN_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((CategoriesLst)Session["DataCategories"])[CategoriesGV.SelectedIndex].DeleteCategory();
                Response.Redirect("/ProductsAdmin.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        protected void SubmitChngCategory_Click(object sender, EventArgs e)
        {
            Category category = ((CategoriesLst)Session["DataCategories"])[CategoriesGV.SelectedIndex + CategoriesGV.PageIndex*3];
            RolesLst RL = new RolesLst();
            foreach (GridViewRow gvrow in AllowedRolesGV.Rows)
            {
                CheckBox checkbox = (CheckBox)gvrow.FindControl("AllowedCB");
                if (checkbox.Checked && gvrow.RowIndex >= 0)
                {
                    RL.Add(((RolesLst)Session["RolesLstProducts"])[gvrow.RowIndex]);
                }
            }
            category.updateProductAllowedRoles(RL);
            if (category.NameExistsOtherCategory(CatName.Text))
            {
                err12.Text = "השם כבר קיים במערכת";
                err12.Visible = true;
            }
            else if (FileUploadCategory.FileName != null && !FileUploadCategory.FileName.Equals(""))
            {
                if (category.ImageNameExistsOtherCategory(FileUploadCategory.FileName))
                {
                    err12.Text = "שם התמונה כבר קיים במערכת";
                    err12.Visible = true;
                }
                else
                {
                    FileUploadCategory.SaveAs(General.ProjectFolder + "StorageManagment/CategoriesImages/" + FileUploadCategory.FileName);
                    category.UpdateCategory(CatName.Text, CatDesc.Text, "~/CategoriesImages/" + FileUploadCategory.FileName, CatActiveCB.Checked);
                    err12.Text = "";
                    err12.Visible = false;
                    CategoriesGV_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                category.UpdateCategory(CatName.Text, CatDesc.Text, "", CatActiveCB.Checked);
                err12.Text = "";
                err12.Visible = false;
                CategoriesGV_SelectedIndexChanged(null, null);
            }

        }

        protected void AddCategoryToDB_Click(object sender, EventArgs e)
        {
            if (General.CategoryNameExists(NewCategoryName.Text))
            {
                err13.Text = "השם כבר קיים במערכת";
                err13.Visible = true;
            }
            else if (NewCategoryImage.FileName != null && !NewCategoryImage.FileName.Equals(""))
            {
                if (General.ImageNameExists(NewCategoryImage.FileName))
                {
                    err13.Text = "שם התמונה כבר קיים במערכת";
                    err13.Visible = true;
                }
                else
                {
                    NewCategoryImage.SaveAs(General.ProjectFolder + "StorageManagment/CategoriesImages/" + NewCategoryImage.FileName);
                    Category category = new Category(NewCategoryName.Text, NewCategoryDescription.Text, "~/CategoriesImages/" + NewCategoryImage.FileName);
                    category.AddCategory();
                    Response.Redirect("/ProductsAdmin.aspx");
                }
            }
            else
            {
                Category category = new Category(NewCategoryName.Text, NewCategoryDescription.Text, "");
                category.AddCategory();
                Response.Redirect("/ProductsAdmin.aspx");
            }
        }

        protected void SearchCategories_Click(object sender, EventArgs e)
        {
            if (ByCategoryName.Text.Length == 0)
            {
                Session["ConditionCategories"] = null;
                Session["ConditionCategoriesActive"] = NotActiveCat.Checked;
            }
            else
            {
                Session["ConditionCategories"] = ByCategoryName.Text;
                Session["ConditionCategoriesActive"] = NotActiveCat.Checked;
            }

            LoadCategoriesGV();
        }

        protected void CategoriesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["EditingCategory"] = false;
            Panel11.Visible = false;
            Panel12.Visible = true;
            Panel13.Visible = false;
            Panel121.Visible = true;
            Panel122.Visible = false;
            Panel1211.Visible = false;
            Panel1212.Visible = true;
            Panel12amount.Visible = false;

            Category categoryToChng = ((CategoriesLst)Session["DataCategories"])[CategoriesGV.SelectedIndex + 3 * CategoriesGV.PageIndex];
            CatName.Text = categoryToChng.CategoryName;
            CatDesc.Text = categoryToChng.CategoryDescription;
            CatActiveCB.Checked = categoryToChng.CategoryActive;
            CatActiveCB.Enabled = false;
            NameOfCategory.Text = categoryToChng.CategoryName;
            CatDescriptionNotEditing.Text = categoryToChng.CategoryDescription;

            Session["ProductsLst"] = categoryToChng.getProductsLst();
            ProductsGV.DataSource = (ProductsLst)Session["ProductsLst"];
            ProductsGV.DataBind();

            Session["RolesLstProducts"] = General.SelectRolesDB();
            AllowedRolesGV.DataSource = (RolesLst)Session["RolesLstProducts"];
            AllowedRolesGV.DataBind();

            EditCategoriesButton.Visible = true;
            SubmitChngCategory.Visible = false;
            CancelCategoryBTN11.Text = "חזור";
        }

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            Panel11.Visible = false;
            Panel13.Visible = true;
        }
        protected void EditCategoriesButton_Click(object sender, EventArgs e)
        {
            Session["EditingCategory"] = true;
            EditCategoriesButton.Visible = false;
            SubmitChngCategory.Visible = true;
            Session["RolesLstProducts"] = General.SelectRolesDB();
            AllowedRolesGV.DataSource = (RolesLst)Session["RolesLstProducts"];
            AllowedRolesGV.DataBind();
            ProductsGV.DataSource = (ProductsLst)Session["ProductsLst"];
            ProductsGV.DataBind();
            Panel1211.Visible = true;
            Panel1212.Visible = false;
            CatActiveCB.Enabled = true;
            CancelCategoryBTN11.Text = "בטל";
        }
        #endregion

        protected void GoBackTooptions_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            ChooseEdits.Visible = true;
        }

        protected void CatButton_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            ChooseEdits.Visible = false;
        }

        protected void ColButton_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
            ChooseEdits.Visible = false;
        }

        protected void SzeButton_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            ChooseEdits.Visible = false;
        }

        protected void loadAddProduct(object sender, EventArgs e)
        {
            Panel121.Visible = false;
            Panel122.Visible = true;

            loadColorsProductsGV();
            loadSizesProductsGV();
        }

        protected void loadColorsProductsGV(){
            if (Session["ConditionColorsProducts"] == null)
                Session["DataColorsProducts"] = General.SelectColorsDB();
            else
                Session["DataColorsProducts"] = General.SelectColorsConditioned(Session["ConditionColorsProducts"].ToString());
            if ((ColorLst) Session["DataColorsProducts"] == null)
            {
                err121.Text = "לא נמצאו צבעים העונים על התנאים";
                err121.Visible = true;
            }
            else
            {
                err121.Visible = false;
            }
            if (Session["ConditionColorsProducts"] != null)
                ByColorName.Text = Session["ConditionColorsProducts"].ToString();

            ColorPanelProducts.DataSource = (ColorLst) Session["DataColorsProducts"];
            ColorPanelProducts.DataBind();
        }
        protected void loadSizesProductsGV()
        {
            if (Session["ConditionSizesProducts"] == null)
                Session["DataSizesProducts"] = General.SelectSizesDB();
            else
                Session["DataSizesProducts"] = General.SelectSizesConditioned(Session["ConditionSizesProducts"].ToString());
            if ((SizesLst)Session["DataSizesProducts"] == null)
            {
                err122.Text = "לא נמצאו צבעים העונים על התנאים";
                err122.Visible = true;
            }
            else
            {
                err122.Visible = false;
            }
            if (Session["ConditionSizesProducts"] != null)
                ByColorName.Text = Session["ConditionSizesProducts"].ToString();

            SizePanelProducts.DataSource = (SizesLst)Session["DataSizesProducts"];
            SizePanelProducts.DataBind();
        }
        protected void SearchColorPanelProducts_Click(object sender, EventArgs e)
        {
            if (ByColorProductName.Text.Length == 0)
                Session["ConditionColorsProducts"] = null;
            else
                Session["ConditionColorsProducts"] = ByColorProductName.Text;

            loadColorsProductsGV();
        }
        protected void SearchSizePanelProducts_Click(object sender, EventArgs e)
        {
            if (BySizeProductName.Text.Length == 0)
                Session["ConditionSizesProducts"] = null;
            else
                Session["ConditionSizesProducts"] = BySizeProductName.Text;

            loadSizesProductsGV();
        }
        protected void ColorPanelProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NewColor"] = ((ColorLst)Session["DataColorsProducts"])[ColorPanelProducts.SelectedIndex];
        }
        protected void SizePanelProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["NewSize"] = ((SizesLst)Session["DataSizesProducts"])[SizePanelProducts.SelectedIndex];
        }

        protected void DoChangesoProduct_Click(object sender, EventArgs e)
        {
            if (amountLabel.Text.Equals(""))
                amountLabel.Text = "0";
            Category catToChng = ((CategoriesLst)Session["DataCategories"])[CategoriesGV.SelectedIndex +3 * CategoriesGV.PageIndex];
            Product prod = new Product(0, catToChng,
                ((ColorLst)Session["DataColorsProducts"])[ColorPanelProducts.SelectedIndex],
                ((SizesLst)Session["DataSizesProducts"])[SizePanelProducts.SelectedIndex], int.Parse(amountLabel.Text));
            if (prod.exists())
            {
                err123.Visible = true;
                err123.Text = "המוצר כבר קיים";
            }
            else
            {
                err123.Visible = false;
                err123.Text = "";
                prod.InsertToDB();
                CategoriesGV_SelectedIndexChanged(null, null);
            }
        }
        protected void CancelChange_Click(object sender, EventArgs e)
        {
            CategoriesGV_SelectedIndexChanged(null, null);
        }

        protected void AllowedRolesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            CheckBox cb = (CheckBox)(e.Row.FindControl("AllowedCB"));
            cb.Checked = (((CategoriesLst)Session["DataCategories"])[CategoriesGV.SelectedIndex + 3 * CategoriesGV.PageIndex]).RoleAllowedForCategory((Role)((RolesLst)Session["RolesLstProducts"])[e.Row.RowIndex]);
            cb.Enabled = (bool)Session["EditingCategory"];
        }

        protected void ProductsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            (e.Row.Cells[2]).Visible = (bool)Session["EditingCategory"];
        }

        protected void CategoriesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            e.Row.Cells[2].Enabled = (bool)Session["EditingCategory"];
        }

        protected void AllowedRolesGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AllowedRolesGV.PageIndex = e.NewPageIndex;
            AllowedRolesGV.DataSource = (RolesLst)Session["RolesLstProducts"];
            AllowedRolesGV.DataBind();
        }

        protected void ProductsGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductsGV.PageIndex = e.NewPageIndex;
            ProductsGV.DataSource = (ProductsLst)Session["ProductsLst"];
            ProductsGV.DataBind();
        }

        protected void ProductsGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product prodToChng = ((ProductsLst)Session["ProductsLst"])[ProductsGV.SelectedIndex + ProductsGV.PageIndex*10];
            AmountHeader.Text = prodToChng.ProductCatColSzeString;
            Session["ProdToChngAmount"] = prodToChng;
            CurrentAmount.Text = "כמות נוכחית: " + prodToChng.Amount;
            Panel12.Visible = false;
            Panel12amount.Visible = true;
        }

        protected void CancelUpdateAmount_Click(object sender, EventArgs e)
        {
            Panel12.Visible = true;
            Panel12amount.Visible = false;
        }

        protected void UpdateAmount_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(UpdateAmountValue.Text);
                if(x < 0)
                    throw new Exception();
            }
            catch(Exception exp)
            {
                errAmount.Text = "כמות צריכה להיות מספר לא שלילי";
                errAmount.Visible = true;
                return;
            }

            ((Product)Session["ProdToChngAmount"]).UpdateAmount(int.Parse(UpdateAmountValue.Text));
            Panel12.Visible = true;
            Panel12amount.Visible = false;
            CategoriesGV_SelectedIndexChanged(null, null);

        }
    }
}
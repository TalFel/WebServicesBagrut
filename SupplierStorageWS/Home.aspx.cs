using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SupplierStorageWS
{
    public partial class Home : System.Web.UI.Page
    {
        protected int page;
        private int pageSize = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = 0;
                zofim.WebServices proxy = new zofim.WebServices();
                
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                Session["CategoriesData"] = proxy.GetAllCategories().ToList();
                CategoriesDataList.DataSource = getCategoriesPageData();
                CategoriesDataList.DataBind();

                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                Session["ProductsData"] = proxy.GetAllProducts().ToList();
                RestockGV.DataSource = (List<zofim.ProductWS>)Session["ProductsData"];
                RestockGV.DataBind();

                ChoosePanel.Visible = true;
                ViewRestockPanel.Visible = false;
                ViewCategoriesPanel.Visible = false;
            }
        }

        public List<zofim.CategoryWS> getCategoriesPageData()
        {
            List<zofim.CategoryWS> lst = (List<zofim.CategoryWS>)Session["CategoriesData"];
            List<zofim.CategoryWS> ret = new List<zofim.CategoryWS>();
            for (int i = page*pageSize; i < (page + 1) * pageSize && i < lst.Count; i++)
            {
                ret.Add(lst[i]);
            }
            return ret;
        }

        protected void CategoriesDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            zofim.WebServices proxy = new zofim.WebServices();
            Image img = (Image)(e.Item.FindControl("Image"));
            zofim.CategoryWS category = (zofim.CategoryWS)e.Item.DataItem;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            img.ImageUrl = proxy.GetNewUrl(category);
        }

        protected void CategoriesDataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            CategoriesDataListPanel.Visible = true;
            UpdateCategoryImagePanel.Visible = false;
        }

        protected void prev_Click(object sender, EventArgs e)
        {
            page = Math.Max(0, page - 1);
            CategoriesDataList.DataSource = getCategoriesPageData();
            CategoriesDataList.DataBind();
        }

        protected void next_Click(object sender, EventArgs e)
        {
            int maxPage = (((List<zofim.CategoryWS>)Session["CategoriesData"]).Count - 1) / pageSize;
            page = Math.Min(maxPage, page + 1);
            CategoriesDataList.DataSource = getCategoriesPageData();
            CategoriesDataList.DataBind();
        }

        protected void updateStock_Click(object sender, EventArgs e)
        {
            ChoosePanel.Visible = false;
            ViewRestockPanel.Visible = true;
            GVRestockPanel.Visible = true;
            ProductRestock.Visible = false;
        }

        protected void updatePhotos_Click(object sender, EventArgs e)
        {
            ChoosePanel.Visible = false;
            ViewCategoriesPanel.Visible = true;
            CategoriesDataListPanel.Visible = true;
            UpdateCategoryImagePanel.Visible = false;
        }

        protected void RestockGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RestockGV.PageIndex = e.NewPageIndex;
            RestockGV.DataSource = (List<zofim.ProductWS>)Session["ProductsData"];
            RestockGV.DataBind();
        }

        protected void RestockGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVRestockPanel.Visible = false;
            ProductRestock.Visible = true;
            Session["Selected"] = ((List<zofim.ProductWS>)Session["ProductsData"])[RestockGV.SelectedIndex];
        }

        protected void SubmitNewAmount_Click(object sender, EventArgs e)
        {
            zofim.WebServices proxy = new zofim.WebServices();
            zofim.ProductWS product = new zofim.ProductWS();
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                proxy.AddAmount((zofim.ProductWS)Session["Selected"], int.Parse(InputNewAmount.Text));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            ProductRestock.Visible = false;
            GVRestockPanel.Visible = true;
        }

        protected void GoBackSelect_Click(object sender, EventArgs e)
        {
            ChoosePanel.Visible = true;
            ViewRestockPanel.Visible = false;
            ViewCategoriesPanel.Visible = false;
        }

        protected void GoBackRestock_Click(object sender, EventArgs e)
        {
            GVRestockPanel.Visible = true;
            ProductRestock.Visible = false;
        }

        protected void GoBackCategories_Click(object sender, EventArgs e)
        {
            CategoriesDataListPanel.Visible = true;
            UpdateCategoryImagePanel.Visible = true;
        }

        protected void SearchProducts_Click(object sender, EventArgs e)
        {
            zofim.WebServices proxy = new zofim.WebServices();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            Session["ProductsData"] = proxy.GetProductsConditioned(ByNameTB.Text, OnlyStockCB.Checked).ToList();
            RestockGV.DataSource = (List<zofim.ProductWS>)Session["ProductsData"];
            RestockGV.DataBind();
        }
    }
}
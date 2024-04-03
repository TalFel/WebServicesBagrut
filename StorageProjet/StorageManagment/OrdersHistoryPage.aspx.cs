using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class OrdersHistoryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/UsersAdmin.aspx");
            if (!IsPostBack)
            {
                Session["SelectedOrderFromHistory"] = null;
                Session["OrdersHistoryData"] = ((User)Session["CurrentUser"]).Orders;
                OrdersHistoryGV.DataSource = (OrdersLst)Session["OrdersHistoryData"];
                OrdersHistoryGV.DataBind();
            }
        }

        protected void OrdersHistoryGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            e.Row.Cells[2].Text = General.StatusToString(((OrdersLst)Session["OrdersHistoryData"])[e.Row.RowIndex].OrderStatus);
        }

        protected void OrdersHistoryGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["SelectedOrderFromHistory"] = ((OrdersLst)Session["OrdersHistoryData"])[int.Parse(e.CommandArgument.ToString())];
            Session["OrderData"] = ((Order)Session["SelectedOrderFromHistory"]).GetOrderProducts();
            Response.Redirect("/OrderPage.aspx?PastOrder=True");
        }

        protected void OrderGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            ProductInOrder current = ((Order)Session["SelectedOrderFromHistory"]).GetOrderProducts()[e.Row.RowIndex];
            ((Image)e.Row.FindControl("ProductImage")).ImageUrl = current.TheProduct.TheCategory.CategoryImageLink;
            e.Row.Cells[1].Text = current.TheProduct.ProductCatColSzeString;
            e.Row.Cells[2].Text = current.Amount.ToString();
        }
    }
}
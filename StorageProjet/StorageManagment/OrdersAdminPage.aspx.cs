using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class OrdersAdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (!((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/ProfilePage.aspx");
            if (!IsPostBack)
            {
                OrdersListPanel.Visible = true;
                OrderMoreDetailsPanel.Visible = false;
                EditProductStatusPanel.Visible = false;
                Session["AdminOrdersHistoryData"] = General.SelectOrdersDB();
                Session["EditingOrderAdmin"] = false;
                OrdersGV.DataSource = (OrdersLst)Session["AdminOrdersHistoryData"];
                OrdersGV.DataBind();
                IncludePastOrders.Checked = true;
                err.Visible = false;
            }
        }
        protected void OrdersHistoryGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            Order currentOrder = ((OrdersLst)Session["AdminOrdersHistoryData"])[e.Row.RowIndex];
            e.Row.Cells[0].Text = currentOrder.OrderingUser.UserFirstName + " " + currentOrder.OrderingUser.UserFamilyName;
            e.Row.Cells[3].Text = General.StatusToString(currentOrder.OrderStatus);            
        }
        protected void OrdersHistoryGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["AdminSelectedOrder"] = ((OrdersLst)Session["AdminOrdersHistoryData"])[int.Parse(e.CommandArgument.ToString()) + OrdersGV.PageIndex * 10];
            Session["AdminSelectedOrderData"] = ((Order)Session["AdminSelectedOrder"]).GetOrderProducts();
            //view selected order
            OrdersListPanel.Visible = false;
            OrderMoreDetailsPanel.Visible = true;
            SelectedOrderGV.DataSource = (ProductsInOrdersLst)Session["AdminSelectedOrderData"];
            SelectedOrderGV.DataBind();
            Order ord = ((Order)Session["AdminSelectedOrder"]);
            DateNameLabelViewOrder.Text = ord.OrderTimeShortString + " " + ord.OrderingUser.UserFirstName + " " + ord.OrderingUser.UserFamilyName;
            StatusLabelViewOrder.Text = "סטטוס: " + General.StatusToString(ord.OrderStatus);
            if (ord.OrderTime < DateTime.Today)
                EditButton.Visible = false;
        }

        protected void OrdersGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrdersListPanel.Visible = true;
            OrderMoreDetailsPanel.Visible = false;
            OrdersGV.PageIndex = e.NewPageIndex;
            OrdersGV.DataSource = (OrdersLst)Session["AdminOrdersHistoryData"];
            OrdersGV.DataBind();
        }

        protected void SelectedOrderGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelectedOrderGV.PageIndex = e.NewPageIndex;
            SelectedOrderGV.DataSource = (ProductsInOrdersLst)Session["AdminSelectedOrderData"];
            SelectedOrderGV.DataBind();
        }

        protected void SelectedOrderGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductInOrder p = ((ProductsInOrdersLst)Session["AdminSelectedOrderData"])[SelectedOrderGV.PageIndex * 3 + SelectedOrderGV.SelectedIndex];
            Session["ProductToEditStatus"] = p;
            ProductName.Text = p.TheProduct.ProductCatColSzeString;
            Order ord = ((Order)Session["AdminSelectedOrder"]);
            DateNameLabelEditStatus.Text = ord.OrderTimeShortString + " " + ord.OrderingUser.UserFirstName + " " + ord.OrderingUser.UserFamilyName;
            EditProductStatusPanel.Visible = true;
            OrderMoreDetailsPanel.Visible = false;
            AddStatusToDDL(ProductStatusDDL);
        }
        protected void SelectedOrderGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            ProductInOrder current = ((ProductsInOrdersLst)Session["AdminSelectedOrderData"])[e.Row.RowIndex + 3* SelectedOrderGV.PageIndex];
            if (current.TheStatus == Status.NotOrdered)
                e.Row.Visible = false;
            ((Image)e.Row.FindControl("ProductImage")).ImageUrl = current.TheProduct.TheCategory.CategoryImageLink;
            e.Row.Cells[1].Text = current.TheProduct.ProductCatColSzeString;
            e.Row.Cells[2].Text = current.Amount.ToString();
            e.Row.Cells[3].Text = General.StatusToString(current.TheStatus);
            if (!(bool)Session["EditingOrderAdmin"])
                e.Row.Cells[4].Visible = false;
        }

        protected void SearchConditions_Click(object sender, EventArgs e)
        {
            string[] cond = new string[]{ "OrderTime DESC", "UserID", "OrderStatus" };
            if (RBList.SelectedIndex != 0 && RBList.SelectedIndex != 1 && RBList.SelectedIndex != 2)
                RBList.SelectedIndex = 0;
            Session["AdminOrdersHistoryData"] = General.SelectOrdersByNameAndOrdered(ByNameTB.Text, cond[RBList.SelectedIndex], IncludePastOrders.Checked);
            OrdersGV.DataSource = (OrdersLst)Session["AdminOrdersHistoryData"];
            OrdersGV.DataBind();
            if (OrdersGV.Rows.Count == 0)
                err.Visible = true;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Order currentOrder = (Order)Session["AdminSelectedOrder"];
            Session["EditingOrderAdmin"] = true;
            SelectedOrderGV.DataSource = (ProductsInOrdersLst)Session["AdminSelectedOrderData"];
            SelectedOrderGV.DataBind();
            AddStatusToDDL(StatusDDL);
            StatusDDL.SelectedValue = "" + (int)currentOrder.OrderStatus;
            StatusDDL.Visible = true;
            StatusLabelViewOrder.Text = "סטטוס:";
            DoChanges.Visible = true;
            CancelChanges.Visible = true;
            EditButton.Visible = false;
        }
        public void AddStatusToDDL(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(General.StatusToString(Status.Ordered), "" + (int)Status.Ordered));
            ddl.Items.Add(new ListItem(General.StatusToString(Status.InProcess), "" + (int)Status.InProcess));
            ddl.Items.Add(new ListItem(General.StatusToString(Status.Ready), "" + (int)Status.Ready));
            ddl.Items.Add(new ListItem(General.StatusToString(Status.Given), "" + (int)Status.Given));
            ddl.Items.Add(new ListItem(General.StatusToString(Status.Returned), "" + (int)Status.Returned));
        }
        public Status GetStatusOfDDL(DropDownList ddl)
        {
            Dictionary<String, Status> dict = new Dictionary<string, Status>();
            dict.Add("" + ((int)Status.Ordered), Status.Ordered);
            dict.Add("" + ((int)Status.InProcess), Status.InProcess);
            dict.Add("" + ((int)Status.Ready), Status.Ready);
            dict.Add("" + ((int)Status.Given), Status.Given);
            dict.Add("" + ((int)Status.Returned), Status.Returned);
            Status val;
            dict.TryGetValue(ddl.SelectedValue, out val);
            return val;
        }
        protected void DoChanges_Click(object sender, EventArgs e)
        {
            
            ((Order)Session["AdminSelectedOrder"]).Update((ProductsInOrdersLst)Session["AdminSelectedOrderData"], GetStatusOfDDL(StatusDDL));
        }

        protected void CancelChanges_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdersAdminPage.aspx");
        }

        protected void CancelChangesStatus_Click(object sender, EventArgs e)
        {
            EditProductStatusPanel.Visible = false;
            OrderMoreDetailsPanel.Visible = true;
        }

        protected void DoChangesStatus_Click(object sender, EventArgs e)
        {
            ((ProductInOrder)Session["ProductToEditStatus"]).TheStatus = GetStatusOfDDL(ProductStatusDDL);
            SelectedOrderGV.DataSource = (ProductsInOrdersLst)Session["AdminSelectedOrderData"];
            SelectedOrderGV.DataBind();
            CancelChangesStatus_Click(null, null);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Model;

namespace StorageManagment
{
    public partial class OrderPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/UsersAdmin.aspx");
            //means that we came from orderHistoryPage
            bool isHistory = Request.QueryString["PastOrder"].Equals("True");

            if (!IsPostBack && !isHistory)
            {
                OrderCalendar.SelectedDate = DateTime.Now;
                HeaderOrder.Text = "הזמנה נוכחית";
                Session["OrderData"] = Session["CurrentOrder"];
                err1.Visible = false;
                EditProduct.Visible = false;
                CancelChanges.Visible = false;
                DeleteOrder.Visible = false;
            }
            
            if (isHistory && !IsPostBack)
            {
                OrderCalendar.SelectedDate = DateTime.Now;
                //viewing past order
                Order CurrentOrder = (Order)Session["SelectedOrderFromHistory"];
                HeaderOrder.Text = "הזמנה לתאריך: " + CurrentOrder.OrderTimeShortString;
                DoOrder.Text = "בצע שינויים";
                EditProduct.Visible = false;
                OrderCalendar.SelectedDate = CurrentOrder.OrderTime;
                DescriptionTB.InnerText = CurrentOrder.OrderDescription;
                err1.Visible = false;
                //cant change order, because it has already happened.
                if(CurrentOrder.OrderTime < DateTime.Today)
                {
                    OrderCalendar.Enabled = false;
                    DescriptionTB.EnableViewState = false;
                    GoToItemsButton.Visible = false;
                    DoOrder.Visible = false;
                    CancelChanges.Text = "חזור";
                    DeleteOrder.Visible = false;
                }
            }
            if(Session["OrderData"] == null)
            {
                CurrentOrderPanel2.Visible = false;
            }
            OrderGV.DataSource = (ProductsInOrdersLst)Session["OrderData"];
            OrderGV.DataBind();
        }

        protected void OrderGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            ProductInOrder current = ((ProductsInOrdersLst)Session["OrderData"])[e.Row.RowIndex];
            if (current.TheStatus == Status.NotOrdered)
                e.Row.Visible = false;
            ((Image)e.Row.FindControl("ProductImage")).ImageUrl = current.TheProduct.TheCategory.CategoryImageLink;
            e.Row.Cells[1].Text = current.TheProduct.ProductCatColSzeString;
            e.Row.Cells[2].Text = current.Amount.ToString();
            //viewing past order, cant edit
            if(Request.QueryString["PastOrder"].Equals("True") && OrderCalendar.SelectedDate < DateTime.Today)
            {
                e.Row.Cells[3].Text = "";
            }
        }

        protected void DoOrder_Click(object sender, EventArgs e)
        {
            bool isHistory = Request.QueryString["PastOrder"].Equals("True");
            DateTime selected = OrderCalendar.SelectedDate.Date;
            if (selected == null)
            {
                err1.Text = "בחר תאריך";
                err1.Visible = true;
                return;
            }
            Order order;
            if (isHistory)
            {
                order = (Order)Session["SelectedOrderFromHistory"];
                order.Update((ProductsInOrdersLst)Session["OrderData"], selected, DescriptionTB.InnerText);
                Response.Redirect("/OrdersHistoryPage.aspx");
            }
            
            order = new Order((User)Session["CurrentUser"], selected, DescriptionTB.InnerText);
            if (!order.Insert())
            {
                err1.Text = "כבר קיימת הזמנה לאותו היום";
                err1.Visible = true;
                return;
            }
            else
            {
                order.InsertProducts((ProductsInOrdersLst)Session["OrderData"]);
                Session["CurrentOrder"] = null;
                Response.Redirect("/OrdersHistoryPage.aspx");
            }
        }

        protected void OrderCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Today.Date)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void GoToItemsButton_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PastOrder"].Equals("True"))
                Response.Redirect("/ItemsPage.aspx?PastOrder=True");
            else
                Response.Redirect("/ItemsPage.aspx?PastOrder=False");
        }
        
        protected void OrderGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OrderGV.PageIndex = e.NewPageIndex;
            OrderGV.DataSource = (ProductsInOrdersLst)Session["OrderData"];
            OrderGV.DataBind();
        }

        protected void CancelChanges_Click(object sender, EventArgs e)
        {
            Session["SelectedOrderFromHistory"] = null;
            Response.Redirect("OrdersHistoryPage.aspx");
        }

        protected void OrderGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductInOrder p = ((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex];
            AmountChangeProduct.Text = p.Amount.ToString();
            EditProductHeaderLabel.Text = p.TheProduct.ProductCatColSzeString;
            EditProduct.Visible = true;
            CurrentOrderPanel1.Visible = false;
            CurrentOrderPanel2.Visible = false;
            AmountChangeProduct.Attributes.Add("placeHolder", "כמות במחסן: " + (p.Amount + p.TheProduct.Amount));
        }

        protected void DoChangesToProduct_Click(object sender, EventArgs e)
        {
            //if the order was already sent we need a different calculation..
            if (Request.QueryString["PastOrder"].Equals("True"))
            {
                if ((((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex]).TheProduct.Amount + (((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex]).Amount <
                int.Parse(AmountChangeProduct.Text))
                {
                    err2.Text = "אין מספיק במחסן! נסו להזמין פחות או לבחור פריט אחר.";
                    err2.Visible = true;
                    return;
                }
            }
            //not enough left in stock
            else if ((((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex]).TheProduct.Amount < 
                int.Parse(AmountChangeProduct.Text))
            {
                err2.Text = "אין מספיק במחסן! נסו להזמין פחות או לבחור פריט אחר.";
                err2.Visible = true;
                return;
            }

            try
            {
                ((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex].Amount = int.Parse(AmountChangeProduct.Text);
            }
            catch(Exception excp)
            {
                err2.Text = "הכנס מספר שלם בלבד";
                err2.Visible = true;
                return;
            }

            OrderGV.DataSource = (ProductsInOrdersLst)Session["OrderData"];
            OrderGV.DataBind();
            EditProduct.Visible = false;
            CurrentOrderPanel1.Visible = true;
            CurrentOrderPanel2.Visible = true;
        }

        protected void CancelChangesToProduct_Click(object sender, EventArgs e)
        {
            EditProduct.Visible = false;
            CurrentOrderPanel1.Visible = true;
            CurrentOrderPanel2.Visible = true;
        }

        protected void DeleteProduct_Click(object sender, EventArgs e)
        {
            ((ProductsInOrdersLst)Session["OrderData"])[OrderGV.PageIndex * 3 + OrderGV.SelectedIndex].TheStatus = Status.NotOrdered;
            OrderGV.DataSource = (ProductsInOrdersLst)Session["OrderData"];
            OrderGV.DataBind();
            EditProduct.Visible = false;
            CurrentOrderPanel1.Visible = true;
            CurrentOrderPanel2.Visible = true;
        }

        protected void DeleteOrder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((Order)Session["SelectedOrderFromHistory"]).DeleteOrder();
                Response.Redirect("/OrdersHistoryPage.aspx");
                Session["SelectedOrderFromHistory"] = null;
            }
        }
    }
}
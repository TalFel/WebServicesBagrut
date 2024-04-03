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
    public partial class RequestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/UsersAdmin.aspx");
            if (!IsPostBack)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                OrderCalendar.SelectedDate = DateTime.Today;
            }
        }
        protected void SendBTN_Click(object sender, EventArgs e)
        {
            reqRequestText.Visible = false;
            Request req = new Request((User)Session["CurrentUser"], requestText.InnerText, RequestStatus.Requested, OrderCalendar.SelectedDate);
            req.AddRequest();
            DialogResult dialogResult = MessageBox.Show("בקשה נשלחה!", "", MessageBoxButtons.OK);
            OldRequests_Click(null, null);
        }

        protected void OldRequests_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;
            HistoryGV.DataSource = ((User)Session["CurrentUser"]).SelectRequestOfUser();
            HistoryGV.DataBind();
        }
        protected void OrderCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }
        }
        protected void NewRequest_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            Panel2.Visible = false;
        }

        protected void HistoryGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם את\'ה בטוח\'ה?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Request reqToDelete = ((User)Session["CurrentUser"]).SelectRequestOfUser()[int.Parse(e.CommandArgument.ToString())];
                reqToDelete.UpdateRequestStatus(RequestStatus.Deleted);
                HistoryGV.DataSource = ((User)Session["CurrentUser"]).SelectRequestOfUser();
                HistoryGV.DataBind();
            }
            HistoryGV.DataSource = ((User)Session["CurrentUser"]).SelectRequestOfUser();
            HistoryGV.DataBind();
        }

        protected void HistoryGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex < 0)
                return;
            e.Row.Cells[1].Text.Replace("\n", Environment.NewLine);
            DateTime d1 = ((Request)e.Row.DataItem).RequestDate;
            DateTime d2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            //DateTime d2 = DateTime.Parse("dd-MM-yyyy");
            if (d1 < d2)
                e.Row.Cells[3].Text = "";
        }
    }
}
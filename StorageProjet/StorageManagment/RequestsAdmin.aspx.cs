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
    public partial class RequestsAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (!((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/ProfilePage.aspx");
            if (!IsPostBack)
            {
                if (((User)Session["CurrentUser"]).UserID != 1)//not admin
                    Response.Redirect("/ProfilePage.aspx");

                Panel1.Visible = true;
                Panel2.Visible = false;
                if (Session["ConditionReq"] == null)
                    Session["Data"] = General.SelectRequestsDB();
                else
                {
                    Session["Data"] = General.SelectConditionedRequests(Session["ConditionReq"].ToString());
                    CBList.Items[0].Selected = (bool)Session["CB0"];
                    CBList.Items[1].Selected = (bool)Session["CB1"];
                    CBList.Items[2].Selected = (bool)Session["CB2"];
                }

                if ((RequestsLst)Session["Data"] == null)//no items found
                {
                    err.Text = "לא נמצאו פרטים התואמים את התנאים";
                    err.Visible = true;
                    return;
                }
                Session["Data"] = ((RequestsLst)Session["Data"]).GetByDate();
                RequestsGV.DataSource = ((RequestsLst)Session["Data"]);
                RequestsGV.DataBind();
            }
            if (CBList.Items[3].Selected)
            {
                CBList.Items[0].Selected = true;
                CBList.Items[1].Selected = true;
                CBList.Items[2].Selected = true;
            }
        }


        protected void RequestsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1)
                return;
            Request req = (Request)e.Row.DataItem;
            e.Row.Cells[0].Text = req.RequestingUser.UserFirstName + ' ' + req.RequestingUser.UserFamilyName;
            e.Row.Cells[1].Text = req.RequestDate.ToShortDateString();
            e.Row.Cells[2].Text = req.ResponseString;
            e.Row.Cells[3].Text = General.GetRequestStatusToString(req.RequestStatus);
        }

        protected void RequestsGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;

            Request reqToChng = ((RequestsLst)Session["Data"])[RequestsGV.SelectedIndex];

            StatusDDL.Items.Add(new ListItem(General.GetRequestStatusToString(RequestStatus.Requested), (int)RequestStatus.Requested + ""));
            StatusDDL.Items.Add(new ListItem(General.GetRequestStatusToString(RequestStatus.InProcess), (int)RequestStatus.InProcess + ""));
            StatusDDL.Items.Add(new ListItem(General.GetRequestStatusToString(RequestStatus.Completed), (int)RequestStatus.Completed + ""));

            StatusDDL.SelectedValue = (int)reqToChng.RequestStatus + "";
        }

        protected void SelectNewStatus_Click(object sender, EventArgs e)
        {
            Request reqToChng = ((RequestsLst)Session["Data"])[RequestsGV.SelectedIndex];
            reqToChng.UpdateRequestStatus((RequestStatus)int.Parse(StatusDDL.SelectedItem.Value));
            Response.Redirect("/RequestsAdmin.aspx");
        }

        protected void SearchRequests_Click(object sender, EventArgs e)
        {
            string SqlCondition = "WHERE NOT RequestProcessed=3 ";
            int count = 0;
            if (!CBList.Items[0].Selected)//requested
            {
                count++;
                SqlCondition += "AND NOT RequestProcessed=0 ";
            }
            if (!CBList.Items[1].Selected)//inprocess
            {
                SqlCondition += "AND NOT RequestProcessed=1 ";
            }
            if (!CBList.Items[2].Selected)//completed
            {
                SqlCondition += "AND NOT RequestProcessed=2";
            }
            Session["CB0"] = CBList.Items[0].Selected;
            Session["CB1"] = CBList.Items[1].Selected;
            Session["CB2"] = CBList.Items[2].Selected;

            Session["ConditionReq"] = SqlCondition;
            Response.Redirect("/RequestsAdmin.aspx");
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RequestsAdmin.aspx");
        }

        protected void RequestsGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RequestsGV.PageIndex = e.NewPageIndex;
            RequestsGV.DataSource = (RequestsLst)Session["Data"];
            RequestsGV.DataBind();
        }
    }
}
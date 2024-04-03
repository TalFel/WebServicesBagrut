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
    public partial class RolesAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (!((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/ProfilePage.aspx");
            if (!IsPostBack)
            {
                if (((User)Session["CurrentUser"]).UserID != 1)
                    Response.Redirect("/ProfilePage.aspx");
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                err.Visible = false;
                err2.Visible = false;
                err3.Visible = false;

                if (Session["ConditionRol"] == null)
                    Session["Data"] = General.SelectRolesDB();
                else
                    Session["Data"] = General.SelectRolesConditioned(Session["ConditionRol"].ToString());

                if ((RolesLst)Session["Data"] == null)
                {
                    err3.Text = "לא נמצאו תפקידים העונים על התנאים";
                    err3.Visible = true;
                }
                if(Session["ConditionRol"] != null)
                    ByName.Text = Session["ConditionRol"].ToString();
                RolesGV.DataSource = (RolesLst)Session["Data"];
                RolesGV.DataBind();
            }
        }

        protected void RolesGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = true;

            Role rolToChng = ((RolesLst)Session["Data"])[RolesGV.SelectedIndex + 10*RolesGV.PageIndex];
            RName.Text = rolToChng.RoleName;
        }
        protected void SubmitChngDetails_Click(object sender, EventArgs e)
        {
            Role role = ((RolesLst)Session["Data"])[RolesGV.SelectedIndex];
            if (role.NameExistsOtherRole(RName.Text))
            {
                err.Text = "שם תפקיד כבר קיים במערכת";
                err.Visible = true;
            }
            else
            {
                role.UpdateRole(RName.Text);
                err.Text = "";
                err.Visible = false;
                Response.Redirect("/RolesAdmin.aspx");
            }
        }

        protected void DeleteBTN_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((RolesLst)Session["Data"])[RolesGV.SelectedIndex].DeleteRole();
                Response.Redirect("/RolesAdmin.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        protected void CancelBTN_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RolesAdmin.aspx");
        }

        protected void AddRole_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Panel3.Visible = true;
        }
        
        protected void AddRoleToDB_Click(object sender, EventArgs e)
        {
            if (General.RoleNameExists(NewRoleName.Text))
            {
                err2.Text = "השם כבר קיים במערכת";
                err2.Visible = true;
            }
            else
            {
                err.Text = "";
                err.Visible = false;
                Role newRole = new Role();
                newRole.RoleName = NewRoleName.Text;
                newRole.AddRole();
                Response.Redirect("/RolesAdmin.aspx");
            }
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            if (ByName.Text.Length == 0)
                Session["ConditionRol"] = null;
            else
            {
                Session["ConditionRol"] = ByName.Text;
                Session["ByName"] = ByName.Text;
            }
            Response.Redirect("/RolesAdmin.aspx");
        }

        protected void RolesGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1)
                return;
            if (((Role)e.Row.DataItem).IsAdminRole)
                e.Row.Cells[1].Text = "";
        }

        protected void RolesGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RolesGV.PageIndex = e.NewPageIndex;
            RolesGV.DataSource = (RolesLst)Session["Data"];
            RolesGV.DataBind();
        }
    }
}
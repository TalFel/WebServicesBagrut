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
    public partial class UsersAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (!(((User)Session["CurrentUser"]).IsAdmin))
                Response.Redirect("/ProfilePage.aspx");
            if (!IsPostBack)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                err.Text = "";
                err.Visible = false;

                RoleDDLSearch.Items.Add(new ListItem("הכל", "-1"));
                RolesLst lst = General.SelectRolesDB();
                foreach (BaseEntity ent in lst)
                {
                    RoleDDLSearch.Items.Add(new ListItem(((Role)ent).RoleName, "" + ((Role)ent).RoleID));
                    RoleDDL.Items.Add(new ListItem(((Role)ent).RoleName, "" + ((Role)ent).RoleID));
                }
                if (Session["ConditionUsr"] == null)
                    Session["Data"] = General.SelectUsersDB();
                else
                    Session["Data"] = General.SelectUsersConditionedDB(Session["TB1"].ToString(), Session["TB2"].ToString(), Session["TB3"].ToString(), (bool)Session["CB1"], int.Parse(Session["DDL"].ToString()));
                if(Session["ConditionUsr"] != null)
                {
                    FirstNameTB.Text = Session["TB1"].ToString();
                    LastNameTB.Text = Session["TB2"].ToString();
                    PhoneTB.Text = Session["TB3"].ToString();
                    OldUsersCB.Checked = (bool)Session["CB1"];
                    RoleDDLSearch.SelectedValue = Session["DDL"].ToString();
                }
                if((UsersLst)Session["Data"] == null)
                {
                    err2.Text = "לא נמצאו משתמשים העונים על התנאים";
                    err2.Visible = true;
                }
                UsersGV.DataSource = (UsersLst)Session["Data"];
                UsersGV.DataBind();

                firstName.Enabled = false;
                lastName.Enabled = false;
                cell.Enabled = false;
                RoleDDL.Enabled = false;
                ChngDetails.Text = "שינוי פרטים";
                CancelBtn.Visible = false;
                ChngDetails.CausesValidation = false;
            }
        }

        protected void UsersGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == -1)
                return;
            //if (!((User)e.Row.DataItem).IsAdmin)
            //    Console.WriteLine("");
            Role role = ((User)e.Row.DataItem).UserRole;
            e.Row.Cells[3].Text = role.RoleName;
            if(((User)e.Row.DataItem).IsAdmin)
                e.Row.Cells[5].Text = "";
        }

        //edit user
        protected void UsersGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            User usrToChng = ((UsersLst)Session["Data"])[UsersGV.SelectedIndex];

            if (usrToChng.IsAdmin)
            {
                err2.Text = "לא ניתן לערוך את משתמש האדמין";
                err2.Visible = true;
                return;
            }
            Panel1.Visible = false;
            Panel2.Visible = true;


            firstName.Text = usrToChng.UserFirstName;
            lastName.Text = usrToChng.UserFamilyName;
            cell.Text = usrToChng.UserPhoneNumber;
            RoleDDL.SelectedValue = usrToChng.UserRole.RoleID.ToString();
            IsActiveCB.Checked = usrToChng.UserActive;

        }

        protected void ChngDetails_Click(object sender, EventArgs e)
        {
            if (!firstName.Enabled)
            {
                firstName.Enabled = true;
                lastName.Enabled = true;
                cell.Enabled = true;
                RoleDDL.Enabled = true;
                ChngDetails.Text = "בצע שינויים";
                CancelBtn.Visible = true;
                ChngDetails.CausesValidation = true;
                return;
            }
            User usr = ((UsersLst)Session["Data"])[UsersGV.SelectedIndex];
            if (usr.PhoneExistsOtherUser(cell.Text))
            {
                err.Text = "מספר טלפון כבר קיים במערכת";
                err.Visible = true;
            }
            else if (Page.IsValid)
            {
                usr.UpdateUser(RoleDDL.SelectedItem.Value, RoleDDL.SelectedItem.Text, firstName.Text, lastName.Text, cell.Text, IsActiveCB.Checked);
                err.Text = "";
                err.Visible = false;
                Response.Redirect("/UsersAdmin.aspx");
            }
            else
            {
                err.Text = "הפרטים לא עומדים בתנאים";
                err.Visible = true;
            }
        }

        protected void DeleteBTN_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("האם אתה בטוח?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ((UsersLst)Session["Data"])[UsersGV.SelectedIndex].DeleteUser();
                Response.Redirect("/UsersAdmin.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        protected void CancelBTN_Click(object sender, EventArgs e)
        {
            if (firstName.Enabled)
            {
                firstName.Enabled = false;
                lastName.Enabled = false;
                cell.Enabled = false;
                RoleDDL.Enabled = false;
                ChngDetails.Text = "שינוי פרטים";
                CancelBtn.Visible = false;
                ChngDetails.CausesValidation = false;
                return;
            }
            Response.Redirect("/UsersAdmin.aspx");
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            Session["TB1"] = FirstNameTB.Text;
            Session["TB2"] = LastNameTB.Text;
            Session["TB3"] = PhoneTB.Text;
            Session["CB1"] = OldUsersCB.Checked;
            Session["DDL"] = RoleDDLSearch.SelectedValue;
            Session["ConditionUsr"] = 1;
            Response.Redirect("/UsersAdmin.aspx");
        }

        protected void UsersGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsersGV.PageIndex = e.NewPageIndex;
            UsersGV.DataSource = (UsersLst)Session["Data"];
            UsersGV.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["CurrentUser"]) == null)
                Response.Redirect("/HomeSignInPage.aspx");
            if (((User)Session["CurrentUser"]).IsAdmin)
                Response.Redirect("/UsersAdmin.aspx");
            if (!IsPostBack)
            {
                User currUser = (User)Session["CurrentUser"];
                firstName.Text = currUser.UserFirstName;
                lastName.Text = currUser.UserFamilyName;
                cell.Text = currUser.UserPhoneNumber;

                err.Text = "";
                err.Visible = false;

                //add roles from db to the sign in page
                RolesLst lst = General.SelectRolesDB();
                foreach (BaseEntity ent in lst)
                {
                    if(!((Role)ent).IsAdminRole)//not admin
                        RoleDDL.Items.Add(new ListItem(((Role)ent).RoleName, "" + ((Role)ent).RoleID));
                }
                RoleDDL.SelectedValue = currUser.UserRole.RoleID.ToString();

                firstName.Enabled = false;
                lastName.Enabled = false;
                cell.Enabled = false;
                RoleDDL.Enabled = false;
                ChngDetails.Text = "שינוי פרטים";
                CancelBtn.Visible = false;
                ChngDetails.CausesValidation = false;
            }
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
            if (((User)Session["CurrentUser"]).PhoneExistsOtherUser(cell.Text))
            {
                err.Text = "טלפון קיים כבר במערכת";
                err.Visible = true;
            }
            else if (Page.IsValid)
            {
                User newUser = ((User)Session["CurrentUser"]).UpdateUser(RoleDDL.SelectedItem.Value, RoleDDL.SelectedItem.Text, firstName.Text, lastName.Text, cell.Text, ((User)Session["CurrentUser"]).UserActive);
                if (RoleDDL.SelectedItem.Value == "1")//another security layer
                    return;
                Session["CurrentUser"] = newUser;
                Response.Redirect("/ProfilePage.aspx");
            }
            else
            {
                err.Text = "קיימים שדות לא תקינים";
                err.Visible = true;
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ProfilePage.aspx");
        }
    }
}
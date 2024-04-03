using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                err.Text = "";
                err.Visible = false;

                //add roles from db to the sign in page
                RolesLst lst = General.SelectRolesDB();
                foreach (BaseEntity ent in lst)
                {
                    if(!((Role)ent).IsAdminRole) //not admin
                        RoleDDL.Items.Add(new ListItem(((Role)ent).RoleName, "" + ((Role)ent).RoleID));
                }
            }
        }

        protected void SubmitSignUp_Click(object sender, EventArgs e)
        {
            if (General.PhoneExists(cell.Text))
            {
                err.Text = "טלפון קיים כבר במערכת";
                err.Visible = true;
            }
            else if (Page.IsValid)
            {
                if (RoleDDL.SelectedItem.Value == "1")//another security layer
                    return;
                User newUser = General.CreateNewUser(RoleDDL.SelectedItem.Value, RoleDDL.SelectedItem.Text, firstName.Text, lastName.Text, cell.Text, password.Text);

                Session["CurrentUser"] = newUser;
                Response.Redirect("~/ItemsPage.aspx?PastOrder=Flase");
            }
            else
            {
                err.Text = "קיימים שדות לא תקינים";
                err.Visible = true;
            }
        }
    }
}
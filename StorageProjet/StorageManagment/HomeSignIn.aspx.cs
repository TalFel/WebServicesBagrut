using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using ViewModel;

namespace StorageManagment
{
    public partial class HomeSignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { }
        }

        protected void SubmitLogin_Click(object sender, EventArgs e)
        {
            User curUser = General.GetUser(cell.Text, password.Text);

            if (curUser == null)
            {
                if (General.PhoneExists(cell.Text))
                {
                    err.Text = "סיסמה שגויה";
                    err.Visible = true;
                }
                else
                {
                    err.Text = "טלפון שגוי";
                    err.Visible = true;
                }
                
            }
            else
            {
                err.Text = "";
                err.Visible = false;
                Session["CurrentUser"] = curUser;
                Response.Redirect("~/ItemsPage.aspx?PastOrder=False");
            }
        }
    }
}
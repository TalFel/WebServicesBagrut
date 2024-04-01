using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected string name { get; set; } = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["CurrentUser"] == null)
            {
                TopNavPlaceHolder.Visible = false;
                Session["CurrentUser"] = null;
            }
            if (Session["CurrentUser"] != null)
            {
                TopNavPlaceHolder.Visible = true;
                if (((User)Session["CurrentUser"]).IsAdmin)
                {
                    name = "אדמין";
                }
                else
                    name = ((User)Session["CurrentUser"]).UserFirstName + " " + ((User)Session["CurrentUser"]).UserFamilyName;
            }
        }
    }
}
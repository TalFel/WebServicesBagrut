using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace StorageManagment
{
    public partial class StatisticsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ForToday = General.NuberOfOrdersForToday();
                int forTodayCompleted = General.NumberOfOrdersCompletedForToday();
                int progress = ForToday == 0 ? 100 : 100 * forTodayCompleted / ForToday;
                int InProcess = General.NumberOfOrdersInProcessForToday();
                ForTodayTotOrders.Text = ForToday.ToString() + " הזמנות להיום";
                ForTodayCompletedOrders.Text = forTodayCompleted.ToString() + " הושלמו";
                ForTodayInProccessOrders.Text = InProcess.ToString() + " בתהליך";
                ForTodayRemainingOrders.Text = (ForToday - forTodayCompleted).ToString() + " נותרו";
                Literal lit = new Literal();
                lit.Text = $"<div role=\"progressbar\" aria-valuenow=\"{progress}\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"--value: {progress}\"></div>";
                pnl2.Controls.Add(lit);

                ForToday = General.NuberOfRequestsForToday();
                forTodayCompleted = General.NumberOfRequestsCompletedForToday();
                progress = ForToday == 0 ? 100 : 100 * forTodayCompleted / ForToday;
                InProcess = General.NumberOfRequestsInProcessForToday();
                ForTodayTotReq.Text = ForToday.ToString() + " בקשות להיום";
                ForTodayCompletedReq.Text = forTodayCompleted.ToString() + " הושלמו";
                ForTodayInProccessReq.Text = InProcess.ToString() + " בתהליך";
                ForTodayRemainingReq.Text = (ForToday - forTodayCompleted).ToString() + " נותרו";
                lit = new Literal();
                lit.Text = $"<div role=\"progressbar\" aria-valuenow=\"{progress}\" aria-valuemin=\"0\" aria-valuemax=\"100\" style=\"--value: {progress}\"></div>";
                pnl1.Controls.Add(lit);

                NoOfUsers.Text = General.SelectUsersConditionedDB("", "", "", true, -1).Count + " משתמשים";
                NoOfActiveUsers.Text = General.SelectUsersConditionedDB("", "", "", false, -1).Count + " פעילים";


                BestProductsLst plst = General.Get3MostOrderedProducts();
                insertToPnlProductString(Product1, plst[0]);
                insertToPnlProductString(Product2, plst[1]);
                insertToPnlProductString(Product3, plst[2]);
            }
        }
        private void insertToPnlProductString(Panel pnl, BestProduct bp)
        {
            Literal lit = new Literal();
            lit.Text = "שם: " + bp.product.TheCategory.CategoryName;
            pnl.Controls.Add(lit);
            newLine(pnl);

            lit = new Literal();
            lit.Text = "צבע: " + bp.product.TheColor.ColorName;
            pnl.Controls.Add(lit);
            newLine(pnl);

            lit = new Literal();
            lit.Text = "גודל: " + bp.product.TheSize.SizeName;
            pnl.Controls.Add(lit);
            newLine(pnl);

            lit = new Literal();
            lit.Text = "הוזמנו: " + bp.count;
            pnl.Controls.Add(lit);
            newLine(pnl);
        }
        private void newLine(Panel pnl)
        {
            Literal lit = new Literal();
            lit.Text = "<br>";
            pnl.Controls.Add(lit);
        }

        protected void GoToOrders_Click(object sender, EventArgs e)
        {
            Response.Redirect("/OrdersAdminPage.aspx");
        }

        protected void GoToRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RequestsAdmin.aspx");
        }
    }
}
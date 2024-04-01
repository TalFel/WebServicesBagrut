<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="StatisticsPage.aspx.cs" Inherits="StorageManagment.StatisticsPage" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="Mainform" class="HalfScrollablePanelContainer">
        <h3>סטטיטיקות</h3>
        <br /><br />
        <center><h4>בקשות להיום</h4></center><br />
        <table style="align-self:center; ruby-align:center; width:100%; align-items:center;">
            <tr style="width:100%; padding:20px">
                <td style="width:50%; text-align:right; padding:20px">
                    <asp:Panel ID="pnl1" runat="server"></asp:Panel><br />
                </td>
                <td style="padding:20px">
                    <div>
                        <asp:Label ID="ForTodayTotReq" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayCompletedReq" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayInProccessReq" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayRemainingReq" CssClass="h4" runat="server" Text=""></asp:Label><br />
                    </div>
                    <asp:Button ID="GoToRequests" CssClass="button2" runat="server" Text="מעבר לבקשות" OnClick="GoToRequests_Click" />
                </td>
            </tr>
        </table>
        <center><h4>הזמנות להיום</h4></center><br />
        <table style="align-self:center; ruby-align:center; width:100%; align-items:center;">
            <tr style="width:inherit; padding:20px">
                <td style="width:50%; text-align:left; padding:20px">
                    <asp:Panel ID="pnl2" runat="server"></asp:Panel><br />
                </td>
                <td style="padding:20px">
                    <div>
                        <asp:Label ID="ForTodayTotOrders" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayCompletedOrders" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayInProccessOrders" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="ForTodayRemainingOrders" CssClass="h4" runat="server" Text=""></asp:Label><br />
                    </div>
                    <asp:Button ID="GoToOrders" CssClass="button2" runat="server" Text="מעבר להזמנות" OnClick="GoToOrders_Click" />
                </td>
            </tr>
        </table>
        <br />
        <center><h4>משתמשים</h4></center><br />
        <table style="align-self:center; ruby-align:center; width:100%; align-items:center;">
            <tr>
                <td>
                    <center>
                        <asp:Label ID="NoOfUsers" CssClass="h4" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="NoOfActiveUsers" CssClass="h4" runat="server" Text=""></asp:Label><br />
                    </center>
                </td>
            </tr>
        </table>
        <br /><br />
        <center><h4>מוצרים מבוקשים</h4></center><br />
        <center>
            <table style="width: 700px;">
                <tr style="width:700px;">
                    <td style="width:30%">
                        <asp:Panel ID="Product1" CssClass="maxwidth" runat="server"></asp:Panel><br />
                    </td>
                    <td style="width:30%">
                        <asp:Panel ID="Product2" CssClass="maxwidth" runat="server"></asp:Panel><br />
                    </td>
                    <td style="width:30%">
                        <asp:Panel ID="Product3" CssClass="maxwidth" runat="server"></asp:Panel><br />
                    </td>
                </tr>
            </table>
        </center>
    </form>
</asp:Content>

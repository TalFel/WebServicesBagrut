<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="OrdersHistoryPage.aspx.cs" Inherits="StorageManagment.OrdersHistoryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="HalfScrollablePanelContainer">
        <center>
            <asp:Panel ID="HistoryPanel" runat="server">
                <h3>היסטורית ההזמנות שלי</h3>
                <br />
                <asp:GridView ID="OrdersHistoryGV" runat="server" AutoGenerateColumns="False" CellPadding="10" OnRowDataBound="OrdersHistoryGV_RowDataBound" OnRowCommand="OrdersHistoryGV_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="OrderTimeShortString" HeaderText="תאריך" />
                        <asp:BoundField DataField="OrderDescription" HeaderText="הערות" />
                        <asp:BoundField HeaderText="סטטוס" />
                        <asp:CommandField EditText="עוד פרטים" SelectText="עוד פרטים" ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </asp:Panel>
        </center>
    </form>
</asp:Content>

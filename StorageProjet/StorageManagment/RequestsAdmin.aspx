<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RequestsAdmin.aspx.cs" Inherits="StorageManagment.RequestsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="HalfScrollablePanelContainer" runat="server" style="margin-top:5%; width:50%">
        <asp:Panel ID="Panel1" runat="server">
            <h3>בקשות מיוחדות</h3><br /><br />
            <center>
                <h4>מיין לפי:</h4>
                <asp:CheckBoxList ID="CBList" AutoPostBack="true" runat="server" verifyMultiSelect="True" RepeatDirection="Horizontal" CellSpacing="10">
                    <asp:ListItem Text="חדש"></asp:ListItem>
                    <asp:ListItem Text="בטיפול"></asp:ListItem>
                    <asp:ListItem Text="טופל"></asp:ListItem>
                    <asp:ListItem Text="הכל"></asp:ListItem>
                </asp:CheckBoxList><br />
            <asp:Button runat="server" id="SearchRequests" CssClass="button2" Text="חיפוש" OnClick="SearchRequests_Click" /></center>
            <br /><br /><br />
            <center>
            <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
            <asp:GridView ID="RequestsGV" runat="server" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10"
             ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowDataBound="RequestsGV_RowDataBound" OnSelectedIndexChanged="RequestsGV_SelectedIndexChanged"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="RequestsGV_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="שם" />
                    <asp:BoundField HeaderText="תאריך" />
                    <asp:BoundField HeaderText="בקשה" ItemStyle-CssClass="maxWidth"/>
                    <asp:BoundField HeaderText="סטטוס" />                    
                    <asp:CommandField SelectText="שינוי סטטוס" ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
            </asp:GridView></center>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" CssClass="sign">
            <center>
                <h3>סטטוס חדש</h3><br />
                <asp:DropDownList id="StatusDDL" CssClass="input" width="30%" runat="server"></asp:DropDownList><br /><br />
                <asp:Button runat="server" id="SelectNewStatus" CssClass="button3" Text="שינוי סטטוס" OnClick="SelectNewStatus_Click"/>
                <asp:Button runat="server" id="CancelButton" CssClass="button3" Text="ביטול" OnClick="CancelButton_Click"/>
            </center>
        </asp:Panel>
    </form>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RolesAdmin.aspx.cs" Inherits="StorageManagment.RolesAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="ThirdScrollablePanelContainer" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="sign">
            <h3>חיפוש תפקיד</h3><br />
            <asp:TextBox CssClass="input" ID="ByName" placeHolder="שם" runat="server"></asp:TextBox><br />
            <center><asp:Button ID="Search" runat="server" Text="חיפוש" CssClass="button2" OnClick="Search_Click" /></center><br /><br />
            <asp:Label ID="err3" runat="server" Text="" Visible="false"></asp:Label>
            <center>
            <asp:GridView ID="RolesGV" runat="server" OnRowDataBound="RolesGV_RowDataBound" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="RolesGV_SelectedIndexChanged"
             AllowPaging="true" PageSize="10" OnPageIndexChanging="RolesGV_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="RoleName" HeaderText="שם תפקיד" />
                    <asp:CommandField EditText="עריכה" SelectText="עריכה" ShowSelectButton="True" />
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
            </center><br />
            <center><asp:Button ID="AddRole" runat="server" Text="הוספת תפקיד" CssClass="button2" OnClick="AddRole_Click"/></center><br /><br />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" CssClass="sign">
            <h3>עריכת תפקיד</h3>
            <asp:TextBox CssClass="input" placeHolder="שם התפקיד" ID="RName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="regName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="RName"></asp:RequiredFieldValidator><br />
            <asp:Button CssClass="button" ID="SubmitChngRole" runat="server" Text="ביצוע שינויים" OnClick="SubmitChngDetails_Click" CausesValidation="true" />
            <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
            <div style="padding:10px; text-align:center;">
                <asp:Button CssClass="button3" ID="DeleteBTN" runat="server" Text="מחיקה" OnClick="DeleteBTN_Click" CausesValidation="false" />
                <asp:Button CssClass="button3" ID="CancelBTN" runat="server" Text="ביטול" OnClick="CancelBTN_Click" CausesValidation="false" />
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server">
            <h3>הוספת תפקיד</h3>
            <asp:TextBox CssClass="input" placeHolder="שם התפקיד" ID="NewRoleName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqName2" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="NewRoleName"></asp:RequiredFieldValidator><br />
            <asp:Label ID="err2" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Button CssClass="button" ID="AddRoleToDB" runat="server" OnClick="AddRoleToDB_Click" Text="ביצוע הוספה" CausesValidation="true" />
            <div style="padding:10px; text-align:center;">
                <asp:Button CssClass="button3" ID="CancelBTN2" runat="server" OnClick="CancelBTN_Click" Text="ביטול" CausesValidation="false" />
            </div>
        </asp:Panel>
    </form>
</asp:Content>
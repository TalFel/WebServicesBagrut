<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="UsersAdmin.aspx.cs" Inherits="StorageManagment.UsersAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="ThirdScrollablePanelContainer" style="margin-top:30px; margin-bottom:30px;" runat="server">
        <asp:Panel ID="Panel1" runat="server">
        <h3>חיפוש משתמשים</h3><br />
        <asp:TextBox CssClass="input" ID="FirstNameTB" placeHolder="שם פרטי" runat="server"></asp:TextBox>
        <asp:TextBox CssClass="input" ID="LastNameTB" placeHolder="שם משפחה" runat="server"></asp:TextBox>
        <asp:TextBox CssClass="input" ID="PhoneTB" placeHolder="מספר טלפון" runat="server"></asp:TextBox><br />
        <center><h4>תפקיד</h4></center>
        <asp:DropDownList CssClass="input" ID="RoleDDLSearch" runat="server"></asp:DropDownList><br />
        <asp:CheckBox ID="OldUsersCB" runat="server" Text="כלילת משתמשי עבר" Checked="false" />
        <center><asp:Button ID="Search" runat="server" Text="חיפוש" CssClass="button2" OnClick="Search_Click" /></center><br /><br />
        <asp:Label ID="err2" runat="server" Text="" Visible="false"></asp:Label>
        <asp:GridView ID="UsersGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="UsersGV_RowDataBound" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="UsersGV_SelectedIndexChanged"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="UsersGV_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="UserFirstName" HeaderText="שם פרטי" />
                <asp:BoundField DataField="UserFamilyName" HeaderText="שם משפחה" />
                <asp:BoundField DataField="UserPhoneNumber" HeaderText="מספר טלפון" />
                <asp:BoundField DataField="UserRole" HeaderText="תפקיד" />
                <asp:CheckBoxField DataField="UserActive" HeaderText="פעיל" />
                <asp:CommandField SelectText="צפייה" ControlStyle-CssClass="hyper" ShowSelectButton="True" EditText="צפייה">
                <ControlStyle CssClass="hyper" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
        </asp:GridView>
    </asp:Panel>
        <center>
        <asp:Panel ID="Panel2" runat="server" Width="400px">
            <asp:HyperLink ID="HyperGoBack" CssClass="hyper" runat="server" NavigateUrl="~/UsersAdmin.aspx"><-חזור</asp:HyperLink>
            <h3>ערוך משתמש</h3>
            <h4>שם פרטי</h4>
            <asp:TextBox CssClass="input" placeHolder="" ID="firstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="firstName"></asp:RequiredFieldValidator><br />
            <asp:RegularExpressionValidator ID="regFirstName" runat="server" ErrorMessage="חייב להכיל רק אותיות באנגלית" ControlToValidate="firstName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
            <h4>שם משפחה</h4>
            <asp:TextBox CssClass="input" placeHolder="" ID="lastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="lastName"></asp:RequiredFieldValidator><br />
            <asp:RegularExpressionValidator ID="reglastname" runat="server" ErrorMessage="חייב להכיל רק אותיות באנגלית" ControlToValidate="lastName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
            <h4>מספר טלפון</h4>
            <asp:TextBox CssClass="input" placeHolder="" ID="cell" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="regCell" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="cell"></asp:RequiredFieldValidator><br />
            <asp:RegularExpressionValidator ID="reqCell" runat="server" ErrorMessage="חייב להיות מספר תקין" ControlToValidate="cell" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
            <h4>תפקיד</h4>
            <asp:DropDownList CssClass="input" ID="RoleDDL" runat="server"></asp:DropDownList><br />
            <table>
                <tr>
                    <td>
                        <h4>משתמש פעיל</h4>
                    </td>
                    <td>
                        <asp:CheckBox ID="IsActiveCB" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Button CssClass="button" ID="ChngDetails" runat="server" Text="ביצוע שינויים" OnClick="ChngDetails_Click" CausesValidation="true" />
            <div style="padding:10px; text-align:center;">
                <asp:Button CssClass="button3" ID="DeleteBTN" runat="server" Text="מחיקה" OnClick="DeleteBTN_Click" CausesValidation="false" />
                <asp:Button CssClass="button3" ID="CancelBtn" runat="server" Text="ביטול" OnClick="CancelBTN_Click" CausesValidation="false" />
            </div>
        </asp:Panel>
        </center>
    </form><br />
</asp:Content>

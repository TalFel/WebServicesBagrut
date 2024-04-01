<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="StorageManagment.ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="ThirdScrollablePanelContainer" runat="server">
        <center>
        <asp:Panel ID="Panel1" runat="server" CssClass="sign" Width="300px">
            <div style="text-align: center;">
                <h3>פרטים אישיים</h3><br />
                <h4>שם פרטי</h4>
                <asp:TextBox CssClass="input" ID="firstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="firstName"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="regFirstName" runat="server" ErrorMessage="חייב להכיל רק אותיות" ControlToValidate="firstName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
                <h4>שם משפחה</h4>
                <asp:TextBox CssClass="input" ID="lastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="lastName"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="reglastname" runat="server" ErrorMessage="חייב להכיל רק אותיות" ControlToValidate="lastName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
                <h4>מספר טלפון</h4>
                <asp:TextBox CssClass="input" ID="cell" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="regCell" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="cell"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator ID="reqCell" runat="server" ErrorMessage="חייב להיות מספר תקין" ControlToValidate="cell" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                <h4>תפקיד</h4>
                <asp:DropDownList CssClass="input" ID="RoleDDL" runat="server"></asp:DropDownList>
                <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Button CssClass="button" ID="ChngDetails" CausesValidation="false" runat="server" Text="שינוי פרטים" OnClick="ChngDetails_Click" />
                <center><asp:Button CssClass="button3" ID="CancelBtn" runat="server" CausesValidation="false" Text="ביטול" OnClick="CancelBtn_Click" /></center>
            </div>
        </asp:Panel>
        </center>
    </form>
</asp:Content>

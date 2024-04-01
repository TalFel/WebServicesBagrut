<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" Codebehind="SignUp.aspx.cs" Inherits="StorageManagment.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="ThirdScrollablePanelContainer" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="sign">
            <div>
                <center>
                    <h3>שכבגיסט\ית חדש\ה</h3><br />
                    <h4>שם פרטי</h4>
                    <asp:TextBox CssClass="input" ID="firstName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="firstName"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="regFirstName" runat="server" ErrorMessage="חייב להכיל רק אותיות באנגלית" ControlToValidate="firstName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
                    <h4>שם משפחה</h4>
                    <asp:TextBox CssClass="input" ID="lastName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="lastName"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="reglastname" runat="server" ErrorMessage="חייב להכיל רק אותיות באנגלית" ControlToValidate="lastName" ValidationExpression="^[a-zA-Z]*"></asp:RegularExpressionValidator>
                    <h4>מספר טלפון</h4>
                    <asp:TextBox CssClass="input" ID="cell" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="regCell" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="cell"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="reqCell" runat="server" ErrorMessage="חייב להיות מספר תקין" ControlToValidate="cell" ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                    <h4>סיסמה</h4>
                    <asp:TextBox CssClass="input" ID="password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPass" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="password"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator ID="regPass" runat="server" ErrorMessage="חייב להיות באורך 6 עד 10, להכיל לפחות מספר אחד ואות אחד (רק באנגלית)" ControlToValidate="password" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,10})$"></asp:RegularExpressionValidator>
                    <h4>תפקיד</h4>
                    <asp:DropDownList CssClass="input" ID="RoleDDL" runat="server"></asp:DropDownList>
                    <br />
                    <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Button CssClass="button" ID="SubmitSignUp" runat="server" Text="הרשמה" OnClick="SubmitSignUp_Click" />
                    <center><asp:HyperLink ID="HyperLinkLogup" CssClass="hyper" runat="server" NavigateUrl="~/HomeSignIn.aspx">כבר יש לי חשבון</asp:HyperLink></center>
                    <br /><br />
                </center>
            </div>
        </asp:Panel>
    </form>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" Codebehind="HomeSignIn.aspx.cs" Inherits="StorageManagment.HomeSignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="container" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="sign">
            <h3>ברוכים הבאים לאתר ההזמנות<br /> של מחסן איתן!</h3><br /><br />
            <center><asp:TextBox CssClass="input" ID="cell" placeholder="טלפון" Width="100%" runat="server"></asp:TextBox>
            <asp:TextBox CssClass="input" ID="password" placeholder="סיסמה" Width="100%" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:Label ID="err" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Button ID="SubmitLogin" CssClass="button" runat="server" Text="התחברות" OnClick="SubmitLogin_Click" /></center>
            <center><asp:HyperLink ID="HyperLinkLogup" CssClass="hyper" runat="server" NavigateUrl="~/SignUp.aspx">הרשמה</asp:HyperLink><br /></center>    
        </asp:Panel>
    </form>
</asp:Content>

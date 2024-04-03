<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="OrdersAdminPage.aspx.cs" Inherits="StorageManagment.OrdersAdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="HalfScrollablePanelContainer">
        <center>
            <asp:Panel ID="OrdersListPanel" runat="server">
                <h3>ניהול הזמנות</h3><br />
                <h4>חיפוש לפי שם פרטי/משפחה:</h4>
                <asp:TextBox ID="ByNameTB" CssClass="input" Width="50%" placeHolder="שם" runat="server"></asp:TextBox>
                <br />
                <center><h4>סידור לפי:</h4></center>
                <asp:RadioButtonList ID="RBList" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" CellSpacing="10">
                    <asp:ListItem Text="תאריך"></asp:ListItem>
                    <asp:ListItem Text="משתמש"></asp:ListItem>
                    <asp:ListItem Text="סטטוס"></asp:ListItem>
                </asp:RadioButtonList>
                <table>
                    <tr>
                        <td>
                            <h4>כלילת הזמנות שבוצעו</h4>
                        </td>
                        <td>
                            <asp:CheckBox ID="IncludePastOrders" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="SearchConditions" runat="server" CssClass="button2" Text="חפש" OnClick="SearchConditions_Click" /><br /><br />
                <asp:Label ID="err" runat="server" Text="לא נמצאו הזמנות העונות על התנאים"></asp:Label>
                <asp:GridView ID="OrdersGV" cellpadding="10" cellspacing="5" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrdersHistoryGV_RowDataBound" OnRowCommand="OrdersHistoryGV_RowCommand" AllowPaging="true" OnPageIndexChanging="OrdersGV_PageIndexChanging" PageSize="10">
                    <Columns>
                        <asp:BoundField HeaderText="שם" ItemStyle-CssClass="cellspcaer" />
                        <asp:BoundField DataField="OrderTimeShortString" HeaderText="תאריך" ItemStyle-CssClass="cellspcaer" />
                        <asp:BoundField DataField="OrderDescription" HeaderText="הערות" ItemStyle-CssClass="cellspcaer" />
                        <asp:BoundField HeaderText="סטטוס" ItemStyle-CssClass="cellspcaer" />
                        <asp:CommandField EditText="עוד פרטים" SelectText="עוד פרטים" ShowSelectButton="True" ItemStyle-CssClass="cellspcaer" />
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
            <asp:Panel ID="OrderMoreDetailsPanel" runat="server">
                <asp:HyperLink ID="HyperGoBackToOrders" CssClass="hyper" runat="server" NavigateUrl="~/OrdersAdminPage.aspx"><-חזרה לרשימה</asp:HyperLink><br />
                <asp:Label ID="DateNameLabelViewOrder" CssClass="h3" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="StatusLabelViewOrder" CssClass="h4" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="StatusDDL" runat="server" CssClass="input" Width="30%" Visible="false"></asp:DropDownList><br /><br />
                <asp:GridView ID="SelectedOrderGV" CellSpacing="10" runat="server" AutoGenerateColumns="False" OnRowDataBound="SelectedOrderGV_RowDataBound" AllowPaging="true" PageSize="3" OnPageIndexChanging="SelectedOrderGV_PageIndexChanging" OnSelectedIndexChanged="SelectedOrderGV_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="תמונה">
                            <ItemTemplate>
                                <asp:Image ID="ProductImage" runat="server" CssClass="BotonDeImagen2"></asp:Image>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="פירוט" />
                        <asp:BoundField HeaderText="כמות" />
                        <asp:BoundField HeaderText="סטטוס" />
                        <asp:CommandField EditText="שינוי סטטוס" SelectText="שינוי סטטוס" ShowSelectButton="True" />
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
                </asp:GridView><br />
                <asp:Button ID="EditButton" runat="server" CssClass="button2" Text="עדכון" OnClick="EditButton_Click" />
                <asp:Button ID="DoChanges" runat="server" CssClass="button3" Text="ביצוע שינויים" Visible="false" OnClick="DoChanges_Click" />
                <asp:Button ID="CancelChanges" runat="server" CssClass="button3" Text="ביטול" Visible="false" OnClick="CancelChanges_Click" />
            </asp:Panel>
            <asp:Panel ID="EditProductStatusPanel" runat="server">
                <asp:Label ID="DateNameLabelEditStatus" CssClass="h3" runat="server" Text=""></asp:Label><br />
                <asp:Label ID="ProductName" CssClass="h3" runat="server" Text=""></asp:Label><br />
                <h4>סטטוס:</h4><br />
                <asp:DropDownList ID="ProductStatusDDL" runat="server" CssClass="input" Width="30%"></asp:DropDownList><br /><br />
                <asp:Button ID="DoChangesStatus" runat="server" CssClass="button3" Text="אישור" OnClick="DoChangesStatus_Click" />
                <asp:Button ID="CancelChangesStatus" runat="server" CssClass="button3" Text="ביטול" OnClick="CancelChangesStatus_Click" />
            </asp:Panel>
        <center>
        
    </form>
</asp:Content>
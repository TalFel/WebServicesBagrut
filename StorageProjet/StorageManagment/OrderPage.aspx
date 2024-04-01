<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="OrderPage.aspx.cs" Inherits="StorageManagment.OrderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="HalfScrollablePanelContainer">
        <asp:Panel ID="CurrentOrderPanel1" runat="server">
            <center><asp:Label CssClass="h3" runat="server" ID="HeaderOrder"></asp:Label></center>
            <center>
                <asp:Button ID="GoToItemsButton" CssClass="button2" runat="server" Text="הוספת מוצרים להזמנה" OnClick="GoToItemsButton_Click" />
            </center>
            <br />
            <center>
                <asp:GridView ID="OrderGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="OrderGV_RowDataBound" AllowPaging="true" PageSize="3" OnPageIndexChanging="OrderGV_PageIndexChanging" OnSelectedIndexChanged="OrderGV_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="תמונה">
                            <ItemTemplate>
                                <asp:Image ID="ProductImage" runat="server" CssClass="BotonDeImagen2"></asp:Image>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="פירוט" />
                        <asp:BoundField HeaderText="כמות" />
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
            </center>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="CurrentOrderPanel2" runat="server">
            <center>
                <table style="padding:15px; border-spacing: 30px;">
                    <tr>
                        <td>
                            <h4 style="text-align:center">לתאריך:</h4><br />
                        </td>
                        <td>
                            <h4 style="text-align:center">הערות(אופציונלי):</h4><br />
                        </td>
                    </tr>
                    <tr style="margin: 5px 5px 5px 5px">
                        <td>
                            <asp:Calendar ID="OrderCalendar" runat="server" daynameformat="Shortest" OnDayRender="OrderCalendar_DayRender" BackColor="White" BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
                                <DayHeaderStyle BackColor="#EEEEEE" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                                <DayStyle Width="14%" />
                                <TitleStyle Font-Size="8pt" ForeColor="White" />
                                <NextPrevStyle Font-Size="8pt" ForeColor="White" HorizontalAlign="Center" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="CornflowerBlue" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                <TitleStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="13pt" Height="14pt" />
                                <TodayDayStyle BackColor="DodgerBlue" />
                            </asp:Calendar><br />
                        </td>
                        <td>
                            <textarea style="width:100%; height:200px; resize:none;" placeHolder="הערות להזמנה" id="DescriptionTB" runat="server"></textarea>
                        </td>
                    </tr>
                </table>
            </center>
            <center>
                <asp:Label ID="err1" runat="server" Text="" Visible="false"></asp:Label><br />
                <asp:Button ID="DoOrder" CssClass="button2" runat="server" Text="הזמן" OnClick="DoOrder_Click" />
                <table style="width:100%;">
                    <tr>
                        <td style="text-align:left">
                            <asp:Button ID="DeleteOrder" CssClass="button3" runat="server" Text="מחיקת הזמנה" OnClick="DeleteOrder_Click" />
                        </td>
                        <td>
                            <asp:Button ID="CancelChanges" CssClass="button3" runat="server" Text="ביטול שינויים" OnClick="CancelChanges_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
        <asp:Panel ID="EditProduct" runat="server">
            <center>
                <asp:Label CssClass="h3" runat="server" ID="EditProductHeaderLabel"></asp:Label><br />
                <h4>כמות:</h4>
                <asp:TextBox ID="AmountChangeProduct" CssClass="input" runat="server"></asp:TextBox><br />
                <asp:Label runat="server" Text="" ID="err2" Visible="False"></asp:Label><br />
                <asp:Button ID="DoChangesToProduct" CssClass="button2" runat="server" Text="ביצוע שינויים" OnClick="DoChangesToProduct_Click" />
                <table style="width:100%;">
                    <tr>
                        <td style="text-align:left">
                            <asp:Button ID="DeleteProduct" CssClass="button3" runat="server" Text="מחיקה" OnClick="DeleteProduct_Click" />
                        </td>
                        <td>
                            <asp:Button ID="CancelChangesToProduct" CssClass="button3" runat="server" Text="ביטול" OnClick="CancelChangesToProduct_Click" />
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
    </form>
</asp:Content>

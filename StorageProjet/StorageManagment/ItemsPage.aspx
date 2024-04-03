<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ItemsPage.aspx.cs" Inherits="StorageManagment.ItemsPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:Panel ID="ItemsPanel" runat="server" CssClass="HalfScrollablePanelContainer">
            <asp:Panel runat="server" ID="p1">
                <asp:HyperLink ID="HyperGoBackToOrder" CssClass="hyper" runat="server" NavigateUrl="~/OrderPage.aspx?PastOrder=True"><-חזרה להזמנה</asp:HyperLink>
                <center>
                    <h3>המחסן</h3>
                    <h4>חיפוש מוצרים</h4>
                    <asp:TextBox CssClass="input" ID="ByCategoryName" placeHolder="שם" runat="server"></asp:TextBox><br />
                    <asp:Button ID="SearchCategories" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchItems_Click" /><br /><br />
                    <asp:Label ID="err1" runat="server" Text="" Visible="false"></asp:Label>
                </center>
                <center>
                    <asp:DataList ID="IdagidogDataList" runat="server" RepeatColumns="3" CssClass="DataList" OnItemCommand="IdagidogDataList_ItemCommand" >
                        <ItemTemplate>
                            <div class="ItemsList" style="text-align:center;">
                                <asp:ImageButton ID="ImgItems" runat="server" CssClass="BotonDeImagen2" ImageUrl='<%#Bind("CategoryImageLink") %>'></asp:ImageButton>
                                <asp:Label ID="LblItems" CssClass="h4" runat="server" Width="30%" Text='<%#Bind("CategoryName") %>' ></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </center>
                <center>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align:left; padding:10px 10px">
                                <asp:Button ID="prev" CssClass="button5" Width="30px" runat="server" Text="➡" OnClick="prev_Click" />
                            </td>
                            <td style="padding:10px 10px">
                                <asp:Button ID="next" CssClass="button5" Width="30px" runat="server" Text="⬅" OnClick="next_Click" />
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <asp:Panel runat="server" ID="p2">
                <asp:Button ID="GoBackButton" runat="server" Text="חזרה לבחירה" CausesValidation="false" CssClass="button4" OnClick="GoBackButton_Click" /><br />
                <center>
                    <asp:Label runat="server" ID="CatLabel" Text="" CssClass="h3"></asp:Label>
                    <br /><br />
                    <h4>חפש סוג</h4>
                    <asp:DropDownList runat="server" ID="ColorsDDL" CssClass="input" Width="30%"></asp:DropDownList>
                    <asp:DropDownList runat="server" ID="SizesDDL" CssClass="input" Width="30%"></asp:DropDownList><br />
                    <asp:Button ID="SearchSpecifcProduct" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchSpecifcProduct_Click" /><br /><br />
                    <br /><asp:Label ID="err2" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AutoGenerateColumns="False" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="סוג">
                                <ItemTemplate>
                                    <asp:Label ID="ProductText" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Amount" HeaderText="כמות במלאי" />
                            <asp:CommandField EditText="בחירה" SelectText="בחירה" ShowSelectButton="True" />
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
            <asp:Panel  runat="server" ID="p3">
                <asp:Button ID="Button1" runat="server" Text="חזרה לבחירה" CausesValidation="false" CssClass="button4" OnClick="GoBackButton_Click" /><br />
                <center>
                    <asp:Label runat="server" ID="CatLabel2" Text="" CssClass="h3"></asp:Label><br />
                    <asp:Label runat="server" ID="AmountLabel" Text="כמות:" CssClass="h4"></asp:Label><br />
                    <asp:TextBox CssClass="input" ID="AmountInputTB" runat="server"></asp:TextBox>
                    <asp:Label ID="err3" runat="server" Text="" Visible="false"></asp:Label><br />
                    <asp:Button ID="AddToOrder" runat="server" Text="הוספה להזמנה!" CssClass="button2" OnClick="AddToOrder_Click" />
                </center>
            </asp:Panel>
        </asp:Panel>
    </form>
    <br />
</asp:Content>

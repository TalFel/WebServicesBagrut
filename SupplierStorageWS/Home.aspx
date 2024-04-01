<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SupplierStorageWS.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ישראל ישראלי ספקים בע"מ</title>
    <link rel="stylesheet" href="/styles.css" />
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <h1>ישראל ישראלי ספקים בערבון מוגבל</h1>
            <asp:Panel ID="ChoosePanel" runat="server">
                <asp:Button ID="UpdateStock" runat="server" Text="חידוש מלאי" OnClick="updateStock_Click" />
                <asp:Button ID="UpdatePhotos" runat="server" Text="עדכון תמונות" OnClick="updatePhotos_Click" />
            </asp:Panel>

            <asp:Panel ID="ViewRestockPanel" runat="server">
                <asp:Panel ID="GVRestockPanel" runat="server">
                    <asp:Button ID="GoBackSelect1" CssClass="goBack" runat="server" Text="<" OnClick="GoBackSelect_Click" />
                    <h4>סינון לפי שם:</h4>
                    <asp:TextBox ID="ByNameTB" runat="server"></asp:TextBox>
                    <h4>צפייה רק במוצרים לא במלאי</h4>
                    <asp:CheckBox ID="OnlyStockCB" runat="server" />
                    <asp:Button ID="SearchProducts" runat="server" Text="חיפוש" OnClick="SearchProducts_Click" /><br />
                    <asp:GridView ID="RestockGV" runat="server" AllowPaging="True" OnPageIndexChanging="RestockGV_PageIndexChanging" OnSelectedIndexChanged="RestockGV_SelectedIndexChanged" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ProductCatColSzeString" HeaderText="מוצר" />
                            <asp:BoundField DataField="Amount" HeaderText="כמות" />
                            <asp:CommandField EditText="בחר" HeaderText="בחר" SelectText="בחר" ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="ProductRestock" runat="server">
                    <asp:Button ID="GoBackRestock" CssClass="goBack" runat="server" Text="<" OnClick="GoBackRestock_Click" />
                    <asp:Label ID="ProductLabel" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="ProductAmountLabel" runat="server" Text=""></asp:Label><br />
                    <h3>הוספת כמות:</h3><br />
                    <asp:TextBox ID="InputNewAmount" runat="server"></asp:TextBox><br />
                    <asp:Button ID="SubmitNewAmount" runat="server" Text="הוספה" OnClick="SubmitNewAmount_Click"/>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="ViewCategoriesPanel" runat="server">
                <asp:Panel ID="CategoriesDataListPanel" runat="server">
                    <asp:Button ID="GoBackSelect2" CssClass="goBack" runat="server" Text="<" OnClick="GoBackSelect_Click" />
                    <div style="text-align:center; width:60%;">
                        <asp:DataList ID="CategoriesDataList" runat="server" RepeatColumns="3" CssClass="DataList" OnItemCommand="CategoriesDataList_ItemCommand" OnItemDataBound="CategoriesDataList_ItemDataBound">
                            <ItemTemplate>
                                <div class="ItemsList" style="text-align:center;">
                                    <asp:ImageButton ID="Image" runat="server" CssClass="DataImage" ImageUrl='<%#Bind("CategoryImageLink")%>'></asp:ImageButton>
                                    <asp:Label ID="Name" CssClass="h4" runat="server" Width="30%" Text='<%#Bind("CategoryName") %>' ></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList><br />
                        <table style="width: 100%;">
                            <tr>
                                <td style="padding:10px 10px; text-align:right;">
                                    <asp:Button ID="next" CssClass="arrowButton" Width="30px" runat="server" Text="⬅" OnClick="next_Click" />
                                </td>
                                <td style="text-align: left; padding:10px 10px">
                                    <asp:Button ID="prev" CssClass="arrowButton" Width="30px" runat="server" Text="➡" OnClick="prev_Click"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="UpdateCategoryImagePanel" runat="server">
                    <asp:Button ID="GoBackCategories" CssClass="goBack" runat="server" Text="<" OnClick="GoBackCategories_Click" />

                </asp:Panel>
            </asp:Panel>
        </form>
    </center>
</body>
</html>

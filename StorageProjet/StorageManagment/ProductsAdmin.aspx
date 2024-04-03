<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ProductsAdmin.aspx.cs" Inherits="StorageManagment.ProductsAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" style="all:unset" runat="server">
        <asp:Panel ID="p2" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <asp:Panel ID="Panel11" runat="server" CssClass="HalfScrollablePanelContainer">
                    <asp:Button ID="gobackbtn1" runat="server" Text="חזרה לבחירה" CausesValidation="false" CssClass="button4" OnClick="GoBackTooptions_Click" />
                    <center><h3>חיפוש קטגוריה</h3>
                    <asp:TextBox CssClass="input" ID="ByCategoryName" placeHolder="שם" runat="server"></asp:TextBox><br />
                    <table>
                        <tr>
                            <td>
                                <h4>כלילת קטגוריות לא פעילות </h4>
                            </td>
                            <td>
                                <asp:CheckBox ID="NotActiveCat" runat="server" /><br />
                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="SearchCategories" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchCategories_Click" /><br /><br />
                    <asp:Label ID="err11" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:GridView ID="CategoriesGV" runat="server" Width="50%" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="CategoriesGV_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="CategoriesGV_PageIndexChanging" PageSize="3">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:ImageField DataImageUrlField="CategoryImageLink" HeaderText="תמונה">
                                <ControlStyle CssClass="BotonDeImagen" />
                            </asp:ImageField>
                            <asp:BoundField DataField="CategoryName" HeaderText="שם קטגוריה" />
                            <asp:BoundField DataField="CategoryDescription" HeaderText="תיאור" />
                            <asp:CheckBoxField DataField="CategoryActive" HeaderText="פעילה" />
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
                    </asp:GridView><br />
                    <asp:Button ID="AddCategory" runat="server" Text="הוספת קטגוריה" CssClass="button2" OnClick="AddCategory_Click"/>
                </asp:Panel>
                <asp:Panel ID="Panel12" runat="server" CssClass="ThirdScrollablePanelContainer">
                    <center><asp:Label runat="server" ID="NameOfCategory" CssClass="h3"></asp:Label></center>
                    <asp:Panel ID="Panel121" runat="server">
                        <asp:Panel runat="server" ID="Panel1211">
                            <center>
                            <h4>שם הקטגוריה</h4>
                            <asp:TextBox CssClass="input" placeHolder="" ID="CatName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="CatName"></asp:RequiredFieldValidator><br />
                            <h4>תיאור</h4>
                            <asp:TextBox CssClass="input" placeHolder="" ID="CatDesc" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="CatDesc"></asp:RequiredFieldValidator><br />
                            <h4>העלאת תמונה חדשה </h4><asp:FileUpload ID="FileUploadCategory" runat="server"/>
                            </center>
                        </asp:Panel>
                        <center><asp:Panel ID="Panel1212" runat="server">
                            <asp:Label runat="server" ID="CatDescriptionNotEditing"></asp:Label>
                            <asp:Image CssClass="BotonDeImagen" />
                        </asp:Panel></center>
                        <br /><br />
                        <center>
                        <table>
                            <tr>
                                <td>
                                    <h4>קטגוריה פעילה </h4>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CatActiveCB" runat="server" />
                                </td>
                            </tr>
                        </table>
                        </center>
                        <br />
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:GridView ID="ProductsGV" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="ProductsGV_PageIndexChanging" OnRowDataBound="ProductsGV_RowDataBound" OnSelectedIndexChanged="ProductsGV_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundField DataField="productString" HeaderText="מוצרים" />
                                                <asp:BoundField DataField="Amount" HeaderText="כמות" />
                                                <asp:CommandField EditText="בחירה" SelectText="עריכה" ShowSelectButton="True"/>
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
                                    </td>
                                    <td>
                                        <a style="width:30px"></a>
                                        <br />
                                    </td>
                                    <td>
                                        <asp:GridView ID="AllowedRolesGV" runat="server" AutoGenerateColumns="False" OnRowDataBound="AllowedRolesGV_RowDataBound" AllowPaging="true" PageSize="10" OnPageIndexChanging="AllowedRolesGV_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="RoleName" HeaderText="תפקיד" />
                                                <asp:TemplateField HeaderText="מאושר">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="AllowedCB" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                            <SelectedRowStyle Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <center><asp:Button CssClass="button" ID="EditCategoriesButton" runat="server" Text="שינוי קטגוריה" OnClick="EditCategoriesButton_Click" />
                        <asp:Button CssClass="button" ID="SubmitChngCategory" runat="server" Text="ביצוע שינויים" OnClick="SubmitChngCategory_Click" CausesValidation="true" />
                        <asp:Label ID="err12" runat="server" Text="" Visible="false"></asp:Label>
                        <div style="padding:10px; text-align:center;">
                            <asp:Button CssClass="button3" ID="AddCategoryBTN" runat="server" Text="הוסף מוצר" OnClick="loadAddProduct" CausesValidation="false" />
                            <asp:Button CssClass="button3" ID="CancelCategoryBTN11" runat="server" Text="חזרה" OnClick="CancelBTN_Click" CausesValidation="false" />
                        </div></center>
                    </asp:Panel>
                    <asp:Panel ID="Panel122" runat="server">
                        <center><table style="text-align:center">
                            <center>                            
                            <tr style="width:100%; text-align:center;">
                                <td>
                                    <center><h3 style="font-size: 20px;">חפש צבע</h3>
                                    <asp:TextBox CssClass="input" ID="ByColorProductName" placeHolder="שם" runat="server"></asp:TextBox><br />
                                    <asp:Button ID="SearchColorPanelProducts" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchColorPanelProducts_Click" /><br /><br />
                                    <asp:Label ID="err121" runat="server" Text="" Visible="false"></asp:Label>
                                    <br /></center>
                                </td>
                                <td>
                                    <center><h3 style="font-size: 20px;">חיפוש גודל</h3>
                                    <asp:TextBox CssClass="input" ID="BySizeProductName" placeHolder="שם" runat="server"></asp:TextBox><br />
                                    <asp:Button ID="SearchSizePanelProducts" runat="server" Text="חפש" CssClass="button2" OnClick="SearchSizePanelProducts_Click" /><br /><br />
                                    <asp:Label ID="err122" runat="server" Text="" Visible="false"></asp:Label>
                                    <br /></center>
                                </td>
                            </tr></center>
                            <tr>
                                <td><center>
                                    <asp:GridView ID="ColorPanelProducts" runat="server" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="ColorPanelProducts_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="ColorName" HeaderText="שם צבע" />
                                            <asp:CommandField EditText="בחירה" SelectText="בחירה" ShowSelectButton="True" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                        <SelectedRowStyle Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView></center>
                                </td>
                                <td><center>
                                    <asp:GridView ID="SizePanelProducts" runat="server" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="SizePanelProducts_SelectedIndexChanged">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="SizeName" HeaderText="שם צורה/גודל" />
                                            <asp:CommandField EditText="בחירה" SelectText="בחירה" ShowSelectButton="True" />
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" />
                                        <SelectedRowStyle Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView></center>
                                </td>
                            </tr>
                        </table>
                            <asp:TextBox CssClass="input" ID="amountLabel" PlaceHolder="כמות" runat="server" Text="" Visible="true" Width="50%"></asp:TextBox>
                        </center>
                        <asp:Label ID="err123" runat="server" Text="" Visible="false"></asp:Label>
                        <div style="padding:10px; text-align:center;">
                            <asp:Button CssClass="button3" ID="DoChangesoProduct" runat="server" Text="הוספה" OnClick="DoChangesoProduct_Click" CausesValidation="false" />
                            <asp:Button CssClass="button3" ID="CancelChange" runat="server" Text="ביטול" OnClick="CancelChange_Click" CausesValidation="false" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel12amount" runat="server" CssClass="ThirdScrollablePanelContainer" Visible="false">
                    <center>
                        <asp:Label ID="AmountHeader" CssClass="h3" runat="server" Text=""></asp:Label><br /><br />
                        <asp:Label ID="CurrentAmount" CssClass="h3" runat="server" Text=""></asp:Label>
                        <h3>כמות חדשה:</h3>
                        <asp:TextBox CssClass="input" ID="UpdateAmountValue" runat="server"></asp:TextBox>
                        <asp:Label ID="errAmount" runat="server" placeHolder="כמות.." Text="" Visible="false"></asp:Label>
                        <div style="padding:10px; text-align:center;">
                            <asp:Button CssClass="button3" ID="UpdateAmount" runat="server" Text="עדכון כמות" OnClick="UpdateAmount_Click" CausesValidation="false" />
                            <asp:Button CssClass="button3" ID="CancelUpdateAmount" runat="server" Text="ביטול" OnClick="CancelUpdateAmount_Click" CausesValidation="false" />
                        </div>
                    </center>
                </asp:Panel>
                <center><asp:Panel ID="Panel13" runat="server" CssClass="container">
                    <h3>הוספת קטגוריה</h3>
                    <asp:TextBox CssClass="input" placeHolder="שם הקטגוריה" ID="NewCategoryName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="NewCategoryName"></asp:RequiredFieldValidator><br />
                    <asp:TextBox CssClass="input" placeHolder="תיאור" ID="NewCategoryDescription" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="NewCategoryDescription"></asp:RequiredFieldValidator><br />
                    <h4>העלה תמונה </h4><asp:FileUpload ID="NewCategoryImage" runat="server"/>
                    <asp:Label ID="err13" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Button CssClass="button" ID="AddCategoryToDB" runat="server" OnClick="AddCategoryToDB_Click" Text="ביצוע הוספה" CausesValidation="true" />
                    <div style="padding:10px; text-align:center;">
                        <asp:Button CssClass="button3" ID="CancelCategoryBTN12" runat="server" OnClick="CancelBTN_Click" Text="ביטול" CausesValidation="false" />
                    </div>
                </asp:Panel></center>
            </asp:Panel>
    
            <asp:Panel ID="Panel2" runat="server" CssClass="container" Width="30%">
                <asp:Button ID="gobackbtn2" runat="server" Text="חזרה לבחירה" CausesValidation="false" CssClass="button4" OnClick="GoBackTooptions_Click" />
                <asp:Panel ID="Panel21" runat="server">
                    <center><h3>חיפוש צבע</h3><br />
                    <center><h4>שם:</h4></center>
                    <asp:TextBox CssClass="input" ID="ByColorName" placeHolder="" runat="server"></asp:TextBox></center><br />
                    <center><asp:Button ID="SearchColors" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchColors_Click" /></center><br /><br />
                    <asp:Label ID="err21" runat="server" Text="" Visible="false"></asp:Label>
                    <center><asp:GridView ID="ColorsGV" runat="server" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="ColorsGV_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ColorName" HeaderText="שם צבע" />
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
                    </asp:GridView></center><br />
                    <center><asp:Button ID="AddColor" runat="server" Text="הוספת צבע" CssClass="button2" OnClick="AddColor_Click"/></center><br /><br />
                </asp:Panel>
                <asp:Panel ID="Panel22" runat="server" CssClass="sign">
                    <h3>עריכת צבע</h3><br />
                    <center><h4>שם:</h4></center>
                    <center><asp:TextBox CssClass="input" placeHolder="" ID="ColName" runat="server"></asp:TextBox></center>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="ColName"></asp:RequiredFieldValidator><br />
                    <asp:Button CssClass="button" ID="SubmitChngColor" runat="server" Text="ביצוע שינויים" OnClick="SubmitChngColor_Click" CausesValidation="true" />
                    <asp:Label ID="err22" runat="server" Text="" Visible="false"></asp:Label>
                    <div style="padding:10px; text-align:center;">
                        <asp:Button CssClass="button3" ID="DeleteColorBTN" runat="server" Text="מחיקה" OnClick="DeleteColorBTN_Click" CausesValidation="false" />
                        <asp:Button CssClass="button3" ID="CancelColorBTN21" runat="server" Text="ביטול" OnClick="CancelBTN_Click" CausesValidation="false" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel23" runat="server">
                    <h3>הוספת צורה/גודל</h3><br />
                    <center><h4>שם:</h4></center>
                    <center><asp:TextBox CssClass="input" placeHolder="" ID="NewColorName" runat="server"></asp:TextBox></center>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="NewColorName"></asp:RequiredFieldValidator><br />
                    <asp:Label ID="err23" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Button CssClass="button" ID="AddColorToDB" runat="server" OnClick="AddColorToDB_Click" Text="ביצוע הוספה" CausesValidation="true" />
                    <div style="padding:10px; text-align:center;">
                        <asp:Button CssClass="button3" ID="CancelColorBTN22" runat="server" OnClick="CancelBTN_Click" Text="ביטול" CausesValidation="false" />
                    </div>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server" CssClass="container" Width="30%">
                <asp:Button ID="gobackbtn3" runat="server" Text="חזרה לבחירה" CausesValidation="false" CssClass="button4" OnClick="GoBackTooptions_Click" />
                <asp:Panel ID="Panel31" runat="server">
                    <center><h3>חיפוש צורה/גודל</h3>
                    <br />
                    <center><h4>שם:</h4></center>
                    <asp:TextBox CssClass="input" ID="BySizeName" placeHolder="" runat="server"></asp:TextBox><br />
                    <asp:Button ID="SearchSizes" runat="server" Text="חיפוש" CssClass="button2" OnClick="SearchSizes_Click" /><br /><br />
                    <asp:Label ID="err31" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:GridView ID="SizesGV" runat="server" AutoGenerateColumns="False" CssClass="gv" CellSpacing="2" CellPadding="10" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="SizesGV_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="SizeName" HeaderText="שם צורה\גודל" />
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
                    </asp:GridView></center><br />
                    <center><asp:Button ID="AddSize" runat="server" Text="הוספת צורה\גודל" CssClass="button2" OnClick="AddSize_Click"/></center><br /><br />
                </asp:Panel>
                <asp:Panel ID="Panel32" runat="server" CssClass="sign">
                    <h3>עריכת צורה/גודל</h3><br />
                    <center><h4>שם:</h4></center>
                    <center><asp:TextBox CssClass="input" placeHolder="" ID="SName" runat="server"></asp:TextBox></center>
                    <asp:RequiredFieldValidator ID="regName31" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="SName"></asp:RequiredFieldValidator><br />
                    <asp:Button CssClass="button" ID="SubmitChngSize" runat="server" Text="ביצוע שינויים" OnClick="SubmitChngSize_Click" CausesValidation="true" />
                    <asp:Label ID="err32" runat="server" Text="" Visible="false"></asp:Label>
                    <div style="padding:10px; text-align:center;">
                        <asp:Button CssClass="button3" ID="DeleteSizeBTN" runat="server" Text="מחק" OnClick="DeleteSizeBTN_Click" CausesValidation="false" />
                        <asp:Button CssClass="button3" ID="CancelSizeBTN31" runat="server" Text="בטל" OnClick="CancelBTN_Click" CausesValidation="false" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel33" runat="server" CssClass="sign">
                    <h3>הוספת צורה/גודל</h3><br />
                    <center><h4>שם:</h4></center>
                    <center><asp:TextBox CssClass="input" placeHolder="" ID="NewSizeName" runat="server"></asp:TextBox></center>
                    <asp:RequiredFieldValidator ID="reqName32" runat="server" ErrorMessage="שדה דרוש" ControlToValidate="NewSizeName"></asp:RequiredFieldValidator><br />
                    <asp:Label ID="err33" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Button CssClass="button" ID="AddSizeToDB" runat="server" OnClick="AddSizeToDB_Click" Text="ביצוע הוספה" CausesValidation="true" />
                    <div style="padding:10px; text-align:center;">
                        <asp:Button CssClass="button3" ID="CancelSizeBTN32" runat="server" OnClick="CancelBTN_Click" Text="ביטול" CausesValidation="false" />
                    </div>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="ChooseEdits" runat="server">
                <div class="container">
                   <div style="margin: unset; text-align:center">
                       <asp:Button ID="CatButton" runat="server" Text="עריכת קטגוריות" CssClass="button2" OnClick="CatButton_Click" />
                       <asp:Button ID="ColButton" runat="server" Text="עריכת צבעים" CssClass="button2" OnClick="ColButton_Click" />
                       <asp:Button ID="SzeButton" runat="server" Text="עריכת גדלים" CssClass="button2" OnClick="SzeButton_Click" />
                   </div>
                </div>
            </asp:Panel>

        </asp:Panel></center>
    </form>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="RequestPage.aspx.cs" Inherits="StorageManagment.RequestPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" class="HalfScrollablePanelContainer" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="sign">
            <center>
                <h3>בקשות מיוחדות (דברים שאין במחסן)</h3><br />
                <center><textarea style="width:80%; height:200px; resize:none;" placeHolder="לדוגמה: פאצים לגדוד ד" id="requestText" runat="server"></textarea><br /></center>                
            </center>
                <br /><h4>לתאריך:</h4><br />
            <center>
                <asp:Calendar ID="OrderCalendar" runat="server" daynameformat="Shortest" OnDayRender="OrderCalendar_DayRender" BackColor="White" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px">
                    <DayHeaderStyle BackColor="#EEEEEE" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
                    <DayStyle Width="14%" />
                    <TitleStyle Font-Size="8pt" ForeColor="White" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="Black" HorizontalAlign="Center" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="CornflowerBlue" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                    <TitleStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="13pt" Height="14pt" />
                    <TodayDayStyle BackColor="DodgerBlue" />
                </asp:Calendar><br />
                <asp:RequiredFieldValidator ID="reqRequestText" runat="server" ErrorMessage="צריך למלא בקשה" ControlToValidate="requestText"></asp:RequiredFieldValidator><br />
                <div style="padding:10px; text-align:center;">
                    <asp:Button CssClass="button3" ID="SendBTN" runat="server" OnClick="SendBTN_Click" Text="שלח" CausesValidation="true" />
                    <asp:Button CssClass="button3" ID="OldRequests" runat="server" OnClick="OldRequests_Click" Text="בקשות עבר" CausesValidation="false" />
                </div>
            </center>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <center>
                <h3>היסטורית הבקשות שלי</h3><br />
                <asp:GridView ID="HistoryGV" runat="server" AutoGenerateColumns="False" OnRowCommand="HistoryGV_RowCommand" OnRowDataBound="HistoryGV_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="RequestDateShortString" HeaderText="תאריך" />
                        <asp:BoundField DataField="RequestContent" ItemStyle-Width="200px" HeaderText="תוכן" >
                        <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RequestStatusString" HeaderText="סטטוס" />
                        <asp:CommandField EditText="בטל בקשה" ShowSelectButton="True" SelectText="ביטול בקשה" />
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
                <asp:Button CssClass="button3" ID="NewRequest" runat="server" OnClick="NewRequest_Click" Text="בקשה חדשה" CausesValidation="false" />
            </center>
        </asp:Panel>
    </form>
</asp:Content>

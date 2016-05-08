<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientTransferReport.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.ClientTransferReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label CssClass="control-label col-md-2" Text="Your account number: " runat="server" AssociatedControlID="yourAccount" />
            <div class="col-md-4">
                <asp:Label ID="yourAccount" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:GridView Visible="true" ID="transferHistory" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:BoundField DataField="toAccountNumber" HeaderText="Target Account Number" ReadOnly="True" />
                    <asp:BoundField DataField="amount" HeaderText="Amount" ReadOnly="True" />
                    <asp:BoundField DataField="time" HeaderText="Time" ReadOnly="True" />
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

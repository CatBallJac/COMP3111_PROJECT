<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientReport.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.ClientReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Report Client</h2>

    <h3>Summary</h3>

    <div>
        <asp:Label  runat="server" Text="Account Number:" AssociatedControlID="txtAccountNumber"></asp:Label>
        <asp:Label  runat="server" Text="" ID="txtAccountNumber"></asp:Label>
    </div>

    <asp:gridview ID="gvSummary" runat="server" Visible="True" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="totalValue" HeaderText="Total Value(HKD)" DataFormatString="{0:n2}" ReadOnly="True" SortExpression="totalValue" />
            <asp:BoundField DataField="freeBalance" HeaderText="Free Balance" DataFormatString="{0:n2}" ReadOnly="True" SortExpression="freeBalance" />
            <asp:BoundField DataField="stockValue" HeaderText="Stock Value(HKD)" DataFormatString="{0:n2}" ReadOnly="True" SortExpression="stockValue" />
            <asp:BoundField DataField="bondValue" HeaderText="Bond Value(HKD)" DataFormatString="{0:n2}" ReadOnly="True" SortExpression="bondValue" />
            <asp:BoundField DataField="unitTrustValue" DataFormatString="{0:n2}" HeaderText="Unit Trust Value(HKD)" ReadOnly="True" SortExpression="unitTrustValue" />
            <asp:BoundField DataField="lastOrderDate" HeaderText="Date of Last Order" ReadOnly="True" SortExpression="lastOrderDate" />
            <asp:BoundField DataField="lastOrderValue" DataFormatString="{0:n2}" HeaderText="Value of Last Order" ReadOnly="True" SortExpression="lastOrderValue" />
        </Columns>
    </asp:gridview>
    
    <div>
        <div>
            

            <asp:DropDownList ID="ddlSecurityType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSecurityType_SelectedIndexChanged">
                <asp:ListItem Value="0">Security type</asp:ListItem>
                <asp:ListItem Value="bond">Bond</asp:ListItem>
                <asp:ListItem Value="stock">Stock</asp:ListItem>
                <asp:ListItem Value="unit trust">Unit Trust</asp:ListItem>
                <asp:ListItem></asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                <asp:ListItem Value="0">Currency</asp:ListItem>
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label ID="lblClientName" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblResultMessage" runat="server" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:GridView ID="gvSecurityHolding" runat="server" Visible="False" AutoGenerateColumns="False" OnSorting="gvSecurityHolding_Sorting" OnSelectedIndexChanged="gvSecurityHolding_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="code" HeaderText="Code" ReadOnly="True" SortExpression="code" />
                    <asp:BoundField DataField="name" HeaderText="Name" ReadOnly="True" SortExpression="name" />
                    <asp:BoundField DataField="shares" DataFormatString="{0:n2}" HeaderText="Shares" ReadOnly="True" SortExpression="shares" />
                    <asp:BoundField DataField="base" HeaderText="Base" ReadOnly="True" />
                    <asp:BoundField DataField="price" DataFormatString="{0:n2}" HeaderText="Price" ReadOnly="True" />
                    <asp:BoundField DataField="value" DataFormatString="{0:n2}" HeaderText="Value" ReadOnly="True" SortExpression="value" />
                    <asp:BoundField DataField="convertedValue" DataFormatString="{0:n2}" HeaderText="Value in" ReadOnly="True" SortExpression="convertedValue" />
                </Columns>
                <RowStyle HorizontalAlign="Left" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

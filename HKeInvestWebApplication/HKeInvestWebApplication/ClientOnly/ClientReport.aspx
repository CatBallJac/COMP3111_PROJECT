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
        <div>
            <asp:gridview ID="gvOrder" runat="server" Visible="True" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="referenceNumber" HeaderText="Reference Number"  ReadOnly="True" SortExpression="referenceNumber" />
            <asp:BoundField DataField="buyOrSell" HeaderText="Buy or Sell" ReadOnly="True" SortExpression="buyOrSell" />
            <asp:BoundField DataField="securityType" HeaderText="Security Type" ReadOnly="True" SortExpression="securityType" />
            <asp:BoundField DataField="securityCode" HeaderText="Security Code" ReadOnly="True" SortExpression="bondValue" />
            <asp:BoundField DataField="securityName" HeaderText="Security Name" ReadOnly="True" SortExpression="securityName" />
            <asp:BoundField DataField="dateSubmitted" HeaderText="Submitted Date" ReadOnly="True" SortExpression="dateSubmitted" />
            <asp:BoundField DataField="status" HeaderText="Current Status" ReadOnly="True" SortExpression="status" />
            <asp:BoundField DataField="amount" HeaderText="Amount" ReadOnly="True"  DataFormatString="{0:n2}" SortExpression="amount" />
            <asp:BoundField DataField="shares" HeaderText="Quantity of shares" ReadOnly="True" SortExpression="shares" />
            <asp:BoundField DataField="limitPrice" HeaderText="Limit Price" ReadOnly="True"  DataFormatString="{0:n2}" SortExpression="limitPrice" />
            <asp:BoundField DataField="stopPrice" HeaderText="Stop Price" ReadOnly="True"  DataFormatString="{0:n2}" SortExpression="stopPrice" />
            <asp:BoundField DataField="expiryDay" HeaderText="Expiry Date" ReadOnly="True" SortExpression="expiryDay" />
        </Columns>
    </asp:gridview>
        </div>
    
        <div>
            <asp:CheckBox ID="cbCalendar" runat="server" AutoPostBack="true" OnCheckedChanged="cbCalendar_CheckedChanged" />
            <asp:Label runat="server" Text="Do you want to set a range of dates?"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="Start date" Visible="false"></asp:Label>
            <asp:Calendar ID="startDate" runat="server" AutoPostBack="true" Visible="false"></asp:Calendar>
        </div>
        <div>
            <asp:Label runat="server" Text="End date" Visible="false"></asp:Label>
            <asp:Calendar ID="endDate" runat="server" AutoPostBack="true" Visible="false"></asp:Calendar>
            <asp:CustomValidator ID="cvDate" runat="server" Display="Dynamic" CssClass="text-danger" EnableClientScript="false"  OnServerValidate="cvDate_ServerValidate" Enabled="false"></asp:CustomValidator>
        </div>
        <asp:Button ID="searchHistory" runat="server" Text="search" OnClick="searchHistory_Click" />
        <div>
        <asp:gridview ID="gvHistory" runat="server" Visible="True" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="referenceNumber" HeaderText="Reference Number"  ReadOnly="True" SortExpression="referenceNumber" />
                <asp:BoundField DataField="buyOrSell" HeaderText="Buy or Sell" ReadOnly="True" SortExpression="buyOrSell" />
                <asp:BoundField DataField="securityType" HeaderText="Security Type" ReadOnly="True" SortExpression="securityType" />
                <asp:BoundField DataField="securityCode" HeaderText="Security Code" ReadOnly="True" SortExpression="bondValue" />
                <asp:BoundField DataField="securityName" HeaderText="Security Name" ReadOnly="True" SortExpression="securityName" />
                <asp:BoundField DataField="dateSubmitted" HeaderText="Submitted Date" ReadOnly="True" SortExpression="dateSubmitted" />
                <asp:BoundField DataField="status" HeaderText="Current Status" ReadOnly="True" SortExpression="status" />
                <asp:BoundField DataField="shares" HeaderText="Quantity of shares" ReadOnly="True" SortExpression="shares" />
                <asp:BoundField DataField="executedAmount" HeaderText="Executed amount" ReadOnly="True" SortExpression="executedAmount" />
                <asp:BoundField DataField="fee" HeaderText="Fee charged" ReadOnly="True" SortExpression="fee" />

            </Columns>
        </asp:gridview>
        
    </div>
        <asp:Label runat="server" Text="Transaction"></asp:Label>
        <div>
        <asp:gridview ID="gvTransaction" runat="server" Visible="True" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="transactionNumber" HeaderText="Transaction number"  ReadOnly="True" SortExpression="transactionNumber" />
                <asp:BoundField DataField="executedDate" HeaderText="Executed date" ReadOnly="True" SortExpression="executedDate" />
                <asp:BoundField DataField="executedShares" HeaderText="Executed shares" ReadOnly="True" SortExpression="executedShares" />
                <asp:BoundField DataField="price" HeaderText="Price" DataFormatString="{0:n2}" ReadOnly="True" SortExpression="price" />
            </Columns>
        </asp:gridview>
        
    </div>

        
    </div>
</asp:Content>

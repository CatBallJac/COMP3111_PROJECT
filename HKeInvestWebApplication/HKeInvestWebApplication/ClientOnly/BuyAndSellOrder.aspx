<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyAndSellOrder.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.BuyAndSellOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
    </div>

    <h2>Place Order</h2>

    <div>
        <asp:Label ID="labelSecurityType" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlSecurityType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSecurityType_SelectedIndexChanged">
            <asp:ListItem>-- choose security type --</asp:ListItem>
            <asp:ListItem>bond</asp:ListItem>
            <asp:ListItem>unit trust</asp:ListItem>
            <asp:ListItem>stock</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div ID ="divOrder" runat ="server" visible="false">
        <asp:Label ID="labelIsBuyOrSell" runat="server"></asp:Label>
        <asp:DropDownList ID="ddlIsBuyOrSell" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIsBuyOrSell_SelectedIndexChanged">
            <asp:ListItem>-- choose buy or sell order --</asp:ListItem>
            <asp:ListItem>Buy Order</asp:ListItem>
            <asp:ListItem>Sell Order</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div ID ="divCode" runat ="server" visible ="false" >
        <asp:Label ID="labelCode" runat="server" Text="code"> </asp:Label>
        <div>
        <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" >
            <asp:ListItem>-- choose code of available security --</asp:ListItem>
        </asp:DropDownList>
        </div>

        <asp:Label ID="labelSecurityName" runat="server" Text="name"></asp:Label>
        <div>
            <asp:TextBox ID="TextSecurityName" runat="server" Enabled="False"></asp:TextBox>
        </div>
    </div>

<div>
    <asp:PlaceHolder ID="buyStockOrderPlaceHolder" runat="server" Visible="False">
        <h6>security order: buy stock</h6>
        <asp:Label ID="LabelOrderType" runat="server" Text="Label"></asp:Label>
        <div>
        <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True">
            <asp:ListItem>-- choose Order type --</asp:ListItem>
            <asp:ListItem>market Order</asp:ListItem>
            <asp:ListItem>limit Order</asp:ListItem>
            <asp:ListItem>stop limit Order</asp:ListItem>
            <asp:ListItem>stop Order</asp:ListItem>
        </asp:DropDownList>
        </div>


        <asp:Label ID="LabelShares" runat="server" Text="Label"></asp:Label>
        <div>
             <asp:TextBox ID="TextShares" runat="server"></asp:TextBox>
        </div>

        <asp:Label ID="LabelExpiryDay" runat="server" Text="Label"></asp:Label>
        <div>
        <asp:DropDownList ID="ddlExpiryDay" runat="server" AutoPostBack="True">
            <asp:ListItem>-- choose expiry day --</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
        </asp:DropDownList> 
                  
        </div>
        <asp:Label ID="LabelAllOrNone" runat="server" Text="Label"></asp:Label>
        <asp:DropDownList ID="ddlAllOrNone" runat="server" AutoPostBack="True">
            <asp:ListItem>-- choose if all or none --</asp:ListItem>
            <asp:ListItem>is allornone order</asp:ListItem>
            <asp:ListItem>not allornone order</asp:ListItem>
        </asp:DropDownList> 
        <div>

        <asp:Label ID="LabelHighPrice" runat="server" Text="Label"></asp:Label>
        </div>
              <asp:TextBox ID="TextHighPrice" runat="server"></asp:TextBox>
        <div>

        <asp:Label ID="LabelStopPrice" runat="server" Text="Label"></asp:Label>
        </div>
              <asp:TextBox ID="TextStopPrice" runat="server"></asp:TextBox>
        <div>

        </div>
    </asp:PlaceHolder>
</div>

<div>
    <asp:PlaceHolder ID="buyBondOrderPlaceHolder" runat="server" Visible ="false">
        <h6>security order: buy bond or unit trust</h6>

        <div>
        <asp:Label ID="labelAmount" runat="server" Text="dollar amount to buy (in HKD)"></asp:Label>
            <asp:TextBox ID="TextAmount" runat="server"></asp:TextBox>
        </div>

    </asp:PlaceHolder>
</div>



</asp:Content>



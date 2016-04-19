<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyAndSellOrder.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.BuyAndSellOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
    </div>

    <h2>Place Order</h2>

    <asp:Label ID="labelSecurityType" runat="server"></asp:Label>
    <div>

        <asp:RadioButtonList ID="rbSecurityType" runat="server" OnSelectedIndexChanged="rbSecurityType_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>bond</asp:ListItem>
            <asp:ListItem>unit trust</asp:ListItem>
            <asp:ListItem Selected="True">stock</asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <asp:Label ID="labelIsBuyOrSell" runat="server"></asp:Label>
    <div>

        <asp:RadioButtonList ID="rbIsBuyOrSell" runat="server" OnSelectedIndexChanged="rbIsBuyOrSell_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True">buy order</asp:ListItem>
            <asp:ListItem>sell order</asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div ID ="divCodeAndName" runat ="server">
        <h5>security infomation</h5>
        <asp:Label ID="labelCode" runat="server" Text="code"> </asp:Label>
        <div>
            <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" >
                <asp:ListItem>-- choose code of available security --</asp:ListItem>
           </asp:DropDownList>
        </div>

        <asp:Label ID="labelSecurityName" runat="server" Text="name"></asp:Label>
        <div>
            <asp:Label ID="LabelSecurityNametxt" runat="server" ></asp:Label>
        </div>
        <asp:Label ID="LabelSellLimit" runat="server" Visiable ="false"></asp:Label>

    </div>


    <div id="divStockOrderDetail" runat="server" Visible="false">
        <h5>security order: stock</h5>
        <asp:Label ID="LabelOrderType" runat="server" Text="order type"></asp:Label>
        <div>
           <asp:RadioButtonList ID="rbOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbOrderType_SelectedIndexChanged">
                <asp:ListItem Selected="True">market</asp:ListItem>
                <asp:ListItem>limit</asp:ListItem>
                <asp:ListItem>stop limit</asp:ListItem>
                <asp:ListItem>stop</asp:ListItem>
            </asp:RadioButtonList>
        </div>

        <asp:Label ID="LabelExpiryDay" runat="server" Text="expiry day [1 - 7]"></asp:Label>
        <div>
            <asp:DropDownList ID="ddlExpiryDay" runat="server" AutoPostBack="True">
                <asp:ListItem>-- choose expiry day --</asp:ListItem>
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
            </asp:DropDownList> 
        </div>

        <asp:Label ID="LabelAllOrNone" runat="server" Text="order property"></asp:Label>
        <div>
            <asp:DropDownList ID="ddlAllOrNone" runat="server" AutoPostBack="True">
                <asp:ListItem>-- choose if all or none --</asp:ListItem>
                <asp:ListItem Selected="True">y</asp:ListItem>
                <asp:ListItem>n</asp:ListItem>
            </asp:DropDownList> 
        </div>

        <div id="divStopPrice" runat="server" visible="false">
            <asp:Label ID="LabelStopPrice" runat="server" Text="stop price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextStopPrice" runat="server">1</asp:TextBox>
           </div>
        </div>

        <div id="divMarketPrice" runat="server" visible="false">
            <asp:Label ID="LabelMarketPrice" runat="server" Text="martket price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextMarketPrice" runat="server">1</asp:TextBox>
           </div>
        </div>

        <div id ="divLimitPirce" runat="server" visible ="false">
            <asp:Label ID="LabelLimitPrice" runat="server" Text="limit price" ></asp:Label>
            <div>
                  <asp:TextBox ID="TextLimitPrice" runat="server" Text="0"></asp:TextBox>
            </div>
        </div>

        <div id ="divBuyStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelBuyShares" runat="server" Text="shares to buy (x100)" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextBuyShares" runat="server">200</asp:TextBox>
            </div>
        </div>

        <div id ="divSellStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelSellShares" runat="server" Text="amount of shares to sell" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextSellShares" runat="server">2</asp:TextBox>
            </div>
        </div>

    </div>


    <div id ="divBondOrderDetail" runat="server" visible="false"> 
        <h6>security order: buy bond or unit trust</h6>

        <div id ="divBondOrderDetail_buy" runat="server" visible="false"> 
        <asp:Label ID="labelAmount" runat="server" Text="dollar amount to buy (in HKD)"></asp:Label>
            <asp:TextBox ID="TextAmount" runat="server">22</asp:TextBox>
        </div>
        <div id ="divBondOrderDetail_sell" runat="server" visible="false"> 
        <asp:Label ID="labelShares" runat="server" Text="# shares to sell"></asp:Label>
            <asp:TextBox ID="TextShares" runat="server">32</asp:TextBox>
        </div>

    </div>
        <div>
                <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="submit" />
        </div>




</asp:Content>


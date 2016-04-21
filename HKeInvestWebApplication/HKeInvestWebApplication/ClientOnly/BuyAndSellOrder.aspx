<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyAndSellOrder.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.BuyAndSellOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
    </div>

    <h2>Place Order</h2>

    <asp:Label ID="labelSecurityType" runat="server">please choose the security type</asp:Label>
    <div>

        <asp:RadioButtonList ID="rbSecurityType" runat="server" OnSelectedIndexChanged="rbSecurityType_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>bond</asp:ListItem>
            <asp:ListItem>unit trust</asp:ListItem>
            <asp:ListItem Selected="True">stock</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="rfSecurityType" runat="server" ErrorMessage="security type required!" ControlToValidate="rbSecurityType"></asp:RequiredFieldValidator>
    </div>

    <asp:Label ID="labelIsBuyOrSell" runat="server">please choose buy/sell</asp:Label>
    <div>

        <asp:RadioButtonList ID="rbIsBuyOrSell" runat="server" OnSelectedIndexChanged="rbIsBuyOrSell_SelectedIndexChanged" AutoPostBack="True" ControlToValidate="rbIsBuyOrSell">
            <asp:ListItem Selected="True">buy order</asp:ListItem>
            <asp:ListItem>sell order</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="rfIsBuyOrSell" runat="server" ErrorMessage="buy or sell required" ControlToValidate="rbIsBuyOrSell"></asp:RequiredFieldValidator>
    </div>

    <div ID ="divCodeAndName" runat ="server">
        <h5>security infomation</h5>
        <asp:Label ID="labelCode" runat="server" Text="code"> </asp:Label>
        <div>
            <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" >
                <asp:ListItem Value="nocode">-- choose code of available security --</asp:ListItem>
           </asp:DropDownList>
            <asp:CustomValidator ID="cvCode" runat="server" ErrorMessage="must choose one code"></asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfCode" runat="server" ErrorMessage="security code required" ControlToValidate="ddlCode"></asp:RequiredFieldValidator>
        </div>

        <asp:Label ID="labelSecurityName" runat="server" Text="name"></asp:Label>
        <div>
            <asp:Label ID="LabelSecurityNametxt" runat="server" > </asp:Label>
        </div>
        <asp:Label ID="LabelSellLimit" runat="server" Visiable ="false"> </asp:Label>

    </div>
    
    <div id="divStockOrderDetail" runat="server" >
        <h5>security order: stock</h5>
        
        <asp:Label ID="LabelOrderType" runat="server" Text="order type"></asp:Label>
        <div>
           <asp:RadioButtonList ID="rbOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbOrderType_SelectedIndexChanged">
                <asp:ListItem>market</asp:ListItem>
                <asp:ListItem>limit</asp:ListItem>
                <asp:ListItem>stop limit</asp:ListItem>
                <asp:ListItem>stop</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfOrderType" runat="server" ErrorMessage="order type required" ControlToValidate="rbOrderType"></asp:RequiredFieldValidator>
        </div>

        <asp:Label ID="LabelExpiryDay" runat="server" Text="expiry day [1 - 7]"></asp:Label>
        <div>
            <asp:DropDownList ID="ddlExpiryDay" runat="server" AutoPostBack="False">
                <asp:ListItem>-- choose expiry day --</asp:ListItem>
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
            </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="rfExpiryDay" runat="server" ErrorMessage="ExpiryDay required" ControlToValidate="ddlExpiryDay"></asp:RequiredFieldValidator>
        </div>

        <asp:Label ID="LabelAllOrNone" runat="server" Text=" is all or none order"></asp:Label>
        <div>
            <asp:RadioButtonList ID="rbAllOrNone" runat="server">
                <asp:ListItem Selected="True">y</asp:ListItem>
                <asp:ListItem>n</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfAllOrNone" runat="server" ErrorMessage="all or none order choice required" ControlToValidate="rbAllOrNone"></asp:RequiredFieldValidator>
        </div>
        

   
        <div id="divStopPrice" runat="server" visible="false">
            <asp:Label ID="LabelStopPrice" runat="server" Text="stop price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextStopPrice" runat="server">1</asp:TextBox>
                <asp:RequiredFieldValidator ID="StopPrice" runat="server" ErrorMessage="Stop Price required" ControlToValidate="TextStopPrice"></asp:RequiredFieldValidator>
           </div>
        </div>
    

       
        <div id="divMarketPrice" runat="server" visible="false">
            <asp:Label ID="LabelMarketPrice" runat="server" Text="martket price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextMarketPrice" runat="server">1</asp:TextBox>
                <asp:RequiredFieldValidator ID="rfMarketPrice" runat="server" ErrorMessage="Market Price required" ControlToValidate="TextMarketPrice"></asp:RequiredFieldValidator>
           </div>
        </div>
        

     
        <div id ="divLimitPirce" runat="server" visible ="false">
            <asp:Label ID="LabelLimitPrice" runat="server" Text="limit price" ></asp:Label>
            <div>
                  <asp:TextBox ID="TextLimitPrice" runat="server" Text="0"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfLimitPrice" runat="server" ErrorMessage="limit price are required" ControlToValidate="TextLimitPrice"></asp:RequiredFieldValidator>
            </div>
        </div>
      

       
        <div id ="divBuyStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelBuyShares" runat="server" Text="shares to buy (x100)" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextBuyShares" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfBuyShares" runat="server" ErrorMessage="buy shares are required" ControlToValidate="TextBuyShares"></asp:RequiredFieldValidator>
            </div>
        </div>
    

       
        <div id ="divSellStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelSellShares" runat="server" Text="amount of shares to sell" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextSellShares" runat="server">2</asp:TextBox>
                <asp:RequiredFieldValidator ID="rfSellShares" runat="server" ErrorMessage="sell shares are required" ControlToValidate="TextSellShares"></asp:RequiredFieldValidator>
            </div>

        </div>
        
    </div>
   

    <div id ="divBondOrderDetail" runat="server" visible="false"> 
        <h6>security order: buy bond or unit trust</h6>

        <div id ="divBondOrderDetail_buy" runat="server" visible="false"> 
        <asp:Label ID="labelAmount" runat="server" Text="dollar amount to buy (in HKD)"></asp:Label>
            <asp:TextBox ID="TextAmount" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfAmount" runat="server" ErrorMessage="dollar amount required" ControlToValidate="TextAmount"></asp:RequiredFieldValidator>
        </div>

        <div id ="divBondOrderDetail_sell" runat="server" visible="false"> 
        <asp:Label ID="labelShares" runat="server" Text="# shares to sell"></asp:Label>
            <asp:TextBox ID="TextShares" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfShares" runat="server" ErrorMessage="shares are required" ControlToValidate="TextShares"></asp:RequiredFieldValidator>
        </div>

    </div>
        <div>
                <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="submit" />
                <div>
                <asp:Label ID="LabelResult" runat="server" Font-Bold="True" Font-Strikeout="False" ForeColor="#009933"></asp:Label>
                </div>
        </div>

    




</asp:Content>
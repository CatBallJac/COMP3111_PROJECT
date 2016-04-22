<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyAndSellOrder.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.BuyAndSellOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label" Visible="False"></asp:Label>
    </div>

    <h2>Place Order</h2>

    <asp:Label ID="labelSecurityType" runat="server">please choose the security type</asp:Label>
    <div>

        <asp:RadioButtonList ID="rbSecurityType" runat="server" OnSelectedIndexChanged="rbSecurityType_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>bond</asp:ListItem>
            <asp:ListItem>unit trust</asp:ListItem>
            <asp:ListItem>stock</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="rfSecurityType" runat="server" ErrorMessage="security type required!" ControlToValidate="rbSecurityType"></asp:RequiredFieldValidator>
    </div>

    <asp:Label ID="labelIsBuyOrSell" runat="server">please choose buy/sell</asp:Label>
    <div>

        <asp:RadioButtonList ID="rbIsBuyOrSell" runat="server" OnSelectedIndexChanged="rbIsBuyOrSell_SelectedIndexChanged" AutoPostBack="True" ControlToValidate="rbIsBuyOrSell">
            <asp:ListItem>buy order</asp:ListItem>
            <asp:ListItem>sell order</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="rfIsBuyOrSell" runat="server" ErrorMessage="buy or sell required" ControlToValidate="rbIsBuyOrSell"></asp:RequiredFieldValidator>
    </div>

    <div ID ="divCodeAndName" runat ="server">
        <h5>security infomation</h5>
        <asp:Label ID="labelCode" runat="server" Text="code"> </asp:Label>
        <div>
            <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" >
                <asp:ListItem Selected="True">-- choose code of available security --</asp:ListItem>
           </asp:DropDownList>
            <asp:CustomValidator ID="cvCode" runat="server" ErrorMessage="must choose one code" OnServerValidate="cvCode_ServerValidate"></asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfCode" runat="server" ErrorMessage="security code required" ControlToValidate="ddlCode"></asp:RequiredFieldValidator>
        </div>

        <asp:Label ID="labelSecurityName" runat="server" Text="name"></asp:Label>
        <div>
            <asp:Label ID="LabelSecurityNametxt" runat="server" > </asp:Label>
        </div>
        <asp:Label ID="LabelSellLimit" runat="server" Visiable ="false"> </asp:Label>

        <asp:TextBox ID="TextMaxiShares" runat="server" ReadOnly="True"></asp:TextBox>

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
            <asp:RequiredFieldValidator ID="rfExpiryDay" runat="server" ErrorMessage="ExpiryDay required" ControlToValidate="ddlExpiryDay" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>

        <asp:Label ID="LabelAllOrNone" runat="server" Text=" is all or none order"></asp:Label>
        <div>
            <asp:RadioButtonList ID="rbAllOrNone" runat="server">
                <asp:ListItem>y</asp:ListItem>
                <asp:ListItem>n</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfAllOrNone" runat="server" ErrorMessage="all or none order choice required" ControlToValidate="rbAllOrNone" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        

   
        <div id="divStopPrice" runat="server" visible="false">
            <asp:Label ID="LabelStopPrice" runat="server" Text="stop price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextStopPrice" runat="server" MaxLength="9"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="StopPrice" runat="server" ErrorMessage="Stop Price required" ControlToValidate="TextStopPrice" Display="Dynamic"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="rvStopPrice" runat="server" ErrorMessage="price has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,3}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,3}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextStopPrice" Display="Dynamic"></asp:RegularExpressionValidator>
           </div>
        </div>
    

       
        <div id="divMarketPrice" runat="server" visible="false">
            <asp:Label ID="LabelMarketPrice" runat="server" Text="martket price"></asp:Label>
            <div>
                  <asp:TextBox ID="TextMarketPrice" runat="server" MaxLength="9"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfMarketPrice" runat="server" ErrorMessage="Market Price required" ControlToValidate="TextMarketPrice"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="rvMarketPrice" runat="server" ErrorMessage="price has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,3}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,3}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextMarketPrice" Display="Dynamic"></asp:RegularExpressionValidator>
           </div>
        </div>
        

     
        <div id ="divLimitPirce" runat="server" visible ="false">
            <asp:Label ID="LabelLimitPrice" runat="server" Text="limit price" ></asp:Label>
            <div>
                  <asp:TextBox ID="TextLimitPrice" runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfLimitPrice" runat="server" ErrorMessage="limit price are required" ControlToValidate="TextLimitPrice"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rvLimitPrice" runat="server" ErrorMessage="price has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,3}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,3}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextLimitPrice" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
        </div>
      

       
        <div id ="divBuyStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelBuyShares" runat="server" Text="shares to buy" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextBuyShares" runat="server" MaxLength="13"></asp:TextBox>
                &nbsp;X 100
                <asp:RequiredFieldValidator ID="rfBuyShares" runat="server" ErrorMessage="buy shares are required" ControlToValidate="TextBuyShares" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rvBuyShares" runat="server" ErrorMessage="share amount has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextBuyShares" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
        </div>
    

       
        <div id ="divSellStockOrder" runat="server" visible ="false">
            <asp:Label ID="LabelSellShares" runat="server" Text="amount of shares to sell" ></asp:Label>
            <div>
                 <asp:TextBox ID="TextSellShares" runat="server" MaxLength="13"></asp:TextBox>
                 <asp:CustomValidator ID="cvSellShares" runat="server" ControlToValidate="TextSellShares" OnServerValidate="cvSellShares_ServerValidate"></asp:CustomValidator>

                 <asp:RequiredFieldValidator ID="rfSellShares" runat="server" ErrorMessage="sell shares are required" ControlToValidate="TextSellShares"></asp:RequiredFieldValidator>
                 <asp:RegularExpressionValidator ID="rvSellShares" runat="server" ErrorMessage="share amount has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextSellShares"></asp:RegularExpressionValidator>
            </div>

        </div>
        
    </div>
   

    <div id ="divBondOrderDetail" runat="server" visible="false"> 
        <h6>security order: bond or unit trust</h6>

        <div id ="divBondOrderDetail_buy" runat="server" visible="false"> 
        <asp:Label ID="labelAmount" runat="server" Text="dollar amount to buy (in HKD)"></asp:Label>
            <asp:TextBox ID="TextAmount" runat="server" MaxLength="9"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfAmount" runat="server" ErrorMessage="dollar amount required" ControlToValidate="TextAmount"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rvAmount" runat="server" ErrorMessage="dollar has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextAmount" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

        <div id ="divBondOrderDetail_sell" runat="server" visible="false"> 
        <asp:Label ID="labelShares" runat="server" Text="# shares to sell"></asp:Label>
            <asp:TextBox ID="TextShares" runat="server" MaxLength="13"></asp:TextBox>
            <asp:CustomValidator ID="cvShares" runat="server"  ControlToValidate="TextShares" OnServerValidate="cvShares_ServerValidate"></asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfShares" runat="server" ErrorMessage="shares are required" ControlToValidate="TextShares"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rvShares" runat="server" ErrorMessage="share amount has wrong format" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" ControlToValidate="TextShares" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>

    </div>
        <div>
                <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="submit" />
                <div>
                <asp:Label ID="LabelResult" runat="server" Font-Bold="True" Font-Strikeout="False" ForeColor="#009933"></asp:Label>
                </div>
        </div>




</asp:Content>


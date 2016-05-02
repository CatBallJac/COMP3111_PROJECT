<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BuyAndSellOrder.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.BuyAndSellOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
    </div>

    <h2>Place Order</h2>
    <hr />
    <asp:Label ID="welcomeMsg" runat="server" Font-Bold="False" Font-Strikeout="False" ForeColor="#000066"></asp:Label>
    
    <h5>please choose the security type to buy or sell</h5>
    <div class="col-md-offset-1">
        <div class="form-group">
            <asp:RadioButtonList ID="rbSecurityType" runat="server" OnSelectedIndexChanged="rbSecurityType_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>bond</asp:ListItem>
                <asp:ListItem>unit trust</asp:ListItem>
                <asp:ListItem>stock</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfSecurityType" runat="server" ErrorMessage="security type required!" ControlToValidate="rbSecurityType" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
        </div>
    </div>

    <h5>please choose buy/sell</h5>
    <div class="col-md-offset-1">
        <div class="form-group">
            <asp:RadioButtonList ID="rbIsBuyOrSell" runat="server" OnSelectedIndexChanged="rbIsBuyOrSell_SelectedIndexChanged" AutoPostBack="True" ControlToValidate="rbIsBuyOrSell">
                <asp:ListItem>buy order</asp:ListItem>
                <asp:ListItem>sell order</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfIsBuyOrSell" runat="server" ErrorMessage="buy or sell required" ControlToValidate="rbIsBuyOrSell" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div id="divCodeAndName" runat="server" aria-dropeffect="link" class="form-group">
        <h4>security infomation</h4>
        <h5>code: </h5>
        <div class="col-md-offset-1">
            <div id="inputCode" runat="server" visible="false">
                <div class="form-group">
                    <asp:TextBox ID="TextCode" runat="server" ></asp:TextBox>
                    <asp:CustomValidator ID="cvCodeInput" runat="server" ErrorMessage="security code not found" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ControlToValidate="TextCode" OnServerValidate="cvCodeInput_ServerValidate"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfCodeInput" runat="server" ErrorMessage="must input security code" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ControlToValidate="TextCode"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="erCode" runat="server" ErrorMessage="code input invalid" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ValidationExpression="^\d{1,4}$" ControlToValidate="TextCode"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div id="listCode" runat="server" visible="false">
                <div class="form-group">
                    <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged">
                        <asp:ListItem Selected="True">-- choose code of available security --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CustomValidator ID="cvCode" runat="server" ErrorMessage="must input security code" OnServerValidate="cvCode_ServerValidate"  ControlToValidate="ddlCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfCodeSelect" runat="server" ErrorMessage="security code required" ControlToValidate="ddlCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
                <h5>name: </h5>
                <div>
                    <asp:Label ID="LabelSecurityNametxt" runat="server"> </asp:Label>
                </div>
                <h5>SellLimit: </h5>
                <div>

                    <asp:Label ID="LabelSellLimit" runat="server" Visiable="false"> </asp:Label>
                    <asp:TextBox ID="TextMaxiShares" runat="server" ReadOnly="True" BorderStyle="None" Font-Strikeout="False"></asp:TextBox>
                </div>
            </div>
        </div>

    </div>

    <div id="divStockOrderDetail" runat="server">
        <h5>security order: stock</h5>
        <div class="form-group">
            <asp:Label ID="LabelOrderType" runat="server" Text="order type"></asp:Label>
            <div class="col-md-offset-1">

                <asp:RadioButtonList ID="rbOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbOrderType_SelectedIndexChanged">
                    <asp:ListItem>market</asp:ListItem>
                    <asp:ListItem>limit</asp:ListItem>
                    <asp:ListItem>stop limit</asp:ListItem>
                    <asp:ListItem>stop</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfOrderType" runat="server" ErrorMessage="order type required" ControlToValidate="rbOrderType" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="LabelExpiryDay" runat="server" Text="expiry day [1 - 7]"></asp:Label>
            <div class="col-md-offset-1">

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
                <asp:RequiredFieldValidator ID="rfExpiryDay" runat="server" ErrorMessage="ExpiryDay required" ControlToValidate="ddlExpiryDay" Display="Dynamic" CssClass="text-danger" EnableClientScript="False"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="LabelAllOrNone" runat="server" Text=" is all or none order"></asp:Label>
            <div class="col-md-offset-1">
                <asp:RadioButtonList ID="rbAllOrNone" runat="server">
                    <asp:ListItem>Y</asp:ListItem>
                    <asp:ListItem>N</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfAllOrNone" runat="server" ErrorMessage="all or none order choice required" ControlToValidate="rbAllOrNone" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
            </div>
        </div>



        <div id="divStopPrice" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="LabelStopPrice" runat="server" Text="stop price"></asp:Label>
                <div class="col-md-offset-1">
                    <asp:TextBox ID="TextStopPrice" runat="server" MaxLength="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="StopPrice" runat="server" ErrorMessage="Stop Price required" ControlToValidate="TextStopPrice" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvStopPrice" runat="server" ErrorMessage="price has wrong format" ControlToValidate="TextStopPrice" ValidationExpression="^\$?([1-9]{1}[0-9]{0,3}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,3}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>



        <div id="divMarketPrice" runat="server" visible="false">
            <div>
            </div>
        </div>



        <div id="divLimitPirce" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="LabelLimitPrice" runat="server" Text="limit price"></asp:Label>
                <div class="col-md-offset-1">
                    <asp:TextBox ID="TextLimitPrice" runat="server" MaxLength="9"></asp:TextBox>
                    <asp:CustomValidator ID="cvLimitPrice" runat="server" ControlToValidate="TextLimitPrice" OnServerValidate="cvLimitPrice_ServerValidate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfLimitPrice" runat="server" ErrorMessage="limit price are required" ControlToValidate="TextLimitPrice" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvLimitPrice" runat="server" ErrorMessage="price has wrong format" ControlToValidate="TextLimitPrice" ValidationExpression="^\$?([1-9]{1}[0-9]{0,3}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,3}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$"  CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>



        <div id="divBuyStockOrder" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="LabelBuyShares" runat="server" Text="shares to buy"></asp:Label>
                <div class="col-md-offset-1">
                    <asp:TextBox ID="TextBuyShares" runat="server" MaxLength="13"></asp:TextBox>
                    &nbsp;X 100
                <asp:RequiredFieldValidator ID="rfBuyShares" runat="server" ErrorMessage="buy shares are required" ControlToValidate="TextBuyShares" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvBuyShares" runat="server" ErrorMessage="share amount has wrong format" ControlToValidate="TextBuyShares"  ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$"  CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>



        <div id="divSellStockOrder" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="LabelSellShares" runat="server" Text="amount of shares to sell"></asp:Label>
                <div class="col-md-offset-1">
                    <asp:TextBox ID="TextSellShares" runat="server" MaxLength="13"></asp:TextBox>
                    <asp:CustomValidator ID="cvSellShares" runat="server" ControlToValidate="TextSellShares" OnServerValidate="cvSellShares_ServerValidate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CustomValidator>

                    <asp:RequiredFieldValidator ID="rfSellShares" runat="server" ErrorMessage="sell shares are required" ControlToValidate="TextSellShares" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvSellShares" runat="server" ErrorMessage="share amount has wrong format" ControlToValidate="TextSellShares" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>

    </div>

    <div id="divBondOrderDetail" runat="server" visible="false">
        <div class="form-group">
            <h5>security order: bond or unit trust</h5>

            <div class="col-md-offset-1">
                <div id="divBondOrderDetail_buy" runat="server" visible="false">
                    <asp:Label ID="labelAmount" runat="server" Text="dollar amount to buy (in HKD)"></asp:Label>
                    <asp:TextBox ID="TextAmount" runat="server" MaxLength="9"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfAmount" runat="server" ErrorMessage="dollar amount required" ControlToValidate="TextAmount" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvAmount" runat="server" ErrorMessage="dollar has wrong format" ControlToValidate="TextAmount" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>

            <div class="col-md-offset-1">
                <div id="divBondOrderDetail_sell" runat="server" visible="false">
                    <asp:Label ID="labelShares" runat="server" Text="# shares to sell"></asp:Label>
                    <asp:TextBox ID="TextShares" runat="server" MaxLength="13" EnableViewState="False"></asp:TextBox>
                    <asp:CustomValidator ID="cvShares" runat="server" ControlToValidate="TextShares" OnServerValidate="cvShares_ServerValidate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfShares" runat="server" ErrorMessage="shares are required" ControlToValidate="TextShares" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rvShares" runat="server" ErrorMessage="share amount has wrong format" ControlToValidate="TextShares" ValidationExpression="^\$?([1-9]{1}[0-9]{0,9}(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,9}?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>
    </div>

    <div>
        <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="submit" BackColor="Blue" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" ForeColor="White" />
        <div>
            <asp:Label ID="LabelResult" runat="server" Font-Bold="True" Font-Strikeout="False" ForeColor="#009933"></asp:Label>
        </div>
    </div>




</asp:Content>


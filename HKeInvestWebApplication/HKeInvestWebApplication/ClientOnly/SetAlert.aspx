<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetAlert.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.SetAlert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="msg" runat="server" Text="Label"></asp:Label>
    </div>

    <h2>Set Alert</h2>
    <hr />
    <asp:Label ID="welcomeMsg" runat="server" Font-Bold="False" Font-Strikeout="False" ForeColor="#000066"></asp:Label>
    
    <h5>please choose the security type</h5>
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

    <h5>please choose the security</h5>
    <div class="col-md-offset-1">
        <div class="form-group">
            <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" >
                <asp:ListItem Selected="True" Value="0">-- choose code of available security --</asp:ListItem>
            </asp:DropDownList>
            <asp:CustomValidator ID="cvCode" runat="server" ErrorMessage="must input security code" OnServerValidate="cvCode_ServerValidate"  ControlToValidate="ddlCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:CustomValidator>
            <asp:RequiredFieldValidator ID="rfCodeSelect" runat="server" ErrorMessage="security code required" ControlToValidate="ddlCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div id="setAlert" runat="server" visible="false">
        <asp:Label ID="alertInfo" runat="server" Text=""></asp:Label>
        <asp:CustomValidator ID="AlertValidator" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="AlertValidator_ServerValidate"></asp:CustomValidator>
        <p>
            <asp:CheckBox ID="lowalert" runat="server" />
            I want to change low alert value to
            <asp:TextBox ID="lowvalue" runat="server" MaxLength="10"></asp:TextBox>
        </p>
        <p>
            <asp:CheckBox ID="highalert" runat="server" />
            I want to change high alert value to
            <asp:TextBox ID="highvalue" runat="server" MaxLength="10"></asp:TextBox>
        </p>
        <div id="divCancelLow" runat="server" visible="false">
            <asp:Button ID="cancelLow" runat="server" Text="Cancel Low Value Alert" OnClick="cancelLow_Click" />
        </div>
        <div id="divCancelHigh" runat="server" visible="false">
            <asp:Button ID="cancelhigh" runat="server" Text="Cancel High Value Alert" OnClick="cancelhigh_Click" />
        </div>
    </div>

    Submit alert value changes. (No need to click for canceling alerts).
    <div>
        <asp:Button ID="submit" runat="server" OnClick="submit_Click" Text="submit" BackColor="Blue" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" ForeColor="White" />
        <div>
            <asp:Label ID="LabelResult" runat="server" Font-Bold="True" Font-Strikeout="False" ForeColor="#009933"></asp:Label>
        </div>
    </div>






</asp:Content>

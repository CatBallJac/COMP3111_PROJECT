<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="HKeInvestWebApplication.ClientOnly.Transfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label CssClass="control-label col-md-2" Text="Your account number: " runat="server" AssociatedControlID="yourAccount" />
            <div class="col-md-4">
                <asp:Label ID="yourAccount" runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-md-2" AssociatedControlID="toAccountNumber" Text="Account number transfer to: " runat="server" />
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="toAccountNumber" CssClass="form-control" MaxLength="10" ValidationGroup="Page"  />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Account number of the target account is required" EnableClientScript="False" ControlToValidate="toAccountNumber" CssClass="text-danger" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="accountNumberFormat" runat="server" ControlToValidate="toAccountNumber" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The format of the account number is not correct" ForeColor="Red" ValidationExpression="^[A-Za-z]{2}[0-9]{8}$"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="invalidAccountNumber" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="invalidAccountNumber_ServerValidate"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-md-2" AssociatedControlID="transferAmount" Text="Transfer Amount: " runat="server" />
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="transferAmount" CssClass="form-control" MaxLength="13" ValidationGroup="Page" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Transfer amount is required" EnableClientScript="False" ControlToValidate="transferAmount" CssClass="text-danger" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="transferAmountFormat" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="transferAmountFormat_ServerValidate"></asp:CustomValidator>
                <asp:CustomValidator ID="transferAmountTooMuch" runat="server" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ForeColor="Red" OnServerValidate="transferAmountTooMuch_ServerValidate"></asp:CustomValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Button CssClass="btn-default" ID="transfer_btn" runat="server" Text="Transfer" OnClick="transfer_btn_Click" />
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="result" Font-Size="Medium" ForeColor="Red" />
        </div>
        <div class="form-group">
            <a href="ClientTransferReport.aspx" style="color: #FFFF00; font-size: medium">Click to view you transfer history</a>
        </div>
    </div>
</asp:Content>

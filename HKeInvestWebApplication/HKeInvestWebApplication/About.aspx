<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HKeInvestWebApplication.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>
    <h1>Description of additional feature</h1>
    <h2>&nbsp;Transfer:</h2>
    <p>
        clients can transfer money from his/her account to another account(the account number should be provided) and view his/her transfer history. <br />
        The maximum amount is the remaining balance in his/her account. <br />
        To transfer the cash clients should first login using their login account. <br />
        The cash will be transfered immediately. <br />
        With this additional feature, client will be able to transfer money conveniently in our system. <br />
</p>
</asp:Content>

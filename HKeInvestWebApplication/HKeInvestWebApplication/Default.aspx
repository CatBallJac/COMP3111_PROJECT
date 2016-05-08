<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HKeInvestWebApplication._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>HKeInvest Team107</h1>
        <p class="lead">&nbsp;</p>
        <p>&nbsp;</p>
    </div>

    <div class="row">
         <div class="col-md-4">
            <h2>Home page</h2>
            <p>
                <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/HOME.aspx">security searching</asp:LinkButton>
             </p>
        </div>

        <div class="col-md-4">
            <h2>Client Only</h2>
            <div>
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ClientOnly/ClientSecurityHoldingsDetails.aspx">SecurityHolding</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/ClientOnly/BuyAndSellOrder.aspx">Buy and Sell</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/ClientOnly/ClientReport.aspx">report</asp:LinkButton>
            </div>
            <div>
                <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/ClientOnly/ClientManageAccountAndClientInfo.aspx">manage Account</asp:LinkButton>
            </div>

                        <div>
                <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl="~/ClientOnly/ClientManageAccountAndClientInfo.aspx">manage Account</asp:LinkButton>
            </div>
                        <div>
                <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl="~/ClientOnly/SetAlert.aspx">set alert</asp:LinkButton>
            </div>
                        <div>
                <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl="~/ClientOnly/Track.aspx">profit loss tracking</asp:LinkButton>
            </div>

                        <div>
                <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl="~/ClientOnly/transfer.aspx">transfer</asp:LinkButton>
            </div>
        </div>


        <div class="col-md-4">
            <h2>Employee Only</h2>
            <div>
                <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/EmployeeOnly/EmployeeReport.aspx">Report</asp:LinkButton>
            </div>

                        <div>
                <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl="~/EmployeeOnly/ManageAccountAndClientInfo.aspx">manage client account information</asp:LinkButton>
            </div>
                        <div>
                <asp:LinkButton ID="LinkButton12" runat="server" PostBackUrl="~/EmployeeOnly/SecurityHoldingDetails.aspx">security holding details</asp:LinkButton>
            </div>



        </div>

    </div>

</asp:Content>

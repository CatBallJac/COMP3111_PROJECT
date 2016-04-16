<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="ManageCurrencyConversion.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.ManageCurrencyConversion" Theme="ExternalSystemsTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <h2>Manage Currency Conversion</h2>
    <hr />
    <div class="form-horizontal">
        <div class="form-group">
            <div></div>
            <div class="col-sm-4">
                <asp:GridView ID="gvCurrencyConversion" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="currency" DataSourceID="CurrencyConversionSqlDataSource1" CssClass="table table-bordered table-condensed" 
                    OnRowDataBound="gvCurrencyConversion_RowDataBound">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="currency" HeaderText="Currency" ReadOnly="True" SortExpression="currency" />
                        <asp:BoundField DataField="rate" HeaderText="Rate" >
                        <ControlStyle Width="55px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-5">
                <div>
                    <asp:Button ID="btnNewCurrency" runat="server" Text="New Currency" CssClass="btn btn-primary btn-sm" OnClick="btnNewCurrency_Click" />
                    <asp:Label ID="lblInsertMessage" runat="server" Font-Names="Arial"></asp:Label>
                </div>
                <div>&nbsp;</div>
                <div>
                    <asp:DetailsView ID="dvCurrencyConversion" runat="server" AutoGenerateRows="False" DataKeyNames="currency" 
                        DataSourceID="CurrencyConversionSqlDataSource2" CssClass="table table-bordered table-condensed" 
                        OnItemInserted="dvCurrencyConversion_ItemInserted" OnModeChanging="dvCurrencyConversion_ModeChanging" 
                        Visible="False" OnItemInserting="dvCurrencyConversion_ItemInserting">
                        <Fields>
                            <asp:BoundField DataField="currency" HeaderText="Currency" ReadOnly="True" SortExpression="currency" />
                            <asp:BoundField DataField="rate" HeaderText="Rate" SortExpression="rate" />
                            <asp:CommandField ShowInsertButton="True" />
                        </Fields>
                    </asp:DetailsView>
                </div>
            </div>
            <div class="col-sm-3"></div>
        </div>
    </div>
    <asp:SqlDataSource ID="CurrencyConversionSqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ExternalDatabasesConnectionString %>" 
        DeleteCommand="DELETE FROM [CurrencyRate] WHERE [currency] = @currency" 
        SelectCommand="SELECT * FROM [CurrencyRate]" 
        UpdateCommand="UPDATE [CurrencyRate] SET [rate] = @rate WHERE [currency] = @currency">
        <DeleteParameters>
            <asp:Parameter Name="currency" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="rate" Type="Decimal" />
            <asp:Parameter Name="currency" Type="String" />
        </UpdateParameters>
</asp:SqlDataSource>
    <asp:SqlDataSource ID="CurrencyConversionSqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ExternalDatabasesConnectionString %>" 
        InsertCommand="INSERT INTO [CurrencyRate] ([currency], [rate]) VALUES (@currency, @rate)" 
        SelectCommand="SELECT * FROM [CurrencyRate]">
        <InsertParameters>
            <asp:Parameter Name="currency" Type="String" />
            <asp:Parameter Name="rate" Type="Decimal" />
        </InsertParameters>
            </asp:SqlDataSource>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="ManageCurrencies.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.ManageCurrencies" Theme="ExternalSystemsTheme" %>
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
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ValidationGroup="EditCurrencyValidationGroup" />
                        <asp:BoundField DataField="currency" HeaderText="Currency" ReadOnly="True" SortExpression="currency" />
                        <asp:TemplateField HeaderText="Rate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditRate" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="7" Text='<%# Bind("rate") %>' TextMode="Number" ValidationGroup="EditCurrencyValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEditRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A rate is required." ValidationGroup="EditCurrencyValidationGroup"></asp:RequiredFieldValidator>
                                <asp:CompareValidator runat="server" ControlToValidate="txtEditRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The rate value must be greater than 0." Operator="GreaterThan" ValidationGroup="EditCurrencyValidationGroup" ValueToCompare="0"></asp:CompareValidator>
                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEditRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Rate must be a decimal number less than 999.999." ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,3})?$" ValidationGroup="EditCurrencyValidationGroup"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("rate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                            <asp:TemplateField HeaderText="Currency" SortExpression="currency">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("currency") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertCurrency" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="3" Text='<%# Bind("currency") %>' Width="60px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertCurrency" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for currency is required." ValidationGroup="InsertCurrencyValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertCurrency" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Currency must be letters only." ValidationExpression="^[A-Z]+$" ValidationGroup="InsertCurrencyValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("currency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" SortExpression="rate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("rate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertRate" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="7" Text='<%# Bind("rate") %>' TextMode="Number" ValidationGroup="InsertCurrencyValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A rate is required." ValidationGroup="InsertCurrencyValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The rate value must be greater than 0." Operator="GreaterThan" ValidationGroup="InsertCurrencyValidationGroup" ValueToCompare="0"></asp:CompareValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertRate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Rate must be a decimal number less than 999.999." ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,3})?$" ValidationGroup="InsertCurrencyValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("rate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowInsertButton="True" ValidationGroup="InsertCurrencyValidationGroup" />
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

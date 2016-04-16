<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="ManageStock.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.ManageStock" Theme="ExternalSystemsTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Manage Stock</h2>
    <hr />
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" Text="Code:" AssociatedControlID="txtStockCode" CssClass="control-label col-sm-1"></asp:Label>
            <div class="col-sm-1">
                <asp:TextBox ID="txtStockCode" runat="server" CssClass="form-control input-sm" Height="25px" Width="55px" Wrap="False"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" Font-Bold="True" Height="25px" Width="70px" />
            </div>
            <asp:Label ID="lblSearchResultMessage" runat="server" CssClass="label label-warning" Visible="False"></asp:Label>
        </div>
        <div class="form-group">
            <div class="col-sm-7">
                <asp:GridView ID="gvStock" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="code" CssClass="table table-bordered table-condensed" PageSize="15"
                    OnRowCancelingEdit="gvStock_RowCancelingEdit" OnRowEditing="gvStock_RowEditing" OnRowUpdating="gvStock_RowUpdating"
                    OnPageIndexChanging="gvStock_PageIndexChanging" OnSorting="gvStock_Sorting" OnRowDeleting="gvStock_RowDeleting"
                    OnRowDataBound="gvStock_RowDataBound">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" >
                        <ItemStyle HorizontalAlign="Center" Width="80px" Wrap="False" />
                        </asp:CommandField>
                        <asp:BoundField DataField="code" HeaderText="Code" ReadOnly="True" SortExpression="code">
                        <ItemStyle HorizontalAlign="Center" Width="40px" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="Name" ReadOnly="True" SortExpression="name"></asp:BoundField>
                        <asp:TemplateField HeaderText="Close">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtClose" runat="server" Text='<%# Bind("close") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblClose" runat="server" Text='<%# Bind("close") %>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="55px" />
                            <ItemStyle Width="55px" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="20" />
                </asp:GridView>
            </div>
            <div class="col-sm-5">
                <div>
                    <asp:Button ID="btnAddNewStock" runat="server" Text="New Stock" OnClick="btnAddNewStock_Click" CssClass="btn btn-primary btn-sm"
                        Font-Bold="True" Height="25px" Width="90px" />
                    <asp:Label ID="lblInsertMessage" runat="server"></asp:Label>
                </div>
                <div>&nbsp;</div>
                <div>
                    <asp:DetailsView ID="dvStock" runat="server" AutoGenerateRows="False" CssClass="table table-bordered table-condensed"
                        DataKeyNames="code" DataSourceID="ExternalDatabasesSqlDataSource" Visible="False"
                        OnItemInserting="dvStock_ItemInserting" OnItemInserted="dvStock_ItemInserted" OnItemCommand="dvStock_ItemCommand">
                        <Fields>
                            <asp:TemplateField HeaderText="Code" SortExpression="code">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("code") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertCode" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="4" Text='<%# Bind("code") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="55px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Code is required." ForeColor="Red" ValidationGroup="InsertStockValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Code must be a number." ForeColor="Red" Operator="DataTypeCheck" Type="Integer" ValidationGroup="InsertStockValidationGroup"></asp:CompareValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="name">
                                <EditItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertName" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="80" Text='<%# Bind("name") %>' ValidationGroup="InsertStockValidationGroup" Width="200px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Name is required." ForeColor="Red" ValidationGroup="InsertStockValidationGroup"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Close" SortExpression="close">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("close") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertClose" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("close") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertClose" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Close is required." ForeColor="Red" ValidationGroup="InsertStockValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertClose" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Close must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("close") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Change Percent" SortExpression="changePercent">
                                <EditItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("changePercent") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertChangePercent" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("changePercent") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertChangePercent" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Change Percent is required." ForeColor="Red" ValidationGroup="InsertStockValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtInsertChangePercent" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Change Percent must be a decimal number less than 99.999." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,2}(\.\d{0,3})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("changePercent") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Change Dollar" SortExpression="changeDollar">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("changeDollar") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtChangeDollar" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("changeDollar") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtChangeDollar" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Change Dollar is required." ForeColor="Red" ValidationGroup="InsertStockValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtChangeDollar" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Change Dollar must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("changeDollar") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Volume" SortExpression="volume">
                                <EditItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("volume") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertVolume" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="9" Text='<%# Bind("volume") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="90px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertVolume" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Volume must be a number with at most 9 digits." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,9}?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("volume") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="High" SortExpression="high">
                                <EditItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("high") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertHigh" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("high") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertHigh" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of High must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("high") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Low" SortExpression="low">
                                <EditItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("low") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertLow" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("low") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertLow" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Low must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("low") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="P/E Ratio" SortExpression="peRatio">
                                <EditItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("peRatio") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertPERatio" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("peRatio") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertPERatio" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of P/E Ratio must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("peRatio") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Yield" SortExpression="yield">
                                <EditItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("yield") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertYield" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("yield") %>' TextMode="Number" ValidationGroup="InsertStockValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertYield" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Yield must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertStockValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("yield") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowInsertButton="True" ValidationGroup="InsertStockValidationGroup" />
                        </Fields>
                    </asp:DetailsView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="ExternalDatabasesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ExternalDatabasesConnectionString %>"
        InsertCommand="INSERT INTO [Stock] ([code], [name], [close], [changePercent], [changeDollar], [volume], [high], [low], [peRatio], [yield]) VALUES (@code, @name, @close, @changePercent, @changeDollar, @volume, @high, @low, @peRatio, @yield)">
        <DeleteParameters>
            <asp:Parameter Name="code" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="close" Type="Decimal" />
            <asp:Parameter Name="changePercent" Type="Decimal" />
            <asp:Parameter Name="changeDollar" Type="Decimal" />
            <asp:Parameter Name="volume" Type="Decimal" />
            <asp:Parameter Name="high" Type="Decimal" />
            <asp:Parameter Name="low" Type="Decimal" />
            <asp:Parameter Name="peRatio" Type="Decimal" />
            <asp:Parameter Name="yield" Type="Decimal" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="gvStock" Name="code" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="close" Type="Decimal" />
            <asp:Parameter Name="changePercent" Type="Decimal" />
            <asp:Parameter Name="changeDollar" Type="Decimal" />
            <asp:Parameter Name="volume" Type="Decimal" />
            <asp:Parameter Name="high" Type="Decimal" />
            <asp:Parameter Name="low" Type="Decimal" />
            <asp:Parameter Name="peRatio" Type="Decimal" />
            <asp:Parameter Name="yield" Type="Decimal" />
            <asp:Parameter Name="code" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

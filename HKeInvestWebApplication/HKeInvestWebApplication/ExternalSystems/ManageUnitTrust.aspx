<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="ManageUnitTrust.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.ManageUnitTrust" Theme="ExternalSystemsTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Manage Unit Trust</h2>
    <hr />
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" Text="Code:" AssociatedControlID="txtUnitTrustCode" CssClass="control-label col-sm-1"></asp:Label>
            <div class="col-sm-1">
                <asp:TextBox ID="txtUnitTrustCode" runat="server" CssClass="form-control input-sm" Height="25px" Width="55px" Wrap="False"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" Font-Bold="True" Height="25px" Width="70px" />
            </div>
            <asp:Label ID="lblSearchResultMessage" runat="server" CssClass="label label-warning" Visible="False"></asp:Label>
        </div>
        <div class="form-group">
            <div class="col-sm-7">
                <asp:GridView ID="gvUnitTrust" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="table table-bordered table-condensed" DataKeyNames="code" PageSize="15"
                    OnPageIndexChanging="gvUnitTrust_PageIndexChanging" OnRowCancelingEdit="gvUnitTrust_RowCancelingEdit"
                    OnRowDeleting="gvUnitTrust_RowDeleting" OnRowEditing="gvUnitTrust_RowEditing"
                    OnRowUpdating="gvUnitTrust_RowUpdating" OnSorting="gvUnitTrust_Sorting" OnRowDataBound="gvUnitTrust_RowDataBound">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" >
                        <ItemStyle HorizontalAlign="Center" Width="80px" Wrap="False" />
                        </asp:CommandField>
                        <asp:BoundField DataField="code" HeaderText="Code" ReadOnly="True" SortExpression="code" >
                        <ItemStyle HorizontalAlign="Center" Width="40px" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Price">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
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
                    <asp:Button ID="btnNewUnitTrust" runat="server" Text="New Unit Trust" OnClick="btnNewUnitTrust_Click" CssClass="btn btn-primary btn-sm" Height="25px" Width="110px" />
                    <asp:Label ID="lblInsertMessage" runat="server"></asp:Label>
                </div>
                <div>&nbsp;</div>
                <div>
                    <asp:DetailsView ID="dvUnitTrust" runat="server" AutoGenerateRows="False" CssClass="table table-bordered table-condensed"
                        DataKeyNames="code" DataSourceID="ExternalDatabasesSqlDataSource" Visible="False" OnItemInserted="dvUnitTrust_ItemInserted"
                        OnItemInserting="dvUnitTrust_ItemInserting" OnItemCommand="dvUnitTrust_ItemCommand">
                        <Fields>
                            <asp:TemplateField HeaderText="Code" SortExpression="code">
                                <EditItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("code") %>'></asp:Label>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertCode" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="4" Text='<%# Bind("code") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="55px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Code is required." ForeColor="Red" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertCode" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Code must be a number." ForeColor="Red" Operator="DataTypeCheck" Type="Integer" ValidationGroup="InsertUnitTrustValidationGroup"></asp:CompareValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name" SortExpression="name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertName" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="80" Text='<%# Bind("name") %>' ValidationGroup="InsertUnitTrustValidationGroup" Width="200px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertName" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Name is required." ForeColor="Red" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Launch Date" SortExpression="launchDate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("launchDate") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertLaunchDate" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="8" Text='<%# Bind("launchDate") %>' ValidationGroup="InsertUnitTrustValidationGroup" Width="90px" Wrap="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtInsertLaunchDate" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="A value for Launch Date is required." ForeColor="Red" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("launchDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base" SortExpression="base">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("base") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:DropDownList ID="ddlInsertBase" runat="server" CssClass="form-control input-sm" Height="25px" OnLoad="ddlInsertBase_Load" SelectedValue='<%# Bind("base") %>' ValidationGroup="InsertUnitTrustValidationGroup" Width="100px">
                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlInsertBase" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please select a base currency." ForeColor="Red" InitialValue="0" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("base") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size" SortExpression="size">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("size") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertSize" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="12" Text='<%# Bind("size") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="120px" Wrap="False"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertSize" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Size must be greater than 0." ForeColor="Red" Operator="GreaterThan" ValidationGroup="InsertUnitTrustValidationGroup" ValueToCompare="0"></asp:CompareValidator>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertSize" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Size must be numeric." ForeColor="Red" Operator="DataTypeCheck" Type="Integer" ValidationGroup="InsertUnitTrustValidationGroup"></asp:CompareValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("size") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price" SortExpression="price">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertPrice" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="7" Text='<%# Bind("price") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="80px" Wrap="False"></asp:TextBox>
                                    <asp:CompareValidator runat="server" ControlToValidate="txtInsertPrice" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Price must be greater than 0." ForeColor="Red" Operator="GreaterThan" ValidationGroup="InsertUnitTrustValidationGroup" ValueToCompare="0"></asp:CompareValidator>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertPrice" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Price must be decimal number less than 9999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,4}(\.\d{0,2})?$" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Risk/Return" SortExpression="riskReturn">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("riskReturn") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:DropDownList ID="ddlInsertRiskReturn" runat="server" CssClass="form-control input-sm" Height="25px" SelectedValue='<%# Bind("riskReturn") %>' ValidationGroup="InsertUnitTrustValidationGroup" Width="100px">
                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                        <asp:ListItem Value="L">L-Low</asp:ListItem>
                                        <asp:ListItem Value="M/L">M/L-medium/low</asp:ListItem>
                                        <asp:ListItem Value="M">M-medium</asp:ListItem>
                                        <asp:ListItem Value="M/H">M/H-Medium/high</asp:ListItem>
                                        <asp:ListItem Value="H">H-high</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlInsertRiskReturn" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please select a risk/return rating." ForeColor="Red" InitialValue="0" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RequiredFieldValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("riskReturn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="6 Months" SortExpression="sixMonths">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("sixMonths") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsert6Months" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("sixMonths") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsert6Months" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of 6 Months must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("sixMonths") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="1 Year" SortExpression="oneYear">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("oneYear") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsert1Year" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("oneYear") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsert1Year" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of 1 Year must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("oneYear") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3 Years" SortExpression="threeYears">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("threeYears") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsert3Years" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="6" Text='<%# Bind("threeYears") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsert3Years" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of 3 Years must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,3}(\.\d{0,2})?$" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("threeYears") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Since Launch" SortExpression="sinceLaunch">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("sinceLaunch") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="txtInsertSinceLaunch" runat="server" CssClass="form-control input-sm" Height="25px" MaxLength="7" Text='<%# Bind("sinceLaunch") %>' TextMode="Number" ValidationGroup="InsertUnitTrustValidationGroup" Width="70px" Wrap="False"></asp:TextBox>
                                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtInsertSinceLaunch" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" ErrorMessage="The value of Since Launch must be a decimal number less than 999.99." ForeColor="Red" ValidationExpression="^(?!\.?$)\d{0,4}(\.\d{0,2})?$" ValidationGroup="InsertUnitTrustValidationGroup"></asp:RegularExpressionValidator>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("sinceLaunch") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowInsertButton="True" ValidationGroup="InsertUnitTrustValidationGroup" />
                        </Fields>
                    </asp:DetailsView>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="ExternalDatabasesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ExternalDatabasesConnectionString %>"
        InsertCommand="INSERT INTO [UnitTrust] ([code], [name], [launchDate], [base], [size], [price], [riskReturn], [sixMonths], [oneYear], [threeYears], [sinceLaunch]) VALUES (@code, @name, @launchDate, @base, @size, @price, @riskReturn, @sixMonths, @oneYear, @threeYears, @sinceLaunch)">
        <InsertParameters>
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="launchDate" Type="String" />
            <asp:Parameter Name="base" Type="String" />
            <asp:Parameter Name="size" Type="Decimal" />
            <asp:Parameter Name="price" Type="Decimal" />
            <asp:Parameter Name="riskReturn" Type="String" />
            <asp:Parameter Name="sixMonths" Type="Decimal" />
            <asp:Parameter Name="oneYear" Type="Decimal" />
            <asp:Parameter Name="threeYears" Type="Decimal" />
            <asp:Parameter Name="sinceLaunch" Type="Decimal" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

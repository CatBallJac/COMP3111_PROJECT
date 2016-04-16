<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="BondOrder.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.BondOrder" Theme="ExternalSystemsTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Execute Bond Order</h2>
    <h4 class="auto-style1">Buy Orders</h4>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-4">
                <asp:PlaceHolder ID="phBuyMessage" runat="server">
                    <asp:Label ID="lblBuyMessage" runat="server" CssClass="label label-info" Font-Names="Arial Narrow"></asp:Label>
                </asp:PlaceHolder>
            </div>
            <div class="col-sm-8"></div>
        </div>
        <div class="form-group">
            <div class="col-sm-8">
                <asp:GridView ID="gvBondBuyOrder" runat="server" CssClass="table table-bordered table-condensed" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="referenceNumber" OnRowCommand="gvBondBuyOrder_RowCommand">
                    <Columns>
                        <asp:ButtonField CommandName="ExecuteOrder" Text="Execute" ButtonType="Button" HeaderText="Action" >
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="70px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="GetPrice" HeaderText="Current Price" Text="Get Price">
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Buy Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecutedPrice" runat="server" Width="55px" CssClass="form-control input-sm" Height="25px" Wrap="False" MaxLength="6"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" Height="30px" VerticalAlign="Middle" Width="55px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="referenceNumber" HeaderText="Reference#" ReadOnly="True" DataFormatString="{0:00000000}" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="securityCode" HeaderText="Code" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dateSubmitted" HeaderText="Date Submitted" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="Status" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:n2}" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="headerStyle" HorizontalAlign="Center" />
                    <RowStyle CssClass="rowStyle" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <h4 class="auto-style1">Sell Orders</h4>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-4">
                <asp:PlaceHolder ID="phSellMessage" runat="server">
                    <asp:Label ID="lblSellMessage" runat="server" CssClass="label label-info" Font-Names="Arial Narrow"></asp:Label>
                </asp:PlaceHolder>
            </div>
            <div class="col-sm-8"></div>
        </div>
        <div class="form-group">
            <div class="col-sm-8">
                <asp:GridView ID="gvBondSellOrder" runat="server" CssClass="table table-bordered table-condensed" 
                    AutoGenerateColumns="False" DataKeyNames="referenceNumber" AllowSorting="True" OnRowCommand="gvBondSellOrder_RowCommand">
                    <Columns>
                        <asp:ButtonField CommandName="ExecuteOrder" Text="Execute" ButtonType="Button" HeaderText="Action" >
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="70px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="GetPrice" HeaderText="Current Price" Text="Get Price">
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Sell Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecutedPrice" runat="server" Width="55px" CssClass="form-control input-sm" 
                                    Height="25px" Wrap="False" MaxLength="6"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" Height="30px" VerticalAlign="Middle" Width="55px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="referenceNumber" HeaderText="Reference#" ReadOnly="True" DataFormatString="{0:00000000}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="securityCode" HeaderText="Code">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dateSubmitted" HeaderText="Date Submitted">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="status" HeaderText="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="shares" HeaderText="Shares" DataFormatString="{0:n2}" >
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

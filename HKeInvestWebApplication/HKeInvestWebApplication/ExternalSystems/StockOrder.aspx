<%@ Page Title="" Language="C#" MasterPageFile="~/ExternalSystems/ExternalSite.Master" AutoEventWireup="true" CodeBehind="StockOrder.aspx.cs" Inherits="HKeInvestWebApplication.ExternalSystems.StockOrder" Theme="ExternalSystemsTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Execute Stock Order</h2>
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
                <asp:GridView ID="gvStockBuyOrder" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" 
                    DataKeyNames="referenceNumber" OnRowCommand="gvStockBuyOrder_RowCommand" OnRowDataBound="gvStockBuyOrder_RowDataBound">
                    <Columns>
                        <asp:ButtonField CommandName="ExecuteOrder" Text="Execute" ButtonType="Button" HeaderText="Execute">
                            <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="70px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:ButtonField Text="Cancel" ButtonType="Button" HeaderText="Cancel" CommandName="CancelOrder">
                            <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="60px"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="GetPrice" HeaderText="Current Price" Text="Get Price">
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="80px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Buy Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecutePrice" runat="server" Width="55px" CssClass="form-control input-sm" 
                                    Style="text-align: right" Height="25px" Wrap="False" MaxLength="6" CausesValidation="True"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" Height="30px" VerticalAlign="Middle" Width="40px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="$Amount">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecuteAmount" runat="server" CssClass="form-control input-sm" Style="text-align: right" 
                                    Height="25px" MaxLength="13" Width="115px" Wrap="False"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle Height="30px" HorizontalAlign="Right" VerticalAlign="Middle" Width="40px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="status" HeaderText="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="referenceNumber" HeaderText="Ref#" ReadOnly="True" DataFormatString="{0:00000000}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="securityCode" HeaderText="Code">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dateSubmitted" HeaderText="Date Submitted">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stockOrderType" HeaderText="Type" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="expiryDay" HeaderText="Expiry Day" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="allOrNone" HeaderText="All" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="limitPrice" DataFormatString="{0:n2}" HeaderText="Limit Price" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stopPrice" DataFormatString="{0:n2}" HeaderText="Stop Price" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
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
                <asp:GridView ID="gvStockSellOrder" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" 
                    DataKeyNames="referenceNumber" OnRowCommand="gvStockSellOrder_RowCommand" OnRowDataBound="gvStockSellOrder_RowDataBound">
                    <Columns>
                        <asp:ButtonField CommandName="ExecuteOrder" Text="Execute" ButtonType="Button" HeaderText="Execute">
                            <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="70px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="CancelOrder" HeaderText="Cancel" Text="Cancel">
                            <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="60px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="GetPrice" HeaderText="Current Price" Text="Get Price">
                        <ControlStyle CssClass="btn btn-primary btn-sm" Height="30px" Width="80px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Sell Price">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecutePrice" runat="server" Width="55px" CssClass="form-control input-sm" 
                                    Style="text-align: right" Height="25px" Wrap="False" MaxLength="6"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" Height="30px" VerticalAlign="Middle" Width="40px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#Shares">
                            <ItemTemplate>
                                <asp:TextBox ID="txtExecuteShares" runat="server" CssClass="form-control input-sm" Style="text-align: right" 
                                    Height="25px" MaxLength="13" Width="115px" Wrap="False"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="status" HeaderText="Status">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="referenceNumber" HeaderText="Ref#" ReadOnly="True" DataFormatString="{0:00000000}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="securityCode" HeaderText="Code">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dateSubmitted" HeaderText="Date Submitted">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="shares" HeaderText="Shares" DataFormatString="{0:n2}">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stockOrderType" HeaderText="Type" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="expiryDay" HeaderText="Expiry Day" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="allOrNone" HeaderText="All" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="limitPrice" HeaderText="Limit Price" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stopPrice" HeaderText="Stop Price" ReadOnly="True">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

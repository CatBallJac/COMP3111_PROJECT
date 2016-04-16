using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class UnitTrustOrder : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvUnitTrustBuyOrder.Visible = false;
                gvUnitTrustSellOrder.Visible = false;
                getUnitTrustBuyOrders();
                getUnitTrustSellOrders();
            }
        }

        private void getUnitTrustBuyOrders()
        {
            lblBuyMessage.Visible = false;
            // Get the unit trust buy orders.
            string sql = "select [referenceNumber], [securityCode], [dateSubmitted], [status], [amount] from [Order] " +
                "where [buyOrSell]='buy' and [securityType]='unit trust' and [status]='pending' order by referenceNumber, securityCode";
            DataTable dtBuyUnitTrust = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtBuyUnitTrust == null || dtBuyUnitTrust.Rows.Count == 0)
            {
                showMessage("buy", "There are no buy orders.", "info");
                gvUnitTrustBuyOrder.Visible = false;
                return;
            }
            gvUnitTrustBuyOrder.DataSource = dtBuyUnitTrust;
            gvUnitTrustBuyOrder.DataBind();
            gvUnitTrustBuyOrder.Visible = true;
        }

        private void getUnitTrustSellOrders()
        {
            lblSellMessage.Visible = false;
            // Get the bond sell orders.
            string sql = "select [referenceNumber], [securityCode], [dateSubmitted], [status], [shares] from [Order] " +
                "where [buyOrSell]='sell' and [securityType]='unit trust' and [status]='pending' order by referenceNumber, securityCode";
            DataTable dtSellUnitTrust = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtSellUnitTrust == null || dtSellUnitTrust.Rows.Count == 0)
            {
                showMessage("sell", "There are no sell orders.", "info");
                gvUnitTrustSellOrder.Visible = false;
                return;
            }
            gvUnitTrustSellOrder.DataSource = dtSellUnitTrust;
            gvUnitTrustSellOrder.DataBind();
            gvUnitTrustSellOrder.Visible = true;
        }

        protected void gvUnitTrustBuyOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblBuyMessage.Visible = false;
            // Get the index of the row that was clicked, the order reference number and the unit trust code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvUnitTrustBuyOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[3].Text);
            string unitTrustCode = row.Cells[4].Text;

            if (e.CommandName == "ExecuteOrder")
            {
                // Get the buying price and dollar amount to buy and calculate the number of shares to buy.
                decimal buyPrice;
                if (!decimal.TryParse(((TextBox)row.FindControl("txtExecutedPrice")).Text, out buyPrice) || buyPrice <= 0)
                {
                    showMessage("buy", "The buying price is not valid.", "danger");
                        return;
                }
                decimal buyAmount = Convert.ToDecimal(row.Cells[7].Text);               
                decimal buyShares = buyAmount / buyPrice;

                // Create a transaction for the buy order, change the buy order status to completed, update the security price and refresh the buy orders.
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("insert into [Transaction]([referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                    referenceNumber + ", '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "', " + buyShares + ", " + buyPrice + ")", trans);
                myExternalData.setData("update [Order] set [status]='completed' where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.setData("update [UnitTrust] set [price]=" + buyPrice + " where [code]='" + unitTrustCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getUnitTrustBuyOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("unit trust", unitTrustCode).ToString();
                showMessage("buy", "The current price of unit trust " + unitTrustCode + " is " + currentPrice + ".", "info");
            }
        }

        protected void gvUnitTrustSellOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblSellMessage.Visible = false;
            // Get the index of the row that was clicked, the order reference number and the unit trust code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvUnitTrustSellOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[3].Text);
            string unitTrustCode = row.Cells[4].Text;

            if (e.CommandName == "ExecuteOrder")
            {
                // Get the selling price and number of shares to sell.
                decimal sellPrice;
                if (!decimal.TryParse(((TextBox)row.FindControl("txtExecutedPrice")).Text, out sellPrice) || sellPrice <= 0)
                {
                    showMessage("sell", "The selling price is not valid.", "danger");
                    return;
                }
                decimal sellShares = Convert.ToDecimal(row.Cells[7].Text);               

                // Create a transaction for the sell order, change the sell order status to completed, update the security price and refresh the sell orders.
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("insert into [Transaction]([referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                    referenceNumber + ", '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "', " + sellShares + ", " + sellPrice + ")", trans);
                myExternalData.setData("update [Order] set [status]='completed' where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.setData("update [UnitTrust] set [price]=" + sellPrice + " where [code]='" + unitTrustCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getUnitTrustSellOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("unit trust", unitTrustCode).ToString();
                showMessage("sell", "The current price of unit trust " + unitTrustCode + " is " + currentPrice + ".", "info");
            }
        }

        private void showMessage(string orderType, string message, string messageType)
        {
            if (orderType == "buy")
            {
                lblBuyMessage.Text = message;
                lblBuyMessage.CssClass = "label label-" + messageType;
                lblBuyMessage.Visible = true;
            }
            else // Order type is 'sell'.
            {
                lblSellMessage.Text = message;
                lblSellMessage.CssClass = "label label-" + messageType;
                lblSellMessage.Visible = true;
            }
        }
    }
}
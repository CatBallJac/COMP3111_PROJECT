using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class StockOrder : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvStockBuyOrder.Visible = false;
                gvStockSellOrder.Visible = false;
                getStockBuyOrders();
                getStockSellOrders();
            }
        }

        private void getStockBuyOrders()
        {
            lblBuyMessage.Visible = false;
            // Get the stock buy orders.
            string sql = "select [status], [referenceNumber], [securityCode], [dateSubmitted], [amount], [stockOrderType], [expiryDay], [allOrNone], [limitPrice], [stopPrice] from [Order] " +
                "where [buyOrSell]='buy' and [securityType]='stock' and ([status]='pending' or [status]='partial') order by referenceNumber, securityCode";
            DataTable dtBuyStock = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtBuyStock == null || dtBuyStock.Rows.Count == 0)
            {
                showMessage("buy", "There are no buy orders.", "info");
                gvStockBuyOrder.Visible = false;
                return;
            }
            gvStockBuyOrder.DataSource = dtBuyStock;
            gvStockBuyOrder.DataBind();
            gvStockBuyOrder.Visible = true;
        }

        private void getStockSellOrders()
        {
            lblSellMessage.Visible = false;
            // Get the stock sell orders.
            string sql = "select [status], [referenceNumber], [securityCode], [dateSubmitted], [shares], [stockOrderType], [expiryDay], [allOrNone], [limitPrice], [stopPrice] from [Order] " +
                "where [buyOrSell]='sell' and [securityType]='stock' and ([status]='pending' or [status]='partial') order by referenceNumber, securityCode";
            DataTable dtSellStock = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtSellStock == null || dtSellStock.Rows.Count == 0)
            {
                showMessage("sell", "There are no sell orders.", "info");
                gvStockSellOrder.Visible = false;
                return;
            }
            gvStockSellOrder.DataSource = dtSellStock;
            gvStockSellOrder.DataBind();
            gvStockSellOrder.Visible = true;
        }

        protected void gvStockBuyOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblBuyMessage.Visible = false;
            // Get the index of the row that was clicked, the order reference number and the stock code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvStockBuyOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[6].Text.Trim());
            string stockCode = row.Cells[7].Text.Trim();

            if (e.CommandName == "ExecuteOrder")
            {
                // Get required values from the order.
                decimal limitPrice = 0;
                decimal stopPrice = 0;
                string price = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtExecutePrice")).Text.Trim();
                string buyAmount = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtExecuteAmount")).Text.Trim();
                decimal orderAmount = Convert.ToDecimal(row.Cells[9].Text);
                string orderType = row.Cells[10].Text.Trim();
                if (orderType == "limit" || orderType == "stop limit")
                {
                    limitPrice = Convert.ToDecimal(row.Cells[13].Text.Trim());
                }
                if (orderType == "stop" || orderType == "stop limit")
                {
                    stopPrice = Convert.ToDecimal(row.Cells[14].Text.Trim());
                }

                // Check if inputs are valid.
                if (!executionIsValid("buy", orderType, price, buyAmount, orderAmount, limitPrice, stopPrice)) { return; }
                
                // Calculate the number of shares to buy, the number of shares remaining to buy and set the status accordingly.                              
                decimal buyShares = Convert.ToDecimal(buyAmount) / Convert.ToDecimal(price);
                decimal remainingAmount = orderAmount - Convert.ToDecimal(buyAmount);
                string orderStatus = "partial";
                if (remainingAmount == 0)
                {
                    orderStatus = "completed";
                }

                // Create a transaction for the buy order, change the buy order status, update the security price and refresh the buy orders.
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("insert into [Transaction]([referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                    referenceNumber + ", '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "', " + buyShares + ", '" + price + "')", trans);
                myExternalData.setData("update [Order] set [status]='" + orderStatus + "', [amount]=" + remainingAmount + " where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.setData("update [Stock] set [close]='" + price + "' where [code]='" + stockCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getStockBuyOrders();
            }

            if (e.CommandName == "CancelOrder")
            {
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("update [Order] set [status]='cancelled' where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.commitTransaction(trans);
                getStockBuyOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("stock", stockCode).ToString();
                showMessage("buy", "The current price of stock " + stockCode + " is " + currentPrice + ".", "info");
            }
        }

        protected void gvStockBuyOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Disable input of $Amount textbox if order is all or none.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[12].Text.Trim() == "Y")
                {
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtExecuteAmount")).Text = e.Row.Cells[9].Text;
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtExecuteAmount")).ReadOnly = true;
                }
            }
        }

        protected void gvStockSellOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblSellMessage.Visible = false;
            // Get the index of the row that was clicked, the order reference number and the stock code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvStockSellOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[6].Text.Trim());
            string stockCode = row.Cells[7].Text.Trim();

            if (e.CommandName == "ExecuteOrder")
            {
                // Get required values from the order.
                decimal limitPrice = 0;
                decimal stopPrice = 0;
                string price = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtExecutePrice")).Text.Trim();
                string amount = ((System.Web.UI.WebControls.TextBox)row.FindControl("txtExecuteShares")).Text.Trim();
                decimal orderAmount = Convert.ToDecimal(row.Cells[9].Text);
                string orderType = row.Cells[10].Text.Trim();
                if (orderType == "limit" || orderType == "stop limit")
                {
                    limitPrice = Convert.ToDecimal(row.Cells[13].Text.Trim());
                }
                if (orderType == "stop" || orderType == "stop limit")
                {
                    stopPrice = Convert.ToDecimal(row.Cells[14].Text.Trim());
                }

                // Check if inputs are valid.
                if (!executionIsValid("sell", orderType, price, amount, orderAmount, limitPrice, stopPrice)) { return; }

                // Calculate the remaining shares to sell and set the order status accordingly.
                string orderStatus = "partial";
                decimal remainingShares = orderAmount - Convert.ToDecimal(amount);
                if (remainingShares == 0)
                {
                    orderStatus = "completed";
                }       

                // Create a transaction for the sell order, set the sell order status, update the security price and refresh the sell orders.
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("insert into [Transaction] ([referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                    referenceNumber + ", '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + "', '" + amount + "', '" + price + "')", trans);
                myExternalData.setData("update [Order] set [status]='" + orderStatus + "', [shares]=" + remainingShares + " where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.setData("update [Stock] set [close]='" + price + "' where [code]='" + stockCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getStockSellOrders();
            }

            if (e.CommandName == "CancelOrder")
            {
                SqlTransaction trans = myExternalData.beginTransaction();
                myExternalData.setData("update [Order] set [status]='cancelled' where [referenceNumber]=" + referenceNumber, trans);
                myExternalData.commitTransaction(trans);
                getStockSellOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("stock", stockCode).ToString();
                showMessage("sell", "The current price of stock " + stockCode + " is " + currentPrice + ".", "info");
            }
        }

        protected void gvStockSellOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Disbale input of #Shares textbox if order is all or none.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[12].Text.Trim() == "Y")
                {
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtExecuteShares")).Text = e.Row.Cells[9].Text;
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("txtExecuteShares")).ReadOnly = true;
                }
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

        private bool executionIsValid(string buyOrSell, string orderType, string price, string amount, decimal orderAmount, decimal limitPrice, decimal stopPrice)
        {
            // The input price must be a decimal greater than zero.
            decimal inputPrice;
            if (!decimal.TryParse(price, out inputPrice) || inputPrice <= 0)
            {
                showMessage(buyOrSell, "Invalid or missing price.", "danger");
                return false;
            }

            // The input dollar amount or number of shares must be a decimal greater than zero.
            decimal inputAmount;
            if (!decimal.TryParse(amount, out inputAmount) || inputAmount <= 0)
            {
                if (buyOrSell == "buy")
                {
                   showMessage(buyOrSell, "Invalid or missing dollar amount to buy.", "danger");
                }
                else // Sell order
                {
                    showMessage(buyOrSell, "Invalid or missing number of shares to sell.", "danger");
                }
                return false;
            }

            //The input dollar amount or number of shares must be less than or equal to the order amount or shares.
            if (inputAmount > orderAmount)
            {
                if (buyOrSell == "buy")
                {
                    showMessage(buyOrSell, "The dollar amount to buy is larger than the dollar amount of the order.", "danger");
                }
                else // Sell order
                {
                    showMessage(buyOrSell, "The quantity of shares to sell is larger than the quantity of the order.", "danger");
                }
                return false;
            }

            // The input price must meet the requirements for limit, stop and stop limit orders.
            if (orderType == "limit")
            {
                if (buyOrSell == "buy" && inputPrice > limitPrice)
                {
                    showMessage(buyOrSell, "Buy price must be <= limit price.", "danger");
                    return false;
                }
                if (buyOrSell == "sell" && inputPrice < limitPrice)
                {
                    showMessage(buyOrSell, "Sell price must be >= limit price.", "danger");
                    return false;
                }
            }
            else if (orderType == "stop")
            {
                if (buyOrSell == "buy" && inputPrice < stopPrice)
                {
                    showMessage(buyOrSell, "Buy price must be >= stop price.", "danger");
                    return false;
                }
                if (buyOrSell == "sell" && inputPrice > stopPrice)
                {
                    showMessage(buyOrSell, "Sell price must be <= stop price.", "danger");
                    return false;
                }
            }
            else if (orderType == "stop limit")
            {
                if (buyOrSell == "buy" && (inputPrice < stopPrice || inputPrice > limitPrice))
                {
                    showMessage(buyOrSell, "Buy price must be >= stop \nprice and <= limit price.", "danger");
                    return false;
                }
                if (buyOrSell == "sell" && (inputPrice > stopPrice || inputPrice < limitPrice))
                {
                    showMessage(buyOrSell, "Sell price must be <= to \nstop price and >= limit price.","danger");
                    return false;
                }
            }
            return true;
        }
    }
}
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class BondOrder : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvBondBuyOrder.Visible = false;
                gvBondSellOrder.Visible = false;
                getBondBuyOrders();
                getBondSellOrders();
            }
        }

        private void getBondBuyOrders()
        {
            lblBuyMessage.Visible = false;
            // Get the bond buy orders.
            string sql = "select 0.00 as [currentPrice], [referenceNumber], [securityCode], [dateSubmitted], [status], [amount] from [Order] " +
                "where [buyOrSell]='buy' and [securityType]='bond' and [status]='pending' order by referenceNumber, securityCode";
            DataTable dtBuyBond = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtBuyBond == null || dtBuyBond.Rows.Count == 0)
            {
                showMessage("buy", "There are no buy orders.", "info");
                gvBondBuyOrder.Visible = false;
                return;
            }
            gvBondBuyOrder.DataSource = dtBuyBond;
            gvBondBuyOrder.DataBind();
            gvBondBuyOrder.Visible = true;
        }

        private void getBondSellOrders()
        {
            lblSellMessage.Visible = false;
            // Get the bond sell orders.
            string sql = "select [referenceNumber], [securityCode], [dateSubmitted], [status], [shares] from [Order] " +
                "where [buyOrSell]='sell' and [securityType]='bond' and [status]='pending' order by referenceNumber, securityCode";
            DataTable dtSellBond = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtSellBond == null || dtSellBond.Rows.Count == 0)
            {
                showMessage("sell", "There are no sell orders.", "info");
                gvBondSellOrder.Visible = false;
                return;
            }
            gvBondSellOrder.DataSource = dtSellBond;
            gvBondSellOrder.DataBind();
            gvBondSellOrder.Visible = true;
        }

        protected void gvBondBuyOrder_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            lblBuyMessage.Visible = false;
            // Get the index of the row that was clicked, the order reference number and the bond code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvBondBuyOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[3].Text);
            string bondCode = row.Cells[4].Text;

            if (e.CommandName == "ExecuteOrder")
            {
                // Get the buying price and dollar amount to buy, and calculate the number of shares to buy.
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
                myExternalData.setData("update [Bond] set [price]=" + buyPrice + " where [code]='" + bondCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getBondBuyOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("bond", bondCode).ToString();
                showMessage("buy", "The current price of bond " + bondCode + " is " + currentPrice + ".", "info");
            }
        }

        protected void gvBondSellOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblSellMessage.Visible = false;
            // Get the index of the row that was clicked,the order reference number and the bond code.
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvBondSellOrder.Rows[index];
            int referenceNumber = Convert.ToInt32(row.Cells[3].Text);
            string bondCode = row.Cells[4].Text;

            if (e.CommandName == "ExecuteOrder")
            {
                // Get the selling price and the number of shares to sell.               
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
                myExternalData.setData("update [Bond] set [price]=" + sellPrice + " where [code]='" + bondCode + "'", trans);
                myExternalData.commitTransaction(trans);
                getBondSellOrders();
            }

            if (e.CommandName == "GetPrice")
            {
                string currentPrice = myExternalFunctions.getSecuritiesPrice("bond", bondCode).ToString();
                showMessage("sell", "The current price of bond " + bondCode + " is " + currentPrice +".", "info");
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
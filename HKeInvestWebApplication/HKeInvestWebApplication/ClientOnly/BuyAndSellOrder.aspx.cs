using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using System.Data;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;


namespace HKeInvestWebApplication.ClientOnly
{

    public partial class BuyAndSellOrder : System.Web.UI.Page
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        private static Boolean updated = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlSecurityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = ddlSecurityType.SelectedValue.Trim();
            if(securityType != null)
            labelIsBuyOrSell.Text = "buy/sell " + securityType + ":";
            updateCodeData();
            updateOrderDetail();
        }
        protected void ddlIsBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            // update the code data
            updateCodeData();
            updateOrderDetail();
        }

        protected void updateOrderDetail()
        {
            divStockOrderDetail.Visible = false;
            divBondOrderDetail.Visible = false;
            divSellStockOrder.Visible = false;
            divBuyStockOrder.Visible = false;
            string IsBuyorSell = ddlIsBuyOrSell.SelectedValue.Trim();
            string securityType = ddlSecurityType.SelectedValue.Trim();

            // visual different part based on oeder type
            if (securityType == null || IsBuyorSell == null) return;

            else if (securityType == "bond" || securityType == "unit trust")
                divBondOrderDetail.Visible = true;
            else if (securityType == "stock" && IsBuyorSell == "buy order")
            {
                divStockOrderDetail.Visible = true;
                divBuyStockOrder.Visible = true;
            }else if (securityType == "stock" && IsBuyorSell == "sell order")
            {
                divStockOrderDetail.Visible = true;
                divSellStockOrder.Visible = true;
            }

        }

        protected void updateCodeData()
        {
            // init
            LabelSecurityNametxt.Text = "";
            updated = false;
            ddlCode.Items.Clear();
            // load
            string IsBuyorSell = ddlIsBuyOrSell.SelectedValue.Trim();
            string securityType = ddlSecurityType.SelectedValue.Trim();
            ddlCode.Items.Add("-- select security code to buy/sell --");
            if (IsBuyorSell == null || securityType == null) return;
            if (IsBuyorSell == "buy order")
                updateBuyCodeData(securityType);
            else if(IsBuyorSell == "sell order")
                updateSellCodeData(securityType);

            LabelSellLimit.Visible = false;
        }

        protected void updateBuyCodeData(string securityType)
        {

            // updateBuyCodeData(securityType);
            DataTable dtToBuy = myExternalFunctions.getSecuritiesData(securityType);
            msg.Text = "there are " + dtToBuy.Rows.Count.ToString() + " securities available";
            if (updated == false && dtToBuy != null && dtToBuy.Rows.Count != 0)
            {
                foreach (DataRow row in dtToBuy.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
            updated = true; // after update
        }

        protected void updateSellCodeData(string securityType)
        {
            string accountNumber = getAccountNumber();
            string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}'", accountNumber, securityType);
            DataTable dtToSell = myHKeInvestData.getData(sql);
            msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            // TODO: UPDATE THE CODE DATE BASED ON THE DATEBASE
            // msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            if (updated == false && dtToSell != null && dtToSell.Rows.Count != 0)
            {
                foreach (DataRow row in dtToSell.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
            updated = true; // after update

        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = ddlSecurityType.SelectedValue;
            string securityCode = ddlCode.SelectedValue;
            // 
            if (securityType == "unit trust") securityType = "UnitTrust"; // table name no space
            msg.Text = securityType + securityCode;
            string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");
            LabelSecurityNametxt.Text = securityName;
            updated = true;

            if (ddlIsBuyOrSell.SelectedValue.Trim() == "sell order") showSecuritySharesOwn(securityCode, securityType);
        }

        protected void showSecuritySharesOwn(string securityCode, string securityType)
        {
            string accountNumber = getAccountNumber();
            string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}' and [code] = '{2}'", accountNumber, securityType, securityCode);
            // shares
            DataTable dtShareOwn = myHKeInvestData.getData(sql);
            decimal shares = myHKeInvestData.getData(sql).Rows[0].Field<decimal>("shares");
            msg.Text = "have shared " + shares.ToString();
            LabelSellLimit.Visible = true;
            LabelSellLimit.Text = "total amount to sell should be less than " + shares.ToString();
        }


        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderType = ddlOrderType.SelectedValue.ToString().Trim();
            divLimitPirce.Visible = false;
            divMarketPrice.Visible = false;
            divStopPrice.Visible = false;

            if (orderType == "market order")
            {
                divMarketPrice.Visible = true;
            }
            else if (orderType == "limit order")
            {
                divLimitPirce.Visible = true;
                divMarketPrice.Visible = true;
            }
            else if (orderType == "stop limit order")
            {
                divStopPrice.Visible = true;
                divLimitPirce.Visible = true;
            }
            else if(orderType == "stop order")
            {
                divStopPrice.Visible = true;
            }


        }

        protected string getAccountNumber()
        {
            string userName = Context.User.Identity.Name;
            msg.Text = "welcome " + userName;
            string sql = string.Format("select accountNumber from [AccountTemp] where [userName] = '{0}' ", userName);
            string accountNumber = myHKeInvestData.getData(sql).Rows[0].Field<string>("accountNumber");
           return accountNumber;
        }


    }
}
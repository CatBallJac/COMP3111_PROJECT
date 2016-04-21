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
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        HKeInvestFunctions myHkeInvestFunctions = new HKeInvestFunctions();
        private static Boolean updated = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rbSecurityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = rbSecurityType.SelectedValue.Trim();
            if(securityType != null)
            //labelIsBuyOrSell.Text = "buy/sell " + securityType + ":";
            updateCodeData();
            updateOrderDetail();
        }

        protected void rbIsBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
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
            divBondOrderDetail_sell.Visible = false;
            divBondOrderDetail_buy.Visible = false;
            string IsBuyorSell = rbIsBuyOrSell.SelectedValue.Trim();
            string securityType = rbSecurityType.SelectedValue.Trim();

            // visual different part based on oeder type
            if (securityType == null || IsBuyorSell == null) return;

            else if (securityType == "bond" || securityType == "unit trust")
            {
                divBondOrderDetail.Visible = true;
                if (IsBuyorSell == "buy order")
                {
                    divBondOrderDetail_buy.Visible = true;
                }else
                {
                    divBondOrderDetail_sell.Visible = true;
                }
               
            }
            else if (securityType == "stock" && IsBuyorSell == "buy order")
            {
                divStockOrderDetail.Visible = true;
                divBuyStockOrder.Visible = true;
            } else if (securityType == "stock" && IsBuyorSell == "sell order")
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
            string IsBuyorSell = rbIsBuyOrSell.SelectedValue.Trim();
            string securityType = rbSecurityType.SelectedValue.Trim();
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
            string securityType = rbSecurityType.SelectedValue;
            string securityCode = ddlCode.SelectedValue;
            // 
            // if (securityType == "unit trust") securityType = "UnitTrust"; // table name no space
            msg.Text = securityType + securityCode;
            // string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            // string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");
            DataTable dtSecurities = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            string securityName = "";
            if (dtSecurities != null)
            {
               securityName = dtSecurities.Rows[0].Field<string>("name");
            }else
            {
                securityName = "no security found";
            }


            LabelSecurityNametxt.Text = securityName;
            updated = true;

            if (rbIsBuyOrSell.SelectedValue.Trim() == "sell order") showSecuritySharesOwn(securityCode, securityType);
        }

        protected void showSecuritySharesOwn(string securityCode, string securityType)
        {
            string accountNumber = getAccountNumber();
            string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}' and [code] = '{2}'", accountNumber, securityType, securityCode);
            // shares
            DataTable dtShareOwn = myHKeInvestData.getData(sql);
            if (myHKeInvestData.getData(sql).Rows == null) return;
            decimal shares = myHKeInvestData.getData(sql).Rows[0].Field<decimal>("shares");
            msg.Text = "have shared " + shares.ToString();
            LabelSellLimit.Visible = true;
            LabelSellLimit.Text = "total amount to sell should be less than " + shares.ToString();
        }

        protected void rbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderType = rbOrderType.SelectedValue.ToString().Trim();
            divLimitPirce.Visible = false;
            divMarketPrice.Visible = false;
            // divStopPrice.Visible = false;
            divStopPrice.Visible = false;

            if (orderType == "market")
            {
                divMarketPrice.Visible = true;
            }
            else if (orderType == "limit")
            {
                divLimitPirce.Visible = true;
                divMarketPrice.Visible = true;
            }
            else if (orderType == "stop limit")
            {
                divStopPrice.Visible = true;
                // PanelStopPrice.Enabled = true;

                divLimitPirce.Visible = true;
            }
            else if(orderType == "stop")
            {
                divStopPrice.Visible = true;
                // PanelStopPrice.Enabled = true;
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


        protected void submit_Click(object sender, EventArgs e)
        {
            if(!IsValid)
            {
                return;
            }

            // get all text data:
            string code = ddlCode.Text.Trim();
            string shares_buyStock = TextBuyShares.Text.Trim(); // need to * 100
            string shares_sellStock = TextSellShares.Text.Trim();
            // for stock order
            string orderType = rbOrderType.Text.Trim();
            string expiryDay = ddlExpiryDay.Text.Trim();
            string allOrNone = rbAllOrNone.Text.Trim();

            string stopPrice = TextStopPrice.Text.Trim();
            string marketPrice = TextMarketPrice.Text.Trim();
            string limitPrice = TextLimitPrice.Text.Trim();

            // for stock high price / low price
            // if buy order & stock order & allornone & stop limit || limit 
            string highPrice = limitPrice;
            // if sell order & stock order & allornone & stop limie || limit
            string lowPirce = limitPrice;

            // for bonds:
            string amount_buyBond = TextAmount.Text.Trim();
            string shares_sellBond = TextShares.Text.Trim();
            string isBuyOrSell = rbIsBuyOrSell.Text.Trim(); // buy order; sell order
            string securityType = rbSecurityType.Text.Trim();
            string allmsg = string.Format("0{0}, 1{1},2{2},3{3},4{4},5{5},6{6},7{7},8{8}",
                                             code, shares_buyStock, shares_sellStock, orderType,
                                             expiryDay, allOrNone, stopPrice, marketPrice, limitPrice);
            //System.Diagnostics.Debug.WriteLine(allmsg);
            Response.Write(allmsg);
            msg.Text = securityType + isBuyOrSell + " button click ";
            string referenceNumber = "";
            if (securityType == "stock" && isBuyOrSell == "buy order")
            {
                
                string shares = shares_buyStock;
                referenceNumber = myHkeInvestFunctions.submitStockBuyOrder(code, shares, orderType, expiryDay, allOrNone, highPrice, stopPrice);
            }
            else if(securityType == "stock" && isBuyOrSell == "sell order")
            {
                string shares = shares_sellStock;
                referenceNumber = myHkeInvestFunctions.submitStockSellOrder(code, shares, orderType, expiryDay, allOrNone, highPrice, stopPrice);
            }else if(securityType == "bonds"  && isBuyOrSell == "buy order")
            {
                string amount = amount_buyBond;
                referenceNumber = myHkeInvestFunctions.submitBondBuyOrder(code, amount);
            }else if (securityType == "unit trust" && isBuyOrSell == "buy order")
            {
                string amount = amount_buyBond;
                referenceNumber = myHkeInvestFunctions.submitUnitTrustBuyOrder(code, amount);
            }
            else if (securityType == "bonds" && isBuyOrSell == "sell order")
            {
                string shares = shares_sellBond;
                referenceNumber = myHkeInvestFunctions.submitBondSellOrder(code, shares);
            }
            else if (securityType == "unit trust" && isBuyOrSell == "sell order")
            {
                string shares = shares_sellBond;
                referenceNumber = myHkeInvestFunctions.submitUnitTrustSellOrder(code, shares);
            }
            if (referenceNumber != "" || referenceNumber != null)
            {
                LabelResult.Text = "order submitted, reference number: " + referenceNumber;
            }else
            {
                LabelResult.Text = "order submitted failed";

            }

        }

    }
}
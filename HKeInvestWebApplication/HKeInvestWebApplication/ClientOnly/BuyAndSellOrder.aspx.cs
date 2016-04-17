using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            else if (securityType == "stock" && IsBuyorSell == "Buy Order")
            {
                divStockOrderDetail.Visible = true;
                divBuyStockOrder.Visible = true;
            }else if (securityType == "stock" && IsBuyorSell == "Sell Order")
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
            if (IsBuyorSell == "Buy Order")
                updateBuyCodeData(securityType);
            else if(IsBuyorSell == "Sell Order")
                updateSellCodeData(securityType);
        }
        protected void updateBuyCodeData(string securityType)
        {

            // updateBuyCodeData(securityType);
            DataTable dtIsBuyorSell = myExternalFunctions.getSecuritiesData(securityType);
            msg.Text = "there are " + dtIsBuyorSell.Rows.Count.ToString() + " securities available";
            if (updated == false && dtIsBuyorSell != null && dtIsBuyorSell.Rows.Count != 0)
            {
                foreach (DataRow row in dtIsBuyorSell.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
            updated = true; // after update
        }

        protected void updateSellCodeData(string securityType)
        {
            // TODO: UPDATE THE CODE DATE BASED ON THE DATEBASE
            updated = true; // after update
        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = ddlSecurityType.SelectedValue;
            string securityCode = ddlCode.SelectedValue;
            if (securityType == "unit trust") securityType = "UnitTrust"; // table name no space
            string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");
            LabelSecurityNametxt.Text = securityName;
            updated = true;
        }

        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderType = ddlOrderType.SelectedValue.ToString().Trim();
            divLimitPirce.Visible = false;
            divMarketPrice.Visible = false;
            divStopPrice.Visible = false;

            if (orderType == "market Order")
            {
                divMarketPrice.Visible = true;
            }
            else if (orderType == "limit Order")
            {
                divLimitPirce.Visible = true;
                divMarketPrice.Visible = true;
            }
            else if (orderType == "stop limit Order")
            {
                divStopPrice.Visible = true;
                divLimitPirce.Visible = true;
            }
            else if(orderType == "stop Order")
            {
                divStopPrice.Visible = true;
            }


        }
    }
}
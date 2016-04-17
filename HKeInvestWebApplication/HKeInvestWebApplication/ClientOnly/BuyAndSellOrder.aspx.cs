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

            divOrder.Visible = true;
            labelIsBuyOrSell.Text = "buy/sell " + securityType + ":";
        }

        protected void ddlIsBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IsBuyorSell = ddlIsBuyOrSell.SelectedValue.Trim();
            string securityType = ddlSecurityType.SelectedValue;

            if (IsBuyorSell == "Buy Order")
            {
                divCode.Visible = true;

                DataTable dtIsBuyorSell = myExternalFunctions.getSecuritiesData(securityType);
                if (updated == false && dtIsBuyorSell != null && dtIsBuyorSell.Rows.Count != 0)
                {
                    foreach (DataRow row in dtIsBuyorSell.Rows)
                    {
                        ddlCode.Items.Add(row["code"].ToString().Trim());
                    }
                }
            } else if (IsBuyorSell == "Sell Order")
            {
                // TODO: search security database for the available data
            }

            // show :
            if (securityType == "stock" && IsBuyorSell == "Buy Order")
            {
                buyStockOrderPlaceHolder.Visible = true;
                buyBondOrderPlaceHolder.Visible = false;
            }
            else if (securityType == "bond" && IsBuyorSell == "Buy Order")
            {
                buyBondOrderPlaceHolder.Visible = true;
                buyStockOrderPlaceHolder.Visible = false;
            }
            else if (securityType == "unit trust" && IsBuyorSell == "Buy Order")
            {
                buyBondOrderPlaceHolder.Visible = true;
                buyStockOrderPlaceHolder.Visible = false;
            }


        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = ddlSecurityType.SelectedValue;
            string securityCode = ddlCode.SelectedValue;
            string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");
            TextSecurityName.Text = securityName;
            updated = true;
        }

        
    }
}
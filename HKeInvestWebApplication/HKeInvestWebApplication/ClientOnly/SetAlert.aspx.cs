using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;


namespace HKeInvestWebApplication.ClientOnly
{
    public partial class SetAlert : System.Web.UI.Page
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        HKeInvestFunctions myHkeInvestFunctions = new HKeInvestFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Context.User.Identity.Name;

            welcomeMsg.Text = "welcome " + userName + " account Number: " + getAccountNumber();
        }

        protected string getAccountNumber()
        {
            string userName = Context.User.Identity.Name;
            string sql = string.Format("select accountNumber from [Account] where [userName] = '{0}' ", userName);
            string accountNumber = myHKeInvestData.getData(sql).Rows[0].Field<string>("accountNumber");

            return accountNumber.ToString().Trim();
        }

        protected void updateSellCodeData(string securityType)
        {
            ddlCode.Items.Clear();
            ListItem i = new ListItem("-- choose code of available security --", "0", true);
            ddlCode.Items.Add(i);
          
           string accountNumber = getAccountNumber();
            string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}'", accountNumber, securityType);
            DataTable dtToSell = myHKeInvestData.getData(sql);
            msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            // TODO: UPDATE THE CODE DATE BASED ON THE DATEBASE
            // msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            if (dtToSell != null && dtToSell.Rows.Count != 0)
            {
                foreach (DataRow row in dtToSell.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            string accountNumber = getAccountNumber();

            LabelResult.Text = "";

            // string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            // string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");

            if (!securityCode.Equals("0") && securityCode != null)
            {
                msg.Text = securityType + " " + securityCode + " selected";

                alertInfo.Text = securityType + " " + securityCode + " selected";
                setAlert.Visible = true;

                DataTable dtAlerts = new DataTable();
                dtAlerts = myHKeInvestData.getData("select [lowAlertValue],[highAlertValue] from [SecurityHolding] where [accountNumber] = '" + accountNumber +
                    "' and [type] = '" + securityType + "' and [code] = '" + securityCode + "'");

                if (dtAlerts == null || dtAlerts.Rows.Count == 0)
                {
                    return;
                }

                string low = dtAlerts.Rows[0]["lowAlertValue"].ToString().Trim();
                string high = dtAlerts.Rows[0]["highAlertValue"].ToString().Trim();


                if (String.IsNullOrEmpty(low))
                {
                    alertInfo.Text += "<br />" + "You have not set low alert value";
                    divCancelLow.Visible = false;
                }
                else
                {
                    alertInfo.Text += "<br />" + "The low alert value is: " + low;
                    divCancelLow.Visible = true;
                }

                if (String.IsNullOrEmpty(high))
                {
                    alertInfo.Text += "<br />" + "You have not set high alert value";
                    divCancelHigh.Visible = false;
                }
                else
                {
                    alertInfo.Text += "<br />" + "The high alert value is: " + high;
                    divCancelHigh.Visible = true;
                }
            }
            else
            {
                msg.Text = "Choose code of available security";
                setAlert.Visible = false;

            }

            DataTable dtSecurities = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            string securityName = "";
            if (dtSecurities != null)
            {
                securityName = dtSecurities.Rows[0].Field<string>("name");
            }
            else
            {
                securityName = "no security found for code " + securityCode;
            }

            //LabelSecurityNametxt.Text = securityName;
            //if (rbIsBuyOrSell.SelectedValue.Trim() == "sell order")
            //{ showSecuritySharesOwn(); }
        }

        protected void AlertValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lowalert.Checked && string.IsNullOrEmpty(lowvalue.Text.Trim()))
            {
                args.IsValid = false;
                AlertValidator.ErrorMessage = "Please fill in the low alert value";
                return;
            }
            if (highalert.Checked && string.IsNullOrEmpty(highvalue.Text.Trim()))
            {
                args.IsValid = false;
                AlertValidator.ErrorMessage = "Please fill in the high alert value";
                return;
            }
            decimal lowalertMoney = decimal.Zero;
            if (lowalert.Checked)
            {
                if (decimal.TryParse(lowvalue.Text.Trim(), out lowalertMoney))
                {
                    string[] ca = lowvalue.Text.Trim().Split('.');
                    if (ca.Length == 2 && ca[1].Length > 2)
                    {
                        args.IsValid = false;
                        AlertValidator.ErrorMessage = "The decimal points of the low alert value should be less than 2";
                        return;
                    }
                    if (lowalertMoney.CompareTo(decimal.Zero) <= 0)
                    {
                        args.IsValid = false;
                        AlertValidator.ErrorMessage = "The low alert value cannot be negative";
                        return;
                    }
                }
                else
                {
                    args.IsValid = false;
                    AlertValidator.ErrorMessage = "The low alert value is an invalid number";
                    return;
                }
            }
            decimal highalertMoney = decimal.Zero;
            if (highalert.Checked)
            {
                if (decimal.TryParse(highvalue.Text.Trim(), out highalertMoney))
                {
                    string[] ca = highvalue.Text.Trim().Split('.');
                    if (ca.Length == 2 && ca[1].Length > 2)
                    {
                        args.IsValid = false;
                        AlertValidator.ErrorMessage = "The decimal points of the high alert value should be less than 2";
                        return;
                    }
                    if (highalertMoney.CompareTo(decimal.Zero) <= 0)
                    {
                        args.IsValid = false;
                        AlertValidator.ErrorMessage = "The high alert value cannot be negative";
                        return;
                    }
                }
                else
                {
                    args.IsValid = false;
                    AlertValidator.ErrorMessage = "The high alert value is an invalid number";
                    return;
                }
            }
        }


        protected void rbSecurityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securitytype = rbSecurityType.SelectedValue.Trim();
            updateSellCodeData(securitytype);

        }

        protected void cvCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlCode.SelectedValue.ToString().Trim() == "0" || ddlCode.SelectedValue == null)
            {
                cvCode.ErrorMessage = "please select code";
                msg.Text = "please select code";
                args.IsValid = false;
            }

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            string accountNumber = getAccountNumber();
            string low = lowvalue.Text.Trim();
            string high = highvalue.Text.Trim();

            bool ifLow = lowalert.Checked;
            bool ifHigh = highalert.Checked;

            LabelResult.Text = "Result:";

            
            if (ifLow)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                myHKeInvestData.setData("update [SecurityHolding] set [lowAlertValue]='" + low + "' , [lowLastTriggered] = NULL where [accountNumber]='" + accountNumber + "' and [type] = '" + securityType + "' and [code] = '" + securityCode + "'", trans);
                myHKeInvestData.commitTransaction(trans);
                LabelResult.Text += "<br />" +  "Update low alert value to " + low;
                LabelResult.Text += "<br />" + "Update latest trigger date of low alert value to NULL";

            }

            if (ifHigh)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                myHKeInvestData.setData("update [SecurityHolding] set [highAlertValue] ='" + high + "' , [highLastTriggered] = NULL where [accountNumber]='" + accountNumber + "' and [type] = '" + securityType + "' and [code] = '" + securityCode + "'", trans);
                myHKeInvestData.commitTransaction(trans);
                LabelResult.Text += "<br />" + "Update high alert value to " + high;
                LabelResult.Text += "<br />" + "Update latest trigger date of high alert value to NULL";
            }
           

        }

        protected void cancelLow_Click(object sender, EventArgs e)
        {
            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            string accountNumber = getAccountNumber();

            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [SecurityHolding] set [lowAlertValue]= NULL , [lowLastTriggered] = NULL where [accountNumber]='" + accountNumber + "' and [type] = '" + securityType + "' and [code] = '" + securityCode + "'", trans);
            myHKeInvestData.commitTransaction(trans);
            LabelResult.Text = "<br />" + "Cancel low value alert";
        }

        protected void cancelhigh_Click(object sender, EventArgs e)
        {
            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            string accountNumber = getAccountNumber();

            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [SecurityHolding] set [highAlertValue]= NULL , [highLastTriggered] = NULL where [accountNumber]='" + accountNumber + "' and [type] = '" + securityType + "' and [code] = '" + securityCode + "'", trans);
            myHKeInvestData.commitTransaction(trans);
            LabelResult.Text = "<br />" + "Cancel high value alert";
        }
    }
}
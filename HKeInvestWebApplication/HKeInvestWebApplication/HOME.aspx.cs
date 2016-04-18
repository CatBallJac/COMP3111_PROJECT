using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;
using Microsoft.AspNet.Identity;

namespace HKeInvestWebApplication
{
    public partial class HOME : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        private static Boolean updated = false;
        protected void SaveInSession()
        {

            if (Session.Count < 5)
            {
                DataTable dtCurrency = myExternalFunctions.getCurrencyData();
                foreach (DataRow row in dtCurrency.Rows)
                {
                    Session[row["currency"].ToString().Trim()] = row["rate"].ToString().Trim();
                }
            }

            if (updated == false)
            {
                for (int iCounter = 0; iCounter <= (Session.Count - 1); iCounter++)
                {
                    ddlCurrency.Items.Add(Session.Keys[iCounter]);
                }
                updated = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SaveInSession();

        }

        protected void CheckBoxName_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxName.Checked == true)
            {
                RequiredName.Enabled = true;
                RequiredCode.Enabled = false;

                CheckBoxCode.Checked = false;
            }

        }

        protected void CheckBoxCode_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxCode.Checked == true)
            {
                RequiredCode.Enabled = true;
                RequiredName.Enabled = false;
                CheckBoxName.Checked = false;
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (IsValid == false) { return; }
            string securityType = ddlType.SelectedItem.ToString().Trim();
            string securityName = "-1";
            string securityCode = "-1";
            if (CheckBoxName.Checked == true)
            {
                securityName = TextBoxName.Text.ToString().Trim();

            }
            else if (CheckBoxCode.Checked == true)
            {
                securityCode = TextBoxCode.Text.ToString().Trim();
                int count = 0;
                foreach (Char c in securityCode.ToCharArray())
                {
                    if (c == '0')
                    {
                        count++;
                    }
                    else break;
                }
                securityCode = securityCode.Substring(count, securityCode.Length - count);

            }
            else
            {
                string sql;
                sql = "select * from [" + securityType + "]";
                DataTable dtType = myExternalData.getData(sql);
                if (dtType == null)
                {
                    return;
                }

                if (dtType.Rows.Count == 0)
                {
                    return;
                }
                if (securityType == "Stock")
                {
                    ViewState["SortExpression"] = "code";
                    ViewState["SortDirection"] = "ASC";
                    gvStock.DataSource = dtType;
                    gvStock.DataBind();

                    gvStock.Visible = true;
                    ddlCurrency.Visible = true;
                }
                else if(securityType=="Bond")
                {
                    ViewState["SortExpression"] = "code";
                    ViewState["SortDirection"] = "ASC";
                    gvBond.DataSource = dtType;
                    gvBond.DataBind();

                    gvBond.Visible = true;
                    ddlCurrency.Visible = true;

                }
                else
                {
                    ViewState["SortExpression"] = "code";
                    ViewState["SortDirection"] = "ASC";
                    gvUnitTrust.DataSource = dtType;
                    gvUnitTrust.DataBind();

                    gvBond.Visible = true;
                    ddlCurrency.Visible = true;
                }
            }
        }

        protected void gvStock_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvStock_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtType = myHKeInvestCode.unloadGridView(gvStock);
            string sortExpression = e.SortExpression.ToLower();
            ViewState["SortExpression"] = sortExpression;
            dtType.DefaultView.Sort = sortExpression + " " + myHKeInvestCode.getSortDirection(ViewState, e.SortExpression);
            dtType.AcceptChanges();
            gvStock.DataSource = dtType.DefaultView;
            gvStock.DataBind();
        }
    }
}
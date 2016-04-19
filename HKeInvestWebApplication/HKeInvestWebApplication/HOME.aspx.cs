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

            DataTable dtType = null;
            if (IsValid == false)
            {
                dtType = null;
                gvStock.DataSource = dtType;
                gvStock.DataBind();

                gvStock.Visible = true;
                ddlCurrency.Visible = true;
                return;
            }
            UnloadGV();
            string securityType = ddlType.SelectedItem.ToString().Trim();
            string securityName = "-1";
            string securityCode = "-1";
            
            if (CheckBoxName.Checked == true)
            {
                securityName = TextBoxName.Text.ToString().Trim();
                string newName = securityName;
                int count = 0;
                securityName = securityName.Replace("'", "''");
                string sql;
                
                if (securityType == "Stock")
                {
                    sql = "select * from[" + securityType + "] where [name] like '%" + securityName + "%' and [volume] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvStock.DataSource = dtType;
                    gvStock.DataBind();

                    gvStock.Visible = true;
                    ddlCurrency.Visible = true;

                }
                else if (securityType == "Bond")
                {
                    sql = "select * from[" + securityType + "] where [name] like '%" + securityName + "%' and [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvBond.DataSource = dtType;
                    gvBond.DataBind();

                    gvBond.Visible = true;
                    ddlCurrency.Visible = true;
                }
                else
                {
                    sql = "select * from[" + securityType + "] where [name] like '%" + securityName + "%' and [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvUnitTrust.DataSource = dtType;
                    gvUnitTrust.DataBind();

                    gvUnitTrust.Visible = true;
                    ddlCurrency.Visible = true;
                }
                
                
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
                string sql;
                
                if (securityType == "Stock")
                {
                    sql = "select * from [" + securityType + "] where [code] = '" + securityCode + "' and [volume] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvStock.DataSource = dtType;
                    gvStock.DataBind();

                    gvStock.Visible = true;
                    ddlCurrency.Visible = true;

                }
                else if (securityType == "Bond")
                {
                    sql = "select * from [" + securityType + "] where [code] = '" + securityCode + "' and [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvBond.DataSource = dtType;
                    gvBond.DataBind();

                    gvBond.Visible = true;
                    ddlCurrency.Visible = true;
                }
                else
                {
                    sql = "select * from [" + securityType + "] where [code] = '" + securityCode + "' and [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvUnitTrust.DataSource = dtType;
                    gvUnitTrust.DataBind();

                    gvUnitTrust.Visible = true;
                    ddlCurrency.Visible = true;
                }


            }
            else
            {
                string sql;
                //sql = "select * from [" + securityType + "] where ";
                

                if (securityType == "Stock")
                {
                    sql = "select * from [" + securityType + "] where [volume] IS NOT NULL";
                    
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvStock.DataSource = dtType;
                    gvStock.DataBind();

                    gvStock.Visible = true;
                    ddlCurrency.Visible = true;

                }
                else if (securityType == "Bond")
                {
                    sql = "select * from [" + securityType + "] where [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvBond.DataSource = dtType;
                    gvBond.DataBind();

                    gvBond.Visible = true;
                    ddlCurrency.Visible = true;
                }
                else
                {
                    sql = "select * from [" + securityType + "] where [size] IS NOT NULL ";
                    dtType = myExternalData.getData(sql);
                    if (dtType == null)
                    {
                        return;
                    }

                    if (dtType.Rows.Count == 0)
                    {
                        return;
                    }
                    ViewState["SortExpression"] = "name";
                    ViewState["SortDirection"] = "ASC";
                    gvUnitTrust.DataSource = dtType;
                    gvUnitTrust.DataBind();

                    gvUnitTrust.Visible = true;
                    ddlCurrency.Visible = true;
                }
            }
            
        }
        protected void UnloadGV()
        {
            gvStock.Visible = false;
            gvBond.Visible = false;
            gvUnitTrust.Visible = false;
        }

        protected void display(string type, string sql, DataTable dtType)
        {
            if (type == "Stock")
            {
                string sql2 = "select * from [" + type + "] where [volume] IS NOT NULL ";
                dtType = myExternalData.getData(sql2);

                if (dtType == null)
                {
                    return;
                }

                if (dtType.Rows.Count == 0)
                {
                    return;
                }
                ViewState["SortExpression"] = "name";
                ViewState["SortDirection"] = "ASC";
                gvStock.DataSource = dtType;
                gvStock.DataBind();

                gvStock.Visible = true;
                ddlCurrency.Visible = true;
            }
            if (type == "Bond")
            {
                sql += "[size] != NULL";
                dtType = myExternalData.getData(sql);
                if (dtType == null)
                {
                    return;
                }

                if (dtType.Rows.Count == 0)
                {
                    return;
                }
                ViewState["SortExpression"] = "name";
                ViewState["SortDirection"] = "ASC";
                gvBond.DataSource = dtType;
                gvBond.DataBind();

                gvBond.Visible = true;
                ddlCurrency.Visible = true;
            }
            if (type == "UnitTrust")
            {
                sql += "[size] != NULL ";
                dtType = myExternalData.getData(sql);
                if (dtType == null)
                {
                    return;
                }

                if (dtType.Rows.Count == 0)
                {
                    return;
                }
                ViewState["SortExpression"] = "name";
                ViewState["SortDirection"] = "ASC";
                gvUnitTrust.DataSource = dtType;
                gvUnitTrust.DataBind();

                gvBond.Visible = true;
                ddlCurrency.Visible = true;
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

        protected void CustomCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
            string securityCode = TextBoxCode.Text.ToString().Trim();
            int count = 0;
            foreach (Char c in securityCode.ToCharArray())
            {
                if (c == '0')
                {
                    count++;
                }
                else break;
            }
            int length = securityCode.Length - count;
            if (length > 4)
            {
                args.IsValid = false;

            }
            else args.IsValid = true;
            
        }
    }
}
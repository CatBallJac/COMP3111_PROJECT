using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class ManageBond : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the available currencies and save them in ViewState.
                ViewState["Currencies"] = myExternalFunctions.getCurrencies();

                // Set insert as the default mode for the DetailsView.
                dvBond.DefaultMode = DetailsViewMode.Insert;
                getBond();
            }
        }

        private void getBond()
        {
            // Hide the search result message.
            lblSearchResultMessage.Visible = false;

            // Get the bond code and check if it is an integer.
            string bondCode = txtBondCode.Text.Trim();
            int n;
            if (!int.TryParse(bondCode, out n) && bondCode != "")
            {
                lblSearchResultMessage.Text = "Invalid bond code.";
                lblSearchResultMessage.Visible = true;
                return;
            }
            // Construct and execute the SQL statement.
            string sql = "select cast([code] as int) as [code], [name], [price] from [Bond]";
            if (bondCode != "")
            {
                sql = sql + "where ([code]=" + bondCode + ")";
            }
            sql = sql + " order by [code]";
            DataTable dtBond = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtBond == null || dtBond.Rows.Count == 0)
            {
                lblSearchResultMessage.Text = "No such bond.";
                lblSearchResultMessage.Visible = true;
                gvBond.Visible = false;
            }
            // Otherwise, if a sort direction and sort expression are set in ViewState,
            // then first sort the result and then set and bind the GridView.
            else
            {
                string sortDirection = null, sortExpression = null;
                DataView dvBond = new DataView(dtBond);
                if (ViewState["SortDirection"] != null)
                {
                    sortDirection = ViewState["SortDirection"].ToString();
                }

                if (ViewState["SortExpression"] != null)
                {
                    sortExpression = ViewState["SortExpression"].ToString();
                    dvBond.Sort = string.Concat(sortExpression, " ", sortDirection);
                }
                gvBond.DataSource = dvBond.ToTable();
                gvBond.DataBind();
                gvBond.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            getBond();
        }

        protected void btnNewBond_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            dvBond.Visible = true;
        }

        protected void gvBond_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBond.PageIndex = e.NewPageIndex;
            getBond();
        }

        protected void gvBond_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvBond.EditIndex = -1;
            getBond();
        }

        protected void gvBond_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var updateButton = (LinkButton)e.Row.Cells[0].Controls[0];
            if (updateButton.Text == "Update")
            {
                updateButton.OnClientClick = "return confirm('Do you really want to update this bond?');";
            }
            var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
            if (deleteButton.Text == "Delete")
            {
                deleteButton.OnClientClick = "return confirm('Do you really want to delete this bond?');";
            }
        }

        protected void gvBond_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvBond.Rows[e.RowIndex];
            string code = (gvBond.DataKeys[e.RowIndex].Value.ToString());
            string sql = "delete [Bond] where [code]='" + code + "'";
            //SqlTransaction trans = myExternalData.beginTransaction();
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageBond.aspx");
        }

        protected void gvBond_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvBond.EditIndex = e.NewEditIndex;
            getBond();
        }

        protected void gvBond_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvBond.Rows[e.RowIndex];
            TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
            string code = (gvBond.DataKeys[e.RowIndex].Value.ToString());
            decimal price = Decimal.Parse(txtPrice.Text);
            // Upate the price of the bond.
            string sql = "update [Bond] set [price]='" + price + "' where [code]='" + code + "'";
            // SqlTransaction trans = myExternalData.beginTransaction();
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageBond.aspx");
        }


        protected void gvBond_Sorting(object sender, GridViewSortEventArgs e)
        {
            // If the GridView is sorted for the first time or sorting is being done on a new column, 
            // then set the sort direction to "ASC" in ViewState and the page index to 0.
            if (ViewState["SortDirection"] == null || ViewState["SortExpression"].ToString() != e.SortExpression)
            {
                ViewState["SortDirection"] = "ASC";
                gvBond.PageIndex = 0;
            }
            // Othewise if the same column is clicked for sorting more than once, then toggle its SortDirection.
            else if (ViewState["SortDirection"].ToString() == "ASC")
            {
                ViewState["SortDirection"] = "DESC";
            }
            else if (ViewState["SortDirection"].ToString() == "DESC")
            {
                ViewState["SortDirection"] = "ASC";
            }
            // Save the column name currently being sorted in ViewState.
            ViewState["SortExpression"] = e.SortExpression;
            // Rebind the GridView.
            getBond();
        }


        protected void dvBond_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                lblInsertMessage.Visible = false;
                dvBond.DataSource = null;
                dvBond.DataBind();
                dvBond.Visible = false;
            }
        }
        protected void dvBond_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            dvBond.DataSource = null;
            dvBond.DataBind();
            dvBond.Visible = false;
            txtBondCode.Text = "";
            getBond();
        }

        protected void dvBond_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            // Remove leading zeros and check for duplicate bond code.
            string code = Convert.ToInt32(e.Values["code"]).ToString();
            string sql = "select count(*) from [Bond] where [code]='" + code + "'";
            if (Convert.ToInt32(myExternalData.getAggregateValue(sql)) != 0)
            {
                lblInsertMessage.Text = "Duplicate bond code.";
                lblInsertMessage.CssClass = "label label-warning";
                lblInsertMessage.Visible = true;
                e.Cancel = true;
                getBond();
            }
            else
            {
                // Change code value to remove possible leading zeros before inserting.
                e.Values["code"] = code;
                lblInsertMessage.Text = "Bond " + code + " added.";
                lblInsertMessage.CssClass = "label label-success";
                lblInsertMessage.Visible = true;
            }
        }

        protected void ddlInsertBase_Load(object sender, EventArgs e)
        {
            if (dvBond.CurrentMode == DetailsViewMode.Insert)
            {
                // Find the DroDownList in the DetailsView and populate it from ViewState.
                DropDownList baseCurrency = dvBond.FindControl("ddlInsertBase") as DropDownList;
                DataTable dtCurrencies = ViewState["Currencies"] as DataTable;
                // Check if the currencies have already been added to the DropDownList.
                if (baseCurrency.Items.Count == 1)
                {
                    foreach (DataRow row in dtCurrencies.Rows)
                    {
                        baseCurrency.Items.Add(row["currency"].ToString().Trim());
                    }
                }
            }
        }
    }
}
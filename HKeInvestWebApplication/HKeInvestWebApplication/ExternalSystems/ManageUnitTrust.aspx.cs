using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class ManageUnitTrust : System.Web.UI.Page
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
                dvUnitTrust.DefaultMode = DetailsViewMode.Insert;
                getUnitTrust();
            }
        }

        private void getUnitTrust()
        {
            // Hide the search result message.
            lblSearchResultMessage.Visible = false;
           
            // Get the unit trust code and check if it is an integer.
            string unitTrustCode = txtUnitTrustCode.Text.Trim();
            int n;
            if (!int.TryParse(unitTrustCode, out n) && unitTrustCode != "")
            {
                lblSearchResultMessage.Text = "Invalid unit trust code.";
                lblSearchResultMessage.Visible = true;
                return;
            }

            // Construct and execute the SQL statement.
            string sql = "select cast([code] as int) as [code], [name], [price] from [UnitTrust]";
            if (unitTrustCode != "")
            {
                sql = sql + "where ([code]=" + unitTrustCode + ")";
            }
            sql = sql + " order by [code]";
            DataTable dtUnitTrust = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtUnitTrust == null || dtUnitTrust.Rows.Count == 0)
            {
                lblSearchResultMessage.Text = "No such Unit Trust.";
                lblSearchResultMessage.Visible = true;
                gvUnitTrust.Visible = false;
            }
            // Otherwise, if a sort direction and sort expression are set in ViewState,
            // then first sort the result and then set and bind the GridView.
            else
            {
                string sortDirection = null, sortExpression = null;
                DataView dvUnitTrust = new DataView(dtUnitTrust);
                if (ViewState["SortDirection"] != null)
                {
                    sortDirection = ViewState["SortDirection"].ToString();
                }

                if (ViewState["SortExpression"] != null)
                {
                    sortExpression = ViewState["SortExpression"].ToString();
                    dvUnitTrust.Sort = string.Concat(sortExpression, " ", sortDirection);
                }
                gvUnitTrust.DataSource = dvUnitTrust.ToTable();
                gvUnitTrust.DataBind();
                gvUnitTrust.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            getUnitTrust();
        }

        protected void btnNewUnitTrust_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            dvUnitTrust.Visible = true;
        }

        protected void gvUnitTrust_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUnitTrust.PageIndex = e.NewPageIndex;
            getUnitTrust();
        }

        protected void gvUnitTrust_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvUnitTrust.EditIndex = -1;
            getUnitTrust();
        }

        protected void gvUnitTrust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var updateButton = (LinkButton)e.Row.Cells[0].Controls[0];
            if (updateButton.Text == "Update")
            {
                updateButton.OnClientClick = "return confirm('Do you really want to update this unit trust?');";
            }
            var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
            if (deleteButton.Text == "Delete")
            {
                deleteButton.OnClientClick = "return confirm('Do you really want to delete this unit trust?');";
            }
        }

        protected void gvUnitTrust_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvUnitTrust.Rows[e.RowIndex];
            string code = (gvUnitTrust.DataKeys[e.RowIndex].Value.ToString());
            string sql = "delete [UnitTrust] where [code]='" + code + "'";
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageUnitTrust.aspx");
        }

        protected void gvUnitTrust_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUnitTrust.EditIndex = e.NewEditIndex;
            getUnitTrust();
        }

        protected void gvUnitTrust_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUnitTrust.Rows[e.RowIndex];
            TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
            string code = (gvUnitTrust.DataKeys[e.RowIndex].Value.ToString());
            decimal price = Decimal.Parse(txtPrice.Text);
            // Upate the price of the unit trust.
            string sql = "update [UnitTrust] set [price]='" + price + "' where [code]='" + code + "'";
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageUnitTrust.aspx");
        }

        protected void gvUnitTrust_Sorting(object sender, GridViewSortEventArgs e)
        {
            // If the GridView is sorted for the first time or sorting is being done on a new column, 
            // then set the sort direction to "ASC" in ViewState and the page index to 0.
            if (ViewState["SortDirection"] == null || ViewState["SortExpression"].ToString() != e.SortExpression)
            {
                ViewState["SortDirection"] = "ASC";
                gvUnitTrust.PageIndex = 0;
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
            getUnitTrust();
        }

        protected void dvUnitTrust_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                lblInsertMessage.Visible = false;
                dvUnitTrust.DataSource = null;
                dvUnitTrust.DataBind();
                dvUnitTrust.Visible = false;
            }
        }

        protected void dvUnitTrust_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            dvUnitTrust.DataSource = null;
            dvUnitTrust.DataBind();
            dvUnitTrust.Visible = false;
            txtUnitTrustCode.Text = "";
            getUnitTrust();
        }

        protected void dvUnitTrust_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            // Remove leading zeros and check for duplicate bond code.
            string code = Convert.ToInt32(e.Values["code"]).ToString();
            string sql = "select count(*) from [UnitTrust] where [code]='" + code + "'";
            if (Convert.ToInt32(myExternalData.getAggregateValue(sql)) != 0)
            {
                lblInsertMessage.Text = "Duplicate Unit Trust code.";
                lblInsertMessage.CssClass = "label label-warning";
                lblInsertMessage.Visible = true;
                e.Cancel = true;
                getUnitTrust();
            }
            else
            {
                // Change code value to remove possible leading zeros before inserting.
                e.Values["code"] = code;
                lblInsertMessage.Text = "Unit Trust " + code + " added.";
                lblInsertMessage.CssClass = "label label-success";
                lblInsertMessage.Visible = true;
            }
        }

        protected void ddlInsertBase_Load(object sender, EventArgs e)
        {
            if (dvUnitTrust.CurrentMode == DetailsViewMode.Insert)
            {
                // Find the DroDownList in the DetailsView and populate it from ViewState.
                DropDownList baseCurrency = dvUnitTrust.FindControl("ddlInsertBase") as DropDownList;
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
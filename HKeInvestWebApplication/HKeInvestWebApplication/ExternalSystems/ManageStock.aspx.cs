using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class ManageStock : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dvStock.DefaultMode = DetailsViewMode.Insert;
                getStock();
            }
        }

        private void getStock()
        {
            // Hide the search result message.
            lblSearchResultMessage.Visible = false;

            // Get the stock code and check if it is an integer.
            string stockCode = txtStockCode.Text.Trim();
            int n;
            if (!int.TryParse(stockCode, out n) && stockCode != "")
            {
                lblSearchResultMessage.Text = "Invalid stock code.";
                lblSearchResultMessage.Visible = true;
                return;
            }

            // Construct and execute the SQL statement.
            string sql = "select cast([code] as int) as [code], [name], [close] from [Stock]";
            if (stockCode != "")
            {
                sql = sql + "where ([code]=" + stockCode + ")";
            }
            sql = sql + " order by [code]";
            DataTable dtStock = new DataTable();
            dtStock = myExternalData.getData(sql);

            // If no result is returned, then display a message.
            if (dtStock == null || dtStock.Rows.Count == 0)
            {
                lblSearchResultMessage.Text = "No such stock.";
                lblSearchResultMessage.Visible = true;
                gvStock.Visible = false;
            }
            // Otherwise, if a sort direction and sort expression are set in ViewState,
            // then first sort the result and then set and bind the GridView.
            else
            {
                string sortDirection = null, sortExpression = null;
                DataView dvStock = new DataView(dtStock);
                if (ViewState["SortDirection"] != null)
                {
                    sortDirection = ViewState["SortDirection"].ToString();
                }

                if (ViewState["SortExpression"] != null)
                {
                    sortExpression = ViewState["SortExpression"].ToString();
                    dvStock.Sort = string.Concat(sortExpression, " ", sortDirection);
                }
                gvStock.DataSource = dvStock.ToTable(); ;
                gvStock.DataBind();
                gvStock.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            getStock();
        }

        protected void btnAddNewStock_Click(object sender, EventArgs e)
        {
            lblInsertMessage.Visible = false;
            dvStock.Visible = true;
        }

        protected void gvStock_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvStock.EditIndex = -1;
            getStock();
        }

        protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                if (e.Row.RowType != DataControlRowType.DataRow) return;
                var updateButton = (LinkButton)e.Row.Cells[0].Controls[0];
                if (updateButton.Text == "Update")
                {
                    updateButton.OnClientClick = "return confirm('Do you really want to update this stock?');";
                }
                var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
                if (deleteButton.Text == "Delete")
                {
                    deleteButton.OnClientClick = "return confirm('Do you really want to delete this stock?');";
                }
        }

        protected void gvStock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStock.EditIndex = e.NewEditIndex;
            getStock();
        }

        protected void gvStock_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvStock.Rows[e.RowIndex];
            TextBox txtClose = (TextBox)row.FindControl("txtClose");
            string code = (gvStock.DataKeys[e.RowIndex].Value.ToString());
            decimal close = Decimal.Parse(txtClose.Text);
            // Upate the close value of the stock.
            string sql = "update [Stock] set [close]='" + close + "' where [code]='" + code + "'";
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageStock.aspx");
        }

        protected void gvStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStock.PageIndex = e.NewPageIndex;
            getStock();
        }

        protected void gvStock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvStock.Rows[e.RowIndex];
            string code = (gvStock.DataKeys[e.RowIndex].Value.ToString());
            string sql = "delete [Stock] where [code]='" + code + "'";
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            Response.Redirect("ManageStock.aspx");
        }

        protected void gvStock_Sorting(object sender, GridViewSortEventArgs e)
        {
            // If the GridView is sorted for the first time or sorting is being done on a new column, 
            // then set the sort direction to "ASC" in ViewState and the page index to 0.
            if (ViewState["SortDirection"] == null || ViewState["SortExpression"].ToString() != e.SortExpression)
            {
                ViewState["SortDirection"] = "ASC";
                gvStock.PageIndex = 0;
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
            getStock();
        }

        protected void dvStock_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Cancel")
            {
                lblInsertMessage.Visible = false;
                dvStock.DataSource = null;
                dvStock.DataBind();
                dvStock.Visible = false;
            }
        }

        protected void dvStock_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            dvStock.DataSource = null;
            dvStock.DataBind();
            dvStock.Visible = false;
            txtStockCode.Text = "";
            getStock();
        }

        protected void dvStock_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            // Remove leading zeros and check for duplicate bond code.
            string code = Convert.ToInt32(e.Values["code"]).ToString();
            string sql = "select count(*) from [Stock] where [code]='" + code + "'";
            if (Convert.ToInt32(myExternalData.getAggregateValue(sql)) != 0)
            {
                lblInsertMessage.Text = "Duplicate stock code.";
                lblInsertMessage.CssClass = "label label-warning";
                lblInsertMessage.Visible = true;
                e.Cancel = true;
                getStock();
            }
            else
            {
                // Change code value to remove possible leading zeros before inserting.
                e.Values["code"] = code;
                lblInsertMessage.Text = "Stock " + code + " added.";
                lblInsertMessage.CssClass = "label label-success";
                lblInsertMessage.Visible = true;
            }
        }
    }
}
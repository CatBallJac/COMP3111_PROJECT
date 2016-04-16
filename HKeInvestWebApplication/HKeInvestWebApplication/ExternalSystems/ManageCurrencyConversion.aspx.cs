using System;
using System.Web.UI.WebControls;
using HKeInvestWebApplication.ExternalSystems.Code_File;

namespace HKeInvestWebApplication.ExternalSystems
{
    public partial class ManageCurrencyConversion : System.Web.UI.Page
    {
        ExternalData myExternalData = new ExternalData();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNewCurrency_Click(object sender, EventArgs e)
        {
            dvCurrencyConversion.Visible = true;
            dvCurrencyConversion.DefaultMode = DetailsViewMode.Insert;
        }

        protected void gvCurrencyConversion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var updateButton = (LinkButton)e.Row.Cells[0].Controls[0];
            if (updateButton.Text == "Update")
            {
                updateButton.OnClientClick = "return confirm('Do you really want to update this currency?');";
            }
            var deleteButton = (LinkButton)e.Row.Cells[0].Controls[2];
            if (deleteButton.Text == "Delete")
            {
                deleteButton.OnClientClick = "return confirm('Do you really want to delete this currency?');";
            }
        }

        protected void dvCurrencyConversion_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            gvCurrencyConversion.DataBind();
            dvCurrencyConversion.DataBind();
            dvCurrencyConversion.Visible = false;
        }

        protected void dvCurrencyConversion_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            // Check for duplicate currency.
            string currency = e.Values["currency"].ToString().Trim();
            string sql = "select count(*) from [CurrencyRate] where [currency]='" + currency + "'";
            if (Convert.ToInt32(myExternalData.getAggregateValue(sql)) != 0)
            {
                lblInsertMessage.Text = "Duplicate currency.";
                lblInsertMessage.CssClass = "label label-warning";
                lblInsertMessage.Visible = true;
                e.Cancel = true;
            }
            else
            {
                lblInsertMessage.Text = "Currency " + currency + " added.";
                lblInsertMessage.CssClass = "label label-success";
                lblInsertMessage.Visible = true;
            }
        }

        protected void dvCurrencyConversion_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.CancelingEdit == true)
            {
                dvCurrencyConversion.Visible = false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data;

namespace HKeInvestWebApplication.ClientOnly
{
    public partial class ClientTransferReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                showAccountNumber();
                showTransferHistory();
            }
        }

        protected void showAccountNumber()
        {
            HKeInvestWebApplication.Code_File.HKeInvestData myData = new Code_File.HKeInvestData();
            string user_name = Context.User.Identity.GetUserName().Trim();
            string sql = "SELECT accountNumber FROM Account WHERE userName = '" + user_name.ToLower() + "'";
            DataTable account = myData.getData(sql);
            if (account == null || account.Rows.Count == 0) return;
            yourAccount.Text = account.Rows[0].Field<string>("accountNumber").Trim();
            yourAccount.Enabled = false;
        }

        protected void showTransferHistory()
        {
            HKeInvestWebApplication.Code_File.HKeInvestData myData = new Code_File.HKeInvestData();
            string sql = "SELECT * FROM Transfer WHERE fromAccountNumber = '"+yourAccount.Text.Trim()+"'";
            DataTable transfer = myData.getData(sql);
            if (transfer == null || transfer.Rows.Count == 0) return;
            DataTable converted = new DataTable();
            converted.Columns.Add("toAccountNumber");
            converted.Columns.Add("amount");
            converted.Columns.Add("time");
            foreach(DataRow row in transfer.Rows)
            {
                DateTime time = row.Field<DateTime>("time");
                string mini = row.Field<string>("minisecond");
                time = time.AddMilliseconds(double.Parse(mini));
                converted.Rows.Add(row.Field<string>("toAccountNumber"), row.Field<decimal>("amount").ToString(), time.ToString("dddd, MMMM dd, yyyy hh:mm:ss.fff tt"));
            }
            transferHistory.DataSource = converted;
            transferHistory.DataBind();
        }
    }
}
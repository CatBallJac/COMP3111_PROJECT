using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace HKeInvestWebApplication.ClientOnly
{
    public partial class Transfer : System.Web.UI.Page
    {
        HKeInvestWebApplication.Code_File.HKeInvestData myData = new Code_File.HKeInvestData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string user_name = Context.User.Identity.GetUserName().Trim();
                string sql = "SELECT accountNumber FROM Account WHERE userName = '" + user_name.ToLower() + "'";
                DataTable account = myData.getData(sql);
                if (account == null || account.Rows.Count == 0) return;
                yourAccount.Text = account.Rows[0].Field<string>("accountNumber").Trim();
                yourAccount.Enabled = false;
            }
        }

        protected void transfer_btn_Click(object sender, EventArgs e)
        {
            result.Text = "";
            if (!IsValid) return;
            string to_account = toAccountNumber.Text.Trim().ToUpper();
            string amount = transferAmount.Text.Trim();
            SqlTransaction trans = myData.beginTransaction();
            string sql = string.Format(
                "UPDATE Account SET balance = balance - {0} WHERE accountNumber = '{1}'"
                , amount, yourAccount.Text.Trim());
            myData.setData(sql, trans);
            sql = string.Format(
                "UPDATE Account SET balance = balance + {0} WHERE accountNumber = '{1}'"
                , amount, to_account);
            myData.setData(sql, trans);
            myData.commitTransaction(trans);
            DateTime time = DateTime.Now;
            result.Text = "HK$ " + amount + " has been transfered from your account("
                + yourAccount.Text.Trim() + ") to account(" + toAccountNumber.Text.Trim() + ")"
                + " in " + time.ToString("dddd, MMMM dd, yyyy hh:mm:ss.fff tt");
            // record transfer
            trans = myData.beginTransaction();
            sql = string.Format(
                "INSERT INTO Transfer VALUES('{0}','{1}','{2}','{3}','{4}')"
                , yourAccount.Text.Trim(), to_account, amount, time.ToString(), time.Millisecond.ToString("D3")
                );
            myData.setData(sql, trans);
            myData.commitTransaction(trans);
        }

        protected void invalidAccountNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!accountNumberFormat.IsValid) return;
            string account_number = toAccountNumber.Text.Trim().ToUpper();
            if (account_number.Equals(yourAccount.Text.Trim().ToUpper()))
            {
                args.IsValid = false;
                invalidAccountNumber.ErrorMessage = "Please do not transfer to yourself...";
                return;
            }
            if (string.IsNullOrEmpty(account_number)) return;
            string sql = "SELECT accountNumber FROM Account WHERE accountNumber = '"+account_number+"'";
            DataTable account = myData.getData(sql);
            if(account==null || account.Rows.Count == 0)
            {
                args.IsValid = false;
                invalidAccountNumber.ErrorMessage = "This account does not exists!";
            }
        }

        protected void transferAmountFormat_ServerValidate(object source, ServerValidateEventArgs args)
        {
            decimal money = decimal.Zero;
            string amount = transferAmount.Text.Trim();
            if(decimal.TryParse(amount, out money))
            {
                string[] ca = amount.Split('.');
                if(ca.Length==2 && ca[1].Length > 2)
                {
                    args.IsValid = false;
                    transferAmountFormat.ErrorMessage = "The decimal points of the amount should be less than or equal to 2";
                }
                if (money.CompareTo(decimal.Zero) < 0)
                {
                    args.IsValid = false;
                    transferAmountFormat.ErrorMessage = "The amount transfered cannot be negative";
                }
            }else
            {
                args.IsValid = false;
                transferAmountFormat.ErrorMessage = "The amount is invalid";
            }
        }

        protected void transferAmountTooMuch_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!accountNumberFormat.IsValid) return;
            decimal amount = decimal.Zero;
            if (decimal.TryParse(transferAmount.Text.Trim(), out amount))
            {
                string account_number = yourAccount.Text.Trim();
                string sql = "SELECT balance FROM Account WHERE accountNumber = '" + account_number + "'";
                DataTable balance = myData.getData(sql);
                if (balance == null || balance.Rows.Count == 0) return;
                else
                {
                    decimal money = balance.Rows[0].Field<decimal>("balance");
                    if (amount.CompareTo(money) > 0)
                    {
                        args.IsValid = false;
                        transferAmountTooMuch.ErrorMessage = "The amount of the money transfered is too much";
                        return;
                    }
                }
                string to_account = toAccountNumber.Text.Trim();
                string to_sql = "SELECT balance FROM Account WHERE accountNumber = '" + to_account + "'";
                DataTable to_balance = myData.getData(to_sql);
                if (to_balance == null || to_balance.Rows.Count == 0) return;
                else
                {
                    decimal to_money = to_balance.Rows[0].Field<decimal>("balance");
                    to_money = to_money + amount;
                    if (to_money.ToString("F2").Length > 13)
                    {
                        args.IsValid = false;
                        transferAmountTooMuch.ErrorMessage = "The amount of the money transfered is too much";
                    }
                }
            }
        }
    }
}
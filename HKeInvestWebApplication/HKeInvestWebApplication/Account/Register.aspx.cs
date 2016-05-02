using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using HKeInvestWebApplication.Models;
using HKeInvestWebApplication.Code_File;
using System.Data.SqlClient;
using System.Data;

namespace HKeInvestWebApplication.Account
{
    public partial class Register : Page
    {
        SQLStringHandleHelper mySQLStringHandleHelper = new SQLStringHandleHelper();
        HKeInvestEmail emailManager = new HKeInvestEmail();
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            if(!InfoMatch()){
                return;
            }
            if (alreadyHolding()) {
                ErrorMessage.Text = "This security account already holds a login account!";
                return;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = UserName.Text.Trim().ToLower(), Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                emailManager.sendEmail(user.Email, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                // associate username with accountnumber
                HKeInvestData data_helper = new HKeInvestData();
                System.Data.SqlClient.SqlTransaction trans = data_helper.beginTransaction();
                string sql = "UPDATE [Account] SET [Account].[userName] = '" + UserName.Text.Trim().ToLower() + "' WHERE [Account].[accountNumber] = '" + AccountNumber.Text.Trim() + "'";
                data_helper.setData(sql, trans);
                data_helper.commitTransaction(trans);

                // assign user to Client
                IdentityResult roleResult = manager.AddToRole(user.Id, "Client");
                if (!roleResult.Succeeded)
                {
                    ErrorMessage.Text = roleResult.Errors.FirstOrDefault();
                }
                System.Security.Claims.Claim claim = new System.Security.Claims.Claim("role", "Client");
                manager.AddClaim(user.Id, claim);

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private bool alreadyHolding()
        {
            string sql = "SELECT userName FROM Account WHERE accountNumber = '"+AccountNumber.Text.Trim()+"'";
            HKeInvestData data_helper = new HKeInvestData();
            DataTable account_info = data_helper.getData(sql);
            if (account_info == null || account_info.Rows.Count == 0) return false;
            else
            {
                DataRow row = account_info.Rows[0];
                return !row.IsNull("userName");
            }
        }
        private bool InfoMatch()
        {
            // get information entered
            string first_name = mySQLStringHandleHelper.handleString(FirstName.Text.Trim());
            string last_name = mySQLStringHandleHelper.handleString(LastName.Text.Trim());
            string account = AccountNumber.Text.Trim().ToUpper();
            string hkid = HKID.Text.Trim();
            string birth = DateOfBirth.Text.Trim();
            string email = Email.Text.Trim();

            // get Client information in the table
            string sql = "SELECT * from [Client] WHERE [Client].[accountNumber] = '" + account + "' AND [Client].[isPrimary]='Yes'";
            HKeInvestData data_helper = new HKeInvestData();
            DataTable client_info = data_helper.getData(sql);
            if (client_info == null || client_info.Rows.Count == 0)
            {
                ErrorMessage.Text = "This client does not exist!";
                return false;
            }
            else
            {
                DataRow row = client_info.Rows[0];
                /*
                if (!first_name.Equals(row.Field<string>("firstName").Trim())) {
                    ErrorMessage.Text = "a";
                }
                if (!last_name.Equals(row.Field<string>("lastName").Trim()))
                {
                    ErrorMessage.Text = "b";
                }
                if (!account.Equals(row.Field<string>("accountNumber").Trim()))
                {
                    ErrorMessage.Text = "c";
                }
                if (!hkid.Equals(row.Field<string>("HKIDPassportNumber").Trim()))
                {
                    ErrorMessage.Text = "d";
                }
                if (!birth.Equals(row.Field<DateTime>("dateOfBirth").ToString("dd/MM/yyyy").Trim()))
                {
                    ErrorMessage.Text = "e";
                }
                if (!email.Equals(row.Field<string>("email").Trim()))
                {
                    ErrorMessage.Text = "f";
                }
                */
                if(!first_name.Equals(row.Field<string>("firstName").Trim()) ||
                    !last_name.Equals(row.Field<string>("lastName").Trim()) ||
                    !account.Equals(row.Field<string>("accountNumber").Trim()) ||
                    !hkid.Equals(row.Field<string>("HKIDPassportNumber").Trim()) ||
                    !birth.Equals(row.Field<DateTime>("dateOfBirth").ToString("dd/MM/yyyy").Trim()) ||
                    !email.Equals(row.Field<string>("email").Trim()))
                {
                    //ErrorMessage.Text = "The information entered does not match!";
                    return false;
                }
            }

            return true;
        }

        protected void cvAccountNumber_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string accountNumber = AccountNumber.Text.Trim();
            // check if the account number match with the client's last name
            string lastName = LastName.Text.Trim();
            if (string.IsNullOrEmpty(accountNumber) || string.IsNullOrEmpty(lastName)) return;
            string temp = "";
            for(int i = 0; i < lastName.Length; ++i)
            {
                if (temp.Length == 2) break;
                if (char.IsLetter(lastName[i]))
                {
                    temp += lastName[i];
                }
            }
            if (temp.Length == 1) temp += temp;
            temp = temp.ToUpper();
            if (accountNumber.Substring(0, 2).ToUpper() != temp)
            {
                args.IsValid = false;
                cvAccountNumber.ErrorMessage = "The account number does not match last name";
            }
        }

        protected void userNameValidate_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string sql = "SELECT DISTINCT userName from Account WHERE userName is not null";
            HKeInvestData data = new HKeInvestData();
            DataTable name = data.getData(sql);
            if (name == null || name.Rows.Count == 0) return;
            else
            {
                foreach(DataRow row in name.Rows)
                {
                    if (UserName.Text.Trim() == row.Field<string>("userName").Trim())
                    {
                        args.IsValid = false;
                        break;
                    }
                }
            }
        }
    }
}
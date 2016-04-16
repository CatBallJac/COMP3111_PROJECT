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
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            bool isClientExists = CheckClientRecord(FirstName.Text.Trim(), LastName.Text.Trim(), DateOfBirth.Text.Trim(), Email.Text.Trim(),
    HKID.Text.Trim(), AccountNumber.Text.Trim());
            if (!isClientExists)
            {
                ErrorMessage.Text = "No client exists.";
                return;
            }
            var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text };
            
            
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                IdentityResult roleResult = manager.AddToRole(user.Id, "Client"); 
                if (!roleResult.Succeeded)
                {
                    ErrorMessage.Text = roleResult.Errors.FirstOrDefault();
                }
                UpdateAccountUserName(AccountNumber.Text.Trim(), UserName.Text.Trim());
                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        private bool CheckClientRecord(string firstName, string lastName, string dateOfBirth, string email, string HKID, string accountNumber)
        {
            string isPrimary = "yes";
            string sql = "select [firstName], [lastName], [dateOfBirth], [email], [HKIDPassportNumber] from [ClientTemp] where [accountNumber]='" + accountNumber + "' and [firstName]='"+ firstName+"' and [lastName]='"+lastName+ "' and [dateOfBirth]=CONVERT(date, '" + DateOfBirth.Text+ "', 103) and [email]='"+email+"' and [HKIDPassportNumber]='"+HKID+"' and [isPrimary]='"+isPrimary+"'";
            HKeInvestData myInvestData = new HKeInvestData();
            DataTable dtClient = myInvestData.getData(sql);
            if(dtClient == null || dtClient.Rows.Count==0) { return false; }
            else
            {

                return true;
            }
            
        }

        private void UpdateAccountUserName(string accountNumber, string userName)
        {
            HKeInvestData myInvestData = new HKeInvestData();
            string sql = "update [AccountTemp] set [userName]='" + userName + "' where [accountNumber]='" + accountNumber + "'";
            SqlTransaction trans = myInvestData.beginTransaction();
            myInvestData.setData(sql, trans);
            myInvestData.commitTransaction(trans);
            

        }



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cvAccountNumber_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            string accountNumber = AccountNumber.Text.Trim();
            string lastName = LastName.Text.Trim();
            if (lastName.Equals("")||accountNumber.Equals(""))
            {
                return;
            }
            if (accountNumber.Substring(0, 1).Equals(lastName.Substring(0, 1)))
            {
                if (accountNumber.ToCharArray()[1] == Char.ToUpper(lastName.ToCharArray()[1]))
                {
                    if (accountNumber.Substring(2).All(c => Char.IsDigit(c)) && accountNumber.Length == 10)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;
                        cvAccountNumber.ErrorMessage = "The account number is not in the correct format.";
                    }
                }
                else
                {
                    if (accountNumber.Substring(1).All(c => Char.IsDigit(c)) && accountNumber.Length == 9)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;
                        cvAccountNumber.ErrorMessage = "The account number is not in the correct format.";
                    }

                }

            }
            else
            {
                args.IsValid = false;
                cvAccountNumber.ErrorMessage = "The account number does not match the client's last name.";
            }
        }
    }
}
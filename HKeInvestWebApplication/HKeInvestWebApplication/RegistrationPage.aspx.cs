using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HKeInvestWebApplication
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cvAccountNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {

            string accountNumber = AccountNumber.Text.Trim();
            string lastName = LastName.Text.Trim();
            if (lastName.Equals(""))
            {
                return;
            }
            if (accountNumber.Substring(0, 1).Equals(lastName.Substring(0, 1)))
            {
                if (accountNumber.ToCharArray()[1] == Char.ToUpper(lastName.ToCharArray()[1]))
                {
                    if (accountNumber.Substring(2).All(c => Char.IsDigit(c))&&accountNumber.Length==10)
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
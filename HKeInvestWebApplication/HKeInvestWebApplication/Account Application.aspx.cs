using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HKeInvestWebApplication.Models;
using HKeInvestWebApplication.Code_File;
using System.Data.SqlClient;
using System.Data;

namespace HKeInvestWebApplication
{
    public partial class Account_Application : System.Web.UI.Page
    {
        string ACCOUNTNUMBER = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            if (!IsValid)
            {
                return;
            }

            //Part1: Deal with accountNumber (increment automatically)
            string accountNumber = "";
            //1. get the first 2 letters of the last name, if only one letter: double
            string lastName = LastName.Text.Trim();
            string firstTwoLetterLastName = "";
            if (lastName.Length == 1)
            {
                firstTwoLetterLastName = lastName + lastName;
            }
            else
            {
                firstTwoLetterLastName = lastName.ToCharArray()[0].ToString()+Char.ToUpper(lastName.ToCharArray()[1]).ToString();
            }
            accountNumber += firstTwoLetterLastName;
            //2. get the lastest accountNumber that using the openning as same as these two letters.
            string sqlAccountNumber = "select [accountNumber] from [AccountTemp] where [accountNumber] like'"+firstTwoLetterLastName+"%'";
            HKeInvestData myInvestData = new HKeInvestData();
            DataTable dtAccountNumber = myInvestData.getData(sqlAccountNumber);
            if (dtAccountNumber == null || dtAccountNumber.Rows.Count == 0)
            { //No accountNumber has the opening before
                accountNumber += "00000000";
            }
            else
            {//already has the same opening before
                int i = 1;
                foreach (DataRow row in dtAccountNumber.Rows)
                {
                    if (dtAccountNumber.Rows.Count == i)
                    {
                        string currentAccountNumber = row["accountNumber"].ToString();
                        string NewCurrentAccountNumber = "1" + currentAccountNumber.Substring(2,8);
                        int IntCurrentAccountNumber= Int32.Parse(NewCurrentAccountNumber)+1;
                        accountNumber += IntCurrentAccountNumber.ToString().Substring(1,8);
                    }
                    else
                    {
                        i = i + 1;
                    }
                    

                }
            }
            ACCOUNTNUMBER = accountNumber;
            //chequeText.Text = accountNumber;
            //Part2: deal with AccountTemp table
            string accountType = AccountType.SelectedValue.Trim();
            string depositDescription = DepositDescription.SelectedValue.Trim();
            string otherText = "";
            if (depositDescription == "Other (describe below)")
            {
                otherText = other.Text.Trim();
            }
            string IfSweep = "";
            if (ifSweep.Checked)
            {
                IfSweep = "Yes";
            }
            else
            {
                IfSweep = "No";
            }
            string initialAccountDepositAmount = "";
            string initialAccountDeposit = "";
            if (chequeCheck.Checked)
            {
                initialAccountDeposit = "cheque";
                initialAccountDepositAmount = chequeText.Text.Trim();
                
            }
            else
            {
                initialAccountDeposit = "transfer";
                initialAccountDepositAmount = transferText.Text.Trim();
            }
            string sqlAccountTable = "insert into AccountTemp (accountNumber, accountType, DepositDescription, DepositDescriptionOther, ifSweep, initialAccountDeposit, initialAccountDepositAmount) values ('"+accountNumber+"', '"+accountType+"', '"+depositDescription+"', '"+otherText+"', '"+IfSweep+"', '"+initialAccountDeposit+"', '"+initialAccountDepositAmount+"')";
            SqlTransaction trans = myInvestData.beginTransaction();
            myInvestData.setData(sqlAccountTable, trans);
            myInvestData.commitTransaction(trans);

            //Part2: ClientTemp table
            string firstName = FirstName.Text.Trim();
            //lastName has defined already.
            string dateOfBirth = DateOfBirth.Text.Trim(); //need to do a conversion from dd/mm/yyyy to mm/dd/yyyy using the conversion in sql.
            string email = Email.Text.Trim();
            string HKid = HKID.Text.Trim();
            string Title = title.Text.Trim();
            string BAddress = buildingAddress.Text.Trim();
            string SAddress = streetAddress.Text.Trim();
            string DAddress = districtAddress.Text.Trim();
            string HPhone = homePhone.Text.Trim();
            string HFax = homeFax.Text.Trim();
            string BPhone = businessPhone.Text.Trim();
            string MPhone = mobilePhone.Text.Trim();
            string cCitizenship = countryCitizenship.Text.Trim();
            string cResidence = countryResidence.Text.Trim();
            string cPassport = countryPassport.Text.Trim();
            string isRegistered = isEmployedRegistered.SelectedValue.Trim();
            string isD = isDirector.SelectedValue.Trim();
            string status = employmentStatus.SelectedValue.Trim();
            string Occupation = occupation.Text.Trim();
            string years = year.Text.Trim();
            string EName = employerName.Text.Trim();
            string EPhone = employerPhone.Text.Trim();
            string nature = businessNature.Text.Trim();
            string isPrimary = "yes";
            string sqlClientTemp= "insert into ClientTemp (firstName, lastName, dateOfBirth, email, HKIDPassportNumber, accountNumber, title, buildingAddress, streetAddress, districtAddress, homePhone, homeFax, businessPhone, mobilePhone, countryCitizenship, countryResidence, countryPassport, isEmployedRegistered, isDirector, status, occupation, years, employerName, employerPhone, nature, isPrimary) values ('"+firstName +"', '"+lastName +"', CONVERT(date,'"+DateOfBirth.Text +"',103), '"+email +"', '"+HKid +"', '"+accountNumber +"', '"+Title +"', '"+BAddress +"', '"+SAddress+"', '"+DAddress +"', '"+HPhone +"', '"+HFax +"', '"+BPhone +"', '"+MPhone +"', '"+cCitizenship +"', '"+cResidence +"', '"+cPassport +"', '"+isRegistered +"', '"+isD +"', '"+status +"', '"+Occupation +"', '"+years +"', '"+EName +"', '"+EPhone +"', '"+nature +"', '"+isPrimary+"')";
            SqlTransaction transClient = myInvestData.beginTransaction();
            myInvestData.setData(sqlClientTemp, transClient);
            myInvestData.commitTransaction(transClient);

            //Part3: investmentProfile
            string Object = InvestObject.SelectedValue.Trim();
            string Knowledge = knowledge.SelectedValue.Trim();
            string Experience = experience.SelectedValue.Trim();
            string AnnualIncome = annualIncome.SelectedValue.Trim();
            string NetWorth = netWorth.SelectedValue.Trim();
            string sqlInvest = "insert into investmentProfile (accountNumber, object, knowledge, experience, annualIncome, netWorth) values ('"+accountNumber+"', '"+Object+"', '"+Knowledge+"', '"+Experience+"', '"+AnnualIncome+"', '"+NetWorth+"')";
            SqlTransaction transInvest = myInvestData.beginTransaction();
            myInvestData.setData(sqlInvest, transInvest);
            myInvestData.commitTransaction(transInvest);


      
            //Part2: ClientTemp table
            firstName = FirstNameCoClient.Text.Trim();
            //lastName has defined already.
            dateOfBirth = DateOfBirthCoClient.Text.Trim(); //need to do a conversion from dd/mm/yyyy to mm/dd/yyyy using the conversion in sql.
            email = EmailCoClient.Text.Trim();
            HKid = HKIDCoClient.Text.Trim();
            Title = titleCoClient.Text.Trim();
            BAddress = buildingAddressCoClient.Text.Trim();
            SAddress = streetAddressCoClient.Text.Trim();
            DAddress = districtAddressCoClient.Text.Trim();
            HPhone = homePhoneCoClient.Text.Trim();
            HFax = homeFaxCoClient.Text.Trim();
            BPhone = businessPhoneCoClient.Text.Trim();
            MPhone = mobilePhoneCoClient.Text.Trim();
            cCitizenship = countryCitizenshipCoClient.Text.Trim();
            cResidence = countryResidenceCoClient.Text.Trim();
            cPassport = countryPassportCoClient.Text.Trim();
            isRegistered = isEmployedRegisteredCoClient.SelectedValue.Trim();
            isD = isDirectorCoClient.SelectedValue.Trim();
            status = employmentStatusCoClient.SelectedValue.Trim();
            Occupation = occupationCoClient.Text.Trim();
            years = yearCoClient.Text.Trim();
            EName = employerNameCoClient.Text.Trim();
            EPhone = employerPhoneCoClient.Text.Trim();
            nature = businessNatureCoClient.Text.Trim();
            isPrimary = "no";
            string sqlCoClient = "insert into ClientTemp (firstName, lastName, dateOfBirth, email, HKIDPassportNumber, accountNumber, title, buildingAddress, streetAddress, districtAddress, homePhone, homeFax, businessPhone, mobilePhone, countryCitizenship, countryResidence, countryPassport, isEmployedRegistered, isDirector, status, occupation, years, employerName, employerPhone, nature, isPrimary) values ('" + firstName + "', '" + LastNameCoClient.Text.Trim() + "', CONVERT(date,'" + DateOfBirthCoClient.Text + "',103), '" + email + "', '" + HKid + "', '" + accountNumber + "', '" + Title + "', '" + BAddress + "', '" + SAddress + "', '" + DAddress + "', '" + HPhone + "', '" + HFax + "', '" + BPhone + "', '" + MPhone + "', '" + cCitizenship + "', '" + cResidence + "', '" + cPassport + "', '" + isRegistered + "', '" + isD + "', '" + status + "', '" + Occupation + "', '" + years + "', '" + EName + "', '" + EPhone + "', '" + nature + "', '" + isPrimary + "')";
            SqlTransaction transCoClient = myInvestData.beginTransaction();
            myInvestData.setData(sqlCoClient, transCoClient);
            myInvestData.commitTransaction(transCoClient);
        }



        protected void coclient_Click(object sender, EventArgs e)
        {
            CoClientLabel.Visible = true;
            EmploymentCoClientInfo.Visible = true;
            DisclosureLabel.Visible = true;
            homeAddressCoClientLabel.Visible = true;

            FirstNameCoClient.Visible = true;
            LastNameCoClient.Visible = true;
            DateOfBirthCoClient.Visible = true;
            EmailCoClient.Visible = true;
            HKIDCoClient.Visible = true;
            titleCoClient.Visible = true;
            buildingAddressCoClient.Visible = true;
            streetAddressCoClient.Visible = true;
            districtAddressCoClient.Visible = true;
            homePhoneCoClient.Visible = true;
            homeFaxCoClient.Visible = true;
            businessPhoneCoClient.Visible = true;
            mobilePhoneCoClient.Visible = true;
            countryCitizenshipCoClient.Visible = true;
            countryResidenceCoClient.Visible = true;
            countryPassportCoClient.Visible = true;
            isEmployedRegisteredCoClient.Visible = true;
            isDirectorCoClient.Visible = true;
            employmentStatusCoClient.Visible = true;
            occupationCoClient.Visible = true;
            yearCoClient.Visible = true;
            employerNameCoClient.Visible = true;
            employerPhoneCoClient.Visible = true;
            businessNatureCoClient.Visible = true;

            FirstNameCoClientRequired.Enabled = true;
            LastNameCoClientRequired.Enabled = true;
            DateOfBirthCoClientRequired.Enabled = true;
            EmailCoClientRequired.Enabled = true;
            HKIDCoClientRequired.Enabled = true;
            streetAddressCoClientRequired.Enabled = true;
            districtAddressCoClientRequired.Enabled = true;
            homePhoneCoClientRequired.Enabled = true;
            homeFaxCoClientRequired.Enabled = true;
            businessPhoneCoClientRequired.Enabled = true;
            mobilePhoneCoClientRequired.Enabled = true;
            countryCitizenshipCoClientRequired.Enabled = true;
            countryResidenceCoClientRequired.Enabled = true;
            countryPassportCoClientRequired.Enabled = true;
            isEmployedRegisteredCoClientRequired.Enabled = true;
            isDirectorCoClientRequired.Enabled = true;
            employmentStatusCoClientRequired.Enabled = true;
            occupationCoClientRequired.Enabled = true;
            yearCoClientRequired.Enabled = true;
            employerNameCoClientRequired.Enabled = true;
            employerPhoneCoClientRequired.Enabled = true;
            businessNatureCoClientRequired.Enabled = true;
            titleCoClientRequired.Enabled = true;
            DateOfBirthCoClientExpression.Enabled = true;
            HKIDCoClientExpression.Enabled = true;
            homePhoneCoClientExpression.Enabled = true;
            homeFaxCoClientExpression.Enabled = true;
            businessPhoneCoClientExpression.Enabled = true;
            mobilePhoneCoClientExpression.Enabled = true;
            yearCoClientExpression.Enabled = true;
            employerPhoneCoClientExpression.Enabled = true;

            homePhoneCoClientLabel.Visible = true;
            homeFaxCoClientLabel.Visible = true;
            businessPhoneCoClientLabel.Visible = true;
            mobilePhoneCoClientLabel.Visible = true;
            countryCitizenshipCoClientLabel.Visible = true;
            FirstNameCoClientLabel.Visible = true;
            LastNameCoClientLabel.Visible = true;
            EmailCoClientLabel.Visible = true;
            HKIDCoClientLabel.Visible = true;
            DateOfBirthCoClientLabel.Visible = true;
            titleCoClientLabel.Visible = true;
            streetAddressCoClientLabel.Visible = true;
            districtAddressCoClientLabel.Visible = true;
            countryResidenceCoClientLabel.Visible = true;
            countryPassportCoClientLabel.Visible = true;
            isEmployedRegisteredCoClientLabel.Visible = true;
            isDirectorCoClientLabel.Visible = true;
            employmentStatusCoClientLabel.Visible = true;
            occupationCoClientLabel.Visible = true;
            yearCoClientLabel.Visible = true;
            employerNameCoClientLabel.Visible = true;
            businessNatureCoClientLabel.Visible = true;
            employerPhoneCoClientLabel.Visible = true;

           








        }

        protected void coclient_ServerValidate(object source, ServerValidateEventArgs args)
        {
            
        }

        protected void DepositDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            string depositDescriptionType = DepositDescription.SelectedValue.Trim();
            if(depositDescriptionType=="Other (describe below)")
            {
                other.Visible = true;
                otherValidator.Enabled = true;
            }
            else
            {
                other.Visible = false;
                otherValidator.Enabled= false;
            }
        }

        protected void chequeCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chequeCheck.Checked)
            {
                transferCheck.Checked = false;
                chequeText.Visible = true;
                transferText.Visible = false;
                chequeTextValidatorRequired.Enabled = true;
                chequeTextValidatorExpression.Enabled = true;
                transferTextValidatorRequired.Enabled = false;
                transferTextValidatorExpression.Enabled = false;
            }
        }

        protected void transferCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (transferCheck.Checked)
            {
                chequeCheck.Checked = false;
                transferText.Visible = true;
                chequeText.Visible = false;
                transferTextValidatorRequired.Enabled = true;
                transferTextValidatorExpression.Enabled = true;
                chequeTextValidatorRequired.Enabled = false;
                chequeTextValidatorExpression.Enabled = false;
            }
        }

        protected void employmentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = employmentStatus.SelectedValue.Trim();
            if (status != "Employed")
            {
                //All the following textboxes are disappearing
                occupation.Visible = false;
                year.Visible = false;
                employerName.Visible = false;
                employerPhone.Visible = false;
                businessNature.Visible = false;
                //All related validators should be disabled
                occupationRequired.Enabled = false;
                yearRequired.Enabled = false;
                yearExpression.Enabled = false;
                employerNameRequired.Enabled = false;
                employerPhoneExpression.Enabled = false;
                employerPhoneRequired.Enabled = false;
                businessNatureRequired.Enabled = false;
            }
            else
            {
                occupation.Visible = true;
                year.Visible = true;
                employerName.Visible = true;
                employerPhone.Visible = true;
                businessNature.Visible = true;

                occupationRequired.Enabled = true;
                yearRequired.Enabled = true;
                yearExpression.Enabled = true;
                employerNameRequired.Enabled = true;
                employerPhoneExpression.Enabled = true;
                employerPhoneRequired.Enabled = true;
                businessNatureRequired.Enabled = true;
            }
        }

        protected void employmentStatusCoClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statusCoClient = employmentStatusCoClient.SelectedValue.Trim();
            if (statusCoClient != "Employed")
            {
                //All the following textboxes are disappearing
                occupationCoClient.Visible = false;
                yearCoClient.Visible = false;
                employerNameCoClient.Visible = false;
                employerPhoneCoClient.Visible = false;
                businessNatureCoClient.Visible = false;
                //All related validators should be disabled
                occupationCoClientRequired.Enabled = false;
                yearCoClientRequired.Enabled = false;
                yearCoClientExpression.Enabled = false;
                employerNameCoClientRequired.Enabled = false;
                employerPhoneCoClientExpression.Enabled = false;
                employerPhoneCoClientRequired.Enabled = false;
                businessNatureCoClientRequired.Enabled = false;
            }
            else
            {
                occupationCoClient.Visible = true;
                yearCoClient.Visible = true;
                employerNameCoClient.Visible = true;
                employerPhoneCoClient.Visible = true;
                businessNatureCoClient.Visible = true;

                occupationCoClientRequired.Enabled = true;
                yearCoClientRequired.Enabled = true;
                yearCoClientExpression.Enabled = true;
                employerNameCoClientRequired.Enabled = true;
                employerPhoneCoClientExpression.Enabled = true;
                employerPhoneCoClientRequired.Enabled = true;
                businessNatureCoClientRequired.Enabled = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HKeInvestWebApplication.Code_File;
using System.Data;
using System.Data.SqlClient;

namespace HKeInvestWebApplication.EmployeeOnly
{
    public partial class ManageAccountAndClientInfo : System.Web.UI.Page
    {
        HKeInvestData myData = new HKeInvestData();
        SQLStringHandleHelper myStringHelper = new SQLStringHandleHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void setAllValidator(bool enabled)
        {
            foreach(BaseValidator validator in Page.Validators)
            {
                validator.Enabled = enabled;
            }
        }
        protected void get_btn_Click(object sender, EventArgs e)
        {
            result.Text = "";
            if (!validateAccountNumber()) return;
            setAllValidator(false);
            string account_number = AccountNumber.Text.Trim();
            currentAN.Text = account_number;
            string sql = "SELECT * FROM Account WHERE accountNumber = '"+account_number+"'";
            DataTable account = myData.getData(sql);
            if (account == null || account.Rows.Count == 0)
            {
                ErrorMessage.Text = "No such account exists!";
                infoForm.Visible = false;
                return;
            }
            else
            {
                ErrorMessage.Text = "";
                infoForm.Visible = true;
            }
            // display information for account
            DataRow row = account.Rows[0];
            displayAccountInfo(row);
            // display information for client
            sql = "SELECT * from Client WHERE accountNumber = '" + account_number + "'";
            DataTable client = myData.getData(sql);
            if (client == null || client.Rows.Count == 0) { return; }
            else
            {
                if (client.Rows.Count == 1)
                {
                    hideCoClient();
                    displayPrimaryClient(client.Rows[0]);
                }
                else
                {
                    showCoClient();
                    DataRow p_row = null, c_row = null;
                    foreach (DataRow row0 in client.Rows)
                    {
                        if (row0.Field<string>("isPrimary") == "Yes")
                        {
                            p_row = row0;
                        }
                        else
                        {
                            c_row = row0;
                        }
                        if (p_row != null && c_row != null) break;
                    }
                    displayPrimaryClient(p_row);
                    displayCoClient(c_row);
                }
            }
            setAllValidator(true);
        }
        protected bool validateAccountNumber()
        {
            string a_n = AccountNumber.Text.Trim();
            if (string.IsNullOrEmpty(a_n))
            {
                ErrorMessage.Text = "Account Number is required!";
                return false;
            }
            else
            {
                if (a_n.Length != 10)
                {
                    ErrorMessage.Text = "The format of the account number is invalid!";
                    return false;
                }
                if (!char.IsLetter(a_n[0]) || !char.IsLetter(a_n[1]))
                {
                    ErrorMessage.Text = "The format of the account number is invalid!";
                    return false;
                }
                for (int i = a_n.Length - 1; i > a_n.Length - 9; --i)
                {
                    if (!char.IsDigit(a_n[i]))
                    {
                        ErrorMessage.Text = "The format of the account number is invalid!";
                        return false;
                    }
                }
            }
            return true;
        }
        protected void displayAccountInfo(DataRow row)
        {
            string account_type = row.Field<string>("accountType").Trim();
            accountType.Text = translateAccountType(account_type);

            decimal account_balance = row.Field<decimal>("balance");
            balance.Text = account_balance.ToString();

            if (row.IsNull("userName")) { userName.Text = "No user name"; }
            else { userName.Text = row.Field<string>("userName").Trim(); }

            string p_s = row.Field<string>("primarySource").Trim();
            if (p_s != "salary/wages/savings" &&
                p_s != "investment/capital gains" &&
                p_s != "family/relatives/inheritance")
            {
                primarySource.SelectedValue = "Other";
                other.Text = (p_s);
                other.ReadOnly = false;
                other.BackColor = System.Drawing.Color.White;
            }
            else
            {
                primarySource.SelectedValue = p_s;
                other.ReadOnly = true;
                other.BackColor = System.Drawing.Color.Gray;
            }

            investmentObjective.SelectedValue = row.Field<string>("investmentObjectives").Trim();
            investmentKnowledge.SelectedValue = row.Field<string>("investmentKnowledge").Trim();
            investmentExperience.SelectedValue = row.Field<string>("investmentExperience").Trim();
            annualIncome.SelectedValue = row.Field<string>("annualIncome").Trim();
            liquidNetWorth.SelectedValue = row.Field<string>("approximateLiquidNetWorth").Trim();
            part6.SelectedValue = row.Field<string>("accountFeature").Trim();
        }
        protected void displayPrimaryClient(DataRow row) {
            pTitle.SelectedValue = row.Field<string>("title");
            pLastName.Text = (row.Field<string>("lastName").Trim());
            pFirstName.Text = (row.Field<string>("firstName").Trim());
            DateTime birth = row.Field<DateTime>("dateOfBirth");
            pDateOfBirth.Text = birth.ToString("dd/MM/yyyy");
            pEmail.Text = (row.Field<string>("email").Trim());
            pBuilding.Text = (row.Field<string>("building").Trim());
            pStreet.Text = (row.Field<string>("street").Trim());
            pDistrict.Text = (row.Field<string>("district").Trim());
            pHomePhone.Text = row.Field<string>("homePhone").Trim();
            pHomeFax.Text = row.Field<string>("homeFax").Trim();
            pBusinessPhone.Text = row.Field<string>("businessPhone").Trim();
            pMobilePhone.Text = row.Field<string>("mobilePhone").Trim();
            pCitizenship.Text = (row.Field<string>("countryOfCitizenship").Trim());
            pLegalResidence.Text = (row.Field<string>("countryOfLegalResidence").Trim());
            pHKID.Text = row.Field<string>("HKIDPassportNumber").Trim();
            if (row.Field<string>("isHKIDUsed").Trim() == "Yes")
            {
                pHKIDUsed.Checked = true;
                pPassportCountry.Text = "";
                pHKID.ReadOnly = true;
                pHKIDUsed.Enabled = false;
                pPassportCountry.ReadOnly = true;
            }
            else
            {
                pHKIDUsed.Checked = false;
                pPassportCountry.Text = (row.Field<string>("passportCountryOfIssue").Trim());
            }
            pEmploymentStatus.SelectedValue = row.Field<string>("employmentStatus").Trim();
            if (pEmploymentStatus.SelectedValue == "Employed")
            {
                pOccupation.Text = (row.Field<string>("occupation").Trim());
                pYears.Text = row.Field<decimal>("yearsWithEmployer").ToString();
                pEmployerName.Text = (row.Field<string>("employerName").Trim());
                pEmployerPhone.Text = row.Field<string>("employerPhone").Trim();
                pNatureOfBusiness.Text = (row.Field<string>("natureOfBusiness").Trim());
            }
            else
            {
                pOccupation.ReadOnly = true;
                pYears.ReadOnly = true;
                pEmployerName.ReadOnly = true;
                pEmployerPhone.ReadOnly = true;
                pNatureOfBusiness.ReadOnly = true;
                pOccupation.BackColor = System.Drawing.Color.Gray;
                pYears.BackColor = System.Drawing.Color.Gray;
                pEmployerName.BackColor = System.Drawing.Color.Gray;
                pEmployerPhone.BackColor = System.Drawing.Color.Gray;
                pNatureOfBusiness.BackColor = System.Drawing.Color.Gray;
                pOccupation.Text = "";
                pYears.Text = "";
                pEmployerName.Text = "";
                pEmployerPhone.Text = "";
                pNatureOfBusiness.Text = "";
            }
            part4PrimaryQ1.SelectedValue = row.Field<string>("employedByFinancialInstitution").Trim();
            part4PrimaryQ2.SelectedValue = row.Field<string>("isADirector").Trim();
        }
        protected void displayCoClient(DataRow row) {
            cTitle.SelectedValue = row.Field<string>("title");
            cLastName.Text = (row.Field<string>("lastName").Trim());
            cFirstName.Text = (row.Field<string>("firstName").Trim());
            DateTime birth = row.Field<DateTime>("dateOfBirth");
            cDateOfBirth.Text = birth.ToString("dd/MM/yyyy");
            cEmail.Text = (row.Field<string>("email").Trim());
            cBuilding.Text = (row.Field<string>("building").Trim());
            cStreet.Text = (row.Field<string>("street").Trim());
            cDistrict.Text = (row.Field<string>("district").Trim());
            cHomePhone.Text = row.Field<string>("homePhone").Trim();
            cHomeFax.Text = row.Field<string>("homeFax").Trim();
            cBusinessPhone.Text = row.Field<string>("businessPhone").Trim();
            cMobilePhone.Text = row.Field<string>("mobilePhone").Trim();
            cCitizenship.Text = (row.Field<string>("countryOfCitizenship").Trim());
            cLegalResidence.Text = (row.Field<string>("countryOfLegalResidence").Trim());
            cHKID.Text = row.Field<string>("HKIDPassportNumber").Trim();
            if (row.Field<string>("isHKIDUsed").Trim() == "Yes")
            {
                cHKIDUsed.Checked = true;
                cPassportCountry.Text = "";
                cHKID.ReadOnly = true;
                cHKIDUsed.Enabled = false;
                cPassportCountry.ReadOnly = true;
            }
            else
            {
                cHKIDUsed.Checked = false;
                cPassportCountry.Text = (row.Field<string>("passportCountryOfIssue").Trim());
            }
            cEmploymentStatus.SelectedValue = row.Field<string>("employmentStatus").Trim();
            if (cEmploymentStatus.SelectedValue == "Employed")
            {
                cOccupation.Text = (row.Field<string>("occupation").Trim());
                cYears.Text = row.Field<decimal>("yearsWithEmployer").ToString();
                cEmployerName.Text = (row.Field<string>("employerName").Trim());
                cEmployerPhone.Text = row.Field<string>("employerPhone").Trim();
                cNatureOfBusiness.Text = (row.Field<string>("natureOfBusiness").Trim());
            }
            else
            {
                cOccupation.ReadOnly = true;
                cYears.ReadOnly = true;
                cEmployerName.ReadOnly = true;
                cEmployerPhone.ReadOnly = true;
                cNatureOfBusiness.ReadOnly = true;
                cOccupation.BackColor = System.Drawing.Color.Gray;
                cYears.BackColor = System.Drawing.Color.Gray;
                cEmployerName.BackColor = System.Drawing.Color.Gray;
                cEmployerPhone.BackColor = System.Drawing.Color.Gray;
                cNatureOfBusiness.BackColor = System.Drawing.Color.Gray;
                cOccupation.Text = "";
                cYears.Text = "";
                cEmployerName.Text = "";
                cEmployerPhone.Text = "";
                cNatureOfBusiness.Text = "";
            }
            part4CoQ1.SelectedValue = row.Field<string>("employedByFinancialInstitution").Trim();
            part4CoQ2.SelectedValue = row.Field<string>("isADirector").Trim();
        }
        protected string translateAccountType(string type)
        {
            if (type == "individual")
            {
                type = "Individual";
            }else if (type == "common")
            {
                type = "Joint Tenants in Common";
            }else if (type == "survivor")
            {
                type = "Joint Tenants with Rights of Survivorship";
            }
            return type;
        }
        
        protected void hideCoClient()
        {
            coClientInfo.Visible = false;
            coClientEmploymentInfo.Visible = false;
            coClientDisclosureInfo.Visible = false;
        }
        protected void showCoClient()
        {
            coClientInfo.Visible = true;
            coClientEmploymentInfo.Visible = true;
            coClientDisclosureInfo.Visible = true;
        }
        protected void Update_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;
            string AN = currentAN.Text.Trim();
            SqlTransaction trans = myData.beginTransaction();
            updateAccount(AN, trans);
            updatePrimaryClient(AN, trans);
            if (coClientInfo.Visible)
            {
                updateCoClient(AN, trans);
            }
            myData.commitTransaction(trans);
            result.Text = "Updated Successfully!";
        }
        protected void updateAccount(string AN, SqlTransaction trans)
        {
            string primary_source = primarySource.SelectedValue;
            primary_source = primary_source == "Other" ? myStringHelper.handleString(other.Text.Trim()) : primary_source;
            string investment_objective = investmentObjective.SelectedValue;
            string investment_knowledge = investmentKnowledge.SelectedValue;
            string investment_experience = investmentExperience.SelectedValue;
            string annual_income = annualIncome.SelectedValue;
            string liquid_net_worth = liquidNetWorth.SelectedValue;
            string account_feature = part6.SelectedValue;
            string sql = string.Format(
                "UPDATE Account SET " +
                "primarySource='{0}', investmentObjectives='{1}', investmentKnowledge='{2}', investmentExperience='{3}', annualIncome='{4}', " +
                "approximateLiquidNetWorth='{5}', accountFeature='{6}'" +
                "WHERE accountNumber = '{7}'", primary_source, investment_objective, investment_knowledge, investment_experience, annual_income, liquid_net_worth, account_feature, AN
            );
            myData.setData(sql, trans);
        }
        protected void updatePrimaryClient(string AN, SqlTransaction trans)
        {
            string p_title = pTitle.SelectedValue;
            string p_first_name = myStringHelper.handleString(pFirstName.Text.Trim());
            string p_last_name = myStringHelper.handleString(pLastName.Text.Trim());
            string p_email = myStringHelper.handleString(pEmail.Text.Trim());
            string p_street = myStringHelper.handleString(pStreet.Text.Trim());
            string p_district = myStringHelper.handleString(pDistrict.Text.Trim());
            string p_country_of_citizenship = myStringHelper.handleString(pCitizenship.Text.Trim());
            string p_country_of_legal_residence = myStringHelper.handleString(pLegalResidence.Text.Trim());
            string p_hkid_used = pHKIDUsed.Checked ? "Yes" : "No";
            string p_hkid = myStringHelper.handleString(pHKID.Text.Trim()).ToUpper();
            string p_passport_country = myStringHelper.handleString(pPassportCountry.Text.Trim());
            string p_employment_status = pEmploymentStatus.SelectedValue;
            string p_part4_q1 = part4PrimaryQ1.SelectedValue;
            string p_part4_q2 = part4PrimaryQ2.SelectedValue;
            string occupation = myStringHelper.handleString(pOccupation.Text.Trim());
            string years = myStringHelper.handleString(pYears.Text.Trim());
            string employerName = myStringHelper.handleString(pEmployerName.Text.Trim());
            string employerPhone = myStringHelper.handleString(pEmployerPhone.Text.Trim());
            string natureOfBusiness = myStringHelper.handleString(pNatureOfBusiness.Text.Trim());
            string building = myStringHelper.handleString(pBuilding.Text.Trim());
            string homePhone = myStringHelper.handleString(pHomePhone.Text.Trim());
            string homeFax = myStringHelper.handleString(pHomeFax.Text.Trim());
            string businessPhone = myStringHelper.handleString(pBusinessPhone.Text.Trim());
            string mobilePhone = myStringHelper.handleString(pMobilePhone.Text.Trim());
            string sql = string.Format(
                "UPDATE Client SET " +
                "title='{0}',firstName='{1}',lastname='{2}',email='{3}',street='{4}',district='{5}',employedByFinancialInstitution='{6}',isADirector='{7}'," +
                "countryOfCitizenship='{8}',countryOfLegalResidence='{9}',isHKIDUsed='{10}',HKIDPassportNumber='{11}',employmentStatus='{12}',passportCountryOfIssue='{13}'," +
                "occupation='{14}',yearsWithEmployer='{15}',employerName='{16}',employerPhone='{17}',natureOfBusiness='{18}',building='{19}',homePhone='{20}'," +
                "homeFax='{21}',businessPhone='{22}',mobilePhone='{23}' " +
                "WHERE accountNumber = '{24}' AND isPrimary ='Yes'",
                p_title, p_first_name, p_last_name, p_email, p_street, p_district, p_part4_q1, p_part4_q2, p_country_of_citizenship, p_country_of_legal_residence, p_hkid_used,
                p_hkid, p_employment_status,p_passport_country, occupation, years, employerName, employerPhone, natureOfBusiness,building,homePhone,homeFax,businessPhone,mobilePhone,
                AN
            );
            myData.setData(sql, trans);
        }
        protected void updateCoClient(string AN, SqlTransaction trans)
        {
            string p_title = cTitle.SelectedValue;
            string p_first_name = myStringHelper.handleString(cFirstName.Text.Trim());
            string p_last_name = myStringHelper.handleString(cLastName.Text.Trim());
            string p_email = myStringHelper.handleString(cEmail.Text.Trim());
            string p_street = myStringHelper.handleString(cStreet.Text.Trim());
            string p_district = myStringHelper.handleString(cDistrict.Text.Trim());
            string p_country_of_citizenship = myStringHelper.handleString(cCitizenship.Text.Trim());
            string p_country_of_legal_residence = myStringHelper.handleString(cLegalResidence.Text.Trim());
            string p_hkid_used = cHKIDUsed.Checked ? "Yes" : "No";
            string p_hkid = myStringHelper.handleString(cHKID.Text.Trim()).ToUpper();
            string p_passport_country = myStringHelper.handleString(cPassportCountry.Text.Trim());
            string p_employment_status = cEmploymentStatus.SelectedValue;
            string p_part4_q1 = part4CoQ1.SelectedValue;
            string p_part4_q2 = part4CoQ2.SelectedValue;
            string occupation = myStringHelper.handleString(cOccupation.Text.Trim());
            string years = myStringHelper.handleString(cYears.Text.Trim());
            string employerName = myStringHelper.handleString(cEmployerName.Text.Trim());
            string employerPhone = myStringHelper.handleString(cEmployerPhone.Text.Trim());
            string natureOfBusiness = myStringHelper.handleString(cNatureOfBusiness.Text.Trim());
            string building = myStringHelper.handleString(cBuilding.Text.Trim());
            string homePhone = myStringHelper.handleString(cHomePhone.Text.Trim());
            string homeFax = myStringHelper.handleString(cHomeFax.Text.Trim());
            string businessPhone = myStringHelper.handleString(cBusinessPhone.Text.Trim());
            string mobilePhone = myStringHelper.handleString(cMobilePhone.Text.Trim());
            string sql = string.Format(
                "UPDATE Client SET " +
                "title='{0}',firstName='{1}',lastname='{2}',email='{3}',street='{4}',district='{5}',employedByFinancialInstitution='{6}',isADirector='{7}'," +
                "countryOfCitizenship='{8}',countryOfLegalResidence='{9}',isHKIDUsed='{10}',HKIDPassportNumber='{11}',employmentStatus='{12}',passportCountryOfIssue='{13}'," +
                "occupation='{14}',yearsWithEmployer='{15}',employerName='{16}',employerPhone='{17}',natureOfBusiness='{18}',building='{19}',homePhone='{20}'," +
                "homeFax='{21}',businessPhone='{22}',mobilePhone='{23}' " +
                "WHERE accountNumber = '{24}' AND isPrimary ='Yes'",
                p_title, p_first_name, p_last_name, p_email, p_street, p_district, p_part4_q1, p_part4_q2, p_country_of_citizenship, p_country_of_legal_residence, p_hkid_used,
                p_hkid, p_employment_status, p_passport_country, occupation, years, employerName, employerPhone, natureOfBusiness, building, homePhone, homeFax, businessPhone, mobilePhone,
                AN
            );
            myData.setData(sql, trans);
        }

        protected void primarySourceValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (primarySource.SelectedValue == "other" && string.IsNullOrEmpty(other.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void primarySource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (primarySource.SelectedValue != "Other")
            {
                other.Text = "";
                other.ReadOnly = true;
                other.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                other.ReadOnly = false;
                other.BackColor = System.Drawing.Color.White;
            }
        }

        protected void pPhoneValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(pHomePhone.Text.Trim()) &&
                string.IsNullOrEmpty(pBusinessPhone.Text.Trim()) &&
                string.IsNullOrEmpty(pMobilePhone.Text.Trim()))
            {
                args.IsValid = false;
                string e_m = "At least one of the home phone, business phone and mobile phone should be provided.";
                pHomePhoneValidate.ErrorMessage = e_m;
                pBusinessPhoneValidate.ErrorMessage = e_m;
                pMobilePhoneValidate.ErrorMessage = e_m;
            }
        }

        protected void pHKIDUsed_CheckedChanged(object sender, EventArgs e)
        {
            if (pHKIDUsed.Checked)
            {
                pPassportCountry.Text = "";
                pPassportCountry.ReadOnly = true;
                pPassportCountry.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                pPassportCountry.ReadOnly = false;
                pPassportCountry.BackColor = System.Drawing.Color.White;
            }
        }

        protected void pPassportCountryOfIssueValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!pHKIDUsed.Checked && string.IsNullOrEmpty(pPassportCountry.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pHKIDValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (accountType.Text != "Individual")
            {
                if ((pHKIDUsed.Checked && cHKIDUsed.Checked && pHKID.Text.Trim() == cHKID.Text.Trim()) ||
                    (!pHKIDUsed.Checked && !cHKIDUsed.Checked && pHKID.Text.Trim() == cHKID.Text.Trim() && cPassportCountry.Text.Trim() == pPassportCountry.Text.Trim()))
                {
                    args.IsValid = false;
                    pHKIDValidate.ErrorMessage = "The HKID of the primary holder cannot be the same as co account holder.";
                    return;
                }
            }
            if (pHKIDUsed.Checked)
            {
                string sql = "SELECT HKIDPassportNumber from Client WHERE isHKIDUsed = 'Yes' AND accountNumber <> '" + currentAN.Text.Trim() + "'";
                DataTable table = myData.getData(sql);
                if (table == null || table.Rows.Count == 0) return;
                foreach (DataRow row in table.Rows)
                {
                    if (row.Field<string>("HKIDPassportNumber").Trim() == pHKID.Text.Trim())
                    {
                        args.IsValid = false;
                        pHKIDValidate.ErrorMessage = "Dumplicate HKID is not allowed";
                        break;
                    }
                }
            }
            else
            {
                string sql = "SELECT HKIDPassportNumber, passportCountryOfIssue from Client WHERE isHKIDUsed = 'No' AND accountNumber <> '" + currentAN.Text.Trim() + "'";
                DataTable table = myData.getData(sql);
                foreach (DataRow row in table.Rows)
                {
                    if (row.Field<string>("HKIDPassportNumber").Trim() == pHKID.Text.Trim() && row.Field<string>("passportCountryOfIssue").Trim() == pPassportCountry.Text.Trim())
                    {
                        args.IsValid = false;
                        pHKIDValidate.ErrorMessage = "This passport has been used!";
                        break;
                    }
                }
            }
        }

        protected void cPhoneValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cHomePhone.Text.Trim()) &&
                string.IsNullOrEmpty(cBusinessPhone.Text.Trim()) &&
                string.IsNullOrEmpty(cMobilePhone.Text.Trim()))
            {
                args.IsValid = false;
                string em = "At least one of the home phone, business phone and mobile phone should be provided.";
                cHomePhoneValidate.ErrorMessage = em;
                cBusinessPhoneValidate.ErrorMessage = em;
                cMobilePhoneValidate.ErrorMessage = em;
            }
        }

        protected void cHKIDValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (accountType.Text != "Individual")
            {
                if ((pHKIDUsed.Checked && cHKIDUsed.Checked && pHKID.Text.Trim() == cHKID.Text.Trim()) ||
                    (!pHKIDUsed.Checked && !cHKIDUsed.Checked && pHKID.Text.Trim() == cHKID.Text.Trim() && cPassportCountry.Text.Trim() == pPassportCountry.Text.Trim()))
                {
                    args.IsValid = false;
                    cHKIDValidate.ErrorMessage = "The HKID of the primary holder cannot be the same as co account holder.";
                    return;
                }
            }
            if (cHKIDUsed.Checked)
            {
                string sql = "SELECT HKIDPassportNumber from Client WHERE isHKIDUsed = 'Yes' AND accountNumber <> '" + currentAN.Text.Trim() + "'";
                DataTable table = myData.getData(sql);
                if (table == null || table.Rows.Count == 0) return;
                foreach (DataRow row in table.Rows)
                {
                    if (row.Field<string>("HKIDPassportNumber").Trim() == cHKID.Text.Trim())
                    {
                        args.IsValid = false;
                        cHKIDValidate.ErrorMessage = "Dumplicate HKID is not allowed";
                        break;
                    }
                }
            }
            else
            {
                string sql = "SELECT HKIDPassportNumber, passportCountryOfIssue from Client WHERE isHKIDUsed = 'No' AND accountNumber <> '" + currentAN.Text.Trim() + "'";
                DataTable table = myData.getData(sql);
                foreach (DataRow row in table.Rows)
                {
                    if (row.Field<string>("HKIDPassportNumber").Trim() == cHKID.Text.Trim() && row.Field<string>("passportCountryOfIssue").Trim() == cPassportCountry.Text.Trim())
                    {
                        args.IsValid = false;
                        cHKIDValidate.ErrorMessage = "This passport has been used!";
                        break;
                    }
                }
            }
        }

        protected void cPassportCountryOfIssueValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!cHKIDUsed.Checked && string.IsNullOrEmpty(cPassportCountry.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pEmploymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pEmploymentStatus.SelectedValue == "Employed")
            {
                pOccupation.ReadOnly = false;
                pYears.ReadOnly = false;
                pEmployerName.ReadOnly = false;
                pEmployerPhone.ReadOnly = false;
                pNatureOfBusiness.ReadOnly = false;
                pOccupation.BackColor = System.Drawing.Color.White;
                pYears.BackColor = System.Drawing.Color.White;
                pEmployerName.BackColor = System.Drawing.Color.White;
                pEmployerPhone.BackColor = System.Drawing.Color.White;
                pNatureOfBusiness.BackColor = System.Drawing.Color.White;
            }
            else
            {
                pOccupation.ReadOnly = true;
                pYears.ReadOnly = true;
                pEmployerName.ReadOnly = true;
                pEmployerPhone.ReadOnly = true;
                pNatureOfBusiness.ReadOnly = true;
                pOccupation.BackColor = System.Drawing.Color.Gray;
                pYears.BackColor = System.Drawing.Color.Gray;
                pEmployerName.BackColor = System.Drawing.Color.Gray;
                pEmployerPhone.BackColor = System.Drawing.Color.Gray;
                pNatureOfBusiness.BackColor = System.Drawing.Color.Gray;
                pOccupation.Text = "";
                pYears.Text = "";
                pEmployerName.Text = "";
                pEmployerPhone.Text = "";
                pNatureOfBusiness.Text = "";
            }
        }

        protected void cEmploymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cEmploymentStatus.SelectedValue == "Employed")
            {
                cOccupation.ReadOnly = false;
                cYears.ReadOnly = false;
                cEmployerName.ReadOnly = false;
                cEmployerPhone.ReadOnly = false;
                cNatureOfBusiness.ReadOnly = false;
                cOccupation.BackColor = System.Drawing.Color.White;
                cYears.BackColor = System.Drawing.Color.White;
                cEmployerName.BackColor = System.Drawing.Color.White;
                cEmployerPhone.BackColor = System.Drawing.Color.White;
                cNatureOfBusiness.BackColor = System.Drawing.Color.White;
            }
            else
            {
                cOccupation.ReadOnly = true;
                cYears.ReadOnly = true;
                cEmployerName.ReadOnly = true;
                cEmployerPhone.ReadOnly = true;
                cNatureOfBusiness.ReadOnly = true;
                cOccupation.BackColor = System.Drawing.Color.Gray;
                cYears.BackColor = System.Drawing.Color.Gray;
                cEmployerName.BackColor = System.Drawing.Color.Gray;
                cEmployerPhone.BackColor = System.Drawing.Color.Gray;
                cNatureOfBusiness.BackColor = System.Drawing.Color.Gray;
                cOccupation.Text = "";
                cYears.Text = "";
                cEmployerName.Text = "";
                cEmployerPhone.Text = "";
                cNatureOfBusiness.Text = "";
            }
        }

        protected void cHKIDUsed_CheckedChanged(object sender, EventArgs e)
        {
            if (cHKIDUsed.Checked)
            {
                cPassportCountry.Text = "";
                cPassportCountry.ReadOnly = true;
                cPassportCountry.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                cPassportCountry.ReadOnly = false;
                cPassportCountry.BackColor = System.Drawing.Color.White;
            }
        }

        protected void pOccupationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(pOccupation.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pYearsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(pYears.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pEmployerNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(pEmployerName.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pEmployerPhoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(pEmployerPhone.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void pNatureOfBusinessValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (pEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(pNatureOfBusiness.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void cOccupationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(cOccupation.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void cYearsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(cYears.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void cEmployerNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(cEmployerName.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void cEmployerPhoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(cEmployerPhone.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void cNatureOfBusinessValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cEmploymentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(cNatureOfBusiness.Text.Trim()))
            {
                args.IsValid = false;
            }
        }
    }
}
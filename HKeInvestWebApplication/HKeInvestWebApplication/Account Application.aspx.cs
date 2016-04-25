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
using System.Globalization;

namespace HKeInvestWebApplication
{
    public partial class Account_Application : System.Web.UI.Page
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        protected void Page_Load(object sender, EventArgs e)
        {
            applyResult.Text = "";
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            applyResult.Text = "";
            if (!IsValid) return;
            /*============get data for account and primary client==============*/
            string account_type = AccountType.SelectedValue;
            string p_title = title.SelectedValue;
            string p_first_name = handleString( firstName.Text.Trim());
            string p_last_name = handleString( lastName.Text.Trim());
            string p_date_of_birth = dateOfBirth.Text.Trim();
            string p_email = handleString( email.Text.Trim());
            string p_street = handleString(street.Text.Trim());
            string p_district = handleString( district.Text.Trim());
            string p_country_of_citizenship = handleString( citizenship.Text.Trim());
            string p_country_of_legal_residence = handleString( legalResidence.Text.Trim());
            string p_hkid = handleString(HKID.Text.Trim()).ToUpper();
            string p_employment_status = employmentStatus.SelectedValue;
            string p_part4_q1 = part4PrimaryQ1.SelectedValue;
            string p_part4_q2 = part4PrimaryQ2.SelectedValue;
            string primary_source = primarySource.SelectedValue;
            primary_source = primary_source == "Other" ? handleString(Part4Other.Text.Trim()) : primary_source;
            string investment_objective = investmentObjective.SelectedValue;
            string investment_knowledge = investmentKnowledge.SelectedValue;
            string investment_experience = investmentExperience.SelectedValue;
            string annual_income = annualIncome.SelectedValue;
            string liquid_net_worth = liquidNetWorth.SelectedValue;
            string account_feature = part6.Checked ? "Yes" : "No";
            decimal deposit = decimal.Zero;decimal money = decimal.Zero;
            if (cheque.Checked)
            {
                if(decimal.TryParse(chequeAmount.Text.Trim(), out money))
                {
                    deposit += money;
                }
            }
            if (transfer.Checked)
            {
                if(decimal.TryParse(transferAmount.Text.Trim(),out money))
                {
                    deposit += money;
                }
            }
            string account_number = generateAccountNumber(p_last_name);
            /*============insert primary client data and account data==========*/
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            string sql = string.Format(
                "INSERT INTO Account "+
                "(accountNumber, accountType, balance, primarySource, investmentObjectives,"+
                " investmentKnowledge, investmentExperience, annualIncome, approximateLiquidNetWorth,"+
                " accountFeature) "+
                "VALUES "+
                "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                account_number, account_type, deposit.ToString(), primary_source, investment_objective,
                investment_knowledge, investment_experience, annual_income, liquid_net_worth,
                account_feature
                );
            myHKeInvestData.setData(sql, trans);
            
            sql = string.Format(
                "INSERT INTO Client "+
                "(accountNumber,isPrimary,title,firstName,lastName,dateOfBirth,email,street,"+
                "district,countryOfCitizenship,countryOfLegalResidence,HKIDPassportNumber," +
                "employmentStatus," +
                "employedByFinancialInstitution,isADirector) " +
                "VALUES ('{0}','Yes','{1}','{2}','{3}',CONVERT(date,'{4}',103),'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'," +
                "'{13}')",
                account_number,p_title,p_first_name,p_last_name,p_date_of_birth,p_email,
                p_street,p_district,
                p_country_of_citizenship,p_country_of_legal_residence,p_hkid,
                p_employment_status,
                p_part4_q1,p_part4_q2
                );
            myHKeInvestData.setData(sql, trans);

            if (!string.IsNullOrEmpty(building.Text.Trim()))
            {
                sql = updateClientSql("building", handleString(building.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(homePhone.Text.Trim()))
            {
                sql = updateClientSql("homePhone", handleString(homePhone.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(homeFax.Text.Trim()))
            {
                sql = updateClientSql("homeFax", handleString(homeFax.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(businessPhone.Text.Trim()))
            {
                sql = updateClientSql("businessPhone", handleString(businessPhone.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(mobilePhone.Text.Trim()))
            {
                sql = updateClientSql("mobilePhone", handleString(mobilePhone.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(passportCountry.Text.Trim()))
            {
                sql = updateClientSql("passportCountryOfIssue", handleString(passportCountry.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(occupation.Text.Trim()))
            {
                sql = updateClientSql("occupation", handleString(occupation.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(years.Text.Trim()))
            {
                sql = updateClientSql("yearsWithEmployer", handleString(years.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(employerName.Text.Trim()))
            {
                sql = updateClientSql("employerName", handleString(employerName.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(employerPhone.Text.Trim()))
            {
                sql = updateClientSql("employerPhone", handleString(employerPhone.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }
            if (!string.IsNullOrEmpty(natureOfBusiness.Text.Trim()))
            {
                sql = updateClientSql("natureOfBusiness", handleString(natureOfBusiness.Text.Trim()), true, account_number);
                myHKeInvestData.setData(sql, trans);
            }

            /*======================get data from co-account holder and insert into Client======================*/
            if (account_type != "individual")
            {
                string c_title = title2.SelectedValue;
                string c_first_name = handleString(firstName2.Text.Trim());
                string c_last_name = handleString(lastName2.Text.Trim());
                string c_date_of_birth = dateOfBirth2.Text.Trim();
                string c_email = handleString(email2.Text.Trim());
                string c_street = handleString(street2.Text.Trim());
                string c_district = handleString(district2.Text.Trim());
                string c_country_of_citizenship = handleString(citizenship2.Text.Trim());
                string c_country_of_legal_residence = handleString(legalResidence2.Text.Trim());
                string c_hkid = handleString(HKID2.Text.Trim()).ToUpper();
                string c_employment_status = employmentStatus2.SelectedValue;
                string c_part4_q1 = part4CoQ1.SelectedValue;
                string c_part4_q2 = part4CoQ2.SelectedValue;
                sql = string.Format(
                    "INSERT INTO Client " +
                    "(accountNumber,isPrimary,title,firstName,lastName,dateOfBirth,email,street," +
                    "district,countryOfCitizenship,countryOfLegalResidence,HKIDPassportNumber," +
                    "employmentStatus," +
                    "employedByFinancialInstitution,isADirector)" +
                    "VALUES ('{0}','No','{1}','{2}','{3}',CONVERT(date,'{4}',103),'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}'," +
                    "'{13}')",
                    account_number, c_title, c_first_name, c_last_name, c_date_of_birth, c_email,
                    c_street, c_district,
                    c_country_of_citizenship, c_country_of_legal_residence, c_hkid,
                    c_employment_status,
                    c_part4_q1, c_part4_q2
                    );
                myHKeInvestData.setData(sql, trans);
                if (!string.IsNullOrEmpty(building2.Text.Trim()))
                {
                    sql = updateClientSql("building", handleString(building2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(homePhone2.Text.Trim()))
                {
                    sql = updateClientSql("homePhone", handleString(homePhone2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(homeFax2.Text.Trim()))
                {
                    sql = updateClientSql("homeFax", handleString(homeFax2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(businessPhone2.Text.Trim()))
                {
                    sql = updateClientSql("businessPhone", handleString(businessPhone2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(mobilePhone2.Text.Trim()))
                {
                    sql = updateClientSql("mobilePhone", handleString(mobilePhone2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(passportCountry2.Text.Trim()))
                {
                    sql = updateClientSql("passportCountryOfIssue", handleString(passportCountry2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(occupation2.Text.Trim()))
                {
                    sql = updateClientSql("occupation", handleString(occupation2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(years2.Text.Trim()))
                {
                    sql = updateClientSql("yearsWithEmployer", handleString(years2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(employerName2.Text.Trim()))
                {
                    sql = updateClientSql("employerName", handleString(employerName2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(employerPhone2.Text.Trim()))
                {
                    sql = updateClientSql("employerPhone", handleString(employerPhone2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
                if (!string.IsNullOrEmpty(natureOfBusiness2.Text.Trim()))
                {
                    sql = updateClientSql("natureOfBusiness", handleString(natureOfBusiness2.Text.Trim()), false, account_number);
                    myHKeInvestData.setData(sql, trans);
                }
            }
            myHKeInvestData.commitTransaction(trans);
            
            // notify the applicant that the security account has been successfully created
            applyResult.Text = "Congratulation! Your security account number: " + account_number;
        }

        protected string generateAccountNumber(string last_name)
        {
            string accountNumber = "";
            for (int i = 0; i < last_name.Length; ++i)
            {
                if (char.IsLetter(last_name[i]))
                {
                    accountNumber += last_name[i];
                }
                if (accountNumber.Length == 2) break;
            }
            if (accountNumber.Length == 1) accountNumber += accountNumber;
            accountNumber = accountNumber.ToUpper();

            string sql = "select accountNumber from Account where accountNumber like '" + accountNumber + "%' order by CAST(SUBSTRING(accountNumber, 3, 8) AS INT) DESC";
            DataTable dt = myHKeInvestData.getData(sql);
            if (dt == null || dt.Rows.Count == 0)
            {
                accountNumber += "00000001";
            }
            else
            {
                DataRow row = dt.Rows[0];
                int num;
                int.TryParse(row.Field<string>("accountNumber").Substring(2, 8), out num);
                num++;
                accountNumber += num.ToString("D8");
            }

            return accountNumber;
        }

        protected string handleString(string data) {
            data = data.Replace("'", "\'");
            data = data.Replace('"', '\"');
            return data;
        }

        protected string updateClientSql(string column,string data, bool isPrimary,string account)
        {
            data = handleString(data);string type;
            if (isPrimary) type = "Yes";
            else type = "No";
            string sql = "UPDATE Client SET " + column + " = '" + data + "' "+
                "WHERE accountNumber = '" + account +"' AND isPrimary = '"+type+"'";
            return sql;
        }
/*================================================================================================*/
        protected void AccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string accountType = AccountType.SelectedValue;
            if (accountType == "0") return;
            bool isCoShow = CoClient.Visible;
            if (accountType == "individual" && isCoShow)
            {
                CoClient.Visible = false;
                CoClientEmploymentInfo.Visible = false;
                CoClientPart4.Visible = false;
            }
            else if (accountType != "individual" && !isCoShow)
            {
                CoClient.Visible = true;
                CoClientEmploymentInfo.Visible = true;
                CoClientPart4.Visible = true;
            }
        }

        protected void dateOfBirthValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime birth;
            bool result = DateTime.TryParseExact(dateOfBirth.Text.Trim(), "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,DateTimeStyles.None, out birth);
            if (!result)
            {
                args.IsValid = false;
                dateOfBirthValidate.ErrorMessage = "The date of birth is invalid.";
            }else
            {
                if (birth.CompareTo(DateTime.Today) > 0)
                {
                    args.IsValid = false;
                    dateOfBirthValidate.ErrorMessage = "The client is even not born -_-";
                }
            }
        }

        protected void dateOfBirth2Validate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime birth;
            bool result = DateTime.TryParseExact(dateOfBirth2.Text.Trim(), "dd/MM/yyyy",
    System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out birth);
            if (!result)
            {
                args.IsValid = false;
                dateOfBirth2Validate.ErrorMessage = "The date of birth is not valid.";
            }
            else
            {
                if (birth.CompareTo(DateTime.Today) > 0)
                {
                    args.IsValid = false;
                    dateOfBirth2Validate.ErrorMessage = "The client is even not born -_-";
                }
            }
        }

        protected void PhoneValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(homePhone.Text.Trim()) && string.IsNullOrEmpty(businessPhone.Text.Trim()) && string.IsNullOrEmpty(mobilePhone.Text.Trim()))
            {
                args.IsValid = false;
                string em = "At least one of the home phone, business phone and mobile phone should be provided.";
                homePhoneValidate.ErrorMessage = em;
                businessPhoneValidate.ErrorMessage = em;
                mobilePhoneValidate.ErrorMessage = em;
            }
        }

        protected void passportCountryOfIssueValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!HKIDUsed.Checked && string.IsNullOrEmpty(passportCountry.Text.Trim()))
            {
                args.IsValid = false;
                passportCountryOfIssueValidate.ErrorMessage = "Passport country of issue is required.";
            }
        }

        protected void HKIDUsed_CheckedChanged(object sender, EventArgs e)
        {
            if (HKIDUsed.Checked)
            {
                passportCountry.Text = "";
                passportCountry.ReadOnly = true;
                passportCountry.BackColor = System.Drawing.Color.Gray;
            }else
            {
                passportCountry.ReadOnly = false;
                passportCountry.BackColor = System.Drawing.Color.White;
            }
        }

        protected void phoneValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(homePhone2.Text.Trim()) &&
                string.IsNullOrEmpty(businessPhone2.Text.Trim()) &&
                string.IsNullOrEmpty(mobilePhone2.Text.Trim()))
            {
                args.IsValid = false;
                string em = "At least one of the home phone, business phone and mobile phone should be provided.";
                homePhoneValidate2.ErrorMessage = em;
                businessPhoneValidate2.ErrorMessage = em;
                mobilePhoneValidate2.ErrorMessage = em;
            }
        }

        protected void HKIDUsed2_CheckedChanged(object sender, EventArgs e)
        {
            if (HKIDUsed2.Checked)
            {
                passportCountry2.Text = "";
                passportCountry2.ReadOnly = true;
                passportCountry2.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                passportCountry2.ReadOnly = false;
                passportCountry2.BackColor = System.Drawing.Color.White;
            }
        }

        protected void employmentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employmentStatus.SelectedValue == "Employed")
            {
                occupation.ReadOnly = false;
                years.ReadOnly = false;
                employerName.ReadOnly = false;
                employerPhone.ReadOnly = false;
                natureOfBusiness.ReadOnly = false;
                occupation.BackColor = System.Drawing.Color.White;
                years.BackColor = System.Drawing.Color.White;
                employerName.BackColor = System.Drawing.Color.White;
                employerPhone.BackColor = System.Drawing.Color.White;
                natureOfBusiness.BackColor = System.Drawing.Color.White;
            }else
            {
                occupation.Text = "";
                years.Text = "";
                employerName.Text = "";
                employerPhone.Text = "";
                natureOfBusiness.Text = "";
                occupation.ReadOnly = true;
                years.ReadOnly = true;
                employerName.ReadOnly = true;
                employerPhone.ReadOnly = true;
                natureOfBusiness.ReadOnly = true;
                occupation.BackColor = System.Drawing.Color.Gray;
                years.BackColor = System.Drawing.Color.Gray;
                employerName.BackColor = System.Drawing.Color.Gray;
                employerPhone.BackColor = System.Drawing.Color.Gray;
                natureOfBusiness.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void occupationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus.SelectedValue == "Employed"&&string.IsNullOrEmpty(occupation.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void yearsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(years.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void employerNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(employerName.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void employerPhoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(employerPhone.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void natureOfBusinessValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus.SelectedValue == "Employed" && string.IsNullOrEmpty(natureOfBusiness.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void employmentStatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employmentStatus2.SelectedValue == "Employed")
            {
                occupation2.ReadOnly = false;
                years2.ReadOnly = false;
                employerName2.ReadOnly = false;
                employerPhone2.ReadOnly = false;
                natureOfBusiness2.ReadOnly = false;
                occupation2.BackColor = System.Drawing.Color.White;
                years2.BackColor = System.Drawing.Color.White;
                employerName2.BackColor = System.Drawing.Color.White;
                employerPhone2.BackColor = System.Drawing.Color.White;
                natureOfBusiness2.BackColor = System.Drawing.Color.White;
            }
            else
            {
                occupation2.Text = "";
                years2.Text = "";
                employerName2.Text = "";
                employerPhone2.Text = "";
                natureOfBusiness2.Text = "";
                occupation2.ReadOnly = true;
                years2.ReadOnly = true;
                employerName2.ReadOnly = true;
                employerPhone2.ReadOnly = true;
                natureOfBusiness2.ReadOnly = true;
                occupation2.BackColor = System.Drawing.Color.Gray;
                years2.BackColor = System.Drawing.Color.Gray;
                employerName2.BackColor = System.Drawing.Color.Gray;
                employerPhone2.BackColor = System.Drawing.Color.Gray;
                natureOfBusiness2.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void primarySource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (primarySource.SelectedValue == "Other")
            {
                Part4Other.ReadOnly = false;
                Part4Other.BackColor = System.Drawing.Color.White;
            }else
            {
                Part4Other.Text = "";
                Part4Other.ReadOnly = true;
                Part4Other.BackColor = System.Drawing.Color.Gray;
            }
        }

        protected void part4Validate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(part4PrimaryQ1.SelectedIndex==-1 ||
                part4PrimaryQ2.SelectedIndex==-1 ||
                primarySource.SelectedIndex == -1)
            {
                args.IsValid = false;
                part4Validate.ErrorMessage = "Please select one option for every question in part 4";
            }
            else if (AccountType.SelectedValue != "0" && AccountType.SelectedValue != "individual")
            {
                if(part4CoQ1.SelectedIndex==-1 || part4CoQ2.SelectedIndex == -1)
                {
                    args.IsValid = false;
                    part4Validate.ErrorMessage = "Please select one option for every question in part 4";
                }
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (primarySource.SelectedValue == "Other" && string.IsNullOrEmpty(Part4Other.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void part5Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(investmentObjective.SelectedIndex==-1||
                investmentKnowledge.SelectedIndex==-1||
                investmentExperience.SelectedIndex==-1||
                annualIncome.SelectedIndex==-1||
                liquidNetWorth.SelectedIndex == -1)
            {
                args.IsValid = false;
                part5Validator.ErrorMessage = "Please select an option for every question in part 5";
            }
        }

        protected void part7Validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(!cheque.Checked && !transfer.Checked)
            {
                args.IsValid = false;
                part7Validator.ErrorMessage = "Please select a checkbox and fill in the amount in the corresponding textbox";
                return;
            }
            if (cheque.Checked&&string.IsNullOrEmpty(chequeAmount.Text.Trim()))
            {
                args.IsValid = false;
                part7Validator.ErrorMessage = "Please fill in the cheque amount";
                return;
            }
            if (transfer.Checked && string.IsNullOrEmpty(transferAmount.Text.Trim()))
            {
                args.IsValid = false;
                part7Validator.ErrorMessage = "Please fill in the transfer amount";
                return;
            }
            decimal chequeMoney = decimal.Zero;
            if (cheque.Checked)
            {
                if(decimal.TryParse(chequeAmount.Text.Trim(), out chequeMoney))
                {
                    string[] ca = chequeAmount.Text.Trim().Split('.');
                    if (ca.Length == 2 && ca[1].Length > 2)
                    {
                        args.IsValid = false;
                        part7Validator.ErrorMessage = "The decimal points of the cheque amount should be less than 2";
                        return;
                    }
                    if (chequeMoney.CompareTo(decimal.Zero) <= 0)
                    {
                        args.IsValid = false;
                        part7Validator.ErrorMessage = "The cheque amount cannot be negative";
                        return;
                    }
                }else
                {
                    args.IsValid = false;
                    part7Validator.ErrorMessage = "The cheque amount is an invalid number";
                    return;
                }
            }
            decimal transferMoney = decimal.Zero;
            if (transfer.Checked)
            {
                if (decimal.TryParse(transferAmount.Text.Trim(), out transferMoney))
                {
                    string[] ca = transferAmount.Text.Trim().Split('.');
                    if (ca.Length == 2 && ca[1].Length > 2)
                    {
                        args.IsValid = false;
                        part7Validator.ErrorMessage = "The decimal points of the transfer amount should be less than 2";
                        return;
                    }
                    if (transferMoney.CompareTo(decimal.Zero) <= 0)
                    {
                        args.IsValid = false;
                        part7Validator.ErrorMessage = "The transfer amount cannot be negative";
                        return;
                    }
                }
                else
                {
                    args.IsValid = false;
                    part7Validator.ErrorMessage = "The transfer amount is an invalid number";
                    return;
                }
            }
            decimal sum = chequeMoney + transferMoney;
            if (sum < (decimal)20000.00)
            {
                args.IsValid = false;
                part7Validator.ErrorMessage = "At least HK$20,000 is needed to create a security account";
            }
        }

        protected void passportCountryOfIssue2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(!HKIDUsed2.Checked && string.IsNullOrEmpty(passportCountry2.Text.Trim()))
            {
                args.IsValid = false;
                passportCountryOfIssue2.ErrorMessage = "Passport country of issue is required";
            }
        }

        protected void occupationValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus2.SelectedValue == "Employed" && string.IsNullOrEmpty(occupation2.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void yearsValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus2.SelectedValue == "Employed" && string.IsNullOrEmpty(years2.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void employerNameValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus2.SelectedValue == "Employed" && string.IsNullOrEmpty(employerName2.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void employerPhoneValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus2.SelectedValue == "Employed" && string.IsNullOrEmpty(employerPhone2.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void natureOfBusinessValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (employmentStatus2.SelectedValue == "Employed" && string.IsNullOrEmpty(natureOfBusiness2.Text.Trim()))
            {
                args.IsValid = false;
            }
        }

        protected void HKIDValidate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string hkid = HKID.Text.Trim().ToUpper();
            if (AccountType.SelectedValue != "0" && AccountType.SelectedValue != "individual" && hkid == HKID2.Text.Trim().ToUpper())
            {
                args.IsValid = false;
                HKIDValidate.ErrorMessage = "The HKID of the primary holder cannot be the same as co account holder.";
                return;
            }
            string sql = "SELECT HKIDPassportNumber from Client";
            DataTable table = myHKeInvestData.getData(sql);
            if (table == null || table.Rows.Count == 0) return;
            foreach(DataRow row in table.Rows)
            {
                if (row.Field<string>("HKIDPassportNumber") == hkid)
                {
                    args.IsValid = false;
                    HKIDValidate.ErrorMessage = "Dumplicate HKID is not allowed";
                    break;
                }
            }
        }

        protected void HKID2Validate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string hkid2 = HKID2.Text.Trim().ToUpper();
            if (AccountType.SelectedValue != "0" && AccountType.SelectedValue != "individual" && hkid2 == HKID.Text.Trim().ToUpper())
            {
                args.IsValid = false;
                HKID2Validate.ErrorMessage = "The HKID of the primary holder cannot be the same as co account holder.";
                return;
            }
            string sql = "SELECT HKIDPassportNumber from Client";
            DataTable table = myHKeInvestData.getData(sql);
            if (table == null || table.Rows.Count == 0) return;
            foreach (DataRow row in table.Rows)
            {
                if (row.Field<string>("HKIDPassportNumber") == hkid2)
                {
                    args.IsValid = false;
                    HKIDValidate.ErrorMessage = "Dumplicate HKID is not allowed";
                    break;
                }
            }
        }
    }
}
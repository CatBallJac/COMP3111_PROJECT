﻿using System;
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
        HKeInvestData myHKeInvestData = new HKeInvestData();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            /*==============================================validation ends==============================================*/
            // get data for [Account] Table
            string AN = generateAccountNumber(lastName.Text.Trim());
            string accountType = AccountType.SelectedValue;
            decimal balance = decimal.Zero;
            decimal money = decimal.Zero;
            if (cheque.Checked)
            {
                if (decimal.TryParse(chequeAmount.Text.Trim(), out money))
                {
                    balance += money;
                }
                else return;
            }
            if (transfer.Checked)
            {
                if (decimal.TryParse(transferAmount.Text.Trim(), out money))
                {
                    balance += money;
                }
                else return;
            }
            balance = decimal.Round(balance, 2);
            string pSource = primarySource.SelectedValue;
            if (pSource == "Other") pSource = Part4Other.Text.Trim();
            string investObject = investmentObjective.SelectedValue;
            string investKnow = investmentKnowledge.SelectedValue;
            string investExper = investmentExperience.SelectedValue;
            string annual = annualIncome.SelectedValue;
            string liquidNet = liquidNetWorth.SelectedValue;
            string feature = "No";
            if (part6.Checked) feature = "Yes";


            // insert data into table [dbo].[Account]
            string sql = "INSERT INTO Account " +
"(accountNumber,accountType,balance,primarySource,investmentObjectives,investmentKnowledge,investmentExperience,annualIncome,approximateLiquidNetWorth,accountFeature)" +
string.Format(" VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
  AN, accountType, balance.ToString(), pSource, investObject, investKnow, investExper, annual, liquidNet, feature);

            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData(sql, trans);

            /*==============================Account ends===============================================*/
            // get data [Client] Table primary client
            string ctype = "Primary";
            string tt = title.SelectedValue;
            string fn = firstName.Text.Trim();
            string ln = lastName.Text.Trim();
            string db = dateOfBirth.Text.Trim();
            string el = email.Text.Trim();
            string hf = homeFax.Text.Trim();
            string coc = citizenship.Text.Trim();
            string col = legalResidence.Text.Trim();
            string hk = HKID.Text.Trim();
            string pport = passportCountry.Text.Trim();
            if (pport == "" || !HKIDUsed.Checked) pport = "NULL";
            string es = employmentStatus.Text.Trim();
            string occu = occupation.Text.Trim();
            if (occu == "") occu = "NULL";
            string ename = employerName.Text.Trim();
            if (ename == "") ename = "NULL";
            string nobusiness = natureOfBusiness.Text.Trim();
            if (nobusiness == "") nobusiness = "NULL";
            string esq = part4PrimaryQ1.SelectedValue;
            string csq = part4PrimaryQ2.SelectedValue;
            string build = building.Text.Trim();
            if (build == "") build = "NULL";
            string stre = street.Text.Trim();
            string distr = district.Text.Trim();

            // insert data into table [dbo].[Client] primary client
            sql = string.Format("INSERT INTO Client (accountNumber,clientType,title,firstName,lastName,dateOfBirth,email,homeFax,countryOfCitizenship,countryOfLegalResidence,HKIDPassportNumber,passportCountryOfIssue,employmentStatus,occupation,employerName,natureOfBusiness,employedByBank,clientIsDirector,Building,Street,District) " +
                "VALUES('{0}','{1}','{2}','{3}','{4}',CONVERT(date,'{5}',103),'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                                                            AN, ctype, tt, fn, ln, db, el, hf, coc, col, hk, pport, es, occu, ename, nobusiness, esq, csq, build, stre, distr);
            myHKeInvestData.setData(sql, trans);

            string ephone = employerPhone.Text.Trim();
            if (ephone != "")
            {
                sql = string.Format("UPDATE Client SET employerPhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Primary'", ephone, AN);
                myHKeInvestData.setData(sql, trans);
            }
            string yea = years.Text.Trim();
            if (yea != "")
            {
                sql = string.Format("UPDATE Client SET yearsWithEmployer = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Primary'", yea, AN);
                myHKeInvestData.setData(sql, trans);
            }
            string bphone = businessPhone.Text.Trim();
            if (bphone != "")
            {
                sql = string.Format("UPDATE Client SET businessPhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Primary'", bphone, AN);
                myHKeInvestData.setData(sql, trans);
            }
            string mphone = mobilePhone.Text.Trim();
            if (mphone != "")
            {
                sql = string.Format("UPDATE Client SET mobilePhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Primary'", mphone, AN);
                myHKeInvestData.setData(sql, trans);
            }
            string hphone = homePhone.Text.Trim();
            if (hphone != "")
            {
                sql = string.Format("UPDATE Client SET homePhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Primary'", hphone, AN);
                myHKeInvestData.setData(sql, trans);
            }

            /*=========================primary client ends======================================*/
            // co holder client
            if (accountType == "common" || accountType == "survivor")
            {
                ctype = "Co";
                tt = title2.SelectedValue;
                fn = firstName2.Text.Trim();
                ln = lastName2.Text.Trim();
                db = dateOfBirth2.Text.Trim();
                el = email2.Text.Trim();
                hf = homeFax2.Text.Trim();
                coc = citizenship2.Text.Trim();
                col = legalResidence2.Text.Trim();
                hk = HKID2.Text.Trim();
                pport = passportCountry2.Text.Trim();
                if (pport == "" || !HKIDUsed2.Checked) pport = "NULL";
                es = employmentStatus2.Text.Trim();
                occu = occupation2.Text.Trim();
                if (occu == "") occu = "NULL";
                ename = employerName2.Text.Trim();
                if (ename == "") ename = "NULL";
                nobusiness = natureOfBusiness2.Text.Trim();
                if (nobusiness == "") nobusiness = "NULL";
                esq = part4CoQ1.SelectedValue;
                csq = part4CoQ2.SelectedValue;
                build = building2.Text.Trim();
                if (build == "") build = "NULL";
                stre = street2.Text.Trim();
                distr = district2.Text.Trim();

                // insert data into table [dbo].[Client] primary client
                sql = string.Format("INSERT INTO Client (accountNumber,clientType,title,firstName,lastName,dateOfBirth,email,homeFax,countryOfCitizenship,countryOfLegalResidence,HKIDPassportNumber,passportCountryOfIssue,employmentStatus,occupation,employerName,natureOfBusiness,employedByBank,clientIsDirector,Building,Street,District) " +
                    "VALUES('{0}','{1}','{2}','{3}','{4}',CONVERT(date,'{5}',103),'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                                                                AN, ctype, tt, fn, ln, db, el, hf, coc, col, hk, pport, es, occu, ename, nobusiness, esq, csq, build, stre, distr);
                myHKeInvestData.setData(sql, trans);

                ephone = employerPhone2.Text.Trim();
                if (ephone != "")
                {
                    sql = string.Format("UPDATE Client SET employerPhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Co'", ephone, AN);
                    myHKeInvestData.setData(sql, trans);
                }
                yea = years2.Text.Trim();
                if (yea != "")
                {
                    sql = string.Format("UPDATE Client SET yearsWithEmployer = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Co'", yea, AN);
                    myHKeInvestData.setData(sql, trans);
                }
                bphone = businessPhone2.Text.Trim();
                if (bphone != "")
                {
                    sql = string.Format("UPDATE Client SET businessPhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Co'", bphone, AN);
                    myHKeInvestData.setData(sql, trans);
                }
                mphone = mobilePhone2.Text.Trim();
                if (mphone != "")
                {
                    sql = string.Format("UPDATE Client SET mobilePhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Co'", mphone, AN);
                    myHKeInvestData.setData(sql, trans);
                }
                hphone = homePhone2.Text.Trim();
                if (hphone != "")
                {
                    sql = string.Format("UPDATE Client SET homePhone = '{0}' WHERE accountNumber = '{1}' AND clientType = 'Co'", hphone, AN);
                    myHKeInvestData.setData(sql, trans);
                }

            }

            myHKeInvestData.commitTransaction(trans);
            
            string message = "alter('Congratulation! Your account number is:\n" + AN +"');";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Successful Application", message, true);
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
            bool result = DateTime.TryParse(dateOfBirth.Text.Trim(), out birth);
            if (!result)
            {
                args.IsValid = false;
                dateOfBirthValidate.ErrorMessage = "The date of birth is not valid.";
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
            bool result = DateTime.TryParse(dateOfBirth2.Text.Trim(), out birth);
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
            else if (AccountType.SelectedValue != "0" || AccountType.SelectedValue != "individual")
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
    }
}
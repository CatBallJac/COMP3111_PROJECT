using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
using System.Data;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;
using System.Windows.Forms;

namespace HKeInvestWebApplication.ClientOnly
{

    public partial class BuyAndSellOrder : System.Web.UI.Page
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        HKeInvestFunctions myHkeInvestFunctions = new HKeInvestFunctions();
        private static Boolean updated = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Context.User.Identity.Name;
            decimal balance = myHkeInvestFunctions.getBalance(getAccountNumber());
            welcomeMsg.Text = "welcome " + userName + " account Number: " + getAccountNumber() + ", your balance: " + balance.ToString();
        }

        protected void rbSecurityType_SelectedIndexChanged(object sender, EventArgs e)
        {
                updateCodeData();
                updateOrderDetail();  
        }

        protected void rbIsBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rbIsBuyOrSell.SelectedItem.Text.ToString().Trim() == "buy order")
            {
                inputCode.Visible = true;
                listCode.Visible = false;
            }
            else
            {
                inputCode.Visible = false;
                listCode.Visible = true;
            }
            // update the code data
            updateCodeData();
            updateOrderDetail();

        }

        protected void ddlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            
            // msg.Text = securityType + securityCode + "selected";
            // string sql = string.Format("select name from [{0}] where [code] = '{1}' ", securityType, securityCode);
            // string securityName = myExternalData.getData(sql).Rows[0].Field<string>("name");
            DataTable dtSecurities = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            string securityName = "";
            if (dtSecurities != null)
            {
                securityName = dtSecurities.Rows[0].Field<string>("name");
            }
            else
            {
                securityName = "no security found for code " + securityCode;
            }

            LabelSecurityNametxt.Text = securityName;
            updated = true;

            if (rbIsBuyOrSell.SelectedValue.Trim() == "sell order")
            { showSecuritySharesOwn(); }
        }

        protected void rbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderType = rbOrderType.SelectedValue.ToString().Trim();
            divLimitPirce.Visible = false;

            // divStopPrice.Visible = false;
            divStopPrice.Visible = false;

            if (orderType == "market")
            {
            }
            else if (orderType == "limit")
            {
                divLimitPirce.Visible = true;
            }
            else if (orderType == "stop limit")
            {
                divStopPrice.Visible = true;
                // PanelStopPrice.Enabled = true;

                divLimitPirce.Visible = true;
            }
            else if (orderType == "stop")
            {
                divStopPrice.Visible = true;
                // PanelStopPrice.Enabled = true;
            }


        }

        // ---- event trigged by index changed ----

        /// <summary>
        /// based on the type of the order(buy and sell) and security type
        /// different information input are required, which is controled by `updateOrderDetail`
        /// for buy bond/unit trust:
        /// for buy stock:
        /// for sell bond/unit trust:
        /// for sell stock:
        /// </summary>

        // view control:
        protected void updateOrderDetail()
        {
            divStockOrderDetail.Visible = false;
            divBondOrderDetail.Visible = false;
            divSellStockOrder.Visible = false;
            divBuyStockOrder.Visible = false;
            divBondOrderDetail_sell.Visible = false;
            divBondOrderDetail_buy.Visible = false;
            string IsBuyorSell = rbIsBuyOrSell.SelectedValue.Trim();
            string securityType = rbSecurityType.SelectedValue.Trim();

            // visual different part based on oeder type
            if (securityType == null || IsBuyorSell == null) return;

            else if (securityType == "bond" || securityType == "unit trust")
            {
                divBondOrderDetail.Visible = true;
                if (IsBuyorSell == "buy order")
                {
                    divBondOrderDetail_buy.Visible = true;
                }else if(IsBuyorSell == "sell order")
                {
                    divBondOrderDetail_sell.Visible = true;
                }
               
            }
            else if (securityType == "stock" && IsBuyorSell == "buy order")
            {
                divStockOrderDetail.Visible = true;
                divBuyStockOrder.Visible = true;
            }
            else if (securityType == "stock" && IsBuyorSell == "sell order")
            {
                divStockOrderDetail.Visible = true;
                divSellStockOrder.Visible = true;
            }


        }
        // update code data based on the security type and buy/sell order
        protected void updateCodeData()
        {
            // init

            LabelSecurityNametxt.Text = "";
            updated = false;
            ddlCode.Items.Clear();
            // load
            string IsBuyorSell = rbIsBuyOrSell.SelectedValue.Trim();
            string securityType = rbSecurityType.SelectedValue.Trim();
            ddlCode.Items.Add("-- choose code of available security --");
            if (IsBuyorSell == null || securityType == null) return;
            if (IsBuyorSell == "buy order")
                updateBuyCodeData(securityType);
            else if(IsBuyorSell == "sell order")
                updateSellCodeData(securityType);

            LabelSellLimit.Visible = false;
            TextMaxiShares.Visible = false;
        }

        protected void updateBuyCodeData(string securityType)
        {

            // updateBuyCodeData(securityType);
            DataTable dtToBuy = myExternalFunctions.getSecuritiesData(securityType);
            msg.Text = "there are " + dtToBuy.Rows.Count.ToString() + " securities available";
            if (updated == false && dtToBuy != null && dtToBuy.Rows.Count != 0)
            {
                foreach (DataRow row in dtToBuy.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
            updated = true; // after update
        }

        protected void updateSellCodeData(string securityType)
        {
            string accountNumber = getAccountNumber();
            string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}'", accountNumber, securityType);
            DataTable dtToSell = myHKeInvestData.getData(sql);
            msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            // TODO: UPDATE THE CODE DATE BASED ON THE DATEBASE
            // msg.Text = "there are " + dtToSell.Rows.Count.ToString() + " securities available";
            if (updated == false && dtToSell != null && dtToSell.Rows.Count != 0)
            {
                foreach (DataRow row in dtToSell.Rows)
                {
                    ddlCode.Items.Add(row["code"].ToString().Trim());
                }
            }
            updated = true; // after update

        }

        // ---- information provider ----
        // check for the simultaneous sell orders for the same security & security amount the client own
        // return the maximum amount of shares the client can sell
        protected string showSecuritySharesOwn()
        {

            string securityCode = ddlCode.SelectedValue.ToString().Trim();
            string securityType = rbSecurityType.SelectedValue.Trim();
            if (securityCode == "" || securityType == "" || securityCode == null || securityType == null)
            {
                LabelSellLimit.Visible = true;
                TextMaxiShares.Visible = true;
                LabelSellLimit.Text = "security code: " + securityCode + 
                                            "selected, order type: " + securityType;
                // TextMaxiShares.Text = shares.ToString().Trim();
                return null;

            }
            string accountNumber = getAccountNumber();

            // string sql = string.Format("select * from [SecurityHolding] where [accountNumber] = '{0}' and [type] = '{1}' and [code] = '{2}'", accountNumber, securityType, securityCode);
            // shares
            // DataTable dtShareOwn = myHKeInvestData.getData(sql);
            // if (myHKeInvestData.getData(sql).Rows == null) return null;
            // decimal shares = myHKeInvestData.getData(sql).Rows[0].Field<decimal>("shares");

            decimal shares = checkMaxiSharesSell(accountNumber, securityType, securityCode);
            //msg.Text = "have shared " + shares.ToString();
            LabelSellLimit.Visible = true;
            TextMaxiShares.Visible = true;
            LabelSellLimit.Text = "total amount to sell should be less than ";
            TextMaxiShares.Text = shares.ToString().Trim();
            return shares.ToString();
        }

        protected string getAccountNumber()
        {
            string userName = Context.User.Identity.Name;
            string sql = string.Format("select accountNumber from [Account] where [userName] = '{0}' ", userName);
            string accountNumber = myHKeInvestData.getData(sql).Rows[0].Field<string>("accountNumber");

            return accountNumber.ToString().Trim();
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if(!IsValid)
            {
                return;
            }

            string accountNumber = getAccountNumber();
            if(accountNumber == null || accountNumber == "")
            {
                LabelResult.Text = "can not get accountNumber for user " + Context.User.Identity.Name;
                return;
            }
            // get all text data:
            string code = ddlCode.Text.Trim();
            string codeInput = TextCode.Text.Trim();
            int count = 0;
            foreach (Char c in codeInput.ToCharArray())
            {
                if (c == '0')
                {
                    count++;
                }
                else break;
            }
            codeInput = codeInput.Substring(count, codeInput.Length - count);


            string shares_buyStock = TextBuyShares.Text.Trim() + "00"; // need to * 100
            string shares_sellStock = TextSellShares.Text.Trim();
            // for stock order
            string orderType = rbOrderType.Text.Trim();
            string expiryDay = ddlExpiryDay.Text.Trim();
            string allOrNone = rbAllOrNone.Text.Trim();

            string stopPrice = TextStopPrice.Text.Trim();
            /// string marketPrice = TextMarketPrice.Text.Trim();
            string limitPrice = TextLimitPrice.Text.Trim();

            // for stock high price / low price
            // if buy order & stock order & allornone & stop limit || limit 
            string highPrice = limitPrice;
            // if sell order & stock order & allornone & stop limie || limit
            string lowPirce = limitPrice;
            string numShown = "";
            // for bonds:
            string amount_buyBond = TextAmount.Text.Trim();
            string shares_sellBond = TextShares.Text.Trim();
            string isBuyOrSell = rbIsBuyOrSell.Text.Trim(); // buy order; sell order
            string securityType = rbSecurityType.Text.Trim();
            string allmsg = string.Format("0{0}, 1{1},2{2},3{3},4{4},5{5},6{6},7{7}",
                                             code, shares_buyStock, shares_sellStock, orderType,
                                             expiryDay, allOrNone, stopPrice, limitPrice);
            //System.Diagnostics.Debug.WriteLine(allmsg);
            // Response.Write(allmsg);
            // msg.Text = allmsg; // securityType + isBuyOrSell + " button click ";
            string referenceNumber = "";
            // Show("code is " + code + " input code " + codeInput);
            if (securityType == "stock")
            {
                string shares_stock;
                if (isBuyOrSell == "buy order")
                {
                    shares_stock = shares_buyStock;
                    referenceNumber = submitStockOrder(codeInput, shares_stock, orderType, expiryDay, allOrNone, limitPrice, stopPrice, accountNumber, isBuyOrSell);

                }
                else
                {
                    shares_stock = shares_sellStock;
                    referenceNumber = submitStockOrder(code, shares_stock, orderType, expiryDay, allOrNone, limitPrice, stopPrice, accountNumber, isBuyOrSell);

                }
                numShown = shares_stock;

            }
            else
            {
           
                if (isBuyOrSell == "buy order")
                {
                    numShown = amount_buyBond;
                    referenceNumber = submitBondOrder(codeInput, amount_buyBond, shares_sellBond, accountNumber, securityType, isBuyOrSell);

                }
                else
                {
                    numShown = shares_sellBond;
                    referenceNumber = submitBondOrder(code, amount_buyBond, shares_sellBond, accountNumber, securityType, isBuyOrSell);

                }

            }


            if (referenceNumber != "" && referenceNumber != null)
            {
                LabelResult.Text = "account: " + accountNumber + ": " + securityType + " "+ isBuyOrSell + ", amount/shares " + numShown + " submitted, reference number: " + referenceNumber;
            }else
            {
                
                LabelResult.Text = "account: " + accountNumber + ", order submitted failed" + ", code" + codeInput + code;
            }

        }

        public decimal checkMaxiSharesSell(string accountNumber, string securityType, string securityCode)
        {
            string sql = string.Format("select * from [SecurityHolding]" +
                " where [accountNumber] = '{0}' and [type] = '{1}' and [code] = '{2}'",
                accountNumber, securityType, securityCode);
            // shares
            DataTable dtShareOwn = myHKeInvestData.getData(sql);
            if (dtShareOwn == null || dtShareOwn.Rows.Count == 0) return -1;

            decimal shares = myHKeInvestData.getData(sql).Rows[0].Field<decimal>("shares");

            string sqlInternal = string.Format("select * from [Order] where ([status]='pending' or [status]='partial')" +
                " and [accountNumber] = '{0}' and [securityType] = '{1}' and [securityCode] = '{2}' and [buyOrSell] = '{3}' ",
                accountNumber, securityType, securityCode, "sell");
            
            DataTable dTPreviousOrder = myHKeInvestData.getData(sqlInternal);
            if (dTPreviousOrder == null || dTPreviousOrder.Rows.Count == 0) return shares;
            int numOrderBefore = dTPreviousOrder.Rows.Count;
            decimal sharedInOrdering = 0;
            if (numOrderBefore != 0)
            {
                DataTable dtShareInOrder = myHKeInvestData.getData(sql);
                if (myHKeInvestData.getData(sqlInternal).Rows == null) return -1;

                for (int i = 0; i < numOrderBefore; i++)
                {
                    sharedInOrdering = sharedInOrdering + dTPreviousOrder.Rows[i].Field<decimal>("shares");
                }
            }
            Show("selling: previous :" + shares.ToString() + " pending order: " + sharedInOrdering.ToString());
            return shares - sharedInOrdering;
        }

        // submit order:

        public string submitBondOrder(string code, string amount, string shares, string accountNumber, string securityType, string buyOrSell)
        {

            // Inserts a bond buy order into the Order table.
            // Check if input is valid.


            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            // Submit the order.
            string referenceNumber;

            if (securityType == "bond" && buyOrSell == "sell order")
            {
                referenceNumber = myExternalFunctions.submitBondSellOrder(code, shares);
            }
            else if (securityType == "unit trust" && buyOrSell == "sell order")
            {
                referenceNumber = myExternalFunctions.submitUnitTrustSellOrder(code, shares);
            }
            else if (securityType == "bond" && buyOrSell == "buy order")
            {

                referenceNumber = myExternalFunctions.submitBondBuyOrder(code, amount);
            }
            else if (securityType == "unit trust" && buyOrSell == "buy order")
            {
                referenceNumber = myExternalFunctions.submitUnitTrustBuyOrder(code, amount);
            }
            else return null;

            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;
            // all attribute:
            // (referenceNumber, buyOrSell, securityType, securityCode, dateSubmitted, status, 
            // shares, amount, stockOrderType,
            // expiryDay, allOrNone, limitPrice, stopPrice, accountNumber, fee)

            if (buyOrSell == "buy order")
            {
                buyOrSell = "buy";
                shares = "NULL";
            }
            else
            {
                buyOrSell = "sell";
                amount = "NULL";
            }
            string sql = string.Format(
              "INSERT INTO [Order] " +
              "(referenceNumber, buyOrSell, securityType, securityCode, dateSubmitted, accountNumber," +
              "shares, amount)" +
              "VALUES" +
              "({0},'{1}','{2}','{3}','{4}','{5}'" +
              ",{6},{7})",
               referenceNumber, buyOrSell, securityType, code, dateNow, accountNumber,
               shares.Trim(), amount.Trim()
             );

            //System.Web.HttpContext.Current.Response.Write(sql);

            submitOrder(sql);
            return referenceNumber;
        }

        public string submitStockOrder(string code, string shares, string orderType, string expiryDay, string allOrNone, string limitPrice, string stopPrice, string accountNumber, string buyOrSell)
        {
            // Inserts a stock buy order into the Order table.
            // Check if input is valid.
            string securityType = "stock";
            orderType = orderType.Trim().ToLower();
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            string referenceNumber;
            if (buyOrSell == "buy order")
            {
                buyOrSell = "buy";
                referenceNumber = myExternalFunctions.submitStockBuyOrder(code, shares, orderType, expiryDay, allOrNone, limitPrice, stopPrice);

            }
            else
            {
                buyOrSell = "sell";
                referenceNumber = myExternalFunctions.submitStockSellOrder(code, shares, orderType, expiryDay, allOrNone, limitPrice, stopPrice);
            }
            // Construct the basic SQL statement.
            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;
            // all attribute:
            // (referenceNumber, buyOrSell, securityType, securityCode, dateSubmitted, status, 
            // shares, amount, stockOrderType,
            // expiryDay, allOrNone, limitPrice, stopPrice, accountNumber, fee)

            // Check for order type and set SQL statement accordingly.
            if (orderType == "market")
            {
                limitPrice = "NULL";
                stopPrice = "NULL";
            }
            else if (orderType == "limit")
            {
                stopPrice = "NULL";
            }
            else if (orderType == "stop")
            {
                limitPrice = "NULL";
            }


            string sql = string.Format(
               "INSERT INTO [Order] " +
               "(referenceNumber, buyOrSell, securityType, securityCode, dateSubmitted, accountNumber, " +
               "shares, stockOrderType, expiryDay, allOrNone," +
               "limitPrice, stopPrice)" +
               "VALUES" +
               "({0},'{1}','{2}','{3}','{4}','{5}'" +
               ",'{6}','{7}','{8}','{9}'" +
               ",{10},{11})",
               referenceNumber, buyOrSell, securityType, code, dateNow, accountNumber,
               shares.Trim(), orderType, expiryDay, allOrNone.ToUpper(),
               limitPrice.Trim(), stopPrice.Trim()
               );
            
            //System.Web.HttpContext.Current.Response.Write(sql);
            submitOrder(sql);
            return referenceNumber;
        }

        //private 

        private void submitOrder(string sql)
        {
            Show("aql: " + sql);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData(sql, trans);
            myHKeInvestData.commitTransaction(trans);

            return;
        }





        // ****************************
        // *   custormer validation   *
        // ****************************
        protected void cvCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlCode.SelectedValue.ToString().Trim() == "-- choose code of available security --" || ddlCode.SelectedValue == null )
            {
                cvCode.ErrorMessage = "please select code";
                //msg.Text = "please select code";
                args.IsValid = false;
            }
           
        }

        // sell bond/unit trust shares
        protected void cvSellShares_ServerValidate(object source, ServerValidateEventArgs args)
        {
            decimal numberSell = 0;
            string shares_sellStock = TextSellShares.Text.ToString().Trim();

            decimal sharesOwn = 0;
            if (!decimal.TryParse(TextMaxiShares.Text.ToString().Trim(), out sharesOwn) ||
                !decimal.TryParse(shares_sellStock, out numberSell))
            {
                cvShares.ErrorMessage = "please input decimal number";
                args.IsValid = false;
            }

//            decimal.TryParse(TextMaxiShares.Text.ToString().Trim(), out sharesOwn);
//            msg.Text = sharesOwn.ToString();
//            decimal.TryParse(shares, out number);

            if (numberSell > sharesOwn)
            {
                cvSellShares.ErrorMessage = numberSell.ToString() + " amount exceed maximum " + sharesOwn.ToString();
                args.IsValid = false;
            }
            // msg.Text = shares_sellStock.ToString();
            // msg.Text = "number is " + shares_sellStock.ToString();



        }

        // sell stock shares
        protected void cvShares_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string shares_sellBonds = TextShares.Text.Trim();
            decimal number;

            string shares = showSecuritySharesOwn();
            decimal sharesOwn;

            if (!decimal.TryParse(TextMaxiShares.Text.ToString().Trim(), out sharesOwn) ||
                !decimal.TryParse(shares_sellBonds, out number))
            {
                cvShares.ErrorMessage = "please input decimal number";
                args.IsValid = false;
            }


            decimal.TryParse(TextMaxiShares.Text.ToString().Trim(), out sharesOwn);
            decimal.TryParse(shares_sellBonds, out number);

            if (!(sharesOwn > 0))
            {
                msg.Text = "you have sell all the shares in previous order, no sell anymore";
            }

            // msg.Text = sharesOwn.ToString();
            if (number > sharesOwn)
            {
                cvShares.ErrorMessage = "invalid! Buy shares: " + shares_sellBonds + " exceed maximum value: " + shares;
                args.IsValid = false;
            }

            // msg.Text = "number is " + number.ToString();
            // LabelResult.Text = number.ToString() + LabelResult.Text + shares_buyStock;

        }

        protected void cvLimitPrice_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string stopPricestr = TextStopPrice.Text.Trim();
            decimal stopPrice = 0;

            string limitPricestr = TextLimitPrice.Text.Trim();
            decimal limitPrice = 0;

            if (rbOrderType.SelectedValue.ToString().Trim() != "stop limit") return;

            if (stopPricestr == "" || stopPricestr == null)
            {
                cvLimitPrice.ErrorMessage = "price required";
                args.IsValid = false;
            }
            else decimal.TryParse(stopPricestr, out stopPrice);

            if(limitPricestr == "" || limitPricestr == null)
            {
                cvLimitPrice.ErrorMessage = "price required";
                args.IsValid = false;
            }
            else decimal.TryParse(limitPricestr, out limitPrice);

            if (rbIsBuyOrSell.SelectedValue.ToString().Trim() == "buy order" && stopPrice >= limitPrice)
            {
                cvLimitPrice.ErrorMessage = "invalid! for buy order, stop price: ("+ stopPrice.ToString() + ") should be smaller than limit price (" + limitPrice.ToString() + ")";
                args.IsValid = false;
            }else if (rbIsBuyOrSell.SelectedValue.ToString().Trim() == "sell order" && stopPrice <= limitPrice)
            {
                cvLimitPrice.ErrorMessage = "invalid! for sell order, stop price should be larger than limit price";
                args.IsValid = false;
            }


        }

        protected void cvCodeInput_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string securityCode = TextCode.Text.ToString().Trim();
            int count = 0;
            foreach (Char c in securityCode.ToCharArray())
            {
                if (c == '0')
                {
                    count++;
                }
                else break;
            }
            securityCode = securityCode.Substring(count, securityCode.Length - count);
            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            if (securityType == "" || rbSecurityType == null) return;
            DataTable dtSecurities = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            if (dtSecurities == null || dtSecurities.Rows.Count == 0)
            {
                // showError();
                cvCodeInput.ErrorMessage = "no security found for code " + securityCode;
                args.IsValid = false;
                return;
            }

            string securityName = dtSecurities.Rows[0].Field<string>("name");
            LabelSecurityNametxt.Text = securityName;


        }

        private void Show(string msg)
        {
           // MessageBox.Show(msg);
        }

        protected void cvAmount_ServerValidate(object source, ServerValidateEventArgs args)
        {
            decimal buyAmount = 0;
            if(!decimal.TryParse(TextAmount.Text.ToString().Trim(), out buyAmount))
            {
                cvAmount.ErrorMessage = "amount should be decimal";
                args.IsValid = false;
                return;
            }
            string accountNumber = getAccountNumber();
            string buyOrSell;
            if (rbIsBuyOrSell.SelectedValue.ToString() == "buy order")
            {
                buyOrSell = "buy";
            } else buyOrSell = "sell";

            decimal fee = myHkeInvestFunctions.calculateBondUnitTrustFees(buyOrSell, accountNumber, buyAmount.ToString());
            decimal balance = myHkeInvestFunctions.getBalance(getAccountNumber());
            if (buyAmount + fee> balance)
            {
                cvAmount.ErrorMessage = "balance not enough! you have balance: " + balance.ToString() + " but need " + buyAmount.ToString() + " and fee :" + fee.ToString();
                args.IsValid = false;
                return;
            }

            string securityType = rbSecurityType.SelectedValue.ToString().Trim();
            string securityCode = ddlCode.SelectedValue.ToString().Trim();

            DataTable dtSecurity = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            if (dtSecurity == null || dtSecurity.Rows.Count == 0) return;
            string bases;
            if (securityType == "stock")
            {
                bases = "HKD";
            }
            else
            {
                bases = dtSecurity.Rows[0]["base"].ToString().Trim();
            }

            decimal price = myExternalFunctions.getSecuritiesPrice(securityType, securityCode)*myExternalFunctions.getCurrencyRate(bases);

            if (buyAmount <= (decimal)0.01 * price)
            {
                cvAmount.ErrorMessage = "you have to buy at least 0.01 shares based on current price!";
                args.IsValid = false;
                return;
            }



        }
    }
}
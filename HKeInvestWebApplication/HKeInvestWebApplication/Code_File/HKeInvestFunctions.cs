using System;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;
using System.Web;



namespace HKeInvestWebApplication.Code_File
{

    public class HKeInvestFunctions
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        // getSecurities -> XXX

        /* `getSharesOfSecurityHolding`
           return -1/0 if no securityHolding found
           else return the amount of security holds

            `getPendingorPartialOrder`:
                return the table in our order db which status are 'pending' or  'partial'

            `completeBondOrder`: fetch the complete bond/unit trust order, update fee & securityHolding & balance & order status
            
        */
        public decimal getSharesOfSecurityHolding(string accountNumber, string type, string code)
        {
            DataTable dtShares = new DataTable();
            dtShares = myHKeInvestData.getData("select [shares] from [SecurityHolding] where [accountNumber] = '" + accountNumber +
                "' and [type] = '" + type + "' and [code] = '" + code + "'");
            // Return -1 if no result is returned.
            if (dtShares == null)
            {
                return -1;
            }
            else if (dtShares.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                return dtShares.Rows[0].Field<decimal>("shares");
            }
        }

        public DataTable getPendingorPartialOrder()
        {
            // Returns all pending or partial orders
            DataTable dTpendingOrder = new DataTable();
            string sql = "select * from [Order] where ([status]='pending' or [status]='partial')";

            dTpendingOrder = myHKeInvestData.getData(sql);

            // Return null if no result is returned.
            if (dTpendingOrder == null || dTpendingOrder.Rows.Count == 0)
            {
                return null;
            }
            return dTpendingOrder;
        }


        /*
          handle each row of the pending order table:
          
          1. for bond/unit trust securities, 
               check the numOfTotalTransaction in the [transaction] table in external system
               if # == 1:
                   call `completeBondOrder` 
               

          2. for stock securities, 
             check if out of date:
             if ddl not past, do nothing
             else if ddl past, 
             {  
                calculate the total executedAmountOfShares in the [transaction] table in external system
                1. for allorNone order:
                    if executedAmountOfShares < placedAmountOfShares: 
                        order 'canceled'
                    else: 
                        call `completeStockOrder`:
                            update all the transaction, 
                            update fee: calculate the stock service fee based on the price
                            update order status:
                            update balance & security holding

                2. for not allorNone order:
                    if executedAmountOfShares == 0: 
                        order 'canceled'
                        return
                    eles if executedAmountOfShares < placedAmountOfShares: 
                        order 'partially executed'
                    eles if executedAmountOfShares == executedAmountOfShares:
                        order 'complete executed'

                    call `completeStockOrder`:
                END
               }  
         */

        public void completeBondOrder(string referenceNumber, string amount, string securityCode, string buyOrSell, string accountNumber)
        {
            DataTable dTtransaction = myExternalFunctions.getOrderTransaction(referenceNumber);
            if (dTtransaction == null || dTtransaction.Rows.Count == 0)
            {
                return;
            }
            string tansactionNumber = dTtransaction.Rows[0]["tansactionNumber"].ToString().Trim();
            string executeDate = dTtransaction.Rows[0]["executeDate"].ToString().Trim();
            string executeShares = dTtransaction.Rows[0]["executeShares"].ToString().Trim();
            string executePrice = dTtransaction.Rows[0]["executePrice"].ToString().Trim();

            insertTransaction(tansactionNumber, referenceNumber, executeDate, executeShares, executePrice);

            decimal dAmount;
            decimal.TryParse(amount, out dAmount);

            decimal fee = calculateUnitTrustFees("buy", accountNumber, amount);
            decimal totalAmount = dAmount + fee;

            updateFee(Convert.ToString(fee), referenceNumber);
            updateSecurityHolding(accountNumber, "unit trust", securityCode, Convert.ToDecimal(executeShares));
            updateBalance(accountNumber, -totalAmount);
            updateOrderStatus(referenceNumber, "completed");
        }
        
        // updateOrderStatus
        public void updateOrderStatus(string referenceNumber, string status)
        {
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [Order] set [status]='" + status + "' where [referenceNumber]='" + referenceNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }


        // `getBalance`: return the balance of the account 
        public decimal getBalance(string accountNumber)
        {
            // Returns the balance of account number.
            int number;
            if (int.TryParse(accountNumber, out number))
            {
                DataTable dtBalance = myHKeInvestData.getData("select [balance] from [Account] where [referenceNumber]='" + accountNumber.Trim() + "'");
                // Return null if no result is returned.
                if (!(dtBalance == null || dtBalance.Rows.Count == 0))
                {
                    return Convert.ToDecimal(dtBalance.Rows[0].Field<string>("balance"));
                }
            }
            return 0;
        }

        // `getCurrentValueOfSecuries`: add up all the value of the current securityHoding: 
        // TODO: need currency conversion?
        public decimal getCurrentValueOfSecuries(string accountNumber)
        {
            int number;
            decimal value = 0;
            if (int.TryParse(accountNumber, out number))
            {
                DataTable dtSecurities = myHKeInvestData.getData("select * from [SecurityHolding] where [accountNumber] = '" + accountNumber.Trim() + "' ");
                // Return 0 if no result is returned.
                if (dtSecurities == null || dtSecurities.Rows.Count == 0)
                {
                    return value;
                }
                foreach (DataRow row in dtSecurities.Rows)
                {
                    string securityType = row["type"].ToString().Trim();
                    decimal price = myExternalFunctions.getSecuritiesPrice(securityType, row["code"].ToString());
                    value += price * Convert.ToDecimal(row["shares"]);
                }
            }
            return value;
        }

        // `getAssets`: return all valuesOfSecurityHolds + balance
        public decimal getAssets(string accountNumber)
        {
            int number;
            if (int.TryParse(accountNumber, out number))
            {
                return getBalance(accountNumber) + getCurrentValueOfSecuries(accountNumber);
            }
            return 0;
        }

        // `calculateBondFees`: return the service fee of bonds
        private decimal calculateBondFees(string buyOrSell, string accountNumber, string amount)
        {
            decimal dAmount = Convert.ToDecimal(amount);
            decimal assets = getAssets(accountNumber);
            if (assets < (decimal)500000)
            {
                if (buyOrSell == "buy")
                {
                    return dAmount * (decimal)0.05;
                }
                else if (buyOrSell == "sell")
                {
                    return 100;
                }
            }
            else if (assets >= (decimal)500000)
            {
                if (buyOrSell == "buy")
                {
                    return dAmount * (decimal)0.03;
                }
                else if (buyOrSell == "sell")
                {
                    return 50;
                }
            }
            return 0;
        }

        // calculateUnitTrustFees
        private decimal calculateUnitTrustFees(string buyOrSell, string accountNumber, string amount)
        {
            decimal dAmount = Convert.ToDecimal(amount);
            decimal assets = getAssets(accountNumber);
            if (assets < (decimal)500000)
            {
                if (buyOrSell == "buy")
                {
                    return dAmount * (decimal)0.05;
                }
                else if (buyOrSell == "sell")
                {
                    return 100;
                }
            }
            else if (assets >= (decimal)500000)
            {
                if (buyOrSell == "buy")
                {
                    return dAmount * (decimal)0.03;
                }
                else if (buyOrSell == "sell")
                {
                    return 50;
                }
            }
            return 0;
        }

        // `calculateStockFees`: return the service fee of stocks
        private decimal calculateStockFees(string stockOrderType, string accountNumber, string amount)
        {
            decimal dAmount = Convert.ToDecimal(amount);
            decimal assets = getAssets(accountNumber);
            decimal fee = 0;
            if (assets < (decimal)1000000)
            {
                if (stockOrderType == "market")
                {
                    fee = dAmount * (decimal)0.004;
                }
                else if (stockOrderType == "limit" || stockOrderType == "stop")
                {
                    fee = dAmount * (decimal)0.006;
                }
                else if (stockOrderType == "stop limit")
                {
                    fee = dAmount * (decimal)0.008;
                }

                if (fee > (decimal)150)
                    return fee;
                else return (decimal)150;
            }
            else if (assets >= (decimal)1000000)
            {
                if (stockOrderType == "market")
                {
                    fee = dAmount * (decimal)0.002;
                }
                else if (stockOrderType == "limit" || stockOrderType == "stop")
                {
                    fee = dAmount * (decimal)0.004;
                }
                else if (stockOrderType == "stop limit")
                {
                    fee = dAmount * (decimal)0.006;
                }

                if (fee > (decimal)100)
                    return fee;
                else return (decimal)100;
            }
            return 0;
        }

        // insertTransaction
        // updateFee
        // updateSecurityHolding
        // updateBalance
        private void insertTransaction(string transactionNumber, string referenceNumber, string executeDate, string executeShares, string executePrice)
        {
            MessageBox.Show("transaction detected" + transactionNumber);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("insert into [Transaction]([transactionNumber], [referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                transactionNumber + ", '" + referenceNumber + "', " + executeDate + ", '" + executeShares + ", '" + executePrice + "')", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        private void updateFee(string fee, string referenceNumber)
        {
            string Fee = Convert.ToString(fee);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [order] set [fee]='" + Convert.ToString(Fee).Trim() + "' where [referenceNumber]='" + referenceNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        private void updateSecurityHolding(string accountNumber, string type, string code, decimal shares)
        {
            decimal ownedShares = getSharesOfSecurityHolding(accountNumber, type, code);
            decimal updatedShares = ownedShares + shares;
            DataTable dtSecurity = myExternalFunctions.getSecuritiesByCode(type, code);
            if (dtSecurity == null) return;

            string name = dtSecurity.Rows[0].Field<string>("price").Trim();
            string bases = dtSecurity.Rows[0].Field<string>("base").Trim();

            if (ownedShares == 0)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                myHKeInvestData.setData("insert into [SecurityHolding] values(" + accountNumber + "','" + type + "', '" + code
            + "','" + name + "', '" + Convert.ToString(shares).Trim() + "', '" + bases + "')", trans);
                myHKeInvestData.commitTransaction(trans);
                return;
            }
            else if (ownedShares > 0)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                myHKeInvestData.setData("update [SecurityHolding] set [shares]='" + Convert.ToString(updatedShares).Trim() + "' where [accountNumber] = '" + accountNumber +
                "' and [type] = '" + type + "' and [code] = '" + code + "'", trans);
                myHKeInvestData.commitTransaction(trans);
            }
            return;
        }

        private void updateBalance(string accountNumber, decimal amount)
        {
            decimal balance = getBalance(accountNumber);
            decimal updatedBalance = balance + amount;

            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [AccountTemp] set [balance]='" + Convert.ToString(updatedBalance).Trim() + "' where [accountNumber] = '" + accountNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }
    

// -----------------------------------
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
                " [accountNumber] = '{0}' and [securityType] = '{1}' and [securityCode] = '{2}' ",
                accountNumber, securityType, securityCode);
            decimal numOrderBefore = myHKeInvestData.getAggregateValue(sqlInternal);
            decimal sharedInOrdering = 0;
            if(numOrderBefore != 0)
            {
                DataTable dtShareInOrder = myHKeInvestData.getData(sql);
                if (myHKeInvestData.getData(sqlInternal).Rows == null) return -1;
                for (int i = 0; i < (int)(numOrderBefore); i++)
                {
                    sharedInOrdering = sharedInOrdering + dtShareInOrder.Rows[i].Field<decimal>("shares");
                }
            }
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
            // System.Web.HttpContext.Current.Response.Write(sql);
            // System.Diagnostics.Debug.WriteLine(sql);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData(sql, trans);
            //            string referenceNumber = myExternalFunctions.submitOrder(sql);
            //myExternalData.getOrderReferenceNumber("select max([referenceNumber]) from [Order]", trans);
            myHKeInvestData.commitTransaction(trans);
            //            return referenceNumber;
            //            return sql;
            return;
        }

        private void insertTransaction(string tansactionNumber, string referenceNumber, string executeDate, string executeShares, string executePrice, string accountNumber)
        {
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("insert into [Transaction]([transactionNumber], [referenceNumber], [executeDate], [executeShares], [executePrice], [[accountNumber]]) values (" +
                tansactionNumber + ", '" + referenceNumber + "', " + executeDate + ", '" + executeShares + ", '" + executePrice + ", '" + accountNumber + "')", trans);
            myHKeInvestData.commitTransaction(trans);
        }

    }
}
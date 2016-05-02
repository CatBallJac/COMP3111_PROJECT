﻿using System;
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
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        /* 
          `getSharesOfSecurityHolding`
                return -1/0 if no securityHolding found
                else return the amount of security holds

          `getPendingorPartialOrder`:
                return the table in our order db which status are 'pending' or  'partial'

           
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


        // *****************************
        // *     for stock order only  *
        // *****************************
        // check the order placed day and the current day, compared with order expiryDay 
        public bool isExpired(string referenceNumber)
        {
            DataTable dtStatus = myHKeInvestData.getData("select * from [Order] where [referenceNumber]='" + referenceNumber.Trim() + "'");

            //# TODO: CHECK THE EXPIRY DATE OF THE ORDER
            if (dtStatus == null || dtStatus.Rows.Count == 0) return false; // pending forever???

            //# TODO: CALCULATE IF EXPIRY DATE PASS
            string expiryDayStr = dtStatus.Rows[0]["expiryDay"].ToString();

            int expiryDay  = 0;
            int.TryParse(expiryDayStr, out expiryDay);
            DateTime dateSubmit = (DateTime)dtStatus.Rows[0].Field<DateTime>("dateSubmitted");

            DateTime dateNow = DateTime.Now;
            int dateDiff = dateNow.Subtract(dateSubmit).Days;
            if (dateDiff >= expiryDay)
            {
                Show("detect out of date order: " + dateDiff + " ecpiry : " + expiryDay);
                return true;
            }
            else return false;
        }

        // check executed shares of the stock Order
        // for allOrNone order, if no transaction or shares not enough, failed
        // for not allOrNone order, if no transaction, failed
        public string checkStockOrderTranscation(string referenceNumber, string allOrNone)
        {
            DataTable dTorderTransaction = myExternalFunctions.getOrderTransaction(referenceNumber);

            // 1. no transaction on due day, but external order status still pending.... (can it happen?)
            if (dTorderTransaction == null || dTorderTransaction.Rows.Count == 0)
            {
                return "cancelled"; // cancelled, no transaction;
            };

            // 2. summing up all the transactions
            decimal totalSharesTranscation = 0;

            foreach (DataRow rowTansaction in dTorderTransaction.Rows)
            {
                decimal sharesTranscation = rowTansaction.Field<decimal>("shares");
                totalSharesTranscation += sharesTranscation;   
            }

            DataTable dtShares = new DataTable();
            dtShares = myHKeInvestData.getData("select * from [Order] where [referenceNumber] = '" + referenceNumber + "'");
            if (dtShares == null || dtShares.Rows.Count == 0) return "cancelled";
            decimal sharesPlaced = dtShares.Rows[0].Field<decimal>("shares");
            if (allOrNone == "Y")
            {
                if (sharesPlaced > totalSharesTranscation)
                {
                    return "cancelled"; // shares not enough, cancelled
                }
                else return "completed"; // shares enough, complete
            }
            else // (allOrNone == "N")
            {
                if (totalSharesTranscation <= 0) return "cancelled"; // has shares executed, partial executed or complete executed
                else if (sharesPlaced > totalSharesTranscation) return "partial";
                else return "completed";
                // no shares executeed, cancelled
            }

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

        // *********************************************************************
        // *     deal with internal system's update  && some helper function   *
        // *********************************************************************


        /*
            `completeBondOrder`: fetch the complete bond/unit trust order, update fee & securityHolding & balance & order status
            `completeStockOrder`: fetch the complete stock order, update fee & securityHolding & balance & order status
        */
        // for completed order or partial order
        public void stockTransactionHandler(string referenceNumber, string allOrNone, string buyOrSell, string orderType, string accountNumber, string securityCode)
        {
            DataTable dTstockTransaction = myExternalFunctions.getOrderTransaction(referenceNumber);
            if (dTstockTransaction == null || dTstockTransaction.Rows.Count == 0) return; // error state
            string tansactionNumber;
            string executeDate;
            decimal executeShares = 0;
            decimal executePrice = 0;
            decimal totalAmount = 0;
            decimal totalShares = 0;
            foreach (DataRow row in dTstockTransaction.Rows)
            {
                tansactionNumber = row["transactionNumber"].ToString().Trim();
                executeDate = row["executeDate"].ToString().Trim();
                executeShares = row.Field<decimal>("executeShares");
                executePrice = row.Field<decimal>("executePrice");
                // acc the total spend on the shares
                totalAmount += executeShares * executePrice;
                totalShares += executeShares;
                // update insert all transaction
                insertTransaction(tansactionNumber, referenceNumber, executeDate, executeShares.ToString(), executePrice.ToString());

            }
            decimal totalAmountAndFee = 0;
            decimal updateSecurityHoldingReturn = 0;
            decimal updateBalanceReturn = 0;
            // #TODO: update security holdings & balance
            if (buyOrSell == "buy")
            {
                decimal fee = calculateStockFees(orderType, accountNumber, totalAmount.ToString());
                totalAmountAndFee = totalAmount + fee;

                updateFee(Convert.ToString(fee), referenceNumber);
                updateSecurityHoldingReturn = updateSecurityHolding(accountNumber, "stock", securityCode, totalShares, buyOrSell);
                updateBalanceReturn = updateBalance(accountNumber, -totalAmountAndFee);
                //updateOrderStatus(referenceNumber, "completed");
                Show("stovk buy amount is " + totalAmount + " + fee " +
                    fee.ToString() + " in total " + totalAmountAndFee.ToString() +
                    " now holding " + updateSecurityHoldingReturn.ToString() +
                    " balance " + updateBalanceReturn.ToString());
            }
            else
            {
                decimal fee = calculateStockFees(orderType, accountNumber, totalAmount.ToString());
                totalAmountAndFee = totalAmount - fee;

                updateFee(Convert.ToString(fee), referenceNumber);
                updateSecurityHoldingReturn = updateSecurityHolding(accountNumber, "stock", securityCode, totalShares, buyOrSell);
                updateBalanceReturn = updateBalance(accountNumber, totalAmountAndFee); // increase
                Show("stock sell amount is " + totalAmount + " - fee " +
                    fee.ToString() + " in total " + totalAmountAndFee.ToString() +
                    " now holding " + updateSecurityHoldingReturn.ToString() +
                    " balance " + updateBalanceReturn.ToString());

                //updateOrderStatus(referenceNumber, "completed");
            }


        }

        public void completeBondUnitTrustOrder(string referenceNumber, string amount, string securityCode, string buyOrSell, string accountNumber, string type)
        {
            DataTable dTtransaction = myExternalFunctions.getOrderTransaction(referenceNumber);
            if (dTtransaction == null || dTtransaction.Rows.Count == 0)
            {
                return;
            }
            string tansactionNumber = dTtransaction.Rows[0]["transactionNumber"].ToString().Trim();
            string executeDate = dTtransaction.Rows[0]["executeDate"].ToString().Trim();
            decimal executeShares = dTtransaction.Rows[0].Field<decimal>("executeShares");
            decimal executePrice = dTtransaction.Rows[0].Field<decimal>("executePrice");

            insertTransaction(tansactionNumber, referenceNumber, executeDate, executeShares.ToString(), executePrice.ToString());

            decimal dAmount;
            decimal.TryParse(amount, out dAmount);
            if(buyOrSell == "buy")
            {

                decimal fee = calculateBondUnitTrustFees("buy", accountNumber, amount);

                decimal totalAmount = dAmount + fee;

                updateFee(Convert.ToString(fee), referenceNumber);
                decimal updateSecurityHoldingReturn = updateSecurityHolding(accountNumber, type, securityCode, Convert.ToDecimal(executeShares), buyOrSell);
                decimal updateBalanceReturn = updateBalance(accountNumber, -totalAmount);
                Show("buy amount is " + amount + " + fee " +
                    fee.ToString() + " in total " + totalAmount.ToString() +
                    " now holding " + updateSecurityHoldingReturn.ToString() +
                    " balance " + updateBalanceReturn.ToString());

            }
            else
            {
                decimal spend = executeShares * executePrice;
                decimal fee = calculateBondUnitTrustFees("sell", accountNumber, spend.ToString());
                decimal totalAmount = spend - fee;

                updateFee(Convert.ToString(fee), referenceNumber);
                decimal updateSecurityHoldingReturn = updateSecurityHolding(accountNumber, type, securityCode, Convert.ToDecimal(executeShares), buyOrSell);
                decimal updateBalanceReturn = updateBalance(accountNumber, totalAmount); // increase

                Show("sell amount is " + spend + " - fee " +
                    fee.ToString() + " in total " + totalAmount.ToString() +
                    " now holding " + updateSecurityHoldingReturn.ToString() +
                    " balance " + updateBalanceReturn.ToString());

            }

        }
        
        // updateOrderStatus
        public void updateOrderStatus(string referenceNumber, string status)
        {
            Show("order status update" + status + " refer " + referenceNumber);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [Order] set [status]='" + status + "' where [referenceNumber]='" + referenceNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        // `getBalance`: return the balance of the account 
        public decimal getBalance(string accountNumber)
        {
            // Returns the balance of account number.
         
            decimal balance = 0;

           DataTable dtBalance = myHKeInvestData.getData("select [balance] from [Account] where [accountNumber]='" + accountNumber.Trim() + "'");
            // Return null if no result is returned.

            if (dtBalance == null || dtBalance.Rows.Count == 0) return 0;
            else
                {
                    balance =  dtBalance.Rows[0].Field<decimal>("balance");
                    return balance;
                }

            

         
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
                    decimal convesion = myExternalFunctions.getCurrencyRate(row["base"].ToString());
                    Show("base " + row["base"].ToString() + convesion.ToString());
                    price = price * convesion;
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
        public decimal calculateBondUnitTrustFees(string buyOrSell, string accountNumber, string amount)
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
            Show("transaction detected" + transactionNumber);
            SqlTransaction trans = myHKeInvestData.beginTransaction();


            myHKeInvestData.setData("insert into [Transaction]([transactionNumber], [referenceNumber], [executeDate], [executeShares], [executePrice]) values ('" +
                transactionNumber + "', '" + referenceNumber + "', '" + executeDate + "', '" + executeShares + "', '" + executePrice + "')", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        private void updateFee(string fee, string referenceNumber)
        {
            string Fee = Convert.ToString(fee);
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [order] set [fee]='" + Convert.ToString(Fee).Trim() + "' where [referenceNumber]='" + referenceNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        private decimal updateSecurityHolding(string accountNumber, string type, string code, decimal shares, string buyOrSell)
        {
            Show("update security holding");
            decimal ownedShares = getSharesOfSecurityHolding(accountNumber, type, code);

            if (buyOrSell == "sell") shares = -shares;

            decimal updatedShares = ownedShares + shares;
            DataTable dtSecurity = myExternalFunctions.getSecuritiesByCode(type, code);
            if (dtSecurity == null || dtSecurity.Rows.Count == 0) return 0;
            string bases;
            if (type == "stock")
            {
                bases = "HKD";
            }
            else
            {
                bases = dtSecurity.Rows[0]["base"].ToString().Trim();
            }

            string name = dtSecurity.Rows[0]["name"].ToString().Trim();

            if (ownedShares == 0)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                string sql = string.Format(
                   "INSERT INTO [SecurityHolding] " +
                   "(accountNumber, type, code, name, shares, base) " +
                   "VALUES" +
                   "('{0}','{1}','{2}','{3}','{4}','{5}')",
                   accountNumber, type, code, name, shares.ToString(), bases
                   );

                myHKeInvestData.setData(sql, trans);
                myHKeInvestData.commitTransaction(trans);
                return updatedShares;
            }
            else if (ownedShares > 0)
            {
                SqlTransaction trans = myHKeInvestData.beginTransaction();
                myHKeInvestData.setData("update [SecurityHolding] set [shares]='" + Convert.ToString(updatedShares).Trim() + "' where [accountNumber] = '" + accountNumber +
                "' and [type] = '" + type + "' and [code] = '" + code + "'", trans);
                myHKeInvestData.commitTransaction(trans);
            }
           
            return updatedShares;
        }

        private decimal updateBalance(string accountNumber, decimal amount)
        {
            Show("update security balance");
            decimal balance = getBalance(accountNumber);
            decimal updatedBalance = balance + amount;

            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [Account] set [balance]='" + Convert.ToString(updatedBalance).Trim() + "' where [accountNumber] = '" + accountNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
            return updatedBalance;
        }
    

// -----------------------------------
        private void Show(string msg)
        {
           // MessageBox.Show(msg);
        }

    }
}
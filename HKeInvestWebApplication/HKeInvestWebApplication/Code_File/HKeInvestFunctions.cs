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
        ExternalData myExternalData = new ExternalData();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();
        // Returns the CurrencyRate table.
        public DataTable getCurrencyData()
        {
            DataTable dtCurrencyTable = new DataTable();
            dtCurrencyTable = myExternalData.getData("select * from [CurrencyRate] order by [currency]");
            if (dtCurrencyTable == null || dtCurrencyTable.Rows.Count == 0)
            {
                return null;
            }
            return dtCurrencyTable;
        }

        public DataTable getCurrencies()
        {
            // Returns the list of available currencies.
            DataTable dtCurrencies = new DataTable();
            dtCurrencies = myExternalData.getData("select [currency] from [CurrencyRate] order by [currency]");
            if (dtCurrencies == null || dtCurrencies.Rows.Count == 0)
            {
                return null;
            }
            return dtCurrencies;
        }

        public decimal getCurrencyRate(string currency)
        {
            // Returns the exchange rate to the Hong Kong dollar for the specified currency.
            DataTable dtCurrencies = new DataTable();
            dtCurrencies = myExternalData.getData("select [rate] from [CurrencyRate] where [currency]='" + currency + "'");
            // Return -1 if no result is returned.
            if (dtCurrencies == null || dtCurrencies.Rows.Count == 0)
            {
                return -1;
            }
            else
            {
                return dtCurrencies.Rows[0].Field<decimal>("rate");
            }
        }

        public DataTable getSecuritiesData(string securityType)
        {
            // Returns all the data for the specified security type.
            DataTable dtSecurities = new DataTable();
            if (securityType == "bond")
            {
                dtSecurities = myExternalData.getData("select * from [Bond]");
            }
            else if (securityType == "stock")
            {
                dtSecurities = myExternalData.getData("select * from [Stock]");
            }
            else if (securityType == "unit trust")
            {
                dtSecurities = myExternalData.getData("select * from [UnitTrust]");
            }
            // Unknown security type; return null.
            else { return null; }

            // Return null if no result is returned.
            if (dtSecurities == null || dtSecurities.Rows.Count == 0)
            {
                return null;
            }
            return dtSecurities;
        }

        public DataTable getSecurities(string securityType, string securityCode)
        {
            // Returns all the data for the specified security type and the specified name.
            DataTable dtSecurities = new DataTable();
            if (securityType == "bond")
            {
                dtSecurities = myExternalData.getData("select * from [Bond] where [code] = '" + securityCode.Trim() + "'");
            }
            else if (securityType == "stock")
            {
                dtSecurities = myExternalData.getData("select * from [Stock] where [code] = '" + securityCode.Trim() + "'");
            }
            else if (securityType == "unit trust")
            {
                dtSecurities = myExternalData.getData("select * from [UnitTrust] where [code] = '" + securityCode.Trim() + "'");
            }
            // Unknown security type; return null.
            else { return null; }

            // Return null if no result is returned.
            if (dtSecurities == null || dtSecurities.Rows.Count == 0)
            {
                return null;
            }
            return dtSecurities;
        }

        public decimal getSecuritiesPrice(string securityType, string securityCode)
        {
            // Returns the current price of the specified security type and security code.
            DataTable dtSecurities = new DataTable();
            if (securityType == "bond")
            {
                dtSecurities = myExternalData.getData("select [price] from [Bond] where [code]='" + securityCode + "'");
            }
            else if (securityType == "stock")
            {
                dtSecurities = myExternalData.getData("select [close] as [price] from [Stock] where [code]='" + securityCode + "'");
            }
            else if (securityType == "unit trust")
            {
                dtSecurities = myExternalData.getData("select [price] from [UnitTrust] where [code]='" + securityCode + "'");
            }
            // Unknown security type; return -1.
            else { return -1; }

            // Return -1 if no result is returned.
            if (dtSecurities == null || dtSecurities.Rows.Count == 0)
            {
                return -1;
            }
            else
            {
                return dtSecurities.Rows[0].Field<decimal>("price");
            }
        }

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
            else if ( dtShares.Rows.Count == 0)
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

        public void completeBuyUnitTrustOrder(string referenceNumber, string amount, string securityCode, string buyOrSell, string accountNumber)
        {
            DataTable dTtransaction = getOrderTransaction(referenceNumber);
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

        public void updateOrderStatus(string referenceNumber, string status)
        {
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("update [Order] set [status]='" + status + "' where [referenceNumber]='" + referenceNumber + "'", trans);
            myHKeInvestData.commitTransaction(trans);
        }

        public string submitBondBuyOrder(string code, string amount, string accountNumber)
        {
            // Inserts a bond buy order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("bond", code)) { return null; }
            if (!amountIsValid("bond", amount)) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            // Submit the order.
            string referenceNumber = myExternalFunctions.submitBondSellOrder(code, amount);
            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;
            submitOrder("insert into [Order] values (" + referenceNumber + ", 'buy', 'bond', '" + code + "', '" + dateNow
            + "', 'pending', " + "NULL, " + amount.Trim() + ", NULL, NULL, NULL, NULL, NULL, '" + accountNumber +"', 0)");
            return referenceNumber;
        }

        public string submitBondSellOrder(string code, string shares, string accountNumber)
        {
            // Inserts a bond sell order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("bond", code)) { return null; }
            if (!sharesIsValid("bond", shares)) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            // Submit the order.
            string referenceNumber = myExternalFunctions.submitBondSellOrder(code, shares);
            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;

            submitOrder("insert into [Order] values (" + referenceNumber + ",'sell', 'bond', '" + code + "', '" + dateNow
                + "', 'pending', " + shares.Trim() + ", NULL, NULL, NULL, NULL, NULL, NULL, '"+ accountNumber + "', 0)");
            return referenceNumber;
        }

        public string submitStockBuyOrder(string code, string shares, string orderType, string expiryDay, string allOrNone, string highPrice, string stopPrice, string accountNumber)
        {
            // Inserts a stock buy order into the Order table.
            // Check if input is valid.
            orderType = orderType.Trim().ToLower();
            if (!securityCodeIsValid("stock", code)) { return null; }
            if (!sharesIsValid("stock", shares)) { return null; }
            if (!sharesAmountIsValid(shares)) { return null; }
            if (!orderTypeIsValid("buy", orderType, expiryDay, allOrNone, highPrice, stopPrice)) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // string isBuyOrSell = "buy";
            // string securityType = "stock";
            // string status = "pending";
            // string amount = "NULL";
            string referenceNumber = myExternalFunctions.submitStockBuyOrder(code, shares, orderType, expiryDay, allOrNone, highPrice, stopPrice);
            // Construct the basic SQL statement.
            int referenceNumber_int;

            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;
            string sql = "insert into [Order] values ('" + referenceNumber + "','buy', 'stock', '" + code + "', '" + dateNow + "', 'pending', " +
                shares.Trim() + ", NULL, '" + orderType.Trim() + "', " + expiryDay.Trim() + ", '" + allOrNone.Trim().ToUpper() + "', ";

            // Check for order type and set SQL statement accordingly.
            if (orderType == "market")
            {
                sql = sql + "NULL, NULL,";
                // highPrice = "NULL";
                // stopPrice = "NULL";
            }
            else if (orderType == "limit")
            {
                sql = sql + highPrice.Trim() + ", NULL,";
                // stopPrice = "NULL";
            }
            else if (orderType == "stop")
            {
                 sql = sql + "NULL, " + stopPrice.Trim() + ", ";
                 // highPrice = "NULL";
            }
            else if (orderType == "stop limit")// Order type is stop limit.
            {
                  sql = sql + highPrice.Trim() + ", " + stopPrice.Trim() + ",";

            }
            sql = sql + "'" +accountNumber + ", 0)";
            submitOrder(sql);
            return referenceNumber;
        }

        public string submitStockSellOrder(string code, string shares, string orderType, string expiryDay, string allOrNone, string lowPrice, string stopPrice, string accountNumber)
        {
            // Inserts a stock sell order into the Order table.
            // Check if input is valid.
            orderType = orderType.Trim();
            if (!securityCodeIsValid("stock", code)) { return null; }
            if (!sharesIsValid("stock", shares)) { return null; }
            if (!orderTypeIsValid("sell", orderType, expiryDay.Trim(), allOrNone.Trim(), lowPrice.Trim(), stopPrice.Trim())) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            string referenceNumber = myExternalFunctions.submitStockSellOrder(code, shares, orderType, expiryDay, allOrNone, lowPrice, stopPrice);
            // Construct the basic SQL statement.
            int referenceNumber_int;

            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;

            // Construct the basic SQL statement.
            string sql = "insert into [Order] values (" + referenceNumber + ", 'sell', 'stock', '" + code + "', '" + dateNow + "', 'pending', " +
                shares.Trim() + ", NULL, '" + orderType.Trim() + "', " + expiryDay.Trim() + ", '" + allOrNone.Trim().ToUpper() + "', ";

            // Check for order type and set SQL statement accordingly.
            if (orderType == "market")
            {
                sql = sql + "NULL, NULL, ";
            }
            else if (orderType == "limit")
            {
                sql = sql + lowPrice.Trim() + ", NULL,";
            }
            else if (orderType == "stop")
            {
                sql = sql + "NULL, " + stopPrice.Trim() + ", ";
            }
            else // Order type is stop limit.
            {
                sql = sql + lowPrice.Trim() + ", " + stopPrice.Trim() + ",";
            }
            // Submit the order.

            sql = sql + "'" + accountNumber + "', 0)";
                submitOrder(sql);


            return referenceNumber;
            // myExternalFunctions.submitStockSellOrder(code, shares, orderType, expiryDay, allOrNone, lowPrice, stopPrice);
        }

        public string submitUnitTrustBuyOrder(string code, string amount, string accountNumber)
        {
            // Inserts a unit trust buy order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("unit trust", code)) { return null; }
            if (!amountIsValid("unit trust", amount)) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            // Submit the order.

            string referenceNumber = myExternalFunctions.submitUnitTrustBuyOrder(code, amount);
            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;
            submitOrder("insert into [Order] values (" + referenceNumber + ", 'buy', 'unit trust', '" + code + "', '" + dateNow
                + "', 'pending', " + "NULL, " + amount.Trim() + ", NULL, NULL, NULL, NULL, NULL, '" + accountNumber + "', 0)");
            return referenceNumber;

        }

        public string submitUnitTrustSellOrder(string code, string shares, string accountNumber)
        {
            // Inserts a unit trust sell order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("unit trust", code)) { return null; }
            if (!sharesIsValid("unit trust", shares)) { return null; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            // Submit the order.
            string referenceNumber = myExternalFunctions.submitUnitTrustBuyOrder(code, shares);
            int referenceNumber_int;
            if (!int.TryParse(referenceNumber, out referenceNumber_int)) return null;

            submitOrder("insert into [Order] values (" + referenceNumber + ", 'sell', 'unit trust', '" + code + "', '" + dateNow
                + "', 'pending', " + shares.Trim() + ", NULL, NULL, NULL, NULL, NULL, NULL, '"+ accountNumber + "', 0)");
            return referenceNumber;
        }

        public string getOrderStatus(string referenceNumber)
        {
            // Returns the status of the order specified by its reference number.
            int number;
            if (int.TryParse(referenceNumber, out number))
            {
                DataTable dtStatus = myExternalData.getData("select [status] from [Order] where [referenceNumber]='" + referenceNumber.Trim() + "'");
                // Return null if no result is returned.
                if (!(dtStatus == null || dtStatus.Rows.Count == 0))
                {
                    return dtStatus.Rows[0].Field<string>("status");
                }
            }
            return null;
        }

        public DataTable getOrderTransaction(string referenceNumber)
        {
            // Returns all the transactions for an order specified by its reference number.
            int number;
            if (int.TryParse(referenceNumber, out number))
            {
                DataTable dtTransaction = myExternalData.getData("select * from [Transaction] where [referenceNumber]='" + referenceNumber.Trim() + "'");
                // Return null if no result is returned.
                if (!(dtTransaction == null || dtTransaction.Rows.Count == 0))
                {
                    return dtTransaction;
                }
            }
            return null;
        }

        public decimal getBalance(string accountNumber)
        {
            // Returns the balance of account number.
            int number;
            if (int.TryParse(accountNumber, out number))
            {
                DataTable dtBalance = myExternalData.getData("select [balance] from [Account] where [referenceNumber]='" + accountNumber.Trim() + "'");
                // Return null if no result is returned.
                if (!(dtBalance == null || dtBalance.Rows.Count == 0))
                {
                    return Convert.ToDecimal(dtBalance.Rows[0].Field<string>("balance"));
                }
            }
            return 0;
        }

        public decimal getCurrentValueOfSecuries(string accountNumber)
        {
            int number;
            decimal value = 0;
            if (int.TryParse(accountNumber, out number))
            {
                DataTable dtSecurities = myExternalData.getData("select * from [SecurityHolding] where [SecurityHolding].[accountNumber] = '" + accountNumber.Trim() + "' ");
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

        public decimal getAssets(string accountNumber)
        {
            int number;
            if (int.TryParse(accountNumber, out number))
            {
                return getBalance(accountNumber) + getCurrentValueOfSecuries(accountNumber);
            }
            return 0;
        }

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

        private bool securityCodeIsValid(string securityType, string securityCode)
        {
            DataTable dtSecurities = myExternalFunctions.getSecuritiesByCode(securityType, securityCode);
            if (dtSecurities == null || dtSecurities.Rows.Count == 0)
            {
                // showError();
                return false;
            }
            return true;
        }

        private bool amountIsValid(string securityType, string amount)
        {
            decimal number;
            if (!decimal.TryParse(amount, out number) || number <= 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing dollar amount of " + securityType + " to buy.\nValue is '" + amount + "'.");
                return false;
            }
            return true;
        }

        private bool sharesIsValid(string securityType, string shares)
        {
            decimal number;
            if (!decimal.TryParse(shares, out number) || number <= 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing number of " + securityType + " shares to sell.\nValue is '" + shares + "'.");
                return false;
            }
            return true;
        }

        private bool sharesAmountIsValid(string shares)
        {
            decimal number = Convert.ToDecimal(shares);
            if ((number % 100) != 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Shares to buy is not a multiple of 100.\nValue is '" + shares + "'.");
                return false;
            }
            return true;
        }

        private bool orderTypeIsValid(string buyOrSell, string orderType, string expiryDay, string allOrNone, string limitPrice, string stopPrice)
        {
            int intNumber;
            decimal decLimitPrice = 0;
            decimal decStopPrice = 0;

            // Check if order type is valid.
            if (!(orderType == "market" || orderType == "limit" || orderType == "stop" || orderType == "stop limit"))
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing stock order type.\nValue is '" + orderType + "'.");
                return false;
            }

            // Check if expiry day is valid.
            if (!int.TryParse(expiryDay, out intNumber) || intNumber < 1 || intNumber > 7)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing expiry day.\nValue is '" + expiryDay + "'.");
                return false;
            }

            // Check if all or none is valid.
            if (!(allOrNone.ToUpper() == "Y" || allOrNone.ToUpper() == "N"))
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing all or none.\nValue is '" + allOrNone + "'.");
                return false;
            }

            // Check if limit price is valid.
            if (orderType == "limit" || orderType == "stop limit")
            {
                if (!decimal.TryParse(limitPrice, out decLimitPrice) || decLimitPrice <= 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Invalid or missing limit price.\nValue is '" + limitPrice + "'.");
                    return false;
                }
            }

            // Check if stop price is valid.
            if (orderType == "stop" || orderType == "stop limit")
            {
                if (!decimal.TryParse(stopPrice, out decStopPrice) || decStopPrice <= 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Invalid or missing stop price.\nValue is '" + stopPrice + "'.");
                    return false;
                }

            }

            // Check if stop and limit prices are in correct relationship to each other.
            if (orderType == "stop limit")
            {
                if (buyOrSell == "buy")
                {
                    if (decStopPrice > decLimitPrice)
                    {
                        MessageBox.Show(new Form { TopMost = true }, "Stock buy order:\nstop price must be <= limit price.");
                        return false;
                    }
                }
                else // Sell order.
                {
                    if (decStopPrice < decLimitPrice)
                    {
                        MessageBox.Show(new Form { TopMost = true }, "Stock sell order:\n stop price must be >= limit price.");
                        return false;
                    }
                }
            }
            return true;
        }

        private void insertTransaction(string tansactionNumber, string referenceNumber, string executeDate, string executeShares, string executePrice)
        {
            SqlTransaction trans = myHKeInvestData.beginTransaction();
            myHKeInvestData.setData("insert into [Transaction]([transactionNumber], [referenceNumber], [executeDate], [executeShares], [executePrice]) values (" +
                tansactionNumber + ", '" + referenceNumber + "', " + executeDate + ", '" + executeShares + ", '" + executePrice + "')", trans);
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
            DataTable dtSecurity = getSecurities(type, code);
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
    }
}
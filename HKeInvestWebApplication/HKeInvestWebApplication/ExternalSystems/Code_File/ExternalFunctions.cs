using System;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HKeInvestWebApplication.ExternalSystems.Code_File
{
    //**********************************************************
    //* THE CODE IN THIS CLASS CANNOT BE MODIFIED OR ADDED TO. *
    //*        Report problems to 3111rep@cse.ust.hk.          *
    //**********************************************************

    public class ExternalFunctions
    {
        ExternalData myExternalData = new ExternalData();

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

        public bool submitBondBuyOrder(string code, string amount)
        {
            // Inserts a bond buy order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("bond", code)) { return false; }
            if (!amountIsValid("bond", amount)) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData("insert into [Order] values ('buy', 'bond', '" + code + "', '" + dateNow + "', 'pending', " +
                "NULL, " + amount.Trim() + ", NULL, NULL, NULL, NULL, NULL)", trans);
            myExternalData.commitTransaction(trans);
            return true;
        }

        public bool submitBondSellOrder(string code, string shares)
        {
            // Inserts a bond sell order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("bond", code)) { return false; }
            if (!sharesIsValid("bond", shares)) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData("insert into [Order] values ('sell', 'bond', '" + code + "', '" + dateNow + "', 'pending', " +
                shares.Trim() + ", NULL, NULL, NULL, NULL, NULL, NULL)", trans);
            myExternalData.commitTransaction(trans);
            return true;
        }

        public bool submitStockBuyOrder(string code, string amount, string orderType, string expiryDay, string allOrNone, string highPrice, string stopPrice)
        {
            // Inserts a stock buy order into the Order table.
            // Check if input is valid.
            orderType = orderType.Trim().ToLower();
            if (!securityCodeIsValid("stock", code)) { return false; }
            if (!amountIsValid("stock", amount)) { return false; }
            if (!orderTypeIsValid("buy", orderType, expiryDay, allOrNone, highPrice, stopPrice)) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Construct the basic SQL statement.
            string sql = "insert into [Order] values ('buy', 'stock', '" + code + "', '" + dateNow + "', 'pending', NULL, " + 
                amount.Trim() + ", '" + orderType.Trim() + "', " + expiryDay.Trim() + ", '" + allOrNone.Trim().ToUpper() + "', "; 

            // Check for order type and set SQL statement accordingly.
            if (orderType == "market")
            {
                sql = sql + "NULL, NULL)";
            }
            else if (orderType == "limit")
            {
                sql = sql + highPrice.Trim() + ", NULL)";
            }
            else if (orderType == "stop")
            {
                sql = sql + "NULL, " + stopPrice.Trim() + ") ";
            }
            else // Order type is stop limit.
            {
                sql = sql + highPrice.Trim() + ", " + stopPrice.Trim() + ")"; 
            }

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            return true;
        }

        public bool submitStockSellOrder (string code, string shares, string orderType, string expiryDay, string allOrNone, string lowPrice, string stopPrice)
        {
            // Inserts a stock sell order into the Order table.
            // Check if input is valid.
            orderType = orderType.Trim();
            if (!securityCodeIsValid("stock", code)) { return false; }
            if (!sharesIsValid("stock", shares)) { return false; }
            if (!orderTypeIsValid("sell",orderType, expiryDay.Trim(), allOrNone.Trim(), lowPrice.Trim(), stopPrice.Trim())) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Construct the basic SQL statement.
            string sql = "insert into [Order] values ('sell', 'stock', '" + code + "', '" + dateNow + "', 'pending', " +
                shares.Trim() + ", NULL, '" + orderType.Trim() + "', " + expiryDay.Trim() + ", '" + allOrNone.Trim().ToUpper() + "', ";

            // Check for order type and set SQL statement accordingly.
            if (orderType == "market")
            {
                sql = sql + "NULL, NULL)";
            }
            else if (orderType == "limit")
            {
                sql = sql + lowPrice.Trim() + ", NULL)";
            }
            else if (orderType == "stop")
            {
                sql = sql + "NULL, " + stopPrice.Trim() + ") ";
            }
            else // Order type is stop limit.
            {
                sql = sql + lowPrice.Trim() + ", " + stopPrice.Trim() + ")";
            }

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData(sql, trans);
            myExternalData.commitTransaction(trans);
            return true;
        }

        public bool submitUnitTrustBuyOrder(string code, string amount)
        {
            // Inserts a unit trust buy order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("unit trust", code)) { return false; }
            if (!amountIsValid("unit trust", amount)) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData("insert into [Order] values ('buy', 'unit trust', '" + code + "', '" + dateNow + "', 'pending', " +
                "NULL, " + amount.Trim() + ", NULL, NULL, NULL, NULL, NULL)", trans);
            myExternalData.commitTransaction(trans);
            return true;
        }

        public bool submitUnitTrustSellOrder(string code, string shares)
        {
            // Inserts a unit trust sell order into the Order table.
            // Check if input is valid.
            if (!securityCodeIsValid("unit trust", code)) { return false; }
            if (!sharesIsValid("unit trust", shares)) { return false; }
            string dateNow = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            // Execute the SQL statement.
            SqlTransaction trans = myExternalData.beginTransaction();
            myExternalData.setData("insert into [Order] values ('sell', 'unit trust', '" + code + "', '" + dateNow + "', 'pending', " +
                shares.Trim() + ", NULL, NULL, NULL, NULL, NULL, NULL)", trans);
            myExternalData.commitTransaction(trans);
            return true;
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

        private bool securityCodeIsValid( string securityType, string securityCode)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string dbTableName = textInfo.ToTitleCase(securityType).Replace(" ", string.Empty);
            if (myExternalData.getAggregateValue("select count(*) from [" + dbTableName + "] where [code]='" + securityCode + "'") == 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or nonexistent " + securityType + " code.\nInput value is '" + securityCode + "'.");
                return false;
            }
            return true;
        }

        private bool amountIsValid(string securityType, string amount)
        {
            decimal number;
            if (!decimal.TryParse(amount, out number) || number <= 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing dollar amount of " + securityType + " to buy.\nInput value is '" + amount + "'.");
                return false;
            }
            return true;
        }

        private bool sharesIsValid(string securityType, string shares)
        {
            decimal number;
            if (!decimal.TryParse(shares, out number) || number <= 0)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing number of " + securityType + " shares to sell.\nInput value is '" + shares + "'.");
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
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing stock order type.\nInput value is '" + orderType + "'.");
                return false;
            }

            // Check if expiry day is valid.
            if (!int.TryParse(expiryDay, out intNumber) || intNumber < 1 || intNumber > 7)
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing expiry day.\nInput value is '" + expiryDay + "'.");
                return false;
            }

            // Check if all or none is valid.
            if (!(allOrNone.ToUpper() == "Y" || allOrNone.ToUpper() == "N"))
            {
                MessageBox.Show(new Form { TopMost = true }, "Invalid or missing all or none.\nInput value is '" + allOrNone + "'.");
                return false;
            }

            // Check if limit price is valid.
            if (orderType == "limit" || orderType == "stop limit")
            {
                if (!decimal.TryParse(limitPrice, out decLimitPrice) || decLimitPrice <= 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Invalid or missing limit price.\nInput value is '" + limitPrice + "'.");
                    return false;
                }
            }

            // Check if stop price is valid.
            if (orderType == "stop" || orderType == "stop limit")
            {
                if (!decimal.TryParse(stopPrice, out decStopPrice) || decStopPrice <= 0)
                {
                    MessageBox.Show(new Form { TopMost = true }, "Invalid or missing stop price.\nInput value is '" + stopPrice + "'.");
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using HKeInvestWebApplication.Code_File;
using HKeInvestWebApplication.ExternalSystems.Code_File;
using System.Data;
using System.Windows.Forms;

namespace HKeInvestWebApplication
{
    public class Global : HttpApplication
    {
        HKeInvestData myHKeInvestData = new HKeInvestData();
        HKeInvestCode myHKeInvestCode = new HKeInvestCode();
        HKeInvestFunctions myHKeInvestFunction = new HKeInvestFunctions();
        ExternalFunctions myExternalFunctions = new ExternalFunctions();

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Thread mythread = new Thread(CheckOrder);
            mythread.IsBackground = true;
            mythread.Start();

        }

        private void testThread()
        {
            /*
            decimal i = 0;
            do
            {
                MessageBox.Show("point1" + i.ToString());
                
                DataTable dTpendingOrder = myHKeInvestFunction.getPendingorPartialOrder();
                if (dTpendingOrder != null)
                {
                    foreach (DataRow row in dTpendingOrder.Rows)
                    {
                        string referenceNumber = row["referenceNumber"].ToString().Trim();
                        string status = row["status"].ToString().Trim();
                        string securityType = row["securityType"].ToString().Trim();
                        string currentstatus = myExternalFunctions.getOrderStatus(referenceNumber);
                        string buyOrSell = row["buyOrSell"].ToString().Trim();
                        string securityCode = row["securityCode"].ToString().Trim();
                        string accountNumber = row["accountNumber"].ToString().Trim();
                        string allOrNone = row["allOrNone"].ToString().Trim();

                        if (securityType == "unit trust")
                        {
                            string amount = row["amount"].ToString().Trim();
                            if (currentstatus == "completed")
                            {
                                if (buyOrSell == "buy")
                                {
                                    myHKeInvestFunction.completeBuyUnitTrustOrder(referenceNumber, amount, securityCode, buyOrSell, accountNumber);
                                }
                            }
                        }
                        
                    }
                    i = i + 1;
                }

                Thread.Sleep(10000);
            } while (true);
            */
        }

        private void CheckOrder()
        {
            
            do
            {
                MessageBox.Show("threading");
                DataTable dTpendingOrder = myHKeInvestFunction.getPendingorPartialOrder();

                if (dTpendingOrder != null)
                {

                    foreach (DataRow row in dTpendingOrder.Rows)
                    {
                        string referenceNumber = row["referenceNumber"].ToString().Trim();
                        string status = row["status"].ToString().Trim();
                        string securityType = row["securityType"].ToString().Trim();
                        
                        string buyOrSell = row["buyOrSell"].ToString().Trim();
                        string securityCode = row["securityCode"].ToString().Trim();
                        string accountNumber = row["accountNumber"].ToString().Trim();
                        string allOrNone = row["allOrNone"].ToString().Trim();


                        string currentstatus = myExternalFunctions.getOrderStatus(referenceNumber);
                        if (currentstatus == null || status == null) return;
                        if (securityType == "unit trust" || securityType == "bond")
                        {
                            string amount = row["amount"].ToString().Trim();
                            if (currentstatus == "completed")
                            {
                                //MessageBox.Show("complete order found");
                                myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                                myHKeInvestFunction.completeBondUnitTrustOrder(referenceNumber, amount,  securityCode, buyOrSell, accountNumber, securityType);
                            }
                            else continue;
                        }else if (securityType == "stock")
                        {
                            string stockOrderType = row["stockOrderType"].ToString().Trim();
                            if(currentstatus == "cancelled")
                            {
                                //MessageBox.Show("cancelled order found" + currentstatus);
                                currentstatus = "cancelled";
                                myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                            }else if(currentstatus == "completed")
                            {
                                myHKeInvestFunction.stockTransactionHandler(referenceNumber, allOrNone, buyOrSell, stockOrderType, accountNumber, securityCode);
                                myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                            }else if(currentstatus == "partial")
                            {
                                if (myHKeInvestFunction.isExpired(referenceNumber))
                                {
                                    myHKeInvestFunction.stockTransactionHandler(referenceNumber, allOrNone, buyOrSell, stockOrderType, accountNumber, securityCode);
                                    myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                                }
                                else return;

                            }else if(currentstatus == "pending" )
                            {
                                if (myHKeInvestFunction.isExpired(referenceNumber))
                                {
                                    currentstatus = "cancelled";
                                    myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                                }
                                else return;

                            }


                            if (myHKeInvestFunction.isExpired(referenceNumber))
                            {
                                // #TODO: fetch information of the stock order
                                if (myHKeInvestFunction.checkStockOrderTranscation(referenceNumber, allOrNone) == "cancelled")
                                {
                                    //MessageBox.Show("cancelled order found" + currentstatus);
                                    currentstatus = "cancelled";
                                    myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                                }else
                                {
                                    // #TODO: not cancelled, fetch the information of transaction:
                                    currentstatus = myHKeInvestFunction.checkStockOrderTranscation(referenceNumber, allOrNone);
                                    myHKeInvestFunction.stockTransactionHandler(referenceNumber, allOrNone, buyOrSell, stockOrderType, accountNumber, securityCode);
                                    myHKeInvestFunction.updateOrderStatus(referenceNumber, currentstatus);
                                }
                            }
                            else continue;

                        }
                    }
                }

                Thread.Sleep(10000);
            } while (true);

        }
    }
}
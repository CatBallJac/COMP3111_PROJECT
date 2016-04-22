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

            Thread mythread = new Thread(testThread);
            mythread.IsBackground = true;
            mythread.Start();

        }

        private void testThread()
        {
            do
            {
                //MessageBox.Show("point1");
                /*
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
                    
                }*/
                Thread.Sleep(10000);
            } while (true);
        }

        private void CheckOrder()
        {
            do
            {
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

                        if (currentstatus == null || status == null) return;

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
                }

                Thread.Sleep(60000);
            } while (true);

        }
    }
}
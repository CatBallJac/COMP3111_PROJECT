﻿using System;
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

            //Thread mythread = new Thread(CheckOrder);
           // mythread.IsBackground = true;
            //mythread.Start();

        }

        private void CheckOrder()
        {
            do
            {
                DataTable dTpendingOrder = myHKeInvestFunction.getPendingorPartialOrder();

                if (dTpendingOrder == null) { return; }

                int j = 1;
                foreach (DataRow row in dTpendingOrder.Rows)
                {
                    string referenceNumber = row["referenceNumber"].ToString().Trim();
                    string status = row["status"].ToString().Trim();
                    string securityType = row["securityType"].ToString().Trim();
                    string currentstatus = myExternalFunctions.getOrderStatus(referenceNumber);

                    string allOrNone = row["allOrNone"].ToString().Trim();


                    if (currentstatus == null || status == null) return;

                    if (securityType == "unit trust")
                    {
                        string amount = row["amount"].ToString().Trim();
                        if (currentstatus == "completed") ;
                    }

                    else if (currentstatus == "currentstatus")
                    {
                        //hahahah
                        return;
                        //string sql3 = "update [Order] set [status]='" + userName + "' where [accountNumber]='" + accountNumber + "'";
                        //SqlTransaction trans = myInvestData.beginTransaction();
                        //myInvestData.setData(sql, trans);
                        //myInvestData.commitTransaction(trans);
                    }

                    j = j + 1;
                }

                Thread.Sleep(500000);
            } while (true);

        }
    }
}
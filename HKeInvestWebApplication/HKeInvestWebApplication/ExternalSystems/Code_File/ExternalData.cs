using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HKeInvestWebApplication.ExternalSystems.Code_File
{
    //**********************************************************
    //* THE CODE IN THIS CLASS CANNOT BE MODIFIED OR ADDED TO. *
    //*        Report problems to 3111rep@cse.ust.hk.          *
    //**********************************************************

    public class ExternalData
    {
        // Set the connection string to connect to the external database.
        SqlConnection ExternalDBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ExternalDatabasesConnectionString"].ConnectionString);

        // Process a SQL SELECT statement.
        public DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                if (ExternalDBConnection.State != ConnectionState.Open)
                {
                    ExternalDBConnection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql, ExternalDBConnection);
                    da.Fill(dt);
                    ExternalDBConnection.Close();
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter(sql, ExternalDBConnection);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        // Process an SQL SELECT statement that returns only a single value.
        // Return zero if the table is empty or there are no values in the column.
        public decimal getAggregateValue(string sql)
        {
            try
            {
                object aggregateValue;
                if (ExternalDBConnection.State != ConnectionState.Open)
                {
                    ExternalDBConnection.Open();
                    SqlCommand SQLCmd = new SqlCommand(sql, ExternalDBConnection);
                    SQLCmd.CommandType = CommandType.Text;
                    aggregateValue = SQLCmd.ExecuteScalar();
                    ExternalDBConnection.Close();
                }
                else
                {
                    SqlCommand SQLCmd = new SqlCommand(sql, ExternalDBConnection);
                    SQLCmd.CommandType = CommandType.Text;
                    aggregateValue = SQLCmd.ExecuteScalar();
                }
                return (DBNull.Value == aggregateValue ? 0 : Convert.ToDecimal(aggregateValue));
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        // Process SQL INSERT, UPDATE and DELETE statements.
        public void setData(string sql, SqlTransaction trans)
        {
            try
            {
                SqlCommand SQLCmd = new SqlCommand(sql, ExternalDBConnection);
                SQLCmd.Transaction = trans;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.ExecuteNonQuery();
            }
            catch (ApplicationException ex)
            {
                ExternalDBConnection.Close();
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                ExternalDBConnection.Close();
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ExternalDBConnection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public SqlTransaction beginTransaction()
        {
            if (ExternalDBConnection.State != ConnectionState.Open)
            {
                ExternalDBConnection.Open();
                SqlTransaction trans = ExternalDBConnection.BeginTransaction();
                return trans;
            }
            else
            {
                SqlTransaction trans = ExternalDBConnection.BeginTransaction();
                return trans;
            }
        }

        public void commitTransaction(SqlTransaction trans)
        {
            try
            {
                if (ExternalDBConnection.State == ConnectionState.Open)
                {
                    trans.Commit();
                    ExternalDBConnection.Close();
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
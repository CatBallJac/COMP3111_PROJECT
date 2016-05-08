namespace HKeInvestWebApplication.Code_File
{
    public class SQLStringHandleHelper
    {
        public string handleString(string data)
        {
            data = data.Replace("'", "''");
            return data;
        }
        public string backString(string data)
        {
            data = data.Replace("''", "'");
            return data;
        }
    }
}
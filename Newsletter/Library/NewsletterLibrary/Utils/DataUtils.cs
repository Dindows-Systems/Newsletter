using System;
using System.Globalization;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace i386
{
    public class Utils
    {
 
        /// <summary>
        /// Database Time stamp
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            DateTimeFormatInfo formatInfo = new DateTimeFormatInfo();
            return " #" + DateTime.Now.ToString("MM/dd/yyyy", formatInfo) + " " + DateTime.Now.ToLongTimeString() + "# ";
        }

        public static object DbCmdInsert(string SQLStatement)
        {
            string DataConnectionString = Dataconnection();
            try
            {
                if (DbSQLConnection(DataConnectionString)) //SQL Databases
                {
                    return DbCmdExecuteScalar(SQLStatement);
                }
                else
                {
                    OleDbCommand DbCmd = new OleDbCommand(); // MS Access Databases
                    OleDbCommand DbCmdGetId = new OleDbCommand();

                    object RtnObject = null;
                    OleDbConnection DbConn = new OleDbConnection(DataConnectionString);
                    DbCmd.CommandText = SQLStatement;
                    DbCmd.Connection = DbConn;
                    DbConn.Open();
                    DbCmd.ExecuteNonQuery();
                    DbCmdGetId.CommandText = "SELECT @@IDENTITY"; // MS Access 2000 at better to get ID
                    DbCmdGetId.Connection = DbConn;
                    RtnObject = DbCmdGetId.ExecuteScalar();
                    DbConn.Close();
                    DbCmd.Dispose();
                    return RtnObject;
                }
            }
            catch (Exception ErrorMsg)
            {
                throw new Exception("DbCmdInsert Check Permissions to the database!:" + ErrorMsg.Message + " also check the primary field is blank!");
               // return null;
            }

        }
        /// <summary>
        /// Connection String if you want SQL support change the DataConnection string.
        /// </summary>
        /// <returns></returns>
        public static string Dataconnection()
        {
            
            StringBuilder connStr = new StringBuilder("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=");
            connStr.Append(System.Web.HttpContext.Current.Server.MapPath("/Newsletter/App_Data/Newsletter.mdb"));
            connStr.Append(";Persist Security Info=False");
            return connStr.ToString();
        }
        public static DataSet nGetDataSet(string SqlString)
        {
            DataSet ddlDS = new DataSet();
            string DbConnection = Dataconnection();
            if (DbSQLConnection(DbConnection))
            { // SQL Conneciton
                SqlDataAdapter dbAdp = new SqlDataAdapter(SqlString, DbConnection);
                dbAdp.Fill(ddlDS);
            }


            else
            { //OLE Connection
                try
                {
                    OleDbDataAdapter dbAdp = new OleDbDataAdapter(SqlString, DbConnection);
                    dbAdp.Fill(ddlDS);
                }
                catch //(Exception Error)
                {
                    //System.Web.HttpContext.Current.Response.Write(Error.Message);
                }
            }


            return ddlDS;
        }
        /// <summary>
        /// Return DataTable
        /// </summary>
        /// <param name="SqlString"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string SqlString)
        {
            DataTable dt = new DataTable();
            string DbConnection = Dataconnection();
            try
            {
                if (DbSQLConnection(DbConnection))
                { // SQL Conneciton
                    SqlDataAdapter dbAdp = new SqlDataAdapter(SqlString, DbConnection);
                    dbAdp.Fill(dt);
                }
                else
                {
                    OleDbDataAdapter dbAdp = new OleDbDataAdapter(SqlString, DbConnection);
                    dbAdp.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetDataTable: " + ex.Message);
            }
            return dt;
        }
        public static DataTable GetDataTable(string table, string where)
        {
            DataTable dt = new DataTable();
            string SqlString = "SELECT * FROM {0} WHERE ({1});";
            SqlString = String.Format(SqlString, table, where);
            string DbConnection = Dataconnection();
            try
            {
                if (DbSQLConnection(DbConnection))
                { // SQL Conneciton
                    SqlDataAdapter dbAdp = new SqlDataAdapter(SqlString, DbConnection);
                    dbAdp.Fill(dt);
                }
                else
                {
                    OleDbDataAdapter dbAdp = new OleDbDataAdapter(SqlString, DbConnection);
                    dbAdp.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetDataTable: " + ex.Message);
            }
            return dt;
        }

        public static bool DbSQLConnection(string DataConnectionString)
        {

            if ((DataConnectionString.LastIndexOf("OLEDB") > 0) || (DataConnectionString.LastIndexOf("MSIDXS") > 0) || (DataConnectionString.LastIndexOf(".xml") > 0)) //MS Access and Index Server support
            {
                return false;
            }
            else return true;
        }

        public static bool DbCmdExecute(string SQLStatement)
        {
            string DataConnectionString = Dataconnection();
            if (DbSQLConnection(DataConnectionString)) //SQL Databases
            {
                SqlCommand DbCmd = new SqlCommand();
                try
                {
                    SqlConnection DbConn = new SqlConnection(DataConnectionString);
                    DbCmd.CommandText = SQLStatement;
                    DbCmd.Connection = DbConn;
                    DbConn.Open();
                    DbCmd.ExecuteNonQuery();
                    DbConn.Close();
                    return true;
                }
                catch (Exception ErrorMsg)
                {
                    throw new Exception("Error DbCmdExecute : " + ErrorMsg.Message + "\n" + SQLStatement);
                   // return false;
                }
                finally
                {
                    DbCmd.Dispose();
                }
            }
            else
            {
                OleDbCommand DbCmd = new OleDbCommand(); // MS Access Databases
                try
                {
                    OleDbConnection DbConn = new OleDbConnection(DataConnectionString);
                    DbCmd.CommandText = SQLStatement;
                    DbCmd.Connection = DbConn;
                    DbConn.Open();
                    DbCmd.ExecuteNonQuery();
                    DbConn.Close();
                    return true;
                }
                catch (Exception ErrorMsg)
                {
                    throw new Exception("Error DbCmdExecute : " + ErrorMsg.Message + "\n" + SQLStatement);
                   // return false;
                }
                finally
                {
                    DbCmd.Dispose();
                }
            }
        }

        public static object DbCmdExecuteScalar(string SQLStatement)
        {
            string DataConnectionString = Dataconnection();
            object RtnObject;
            if (DbSQLConnection(DataConnectionString)) //SQL Databases
            {
                SqlCommand DbCmd = new SqlCommand();
                try
                {
                    SqlConnection DbConn = new SqlConnection(DataConnectionString);
                    DbCmd.CommandText = SQLStatement;
                    DbCmd.Connection = DbConn;
                    DbConn.Open();
                    RtnObject = DbCmd.ExecuteScalar();
                    DbConn.Close();
                    return RtnObject;
                }
                catch (Exception ErrorMsg)
                {
                    throw new Exception("Error DbCmdExecuteScalar : " + ErrorMsg.Message + "\n" + SQLStatement + " Connection String: " + DataConnectionString);
                   // return null;
                }
                finally
                {
                    DbCmd.Dispose();
                }
            }
            else
            {
                OleDbCommand DbCmd = new OleDbCommand(); // MS Access Databases
                try
                {
                    OleDbConnection DbConn = new OleDbConnection(DataConnectionString);
                    DbCmd.CommandText = SQLStatement;
                    DbCmd.Connection = DbConn;
                    DbConn.Open();
                    RtnObject = DbCmd.ExecuteScalar();
                    if (SQLStatement.Substring(0, 6) == "INSERT")
                    {
                        DbCmd.CommandText = "SELECT @@IDENTITY"; // MS Access 2000 at better to get ID
                        RtnObject = DbCmd.ExecuteScalar();
                    }
                    DbConn.Close();
                    return RtnObject;
                }
                catch (Exception ErrorMsg)
                {
                    throw new Exception("Error DbCmdExecuteScalar : " + ErrorMsg.Message + "\n" + SQLStatement + " Connection String: " + DataConnectionString);
                    //return null;
                }
                finally
                {
                    DbCmd.Dispose();
                }
            }
        }
        public static object DataReader(string SqlString)
        {
            return DataReader(SqlString, Utils.Dataconnection());
        }

        public static object DataReader(string SqlString, string DataConnectionString)
        {
            if (Utils.DbSQLConnection(DataConnectionString)) //SQL Databases
            {
                SqlConnection conn = new SqlConnection(DataConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(SqlString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
                reader.Close();
                conn.Close();
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(DataConnectionString);
                OleDbCommand cmd = new OleDbCommand(SqlString, conn);
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                return reader;
                reader.Close();
                conn.Close();
            }
        }


        public static ArrayList GetArrayList(string SqlString)
        {
            return GetArrayList(SqlString, Utils.Dataconnection());
        }
        /// <summary>
        /// Return the first field values in an Arraylist
        /// </summary>
        /// <param name="SqlString"></param>
        /// <param name="DataConnectionString"></param>
        /// <returns></returns>
        public static ArrayList GetArrayList(string SqlString, string DataConnectionString)
        {
            ArrayList al = new ArrayList();
            if (Utils.DbSQLConnection(DataConnectionString)) //SQL Databases
            {
                SqlConnection conn = new SqlConnection(DataConnectionString);
                SqlCommand cmd = new SqlCommand(SqlString, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    al.Add(reader[0].ToString());
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(DataConnectionString);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(SqlString, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    al.Add(reader[0].ToString());
                }
                reader.Close();
                conn.Close();
            }
            return al;
        }

   
    }
}

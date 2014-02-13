using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;


namespace i386.Newsletter
{
     /// <summary>
    /// Summary description for Subscriber 
    /// </summary>
    public class SubscriberMember
    {
        protected DataRow row = null;
        protected string _email = String.Empty;
        protected string _name = String.Empty;
        protected string _tablename = "SUBSCRIBERS";
         public SubscriberMember()
        {

        }
        public DataRow DataRow
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return DataRow["Email"].ToString();
            }
            set
            {
                _email = value;
            }
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return DataRow["Name"].ToString();
            }
            set
            {
                _name = value;
            }
        }
        /// <summary>
        /// Subscriber ID
        /// </summary>
        public int RecordID
        {
            get
            {
                return int.Parse(DataRow["id"].ToString());
            }
        }

        /// <summary>
        /// Load up List Data
        /// </summary>
        public void GetRecord(string ID)
        {
            int id = int.Parse(ID);
            DataTable dt = Utils.GetDataTable(_tablename, "ID=" + id.ToString());
            if (dt.Rows.Count < 1) DataRow = null;
            DataRow = dt.Rows[0];
        }

        /// <summary>
        /// Load up User Data from Email Address
        /// </summary>
        public DataRow GetRecord(string emailAddress, bool Editing)
        {
            emailAddress = emailAddress.Replace("''", "'").Trim().ToLower();
            DataTable dt = Utils.GetDataTable(_tablename, "Email='" + emailAddress + "'");
            if (dt.Rows.Count < 1) return null;
            row = dt.Rows[0];
            return row;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        public void Create()
        {
            string SqlString = "INSERT INTO " + _tablename + " ([Email],[Format],Created) Values ('{0}','HTML',{1})";
            SqlString = String.Format(SqlString, this.Email.Replace("'", "''"), Utils.GetTimeStamp());
            // Load up the new record
            this.GetRecord(((int)Utils.DbCmdInsert(SqlString)).ToString());          
        }





        ////// LEGACY //////
        /// <summary>
        /// Insert an Email and Name to Newsletter Lists
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Name"></param>
        /// <param name="Lists">Arraylist of record id of the Newsletter lists</param>
        public static void InsertIntoList(string Email, string Name, ArrayList Lists)
        {
            InsertIntoList(Email, Name, "HTML", Lists);
        }
        /// <summary>
        /// Insert an Email and Name to Newsletter Lists
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Name"></param>
        /// <param name="Lists">Arraylist of record id of the Newsletter lists</param>
        /// <param name="Format">Format of the Newsletter HTML or Text</param>
        public static void InsertIntoList(string Email, string Name, string Format, ArrayList Lists)
        {
            // Prepartion for SQL strings
            string IPAddress = ""; //System.Web.HttpContext.Current.Request.UserHostAddress - this work  with threading
            Name = Name.Replace("'", "''");
            Email = Email.Replace("'", "''");
            if (Format != "Text") Format = "HTML";


            Int64 newId;
            // Insert / Get Id from Subscriber
            string SqlExists = "SELECT id FROM SUBSCRIBERS WHERE Email='" + Email + "';";
            object oNewId = Utils.DbCmdExecuteScalar(SqlExists);
            if (oNewId == null)
            {  // Insert new record and return the new ID
                string SqlString = "INSERT INTO SUBSCRIBERS ([Name], Email, [Format], [Created])  Values ('" + Name + "','" + Email + "','" + Format + "'," + Utils.GetTimeStamp() + ");";
                newId = Convert.ToInt64(Utils.DbCmdInsert(SqlString));
            } // return the existing ID
            else
            {
                string SqlUpdate = "UPDATE SUBSCRIBERS SET [Format]='" + Format + "', [Name]='" + Name + "' WHERE Email='" + Email + "';";
                Utils.DbCmdExecute(SqlUpdate);
                newId = Convert.ToInt64(oNewId);
            }
            // Subscribers to List
            foreach (string List in Lists)
            {
                // check if the subscriber is a member of the list, if not then insert
                string SqlCheckList = "SELECT slid FROM SubscribersLists  WHERE sid=" + newId + " AND lid=" + List;
                object oCheckId = Utils.DbCmdExecuteScalar(SqlCheckList);
                if (oCheckId == null)
                {
                    // Insert an entry into Subscriberslists for each Newsletter

                    string SqlInsert = "INSERT INTO SubscribersLists (Confirm, FormatHtml, sid, lid, SubscriptionDate,IPAddress) VALUES (true,true," + newId + "," + List + "," + Utils.GetTimeStamp() + ",'" + IPAddress + "');";
                    Utils.DbCmdExecute(SqlInsert);
                }
            }
        }

        /// <summary>
        /// Remove an Email and Name from Newsletter Lists
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Name"></param>
        /// <param name="Lists">Arraylist of record id of the Newsletter lists</param>
        public static void RemoveFromList(string Email, string Name, ArrayList Lists)
        {
            // Prepartion for SQL strings
            Name = Name.Replace("'", "''");
            Email = Email.Replace("'", "''");


            string SqlExists = "SELECT id FROM SUBSCRIBERS WHERE Email='" + Email + "';";
            object oRecId = Utils.DbCmdExecuteScalar(SqlExists);

            if (oRecId != null) // Does the Subscriber exist?
            {
                Int64 recId = Convert.ToInt64(oRecId);
                foreach (string List in Lists)
                {
                    string SqlRemove = "DELETE * FROM SubscribersLists WHERE sid=" + recId + " AND lid=" + List;
                    Utils.DbCmdExecute(SqlRemove);
                }
                // Now check if there is any subscriptions left in the SubscribersLists table
                string SqlSubRecId = "SELECT slid FROM SUBSCRIBERSLISTS WHERE sid=" + recId;
                object oSubRecId = Utils.DbCmdExecuteScalar(SqlSubRecId);
                if (oSubRecId == null)
                {
                    // Delete the subscribers
                    string SqlDelete = "DELETE * FROM SUBSCRIBERS WHERE id=" + recId;
                    Utils.DbCmdExecute(SqlDelete);
                }

            }

        }

        /// <summary>
        /// Get Subscribers email addresses into Arraylist from a Newsletter List
        /// </summary>
        /// <param name="ListID">Newsletter List ID</param>
        /// <returns></returns>
        public static ArrayList GetFromList(string ListID)
        {
            string SqlString = "SELECT Email, SubscribersLists.lid FROM Subscribers " +
                "INNER JOIN SubscribersLists ON Subscribers.id = SubscribersLists.sid " +
                "WHERE (((SubscribersLists.lid)=" + ListID + "))";
            ArrayList emailaddresses = new ArrayList();
            try
            {
                string DataConnectionString = Utils.Dataconnection();

                OleDbConnection conn = new OleDbConnection(DataConnectionString);

                OleDbCommand cmd = new OleDbCommand(SqlString, conn);
                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    emailaddresses.Add(reader["email"].ToString());
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception ErrMsg)
            {
                System.Web.HttpContext.Current.Response.Write(ErrMsg.Message + " SQLString:" + SqlString);
            }
            return emailaddresses;
        }
    }
}
using System;
using System.Data;
using System.Collections;

namespace i386.Newsletter
{
    /// <summary>
    /// Summary description for Lists
    /// </summary>
    public class Lists
    {
        private DataRow row = null;
        private string _listname = String.Empty;
        public Lists()
        {
        }
        
        /// <summary>
        /// List Name
        /// </summary>
        public string ListName
        {
            get
            {
                return _listname;
            }
            set
            {
                _listname = value;
            }
        }
                /// <summary>
        /// List ID
        /// </summary>
        public int RecordID
        {
            get
            {
                return int.Parse(row["id"].ToString());
            }
        }
        /// <summary>
        /// Load up List Data
        /// </summary>
        public DataRow GetRecord(string ListID)
        {
            int id = int.Parse(ListID);
            DataTable dt = Utils.GetDataTable("SELECT * FROM LISTS WHERE (ID=" + id.ToString() + ");");
            if (dt.Rows.Count < 1) return null;
            row = dt.Rows[0];
            // Set Fields
            _listname = row["ListName"].ToString();
            return row;
        }
        /// <summary>
        /// Get the Lists the subscriber is subscribed to.
        /// </summary>
        /// <returns></returns>
        public Lists[] GetSubscribedLists(string UserId)
        {
            string sql = " ";
            //// Add the SQL
            DataTable dt = Utils.GetDataTable(sql);
            int n = dt.Rows.Count;
            if (n < 1) return null;
            Lists[] lists = new Lists[n];
            for (int i = 0; i < n; i++)
            {
                string ListID = dt.Rows[i][0].ToString();
                lists[i] = new Lists();
                lists[i].GetRecord(ListID);
            }
            return lists;
        }

        /// <summary>
        /// Create a new list
        /// </summary>
        public void Create()
        {
            string SqlString = "INSERT INTO LISTS ([ListName], CreatedBy, DateCreated) Values ('{0}', '{1}',{2})";
            SqlString = String.Format(SqlString, this.ListName.Replace("'", "''"), UserLogin.GetCurrentName(), Utils.GetTimeStamp());
            // Load up the new record
            this.GetRecord(((int)Utils.DbCmdInsert(SqlString)).ToString());
            AddListManager();
        }
        // Add the current logged in user as a list manager
        public void AddListManager()
        {
            string SqlListUsers = "INSERT INTO UsersLists (uid,lid) values (" + UserLogin.GetCurrentUserID() + "," + this.RecordID + ")";
            Utils.DbCmdExecute(SqlListUsers);
        }




        /// ------------------------ LEGACY ----------


        /// <summary>
        /// Delete Newsletter List and Newsletters in the list which are not in any other Newsletter Lists.
        /// </summary>
        /// <param name="ListID">List Newsletter Id</param>
        public static void Delete(string ListID)
        {


            // Retrieve Newsletters associated with this Newsletter List
            string SqlNewsLetterList = "SELECT * FROM NewsletterLists WHERE LID=" + ListID;
            DataTable dt = Utils.GetDataTable(SqlNewsLetterList);
            foreach (DataRow dr in dt.Rows)
            {
                string NewsLetterId = dr["nid"].ToString();
                // Check each Newsletter doesn't exist in any other list.
                string SqlExistAnotherList = "SELECT * FROM NewsletterLists  WHERE NID=" + NewsLetterId + " AND NOT LID=" + ListID;
                // If it only exists in the list set for deletion then delete the newsletter too.
                object oExists = Utils.DbCmdExecuteScalar(SqlExistAnotherList);
                if (oExists == null)
                {
                    string SqlDeleteNewsletter = "DELETE * FROM NEWSLETTER WHERE ID=" + NewsLetterId;
                    Utils.DbCmdExecute(SqlDeleteNewsletter);
                }
            }
            // Delete Newsletter and List Assoications
            string SqlDeleteNewsletterList = "DELETE * FROM NewsletterLists WHERE LID=" + ListID;
            Utils.DbCmdExecute(SqlDeleteNewsletterList);
            // Delete Lists
            string SqlDeleteList = "DELETE * FROM Lists WHERE ID=" + ListID;
            Utils.DbCmdExecute(SqlDeleteList);

            // Delete  Subscriptions
            string SqlDeleteSubLists = "DELETE * FROM SubscribersLists WHERE LID=" + ListID;
            Utils.DbCmdExecute(SqlDeleteSubLists);

            //Delete User Associations
            string SqlDeleteUsersLists = "DELETE * FROM UsersLists WHERE LID=" + ListID;
            Utils.DbCmdExecute(SqlDeleteUsersLists);

        }

        /// <summary>
        /// Get the Newsletter Lists for the current logged in User.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static object GetCurrentUserLists()
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            UserName = UserName.Replace("'", "''");
            string SqlString = "SELECT Lists.ListName, Lists.id FROM (UsersLists INNER JOIN Users ON UsersLists.uid = Users.ID) " +
                "INNER JOIN Lists ON UsersLists.lid = Lists.id WHERE (((Users.UserName)='" + UserName + "')) ORDER BY Lists.ListName;";
            return Utils.DataReader(SqlString);
        }
        /// <summary>
        /// Get the List for the current logged in user
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetUserLists()
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            return GetUserLists(UserName);
        }
        /// <summary>
        /// Get the List id's for a user
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static ArrayList GetUserLists(string UserName)
        {
            UserName = UserName.Replace("'", "''");
            string sql = "SELECT Lists.id FROM (UsersLists INNER JOIN Users ON UsersLists.uid = Users.ID) " +
                "INNER JOIN Lists ON UsersLists.lid = Lists.id WHERE (((Users.UserName)='" + UserName + "')) ORDER BY Lists.ListName;";
            return Utils.GetArrayList(sql);
        }

        public static object GetLists()
        {
            string SqlString = "SELECT * FROM LISTS ORDER BY LISTNAME";
            return Utils.DataReader(SqlString);

        }
    }
}
using System;
using System.Data;
using System.Text;
using System.Collections;
using i386;


namespace i386.Newsletter
{
   
    /// <summary>
    /// Summary description for Newsletter
    /// </summary>
    public class Newsletter : Letters
    {
        private DataRow row = null;
        private string _from = String.Empty;
        private string _name = String.Empty;
        private string _fromname = String.Empty;
        private string _url = String.Empty;
        private string _html = String.Empty;
        private string _subject = String.Empty;
        private string _createdby = String.Empty;
        protected string _tablename = "NEWSLETTERS";
        private DateTime _createdon;
        private DateTime _lastmodified;
        private int _views = 0;
        public Newsletter()
        {
        }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public string CreatedBy
        {
            get
            {
                return _createdby;
            }
            set
            {
                _createdby = value;
            }
        }
        // Created On
        public DateTime CreatedOn
        {
            get
            {
                return _createdon;
            }
            set
            {
                _createdon = value;
            }
        }
        // Last Modified Date
        public DateTime LastModified
        {
            get
            {
                return _lastmodified;
            }
            set
            {
                _lastmodified = value;
            }
        }
        // Number Of Views
        public int Views
        {
            get
            {
                return _views;
            }
            set
            {
                _views = value;
            }
        }
        /// <summary>
        /// Html Content
        /// </summary>
        public string HTMLContent
        {
            get
            {
                return _html;
            }
            set
            {
               _html= value;
            }
        }
        /// <summary>
        /// Content from a URL
        /// </summary>
        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        /// <summary>
        /// Newsletter Name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        /// <summary>
        /// Newsletter Subject
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }
        /// <summary>
        /// Newsletter Status
        /// </summary>
        public string Status
        {
            get
            {
                if (row != null)
                    return row["Status"].ToString();
                else
                    return String.Empty;
            }
        }
        /// <summary>
        /// Newsletter From
        /// </summary>
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }
        /// <summary>
        /// Newsletter FromName
        /// </summary>
        public string FromName
        {
            get
            {
                return _fromname;
            }
            set
            {
                _fromname = value;
            }
        }
        /// <summary>
        /// Newsletter ID
        /// </summary>
        public int RecordID
        {
            get
            {
              return int.Parse(row["id"].ToString());
            }
        }
        /// <summary>
        /// Load up Newsletter Data
        /// </summary>
        /// <param name="newsletterID"></param>
        public DataRow GetRecord(string newsletterID)
        {
            int id = int.Parse(newsletterID);
            string sql = "SELECT * FROM {0} WHERE (ID={1});";
            sql = String.Format(sql, _tablename, id.ToString());
            DataTable dt =Utils.GetDataTable(sql);           
            if (dt.Rows.Count<1) return null;
            row = dt.Rows[0];
            // Set Fields
            _from = row["From"].ToString();
            _html = row["html"].ToString();
            _url = row["url"].ToString();
            _name = row["Name"].ToString();
            _subject = row["Subject"].ToString();
            _fromname = row["FromName"].ToString();
            _views = int.Parse(row["Views"].ToString());
            _createdby = row["CreatedBy"].ToString();
            _createdon = (DateTime)row["Created"];
            if (!row.IsNull("LastModified"))
                _lastmodified = (DateTime)row["LastModified"];
            return row;
        }
        /// <summary>
        /// Get the Body Text based on URL or HTML Content
        /// </summary>
        /// <returns></returns>
        public string GetBody()
        {
            string body = String.Empty;
            if (this.URL !="") 
                body = WebUtils.GrabUrl(this.URL);
			else
			{
                string BaseTag = "";
				/// Check for a base tag
			//	string BaseTag = TextUtils.RemoveRegExp(@"\<BASE( .*?)>", this.HTMLContent);
			//	if (BaseTag=="") BaseTag = @"<base href=""http://" + Request.ServerVariables["SERVER_NAME"] + @""" />";
                return @"<html><head>" + BaseTag + "</head><body>" + this.HTMLContent + "</body></html>"; 
			}
            // Need to put in the footer too!
            string HTMLFooter = Tracking.Footer(this.RecordID.ToString(), (string)this.GetListIds[0], "<!--EMAIL-->", "HTML", true);
            // Replace the closing Body 
            body = body.Replace("</body>", HTMLFooter + "</body>");

        }
        /// <summary>
        /// Get Subscribers of the Newsletter
        /// </summary>
        /// <returns></returns>
        public SubscriberMember[] GetSubscribers()
        {
            // Build SQL with all the Subscribers
            string sql = @"SELECT Subscribers.* FROM NewsletterLists INNER JOIN (Subscribers INNER JOIN SubscribersLists ON Subscribers.ID = SubscribersLists.sid) ON NewsletterLists.lid = SubscribersLists.lid
WHERE (((NewsletterLists.nid)={0}));";
            sql = String.Format(sql, this.RecordID.ToString());

            //// Add the SQL
            DataTable dt = Utils.GetDataTable(sql);
            int recNum = dt.Rows.Count;
            if (recNum < 1) 
                return null;
            SubscriberMember[] subs = new SubscriberMember[recNum];
            for (int i = 0; i < recNum; i++)
            {
                subs[i] = new SubscriberMember();
                subs[i].DataRow = dt.Rows[i];               
            }
            return subs;
        }


        /// <summary>
        /// Delete this Newsletter
        /// </summary>
        /// <param name="newsletterID"></param>
        public void Delete()
        {
            // Remove from all list
            RemoveFromLists();
            // Delete newsletter
            string sql = "DELETE * FROM {0} WHERE (ID={1});";
            sql = String.Format(sql, _tablename, this.RecordID.ToString());
            Utils.DbCmdExecute(sql);
        }
        /// <summary>
        /// Get Lists that the Newsletter belongs to
        /// </summary>
        /// <returns></returns>
        public ArrayList GetListIds()
        {
            // Return DataTable of results
            return Utils.GetArrayList("SELECT lid FROM NewsletterLists WHERE (nid=" + this.RecordID + ");");
        }

        /// <summary>
        /// Get the Mailing List this newsletter is in.
        /// </summary>
        /// <returns></returns>
        public Lists[] GetLists()
        {
            string sql = "SELECT lid FROM NewsletterLists WHERE (nid={0});";
            sql = String.Format(sql, this.RecordID);
            DataTable dt = Utils.GetDataTable(sql);
            int recNum = dt.Rows.Count;
            if (recNum < 1) 
                return null;
            Lists[] lists = new Lists[recNum];
            for (int i = 0; i < recNum; i++)
            {
                string ListID = dt.Rows[i][0].ToString();
                lists[i] = new Lists();
                lists[i].GetRecord(ListID);
            }
            return lists;
        }
        /// <summary>
        /// Remove the Newsletter from all Lists
        /// </summary>
        public void RemoveFromLists()
        {
            // Execute SQL
            Utils.DbCmdExecute("DELETE * FROM NewsletterLists WHERE (nid=" + this.RecordID + ");");
        }
        /// <summary>
        /// Add Newsletter to a List
        /// </summary>
        /// <param name="ListID"></param>
        public void AddToList(string ListID)
        {
            // Safegaurd against SQL injections
            int ID = int.Parse(ListID);
            // Execute SQL
            Utils.DbCmdExecute("INSERT INTO NewsletterLists (nid,lid) VALUES (" +  this.RecordID+ "," + ID.ToString() + ");");
        }
        /// <summary>
        /// Add Newsletter to a List
        /// </summary>
        /// <param name="ListID"></param>
        public void AddToList(ArrayList ListIDs)
        {
            foreach (string ListID in ListIDs)
            {
                // Safegaurd against SQL injections
                int ID = int.Parse(ListID);
                // Execute SQL
                Utils.DbCmdExecute("INSERT INTO NewsletterLists (nid,lid) VALUES (" + this.RecordID + "," + ID.ToString() + ");");
            }
        }
        //Save Newsletter as new record.
        public void SaveNew()
        {
            // Insert a new record into the database
            this.Save("0");
        }
        /// <summary>
        /// Save an new or existing newsletter
        /// </summary>
        public void Save(string RecordID)
        {
            // Safegaurd against SQL injections
            int ID = int.Parse(RecordID);
            StringBuilder sql = new StringBuilder();   
         
            //Insert http into the URL if it's not there            
            if (!this.URL.StartsWith("http://"))
                URL = "http://" + URL;
            if (this.URL == "http://") this.URL = string.Empty;

            // Prepare the Data for a SQL string 
            // Would be better to use SQLCommand / Parameter with a data layer - in the next version :-) 
            string sqlSubject = Subject.Replace("'", "''");
            string sqlName = Name.Replace("'", "''");
            string sqlFrom = From.Replace("'", "''");
            string sqlFromName = FromName.Replace("'", "''");
            string sqlHtml = HTMLContent.Replace("'", "''");
            string sqlURL = URL.Replace("'", "''");
            string sqlCreatedBy = CreatedBy.Replace("'", "''");
            
            if (ID>0)
            {
                // Build the Update SQL String 
                sql.Append("UPDATE NEWSLETTERS SET ");
                sql.Append("[Subject]='" + sqlSubject + "', [Name]='" + Name + "', ");
                sql.Append("[From]='" + sqlFrom + "', [FromName]='" + sqlFromName + "', ");
                sql.Append("[Html]='" + sqlHtml + "', ");
                sql.Append("[URL]='" + sqlURL + "', ");
                sql.Append("LastModified=" + Utils.GetTimeStamp() + " WHERE ID=" + ID.ToString());
                // Update the Record
                Utils.DbCmdExecute(sql.ToString());
                // Reload the Data
                GetRecord(ID.ToString()); 
            }
            else
            {
                // Build the Insert SQL String
                sql.Append("INSERT INTO NEWSLETTERS ([Name],[Status],[CreatedBy], [FromName],[From],[Subject],[html],[url],[Created],[LastModified]) ");
                sql.Append("Values ('" + Name + "',");
                sql.Append("'Edit',"); // Status
                sql.Append("'" + sqlCreatedBy + "',"); // Created By
                sql.Append("'" + sqlFromName + "',");  // From Name
                sql.Append("'" + sqlFrom + "',"); //From
                sql.Append("'" + sqlSubject+ "',"); 
                sql.Append("'" + sqlHtml+ "',"); 
                sql.Append("'" + sqlURL + "',"); // Url
                sql.Append(Utils.GetTimeStamp() + ","); 
                sql.Append(Utils.GetTimeStamp() + ");"); 
                // Insert the Record and return the new ID
                int RecID = Convert.ToInt16(Utils.DbCmdInsert(sql.ToString()));
                // Reload the Data
                GetRecord(RecID.ToString());
            }
        }
        /// <summary>
        /// Copy a newsletter, note doesn't copy the view, status fields
        /// then load it up.
        /// </summary>
        /// <param name="RecordID">Record ID of newsletter you wish to copy</param>
        public void Copy(string RecordID, string CopiedBy)
        {
            //Load the letter
            this.GetRecord(RecordID);
            // Create a new one and copy the data into it.
            Newsletter newletter = new Newsletter();
            newletter.CreatedBy = CopiedBy;
            newletter.From = this.From;
            newletter.FromName = this.FromName;
            newletter.HTMLContent = this.HTMLContent;
            newletter.Name = "Copy of " + this.Name;
            newletter.Subject = this.Name;
            newletter.URL = this.URL;
            // Copy Lists
            newletter.AddToList(this.GetListIds());
            //Save the newsletter as new letter.
            newletter.SaveNew();
            //Load the new letter.
            this.GetRecord(newletter.RecordID.ToString());
        }
        /// <summary>
        /// Set the status of the Newsletter
        /// </summary>
        /// <param name="Status">set to Sent</param>
        public void SetStatus(string Status)
        {

            string sql = "UPDATE {0} SET Status='{1}', Views=0, Sent={2} WHERE ID=(" + this.RecordID.ToString() + ");";
            sql = String.Format(sql, _tablename, Status, Utils.GetTimeStamp());
            // Execute SQL
            Utils.DbCmdExecute(sql);
        }

     }

    /// <summary>
    /// Legacy Object
    /// </summary>
    public class Letters
    {
        /// <summary>
        /// Delete Newsletter and Delete the Associations in the NewslettersList Table
        /// </summary>
        /// <param name="ListID">Newsletter Id</param>
        public static void Delete(string NewsletterID)
        {
            // SQL String to execute
            string SqlString = "DELETE * FROM NEWSLETTERS WHERE ID=" + NewsletterID;
            string SqlAssociates = "DELETE * FROM NEWSLETTERLISTS WHERE NID=" + NewsletterID;
            // Execute SQL
            Utils.DbCmdExecute(SqlAssociates);
            Utils.DbCmdExecute(SqlString);
        }
        /// <summary>
        /// Get lists for a user
        /// </summary>
        /// <param name="ListID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool GetRights(string listID, string userID)
        {
            int lid = int.Parse(listID);
            int uid = int.Parse(userID);
            return GetRights(listID, userID);
        }
        /// <summary>
        /// Get lists for a user
        /// </summary>
        /// <param name="ListID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static bool GetRights(int listID, int userID)
        {
            // SQL String to execute
            StringBuilder sqlString = new StringBuilder("SELECT UsersLists.uid FROM Lists INNER JOIN (Newsletters INNER JOIN ");
            sqlString.Append("(NewsletterLists INNER JOIN UsersLists ON NewsletterLists.lid = UsersLists.lid) ON Newsletters.Id ");
            sqlString.Append("= NewsletterLists.nid ON Lists.id = UsersLists.lid GROUP BY UsersLists.uid, Newsletters.Id ");
            sqlString.Append("HAVING (((UsersLists.uid)={0})) AND ((Newsletters.Id)={1}));");
            // Add Parameter to SQL String
            String sql = String.Format(sqlString.ToString(), listID.ToString(), listID.ToString());
            object Rights = Utils.DbCmdExecuteScalar(sql);
            if (Rights != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Get the lists the current logged on user has access to.
        /// </summary>
        /// <param name="ListID"></param>
        /// <returns></returns>
        public static bool GetRights(string listID)
        {
            string userID = User.GetCurrentUserID().ToString();
            return GetRights(listID, userID);
        }
    }
}
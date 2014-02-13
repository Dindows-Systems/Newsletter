// i386 Newsletter System
// Copyright (c) 2007 i386 - http://www.i386.com
//
// LIMITATION OF LIABILITY
// The software is supplied "as is". Author cannot be held liable to you
// for any direct or indirect damage, or for any loss of income, loss of
// profits, operating losses or any costs incurred whatsoever. The software
// has been designed with care, but the Author does not guarantee that it is
// free of errors.


using System;
using System.Data;
using System.Web;
using System.Web.Security;


namespace i386.Newsletter
{
    public class UserLogin : i386.Newsletter.User
    {
        private DataRow row = null;
        private string _username = String.Empty;
        public UserLogin()
        {

        }
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        /// <summary>
        /// User ID
        /// </summary>
        public int RecordID
        {
            get
            {
                return int.Parse(row["id"].ToString());
            }
        }


        /// <summary>
        /// Load up User Data by UserID
        /// </summary>
        public DataRow GetRecord(string UserID)
        {
            int id = int.Parse(UserID);
            DataTable dt = Utils.GetDataTable("SELECT * FROM USERS WHERE (ID=" + id.ToString() + ");");
            if (dt.Rows.Count < 1) return null;
            row = dt.Rows[0];
            // Set Fields
            _username = row["UserName"].ToString();
            return row;
        }

        /// <summary>
        /// Load up User Data by UserName
        /// </summary>
        public DataRow GetCurrentUser()
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            UserName = UserName.Replace("'", "''");
            DataTable dt = Utils.GetDataTable("SELECT * FROM USERS WHERE ([UserName]='" + UserName + "');");
            if (dt.Rows.Count < 1) return null;
            row = dt.Rows[0];
            // Set Fields
            _username = row["UserName"].ToString();
            return row;
        }

        /// <summary>
        /// Get the List the user has access to.
        /// </summary>
        /// <returns></returns>
        public Lists[] GetLists()
        {
            string sql = "SELECT Lists.id FROM (UsersLists INNER JOIN Users ON UsersLists.uid = Users.ID) " +
              "INNER JOIN Lists ON UsersLists.lid = Lists.id WHERE (((Users.UserName)='" + this.UserName + "')) ORDER BY Lists.ListName;";
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
    }

	public class User
	{
		/// <summary>
		/// Creates a new user and return the record ID. If the UserName and EmailAddress are unique in the database. If they are not unique return the value -1
		/// </summary>
		/// <param name="UserName"></param>
		/// <param name="EmailAddress"></param>
		/// <returns></returns>
		public static Int64 Create(string UserName, string EmailAddress)
		{
			UserName=UserName.Replace("'","''");
			EmailAddress=EmailAddress.Replace("'","''");
			if (Exists(UserName, EmailAddress, false)) // Email is not in the database.
			{
				return -1;
			}
			else 
			{		
				
				
				// SQL String to execute
				string SqlString = "INSERT INTO USERS ([Email],[Role], [CreatedBy], [Created]) Values ('" + EmailAddress +"','User','" + System.Web.HttpContext.Current.User.Identity.Name + "'," + Utils.GetTimeStamp() + ")";
				// Execute SQL
				Int64 uid = Convert.ToInt64(Utils.DbCmdInsert(SqlString));
				return uid;
			}
				
		}
		public static bool Exists(string UserName, string EmailAddress, bool ExcludeCurrentLogin )
		{
			string SqlFilter="";
		//	if (ExcludeCurrentLogin) SqlFilter=" AND UserName<>
			
			
			// CHECK FOR EXISTANCE....
			string SqlExists = "SELECT email FROM Users WHERE (Email='" + EmailAddress + "' OR UserName='" + UserName + "') " + SqlFilter + ";";
			object oRecID = Utils.DbCmdExecuteScalar(SqlExists);
			if (oRecID==null) 
				return false;
			else
				return true;
		}
		public static void Delete(string UserID)
		{
			
			
			// SQL String to execute
			string SqlString = "DELETE * FROM USERSLISTS WHERE [uid]=" + UserID;
			string SqlDel= "DELETE * FROM USERS WHERE [id]=" +UserID;
			// Execute SQL
			Utils.DbCmdExecute(SqlString);
			Utils.DbCmdExecute(SqlDel);
		}
		public static Int64 GetCurrentUserID()
		{
			string UserName = System.Web.HttpContext.Current.User.Identity.Name;
			return GetUserID(UserName);
		}
		public static Int64 GetUserID(string UserName)
		{
			UserName=UserName.Replace("'","''");
			
			
			// SQL String to execute
			string SqlString = "SELECT ID FROM USERS WHERE [UserName]='" + UserName + "';";
			object oUserName = Utils.DbCmdExecuteScalar(SqlString);
			if (oUserName!=null)
				return Convert.ToInt64(oUserName);
			else
				return -1;
		}
		public static string GetCurrentEmailAddress()
		{
			string UserName = System.Web.HttpContext.Current.User.Identity.Name;
			return GetEmailAddress(UserName);
		}
		public static string GetEmailAddress(string UserName)
		{
			UserName=UserName.Replace("'","''");
			
			
			// SQL String to execute
			string SqlString = "SELECT EMAIL FROM USERS WHERE [UserName]='" + UserName + "';";
			object oEmail = Utils.DbCmdExecuteScalar(SqlString);
			if (oEmail!=null)
				return Convert.ToString(oEmail);
			else
				return String.Empty;
		}
		public static string GetCurrentName()
		{
			string UserName = System.Web.HttpContext.Current.User.Identity.Name;
			return GetName(UserName);
		}
		public static string GetName(string UserName)
		{
			UserName=UserName.Replace("'","''");
			
			
			// SQL String to execute
			string SqlString = "SELECT Name FROM USERS WHERE [UserName]='" + UserName + "';";
			object oName = Utils.DbCmdExecuteScalar(SqlString);
			if (oName!=null)
				return Convert.ToString(oName);
			else
				return String.Empty;
		}
		public static void AddLists(string UserID, string ListID)
		{
			
			
			// SQL String to execute
			string SqlString = "INSERT INTO USERSLISTS ([uid],[lid]) Values (" + UserID +"," + ListID + ")";
			// Execute SQL
			Utils.DbCmdExecute(SqlString);
		}
		public static bool Login(string UserId, string Password, bool chkPersistCookie)
		{
			if (Validate(UserId, Password) )
			{
				FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, UserId, DateTime.Now, DateTime.Now.AddMinutes(30), chkPersistCookie, "");
				string cookiestr = FormsAuthentication.Encrypt(tkt);
				HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
				if (chkPersistCookie) ck.Expires=tkt.Expiration;	
				ck.Path = FormsAuthentication.FormsCookiePath; 
				System.Web.HttpContext.Current.Response.Cookies.Add(ck);
				return true;
			}
			else
				return false;
		}
		public static void Logout(string ForwardUrl)
		{
			FormsAuthentication.SignOut();
			if (ForwardUrl!=null) System.Web.HttpContext.Current.Response.Redirect(ForwardUrl,true);
		}
		public static void Logout()
		{
			FormsAuthentication.SignOut();
			System.Web.HttpContext.Current.Response.Redirect("",true);
		}
		public static bool Validate(string userName, string passWord )
		{
			userName=userName.Replace("'","''");
			passWord=passWord.Replace("'","''");
			// Check for invalid userName.
			// userName must not be null and must be between 1 and 15 characters.
			if ( (  null == userName ) || ( 0 == userName.Length ) || ( userName.Length >50 ) )
			{
				System.Web.HttpContext.Current.Response.Write( "[ValidateUser] Input validation of userName failed." );
				return false;
			}
			// Check for invalid passWord.
			// passWord must not be null and must be between 1 and 25 characters.
			if ( (  null == passWord ) || ( 0 == passWord.Length ) || ( passWord.Length > 25 ) )
			{
				System.Web.HttpContext.Current.Response.Write( "[ValidateUser] Input validation of passWord failed." );
				return false;
			}
			string SQLValidate = "SELECT Role FROM Users WHERE (((UserName)='" + userName +"') AND ((Password)='" + passWord + "'));";
			
			object RecordRole = Utils.DbCmdExecuteScalar(SQLValidate);
			if (RecordRole!=null) 
			{
				// Set Role
				HttpCookie CookieRoles = new HttpCookie("NewsletterRole");
				CookieRoles.Value = Convert.ToString(RecordRole);
				System.Web.HttpContext.Current.Response.Cookies.Add(CookieRoles);
				return true;
			}				
			else return false;
		}
		public static string GetCurrentUserRole()
		{
			if (System.Web.HttpContext.Current.Request.Cookies["NewsletterRole"]!=null)
				return System.Web.HttpContext.Current.Request.Cookies["NewsletterRole"].Value;
			else
			{
				string UserName = System.Web.HttpContext.Current.User.Identity.Name;
				UserName = UserName.Replace("'","''");
				string SQLValidate = "SELECT Role FROM Users WHERE ((UserName)='" + UserName +"') ";
				
				object RecordRole = Utils.DbCmdExecuteScalar(SQLValidate);
				if (RecordRole!=null) 
					return Convert.ToString(RecordRole);
				else
					return null;
			}
		}

		/// <summary>
		/// Whether the current login in User has rights to the List
		/// </summary>
		/// <param name="ListID"></param>
		/// <returns></returns>
		public static bool RightsToList(string ListID)
		{
			string UserName = System.Web.HttpContext.Current.User.Identity.Name;
			return RightsToList(ListID, UserName);
		}
		public static bool RightsToList(string ListID,string UserName)
		{
			UserName=UserName.Replace("'","''");
			
			
			// SQL String to execute
			string SqlString = "SELECT Users.UserName FROM Users INNER JOIN UsersLists ON Users.ID = UsersLists.uid " + 
					" WHERE (((UsersLists.lid)= " + ListID + ") AND ((Users.UserName)='" +  UserName + "'));";
			object oUserName = Utils.DbCmdExecuteScalar(SqlString);
			if (oUserName!=null) 
				return true;
			else 
				return false;
		}
	
	}

    
 }

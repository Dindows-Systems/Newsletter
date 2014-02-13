using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Threading;
using Quiksoft.FreeSMTP;

namespace i386.Newsletter
{
    public class Mailer
    {
        protected Newsletter _newsletter;
        protected Config _config;
        protected StringBuilder _errorMsg;
        protected SubscriberMember[] _subscribers = null;
        protected string _mailServer;
        protected string _defaultBody;
        protected string _defaultSubject;
        protected string _defaultStatus = "Processing {0} of {1} Newsletters...";

        // Logging
        protected StringBuilder logData;
        private string _LogFilePath = string.Empty;
        private bool Logging = false;
        
        // --------- Progress Bar -----------------
        protected EO.Web.ProgressTaskEventArgs _progressTaskEventArgs;
        public EO.Web.ProgressTaskEventArgs ProgressTaskEventArgs
        {
            set { _progressTaskEventArgs = value; }
            get { return _progressTaskEventArgs; }
        }
        // -----------------------------------------

        /// <summary>
        /// Initialize objects
        /// </summary>
        public Mailer()
        {
            _errorMsg = new StringBuilder();
            _config = new Config();
            _mailServer = _config.SMTPServer;
            _newsletter = new Newsletter();
        }

        /// <summary>
        /// Newsletter to process
        /// </summary>
        public Newsletter Newsletter
        {
            set {                
                _newsletter = value;
                _subscribers = _newsletter.GetSubscribers();
                _defaultBody = this.Newsletter.GetBody();
                _defaultSubject = this.Newsletter.Subject;
            }
            get { return _newsletter; }
        }

        public SubscriberMember[] Subscribers
        {
            get { return _subscribers; }
        }

        public string LogFilePath
        {
            set
            {
                _LogFilePath = value;
                if (LogFilePath != string.Empty) Logging = true;
            }
            get { return _LogFilePath; }
        }
        /// <summary>
        /// Send to all Recipents
        /// </summary>
        public void Send()
        {
            int subCount = Subscribers.Length;
            int sentCount = 0;
            decimal Counter = 1;
            int LineNum = 0;
            string StatusMessage = String.Empty;
            // --------- Progress Bar -----------------
            ProgressTaskEventArgs.UpdateProgress(0, "Running...");
            // -------------------------------
            foreach (SubscriberMember subscriber in Subscribers)
            {
                // --------- Progress Bar -----------------
                if (ProgressTaskEventArgs.IsStopped)
                    break;
                // -------------------------------
                if (SendToSubscriber(subscriber, LineNum)) 
                    sentCount++;
                System.Threading.Thread.Sleep(1); // Progress Bar doesn't seem to update without this.
                decimal percentCount = (Counter / subCount) * 100;
                int count = Convert.ToInt16(percentCount);
                StatusMessage = String.Format(_defaultStatus, Counter.ToString(), subCount.ToString(), count);
                // --------- Progress Bar -----------------
                ProgressTaskEventArgs.UpdateProgress(count, StatusMessage); // Update Progress Bar                  
                // -------------------------------               
                Counter++;
                LineNum++;
            }
            // Update the Newsletter Status
            Newsletter.SetStatus("Sent");
            StatusMessage = " Sent {0}/{1} Newsletter ";
            StatusMessage = string.Format(StatusMessage, sentCount.ToString(), subCount.ToString());
            // --------- Progress Bar -----------------
            ProgressTaskEventArgs.UpdateProgress(100,  StatusMessage ); // Update Progress Bar                  
            // -------------------------------
        }


        /// <summary>
        /// Mail Merging with Subscriber's details
        /// </summary>
        /// <param name="subscriber">Subscriber Object</param>
        /// <param name="Content">Body of the email</param>
        /// <returns></returns>
        private string MailMerging(SubscriberMember subscriber, string Content)
        {
            string ReplStr = Content;
            foreach (DataColumn Column in subscriber.DataRow.Table.Columns)
            {
                string ColumnName = Column.ToString().ToUpper();
                ReplStr = ReplStr.Replace("<!--" + ColumnName + "-->", Column.ColumnName);
            }
            return ReplStr;
        }
        /// <summary>
        /// Return the last error messages
        /// </summary>
        /// <returns></returns>
        public string GetLastErrorMessages()
        {
            return _errorMsg.ToString();
        }
/// <summary>
/// Send a Subscriber a Newsletter 
/// </summary>
/// <param name="subscriber"></param>
/// <param name="Counter"></param>
/// <returns>True = OK, False = FAILED writes to the log file.</returns>
        public bool SendToSubscriber(SubscriberMember subscriber, int Counter)
        {
            // Reset the Log file
            if (Counter==1 && Logging)
                    File.Delete(this.LogFilePath);
            // Mail Merging with Subscriber's details
            string subscriberBody = MailMerging(subscriber, _defaultBody);
            string subscriberSubject = MailMerging(subscriber, _defaultSubject);
            // Create a mail Message
            SMTP stmpobj = new SMTP(_mailServer);
            EmailMessage email = new EmailMessage();
            email.CustomHeaders.Add("Reply-To", Newsletter.From);//Reply-To Header
            email.From.Email = Newsletter.From;//Friendly From Name                    
            if (Newsletter.FromName != "") email.From.Name = Newsletter.FromName;
            email.Subject = subscriberSubject;
            // Contents
            email.BodyParts.Add(subscriberBody, BodyPartFormat.HTML);
            email.Recipients.Add(subscriber.Email, subscriber.Name);
            try
            {
                stmpobj.Send(email);
                if (Logging)
                    LogMessage(Counter.ToString() + ";" + subscriber.Name + ";" + subscriber.Email + ";" + DateTime.Now.ToString() + ";Success");
                return true;
            }
            catch (Exception ErrorMsg)
            {
                if (Logging)
                    LogMessage(Counter.ToString() + ";" + subscriber.Name + ";" + subscriber.Email + ";" + DateTime.Now.ToString() + ";Fail:" + ErrorMsg.Message);
                return false;
            }
        }
        // Add Message to the log file.
        public void LogMessage(string Message)
        {
            StreamWriter sw = File.AppendText(LogFilePath);
            sw.WriteLine( Message);
            sw.Close();
        }

        /// <summary>
        /// Send a newsletter to an email address
        /// </summary>
        /// <param name="newsletter">Newsletter Object</param>
        /// <param name="ToRecipient">Email Address</param>
        public void Send(Newsletter newsletter, string ToRecipient)
        {
            try
            {
                SMTP stmpobj = new SMTP(_config.SMTPServer);
                EmailMessage email = new EmailMessage();
                email.BodyParts.Add(newsletter.GetBody(), BodyPartFormat.HTML);
                email.From.Name = newsletter.FromName;
                email.From.Email = newsletter.From;
                email.Subject = newsletter.Subject;
                email.Recipients.Add(ToRecipient);
                stmpobj.Send(email);
            }
            catch (Exception ex)
            {
                _errorMsg.Append("Error: ");
                _errorMsg.Append(ex.Message);
                _errorMsg.Append("\n");
            }
        }
    }
}
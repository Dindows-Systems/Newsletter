using System;
using System.Collections.Generic;
using System.Text;

namespace i386.Newsletter
{
    public class LangConfig : BaseConfig
    {
        // Default Values
        public string Delete = "Delete";
        public string Add = "Add";
        public string Edit = "Edit";
        public string Send = "Send";
        public string View = "View";
        public string Clone = "CloneDefault";
        public string Status = "Status";
        public string Newsletters = "Newsletters";
        public string Search = "Search";
        public string SentDate = "Sent Date";
        public string Recipients = "Recipients";
        public string NewsletterName = "Newsletter Name";
        public string ModifiedDate = "Modified Date";
        public string ReleaseDate = "Release Date";
        public string DateCreated = "Date Created";
        public string SubscriptionDate = "Subscription Date";
        public string UserName = "User Name";
        public string Name = "Name";
        public string Subject = "Subject";
        public string FromName = "From Name";
        public string ReplyToEmail = "Reply-To Email";
        public string RetrieveHTMLUrl = "Retrieve HTML from Url";
        public string RetrieveTextUrl = "Retrieve Text from Url";
        public string HTMLBody = "HTML Body";
        public string TextBody = "Text Body";
        public string Format = "Format";
        public string Text = "Text";
        public string HTML = "HTML";

        // Questions
        public string QuestionDeleteList = "Delete this Distribution List with all it Newsletters and Subscribers ?";
        public string SaveList = "Save this Newsletter list";
        public string SaveSubscriber = "Save this subscriber";
        public string QuestionDeleteSubscriber = "Are you sure you want to delete this subscriber ?";
        public string QuestionDeleteUser = "Are you sure you want to delete this user ?";
        //Letter
        public string AddLetter = "Add Newsletter";

        // Lists
        public string NewsletterLists = "Newsletter Lists";
        public string ListName = "List Name";
        public string AddList = "Add List";
        public string EditList = "Edit List";
        public string Lists = "Lists";
        //Subscribers
        public string AddSubscriber = "Add Subscriber";
        public string EditSubscriber = "Edit Subscriber";
        public string ClickUnsubscribe = "Click to here to unsubscribe";
        public string UnsubscribedFrom = "You are now unsubscribed from $NEWSLETTER$";
        public string SubscribedTo = "Thank you, for subscribing to $NEWSLETTER$";
        // Users
        public string AddUser = "Add User";
        public string EditUser = "Edit User";
        public string SaveUser = "Save this user";
        public string DeleteUser = "Delete this user";
        public string Role = "Role";


        //General
        public string CloseWindow = "Close";
        public string EmailAddress = "Email Address";
        public string Password = "Password";
        public string Language = "Language";
        public string Description = "Description";
        public string Email = "Email";
        public string Username = "Username";

        //Error
        public string ErrorInvalidEmail = "This email address is invalid.";
        public string ErrorEmailExists = "This email address already exists.";
        public string ErrorFillField = "Please fill in the field.";
        public string ErrorEmailUserExists = "This email address or username already exists.";
        public string ErrorIncorrectLogin = "User name or password incorrect";
        public string SendSample = "Send a sample newsletter to the Reply-To-Email";

        public string FindNow = "Find Now";
        public string RecordsFound = " records found";
        public string ConfirmDeleteNewsletter = "Are you sure you wish to delete this Newsletter?";
        public string ConfirmSendNewsletter = "Do you wish to send this Newsletter to its Recipients?";
        public string ConfirmCloneNewsletter = "Do you wish to make a copy of this Newsletter?";
        public string AltAddNewsletter = "Add New Newsletter";
        public string AltPreview = "Show a preview of this newsletter";
        public string AltSend = "Send newsletter to the recipients immediately";
        public string AltEditNewsletter = "Edit/Delete this newsletter";
        public string AltClone = "Clone this newsletter";
        public string AltDelete = "Delete this newsletter";
        public string AllLists = "All Lists";
        public string From = "From";
        public string To = "To";
    }
}

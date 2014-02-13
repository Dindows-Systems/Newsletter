using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

using i386.Newsletter;

public partial class NewsletterEdit : BasePage
{
    public Newsletter newsletter;
    protected void Page_Load(object sender, EventArgs e)
    {
      //  System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
      //  System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("de-DE");
  
        if (!Page.IsPostBack)
        {
            string index = Request.QueryString["index"];

            if (index != null)
            {
                // Edit TItle
                string editText = (String)GetGlobalResourceObject("labels", "edit");
                ltrlEdit.Text = editText;
                // Edit
                ViewState["NewsletterID"] = index;
                Bind();     
                PopulateLists();
            }
            else
            {
                // Add Title
                string addText = (String)GetGlobalResourceObject("labels", "add");
                ltrlEdit.Text = addText;
                // Add 
                PopulateListsNew();
                // Fill the form with defaults
                txtFromName.Text = UserLogin.GetCurrentName();
                txtFrom.Text = UserLogin.GetCurrentEmailAddress();
                txtSubject.Text = DateTime.Now.ToString(" dd - MMM. ") + " Newsletter";
                txtName.Text = DateTime.Now.ToString(" dd - MMM. ") + " Newsletter";
                Page.Title = "Add Newsletter";                
                // Set the ViewState to create a new newsletter
                ViewState["NewsletterID"] = -1;
            }

            //Javascript for confirmation of deletion.
            lnkDelete.Attributes["onclick"] = javascriptAreYouSure("theNewsletter", "delete");

            //        lnkSendTest.Attributes.Add("onclick",
            //          "cleanwindow('send.aspx?test=test&index=" + newsletter.RecordID.ToString() + "', 'Send', 'Width=600px, height=70px');");




            if (Request.QueryString["view"] == "1") lnkPreview_Click(sender, e);

        }
    }
    private void Bind()
    {
        lnkPreview.Visible = true;
        lnkDelete.Visible = true;
        lnkSend.Visible = true;
        string index = ViewState["NewsletterID"].ToString();
        if (UserLogin.GetCurrentUserRole() == "Admin" || Letters.GetRights(index))
        {
            // Get Newsletter Data.
            newsletter = new Newsletter();
            newsletter.GetRecord(index);
            // Populate Form
            txtName.Text = newsletter.Name;
            txtFrom.Text = newsletter.From;
            txtFromName.Text = newsletter.FromName;
            txtSubject.Text = newsletter.Subject;
           
            txtURL.Text = newsletter.URL;
            txtHtml.Text = newsletter.HTMLContent;
            ViewState["NewsletterID"] = newsletter.RecordID;
            // Tracking Frame
            iframe1.Attributes.Add("src", "../Tracking/LinkTracking.aspx?n=" + newsletter.RecordID.ToString());
            // Panel Display
            //if (newsletter.URL != string.Empty)
            // View/Status
            if (newsletter.Views > 0)
            {
                String views = (String)GetGlobalResourceObject("labels", "viewcount");
                lblCurrentStatus.Text = newsletter.Status + " " + newsletter.Views.ToString() + " " + views;
            }
            else
            {
                lblCurrentStatus.Text = newsletter.Status;
            }

        }
        else
        {
            Response.Redirect("../Error.aspx?Message=500");
        }

    }
    /// <summary>
    /// Populate the Lists for a new Newslettter
    /// </summary>
    protected void PopulateListsNew()
    {
        // Get the Current User's Lists
        UserLogin userlogin = new UserLogin();
        userlogin.GetCurrentUser();
        foreach (Lists list in userlogin.GetLists())
        {
            ListItem li = new ListItem(list.ListName, list.RecordID.ToString());
            lstRecipients.LeftListBox.Items.Add(li);
        }
    }

    /// <summary>
    /// Populate the Lists for an existing Newsletter
    /// </summary>
    protected void PopulateLists()
    {
        // Get the Current User's Lists
        UserLogin userlogin = new UserLogin();
        userlogin.GetCurrentUser();
        lstRecipients.LeftListBox.Items.Clear();
        lstRecipients.RightListBox.Items.Clear();
        ArrayList newsletterLists = newsletter.GetListIds();
        foreach (Lists list in userlogin.GetLists())
        {
            ListItem li = new ListItem(list.ListName, list.RecordID.ToString());
            if (newsletterLists.Contains(list.RecordID.ToString()))
                lstRecipients.RightListBox.Items.Add(li);
            else
                lstRecipients.LeftListBox.Items.Add(li);
        }
    }
    /// <summary>
    /// Delete Newsletter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Bind();
        // Delete this Newsletter
        newsletter.Delete();
        // Redirect to Newsletter List
        Response.Redirect("default.aspx");
    }

    /// <summary>
    /// Update the newsletter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    /// <summary>
    /// Update the Newsletter Data / Create a new newsletter
    /// </summary>
    protected void SaveData()
    {
        // Get the Form Information
        Newsletter saveData = new Newsletter();
        saveData.From = txtFrom.Text;
        saveData.FromName = txtFromName.Text;
        saveData.HTMLContent = txtHtml.Text;
        saveData.Name = txtName.Text;
        saveData.Subject = txtSubject.Text;
        saveData.URL = txtURL.Text;
        // Save Changes
        saveData.Save(ViewState["NewsletterID"].ToString());
        // Update the Viewstate with the new RecordID for a new newsletter.
        ViewState["NewsletterID"] = saveData.RecordID;
        // Reload the Newsletter
        Bind();
        //Remove the newsletter from all lists
        newsletter.RemoveFromLists();
        //Add the newsletter to lists on the RHS of Picklist control      
        foreach (ListItem li in lstRecipients.ItemsRight)
            newsletter.AddToList(li.Value);
        PopulateLists();
    }
    /// <summary>
    /// Preview the newsletter in the iframe
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkPreview_Click(object sender, EventArgs e)
    {
        if (!plPreview.Visible)
        {
            //Turn on Preview
            plPreview.Visible = true;
            // Save Changes
            SaveData();
            //Present the Preview in an iframe
            ViewNewsletter1.NewsletterID = int.Parse(ViewState["NewsletterID"].ToString());
        }
        else
        {
            //Turn off Preview
            plPreview.Visible = false;
        }
        // Toggle Panel
        plEdit.Enabled = !plPreview.Visible;
        plURL.Visible = !plPreview.Visible;
        plHtml.Visible = !plPreview.Visible;
    }
    /// <summary>
    /// Send the test mail
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSend_Click(object sender, EventArgs e)
    {
        Response.Redirect("send.aspx?index=" + ViewState["NewsletterID"].ToString());
    }
}

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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace i386.Newsletter
{
	/// <summary>
	/// Summary description for FormGenerator.
	/// </summary>
	public partial class FormGenerator : BasePage
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				if (UserLogin.GetCurrentUserRole()=="Admin")
					this.Listbox1.DataSource = Lists.GetLists();
				else
					this.Listbox1.DataSource = Lists.GetCurrentUserLists();
				this.Listbox1.DataTextField = "ListName";
				this.Listbox1.DataValueField = "ID";
				this.Listbox1.DataBind();
                String allLists = (String)GetGlobalResourceObject("labels", "allLists");
				this.Listbox1.Items.Insert(0, new ListItem("[ " + allLists + " ]",""));
			}
		}



		protected void GenerateButton_Click(object sender, System.EventArgs e)
		{
			string Path = Server.MapPath("formgenerator_template.htm");
			StreamReader objStreamReader = new StreamReader(Path);
			string filecontents=objStreamReader.ReadToEnd();
			objStreamReader.Close();
			
			this.CutPasteBox.Value = filecontents;
			this.FormPreview.Text = filecontents;
		}


	}
}

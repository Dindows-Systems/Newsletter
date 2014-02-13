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

namespace i386.Newsletter
{
    /// <summary>
    /// Summary description for Localisation
    /// </summary>
    public class BasePageGrid  : BasePage
    {
        protected DataGrid dg;
        protected int index;
        protected int NumOfRecords;	
        protected void Page_Init(object sender, System.EventArgs e)
        {

            ViewState["SQLStringFilter"] = "";
            if (Request.QueryString["index"]!=null && Request.QueryString["index"] != "")
                index = int.Parse(Request.QueryString["index"]);
            else
                index = 0;
            
        }

        // Datagrid Cancel
        protected void DataGrid1_PreRender(object sender, System.EventArgs e)
        {
            try
            {
                string numberOfDisplayedItems = (String)GetGlobalResourceObject("labels", "numberOfDisplayedItems");
                string strCount = ((DataTable)dg.DataSource).Rows.Count.ToString() + numberOfDisplayedItems;
                ((Label)dg.Controls[0].Controls[0].FindControl("lblNumRecords")).Text = strCount;
                ((Label)dg.Controls[0].Controls[dg.Controls[0].Controls.Count - 1].FindControl("lblNumRecords")).Text = strCount;
            }
            catch
            { }
    
        }

        /// <summary>
        /// Bind which overriden in the various pages.
        /// </summary>
        protected virtual void Bind()
        {
           
        }
        // --------- Various common Datagrid events

        /// <summary>
        /// Datagrid Paging
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dg.EditItemIndex = -1;
            dg.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }
        /// <summary>
        /// Datagrid Sorting
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid1_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            ViewState["SQLStringSort"] = " ORDER BY [" + e.SortExpression + "]";
            dg.EditItemIndex = -1;
            Bind();
        }
        /// <summary>
        /// Datagrid Editing Support
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
        {
            dg.EditItemIndex = e.Item.ItemIndex;
            Bind();
        }

        /// <summary>
        /// Datagrid Cancel Support
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            dg.EditItemIndex = -1;
            Bind();
        }

        /// <summary>
        /// Create the Pager with the record count
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void BaseItem(object source, DataGridItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Pager)
            {
                // Extract the Pager 
                TableCell pager = (TableCell)e.Item.Controls[0];

                //Add Cell to Row to Hold Row Count Label 
                TableCell newcell = new TableCell();
                newcell.ColumnSpan = 1;
                newcell.HorizontalAlign = HorizontalAlign.Center;
                newcell.Style["border-color"] = pager.Style["border-color"]; //For a Seamless Look 

                //Add Label Indicating Row Count 
                Label lblNumRecords = new Label();
                lblNumRecords.ID = "lblNumRecords";
                newcell.Controls.Add(lblNumRecords);

                //Add Table Cell to Pager 
                e.Item.Controls.AddAt(0, newcell);

                //Subtract from Colspan of Original Pager to Account for New Row 
                pager.ColumnSpan = pager.ColumnSpan - 1;

            }
            else if (itemType == ListItemType.Item )
            {
                // Add Javascript Confirmation to the last column which should always have the delete image button
                ImageButton deleteButton = (ImageButton)e.Item.Cells[e.Item.Cells.Count - 1].FindControl("Delete");
                deleteButton.Attributes["onclick"] = javascriptAreYouSure("theNewsletter", "delete");	
                // Hover color
              //  e.Item.Attributes.Add("onmouseout", "this.className='dgitem'");
              //  e.Item.Attributes.Add("onmouseover", "this.className='dgHover'");
            }
            else if (itemType == ListItemType.AlternatingItem)
            {
                // Add Javascript Confirmation to the last column which should always have the delete image button
                ImageButton deleteButton = (ImageButton)e.Item.Cells[e.Item.Cells.Count - 1].FindControl("Delete");
                deleteButton.Attributes["onclick"] = javascriptAreYouSure("theNewsletter", "delete");
                // Hover color
             //   e.Item.Attributes.Add("onmouseout", "this.className='dgalt'");
              //  e.Item.Attributes.Add("onmouseover", "this.className='dgHover'");
            }
        }
    }
}
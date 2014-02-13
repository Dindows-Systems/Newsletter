<%@ Page Language="c#" Inherits="i386.Newsletter.Subscriber" CodeFile="Default.aspx.cs"
    MasterPageFile="~/MasterPage.master"  %>

<%@ Register TagPrefix="i386" Namespace="i386.UI" Assembly="i386.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h2>Manage Subscribers</h2>
    <table cellspacing="0" cellpadding="5" width="800" border="0" class="frame">
        <tr>
            <td colspan="2" class="toolbar">
       [ <asp:HyperLink runat="server" ID="hlAdd" NavigateUrl="~/Subscribers/Add.aspx" Text="<%$ Resources:labels, add %>" /> ]
       &nbsp;[ <asp:HyperLink runat="server" ID="hlBatch" NavigateUrl="~/Subscribers/Batch.aspx" Text="Batch Import/Export" /> ]
       
            </td>
        </tr>
        <tr>
            <td class="formtitle">
                Search Name / Email</td>
            <td class="formdata">
                <asp:TextBox ID="SearchBox" runat="server" Width="80%"/></td>
        </tr>
        <tr>
            <td class="formtitle">
                Subscription Date
            </td>
            <td class="formdata">
                <i386:DropDownDateTime ID="SubFrom" DateFormat="M/d/yyyy" TimeFormat="" runat="server"
                    DisplayFormat="dd MMMM yyyy" EnableSet="true" />
                To
                <i386:DropDownDateTime ID="SubTo" runat="server" TimeFormat="" DateFormat="M/d/yyyy"
                    DisplayFormat="dd MMMM yyyy" EnableSet="true" />
            </td>
        </tr>
        <tr valign="top">
            <td class="formtitle">
                Newsletter Lists</td>
            <td class="formdata">
                <asp:ListBox SelectionMode="Multiple" ID="Listbox1" runat="server" Rows="5" Width="336px">
                </asp:ListBox></td>
        </tr>
        <tr>
            <td class="formtitle">
                Newsletter Format
            </td>
            <td class="formdata">
                <asp:RadioButtonList ID="FormatList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="" Selected="True">Both</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="Text">Text</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="formtitle">
                Confirmation</td>
            <td class="formdata">
                <asp:RadioButtonList ID="ConfirmationList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="" Selected="True">Both</asp:ListItem>
                    <asp:ListItem Value="true">Confirmed</asp:ListItem>
                    <asp:ListItem Value="false">Non-Confirmed</asp:ListItem>
                </asp:RadioButtonList>
                <div align="right">
                    <asp:Button ID="Button1" runat="server" Text="Find Now" OnClick="Button1_Click"></asp:Button></div>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DataGrid1" runat="server" OnCancelCommand="DataGrid1_CancelCommand"
        OnDeleteCommand="DataGrid1_DeleteCommand" OnEditCommand="DataGrid1_EditCommand"
        OnItemCommand="DataGrid1_ItemCommand" OnItemCreated="DataGrid1_ItemCreated" OnPageIndexChanged="DataGrid1_PageIndexChanged"
        OnPreRender="DataGrid1_PreRender">
        <Columns>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    Name
                  <asp:ImageButton CommandName="NameUp" Runat=server ImageUrl="~/images/buttons/up.gif"  width="12" height="10" />
						<asp:ImageButton CommandName="NameDn" Runat=server ImageUrl="~/images/buttons/dn.gif"  width="12" height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                    <asp:TextBox ID="recid" runat="Server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    Email
                   <asp:ImageButton CommandName="EmailUp" Runat=server ImageUrl="~/images/buttons/up.gif"  width="12" height="10" />
				 <asp:ImageButton  CommandName="EmailDn" Runat=server ImageUrl="~/images/buttons/dn.gif"  width="12" height="10" />
            </HeaderTemplate>
                <ItemTemplate>
                    <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "Email") %>">
                        <%# DataBinder.Eval(Container.DataItem, "Email") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    # Subscriptions
                   <asp:ImageButton CommandName="SubsUp" Runat=server ImageUrl="~/images/buttons/up.gif"  width="12" height="10" />
				   <asp:ImageButton CommandName="SubsDn" Runat=server ImageUrl="~/images/buttons/dn.gif"  width="12" height="10" />
       </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CountOflid") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Edit">
                <ItemTemplate>
                    <a href="Edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <img src="../images/buttons/editsub.gif" border="0" /></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Delete">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/buttons/delete.gif" runat="server" ID="Delete" CommandName="Delete" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>        
    </asp:DataGrid>
</asp:Content>

<%@ Page Language="c#" Inherits="i386.Newsletter.manage" CodeFile="default.aspx.cs"
     MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h2>Manage Newsletters</h2>
    <table cellspacing="0" cellpadding="5" width="800" border="0" class="frame">

        <tr>
            <td colspan="2" class="toolbar">
                <asp:HyperLink runat="server" ID="hlAdd" Text="<%$ Resources:labels, add %>" NavigateUrl="~/Newsletters/Edit.aspx" />
            </td>
        </tr>
        <tr valign="top">
            <td class="formtitle">
                <asp:Literal ID="ltrlLists" runat="server" Text="<%$ Resources:labels, NewsletterLists%>" /></td>
            <td class="formdata">
                <asp:ListBox ID="Listbox1" runat="server" Rows="1" Width="336px" SelectionMode="Single" />
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:labels, search%>" OnClick="Button1_Click1" />
             </td>
        </tr>
        <tr>
            <td class="formtitle">
                <asp:Literal ID="ltrlSearch" runat="server" Text="<%$ Resources:labels, search %>" /></td>
            <td class="formdata">
                <asp:TextBox ID="SearchBox" runat="server" Width="80%" /></td>
        </tr>
        <tr>
            <td class="formtitle">
                <asp:Literal ID="ltrlSentDate" runat="server" Text="<%$ Resources:labels, SentDate%>" />
            </td>
            <td class="formdata">
                <asp:Literal ID="ltrlSFrom" runat="server" Text="<%$ Resources:labels, From%>" />
                <i386:DropDownDateTime ID="SentDateFrom" runat="server" DisplayFormat="dd MMMM yyyy"
                    TimeFormat="" DateFormat="M/d/yyyy" EnableSet="true"></i386:DropDownDateTime>
                <asp:Literal ID="ltrlSTo" runat="server" Text="<%$ Resources:labels, To%>" />
                <i386:DropDownDateTime ID="SentDateTo" runat="server" DisplayFormat="dd MMMM yyyy"
                    TimeFormat="" DateFormat="M/d/yyyy" EnableSet="true"></i386:DropDownDateTime>
            </td>
        </tr>
        <tr>
            <td class="formtitle">
                <asp:Literal ID="ltrlModDate" runat="server" Text="<%$ Resources:labels, ModifiedDate%>" />
            </td>
            <td class="formdata">
                <asp:Literal ID="ltrlMFrom" runat="server" Text="<%$ Resources:labels, From%>" />
                <i386:DropDownDateTime ID="ModifiedDateFrom" runat="server" DisplayFormat="dd MMMM yyyy"
                    TimeFormat="" DateFormat="M/d/yyyy" EnableSet="true"></i386:DropDownDateTime>
                <asp:Literal ID="ltrlMTo" runat="server" Text="<%$ Resources:labels, To%>" />
                <i386:DropDownDateTime ID="ModifiedDateTo" runat="server" DisplayFormat="dd MMMM yyyy"
                    TimeFormat="" DateFormat="M/d/yyyy" EnableSet="true"></i386:DropDownDateTime>
            </td>
        </tr>

    </table>
    <asp:DataGrid ID="DataGrid1" runat="server" OnCancelCommand="DataGrid1_CancelCommand"
        OnDeleteCommand="DataGrid1_DeleteCommand" OnEditCommand="DataGrid1_EditCommand"
        OnItemCommand="DataGrid1_ItemCommand" OnItemCreated="DataGrid1_ItemCreated" OnPageIndexChanged="DataGrid1_PageIndexChanged"
        OnPreRender="DataGrid1_PreRender">
        <Columns>
            <asp:TemplateColumn SortExpression="Name">
                <HeaderTemplate>
                    <asp:Literal ID="ltrlName" runat="server" Text="<%$ Resources:labels, name%>" />
                    <asp:LinkButton CommandName="NameUp" runat="server"><img src="../images/buttons/up.gif" border=0 width="12" height="10" /></asp:LinkButton>
                    <asp:LinkButton CommandName="NameDn" runat="server"><img src="../images/buttons/dn.gif" border=0 width="12" height="10" /></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="Edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <%# DataBinder.Eval(Container.DataItem, "Name") %>
                    </a>
                    <asp:TextBox ID="recid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
                    <br>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlRecipients" runat="server" Text='<%$ Resources:labels, recipients%>' />
                    <asp:LinkButton CommandName="RecipsUp" runat="server"><img src="../images/buttons/up.gif" border=0 width="12" height="10" /></asp:LinkButton>
                    <asp:LinkButton CommandName="RecipsDn" runat="server"><img src="../images/buttons/dn.gif" border=0 width="12" height="10" /></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CountOfListID") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlStatus" runat="server" Text="<%$ Resources:labels, status%>" />
                    <asp:LinkButton CommandName="StatusUp" runat="server"><img src="../images/buttons/up.gif" border=0 width="12" height="10" /></asp:LinkButton>
                    <asp:LinkButton CommandName="StatusDn" runat="server"><img src="../images/buttons/dn.gif" border=0 width="12" height="10" /></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Status") %>
                    <br />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlReleaseDate" runat="server" Text="<%$ Resources:labels, releasedate%>" />
                    <asp:LinkButton CommandName="SentDateUp" runat="server" ID="Linkbutton1"><img src="../images/buttons/up.gif" border=0 width="12" height="10" /></asp:LinkButton>
                    <asp:LinkButton CommandName="SentDateDn" runat="server" ID="Linkbutton2"><img src="../images/buttons/dn.gif" border=0 width="12" height="10" /></asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Sent") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlClone" runat="server" Text="<%$ Resources:labels, clone%>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/buttons/clone.gif" CommandName="Clone" runat="server"
                        ID="clone" />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlViw" runat="server" Text="<%$ Resources:labels, view%>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>&view=1">
                        <img src="../images/buttons/preview.gif" border="0" width="32" height="32" /></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlMTo" runat="server" Text="<%$ Resources:labels, send%>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href='send.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>'>
                        <img src="../images/buttons/sendemail.gif" border="0" width="32" height="32" /></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlEdit" runat="server" Text="<%$ Resources:labels, edit%>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="Edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <img src="../images/buttons/edit.gif" border="0" width="32" height="32" /></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    <asp:Literal ID="ltrlDelete" runat="server" Text="<%$ Resources:labels, delete%>" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:ImageButton runat="server" ID="Delete" ImageUrl="~/images/buttons/delete.gif"
                        CommandName="Delete" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Users_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>Manage Users</h2>
    [ <asp:HyperLink ID="hlAdd" NavigateUrl="~/Lists/Add.aspx" runat="server" Text="<%$ Resources:labels, add %>" /> ]
     [ <asp:HyperLink  ID="HyperLink3" runat="server" Text="Settings"  NavigateUrl="~/Admin/Settings.aspx" /> ]
    <table cellspacing="0" cellpadding="5" width="800" border="0" class="frame">
        <tr>
            <td class="formtitle" width="300">
                Search Name / Email:</td>
            <td class="formdata">
                <asp:TextBox ID="SearchBox" runat="server" Width="100%" /></td>
        </tr>
        <tr valign="top">
            <td class="formtitle" width="300">
                Access to Lists:</td>
            <td class="formdata">
                <asp:ListBox SelectionMode="Multiple" ID="Listbox1" runat="server" Rows="5" Width="336px">
                </asp:ListBox></td>
        </tr>
        <tr>
            <td class="formtitle" width="300">
                Role:</td>
            <td class="formdata">
                <asp:RadioButtonList ID="RoleList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="" Selected="True">Both</asp:ListItem>
                    <asp:ListItem>User</asp:ListItem>
                    <asp:ListItem>Admin</asp:ListItem>
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
                    <asp:ImageButton ID="ImageButton1" CommandName="NameUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton ID="ImageButton2" CommandName="NameDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Name") %>
                    <asp:TextBox ID="recid" runat="Server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    Email
                    <asp:ImageButton ID="ImageButton3" CommandName="EmailUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton ID="ImageButton4" CommandName="EmailDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "Email") %>">
                        <%# DataBinder.Eval(Container.DataItem, "Email") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    # Lists
                    <asp:ImageButton ID="ImageButton5" CommandName="SubsUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton ID="ImageButton6" CommandName="SubsDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CountOflid") %>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Edit">
                <ItemTemplate>
                    <a href="Edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <img src="../../images/buttons/edituser.gif" border="0" width="32" height="32" /></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Delete">
                <ItemTemplate>
                    <asp:ImageButton ImageUrl="~/images/buttons/delete.gif" runat="server" ID="Delete"
                        CommandName="Delete" />
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>


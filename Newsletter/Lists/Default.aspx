<%@ Page Language="c#" Inherits="i386.Newsletter.Forms.Lists" CodeFile="Default.aspx.cs"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Manage Mailing Lists</h2>
        [ <asp:HyperLink ID="hlAdd" NavigateUrl="~/Lists/Add.aspx" runat="server" Text="<%$ Resources:labels, add %>" /> ]
    <dl>
        <dt>
            <asp:Label ID="lblSearch" runat="server" Text="Search" />
        </dt>
        <dd>
            <asp:TextBox ID="SearchBox" runat="server" Width="80%" />
            <div align="right">
                <asp:Button ID="Button1" runat="server" Text="Find Now" OnClick="Button1_Click1"></asp:Button></div>
        </dd>
    </dl>
    <asp:DataGrid ID="DataGrid1" runat="server" OnCancelCommand="DataGrid1_CancelCommand"
        OnDeleteCommand="DataGrid1_DeleteCommand" OnEditCommand="DataGrid1_EditCommand"
        OnItemCommand="DataGrid1_ItemCommand" OnItemCreated="DataGrid1_ItemCreated" OnPageIndexChanged="DataGrid1_PageIndexChanged"
        OnPreRender="DataGrid1_PreRender">
        <Columns>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    Name
                    <asp:ImageButton CommandName="NameUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton CommandName="NameDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="../subscription.aspx?ListID=<%# DataBinder.Eval(Container.DataItem, "id") %>"
                        target="Sub">
                        <%# DataBinder.Eval(Container.DataItem, "ListName") %>
                    </a>
                    <asp:TextBox runat="server" ID="recid" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    # Newsletters
                    <asp:ImageButton CommandName="NewsLUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton CommandName="NewsLDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="../Newsletters/default.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <%# DataBinder.Eval(Container.DataItem, "CountOfnid") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>
                    # Subscribers
                    <asp:ImageButton CommandName="SubsUp" runat="server" ImageUrl="~/images/buttons/up.gif"
                        Width="12" Height="10" />
                    <asp:ImageButton CommandName="SubsDn" runat="server" ImageUrl="~/images/buttons/dn.gif"
                        Width="12" Height="10" />
                </HeaderTemplate>
                <ItemTemplate>
                    <a href="../Subscribers/default.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <%# DataBinder.Eval(Container.DataItem, "CountOfsid") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Edit">
                <ItemTemplate>
                    <a href="Edit.aspx?index=<%# DataBinder.Eval(Container.DataItem, "id") %>">
                        <img src="../images/buttons/editlist.gif" border="0" width="32" height="32" /></a>
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

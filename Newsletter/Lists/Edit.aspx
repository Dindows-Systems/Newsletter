<%@ Page Language="c#" Inherits="EditList" CodeFile="Edit.aspx.cs" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Edit Mailing List</h2>
    <asp:DataList ID="DataList1" runat="server" OnDeleteCommand="DataList1_DeleteCommand"
        OnItemCreated="DataList1_ItemCreated" OnUpdateCommand="DataList1_UpdateCommand">
        <EditItemTemplate>
            <table class="frame" cellspacing="0" cellpadding="5" width="800" border="0">
                <tr>
                    <td class="toolbar" colspan="2">
                        <asp:ImageButton ID="Update" runat="server" CommandName="Update" ImageUrl="~/images/buttons/save.gif" />
                        <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/images/buttons/delete.gif" /></td>
                </tr>
            </table>
            <dl>
                <dt>
                    <asp:Literal runat="server" ID="ltrlName" Text="<%$ Resources:labels, name%>" />
                    <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="<%$ Resources:labels, requirefield%>"
                        ControlToValidate="Name" />
                </dt>
                <dd>
                    <asp:TextBox ID="Name" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "ListName") %>' />
                </dd>
                <dt>
                    <asp:Literal runat="server" ID="ltrlDesc" Text="<%$ Resources:labels, description%>" />
                </dt>
                <dd>
                    <asp:TextBox ID="Description" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'
                        TextMode="MultiLine" Height="79px" />
                </dd>
                <dt>
                    <asp:Literal runat="server" ID="ltrlDateCreated" Text="<%$ Resources:labels, datecreated%>" />
             
              </dt>
              <dd>
                     <%# DataBinder.Eval(Container.DataItem, "DateCreated") %>
                    by
                    <%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>
              </dd>
            </dl>
        </EditItemTemplate>
    </asp:DataList>
</asp:Content>

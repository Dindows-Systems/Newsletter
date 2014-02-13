<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Subscribers_Edit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>Edit Subscriber</h2>
    <asp:DataList ID="DataList1" runat="server" OnDeleteCommand="DataList1_DeleteCommand"
        OnItemCreated="DataList1_ItemCreated" 
        OnUpdateCommand="DataList1_UpdateCommand">
        <EditItemTemplate>
            <table class="frame" cellspacing="0" cellpadding="5">
                <tr>
                    <td class="toolbar" colspan="2">
                        <asp:ImageButton ID="Update" runat="server" ImageUrl="~/images/buttons/save.gif"
                            CommandName="Update" />
                        <asp:ImageButton ID="Delete" runat="server" CommandName="Delete" ImageUrl="~/images/buttons/delete.gif" />
                    </td>
                </tr>
                <tr>
                    <td class="formtitle">
                        <asp:Literal runat="server" ID="ltrlName" Text="<%$ Resources:labels, name%>" />
                    </td>
                    <td class="formdata">
                        <asp:TextBox ID="Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                            Width="80%" />
                        <br />
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="Name"
                            ErrorMessage="<%$ Resources:labels, requirefield%>" />
                    </td>
                </tr>
                <tr>
                    <td class="formtitle">
                        <asp:Literal runat="server" ID="ltrlEdit"  />
                    </td>
                    <td class="formdata">
                        <asp:TextBox ID="Email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email") %>'
                            Width="80%">
                        </asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="Email"
                            ErrorMessage="* Please provide an E-Mail address for the subscriber" />
                    </td>
                </tr>
                <tr valign="top">
                    <td class="formtitle" width="100">
                        <asp:Literal runat="server" ID="ltrlLists" Text="<%$ Resources:labels, lists%>" /></td>
                    <td class="formdata" width="700">
                        <asp:ListBox ID="Listbox1" runat="server" SelectionMode="Multiple" Rows="5" Width="336px" />
                        <table style="width: 400px; font-size: 9px;">
                            <tr>
                                <td style="text-align: left; padding-left: 20px; width: 50%;">
                                    &nbsp;</td>
                                <td style="text-align: left; padding-left: 40px; width: 50%; font-weight: bold">
                                    <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:labels, subscribedto%>" /></td>
                            </tr>
                        </table>
                        <i386:PickList ID="lstLists" runat="server" EnableAllItemsButtons="false" EnableLeftMoveButtons="false"
                            EnableRightMoveButtons="false" Width="400" />
                    </td>
                </tr>
 
                <tr>
                    <td class="formdata" colspan="2">
                        <asp:Literal runat="server" ID="ltrlSubDate" Text="<%$ Resources:labels, datesubscription%>" />
                        <%# DataBinder.Eval(Container.DataItem, "Created") %>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:DataList>
</asp:Content>


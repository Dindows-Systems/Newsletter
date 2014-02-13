<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="Edit.aspx.cs" Inherits="NewsletterEdit" Title="Edit Newsletter" %>

<%@ Register Src="../Controls/ViewNewsletter.ascx" TagName="ViewNewsletter" TagPrefix="uc1" %>
<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="plEdit" runat="server">
        <div>
            <asp:HyperLink ID="hlNew" runat="server" Text="<%$ Resources:labels, add %>" NavigateUrl="~/Newsletters/Edit.aspx" />
            [ Copy ] [ Reload ]
            <asp:LinkButton ID="lnkDelete" runat="server" Text="<%$ Resources:labels, delete %>"
                OnClick="lnkDelete_Click" Visible="false" />
            [
            <asp:LinkButton ID="lnkSend" runat="server" Text="Send" Visible="false"
                OnClick="lnkSend_Click" />
            ]
            <asp:LinkButton ID="lnkSave" runat="server" Text="<%$ Resources:labels, save %>"
                OnClick="lnkSave_Click" />
        </div>
        <!-- Preview Panel -->
        <asp:Panel ID="plPreview" runat="server" Visible="false">
            <h2>
                <asp:Literal ID="ltrlPreviews" runat="server" Text="<%$ Resources:labels, showLivePreview%>" /></h2>
            <uc1:ViewNewsletter ID="ViewNewsletter1" runat="server" />
            <asp:LinkButton ID="lnkClosePreview" runat="server" OnClick="lnkPreview_Click" Text="<%$ Resources:labels, edit%>" />
            <br />
        </asp:Panel>
        <h2>
            Newsletter Details</h2>
        <asp:Label runat="server" ID="lblErrorMsg" />
        <table width="100%">
            <tr>
                <td class="formtitle">
                    <asp:Label runat="server" ID="lblName" Text="<%$ Resources:labels, name %>" />
                    <asp:RequiredFieldValidator ErrorMessage="*" runat="server" ControlToValidate="txtName"
                        ID="valName" />
                </td>
                <td class="formdata">
                    <asp:TextBox ID="txtName" runat="server" Width="80%" />
                </td>
            </tr>
            <tr>
                <td class="formtitle">
                    <asp:Label runat="server" ID="lblSubject" Text="<%$ Resources:labels, subject %>" />
                    <asp:RequiredFieldValidator ErrorMessage="*" runat="server" ControlToValidate="txtSubject"
                        ID="valSubject" />
                </td>
                <td class="formdata">
                    <asp:TextBox ID="txtSubject" runat="server" Width="80%" />
                </td>
            </tr>
            <tr valign="top">
                <td class="formtitle">
                    <asp:Label runat="server" ID="lblRecipients" Text="Recipients" />
                </td>
                <td class="formdata">
                    <table style="width: 400px; font-size: 9px;">
                        <tr>
                            <td style="text-align: left; padding-left: 20px; width: 50%;">
                                &nbsp;</td>
                            <td style="text-align: left; padding-left: 40px; width: 50%; font-weight: bold">
                                <asp:Literal runat="server" ID="Literal1" Text="<%$ Resources:labels, belongsto%>" />
                            </td>
                        </tr>
                    </table>
                    <i386:PickList ID="lstRecipients" runat="server" EnableAllItemsButtons="false" EnableLeftMoveButtons="false"
                        EnableRightMoveButtons="false" Width="400" />
                </td>
            </tr>
            <tr>
                <td class="formtitle">
                    <asp:Label runat="server" ID="lblFromName" Text="From Name" /></td>
                <td class="formdata">
                    <asp:TextBox ID="txtFromName" runat="server" Width="80%" />
                </td>
            </tr>
            <tr>
                <td class="formtitle">
                    <asp:Label runat="server" ID="lblFrom" Text="Reply To Email" />
                    <asp:RequiredFieldValidator ErrorMessage="*" runat="server" ControlToValidate="txtFrom"
                        ID="valFrom" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFrom" ValidationExpression=".*@.*\..*"
                        ErrorMessage="* Please use a valid email address" Display="dynamic" ID="Regularexpressionvalidator1" />
                </td>
                <td class="formdata">
                    <asp:TextBox ID="txtFrom" runat="server" Width="80%" />
                </td>
            </tr>
            <tr>
                <td class="formtitle" width="100">
                    <asp:Label runat="server" ID="lblStatus" Text="Status" />
                </td>
                <td class="formdata" width="700">
                    <asp:Label runat="server" ID="lblCurrentStatus" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="plURL" runat="server">
        <h2>
            <asp:Literal ID="ltrlEdit" runat="server" /></h2>
        <asp:LinkButton ID="lnkPreview" runat="server" Text="<%$ Resources:labels, showLivePreview%>"
            OnClick="lnkPreview_Click" Visible="false" />
        <br />
        <br />
        <strong>Content is from external URL</strong><br />
        <asp:TextBox ID="txtURL" runat="server" Width="700" />
    </asp:Panel>
    <asp:Panel ID="plHtml" runat="server">
        <asp:Label runat="server" ID="lblHtml" Text="HTML Contents" />
        <strong>HTML Content</strong><br />
        <ftb:FreeTextBox ID="txtHtml" runat="Server" RemoveServerNameFromUrls="true" Height="300px"
            Width="100%" BackColor="Transparent" UseToolbarBackGroundImage="False" GutterBackColor="Transparent"
            ToolbarBackgroundImage="False" ScriptMode="InPage">
            <Toolbars>
                <ftb:Toolbar>
                </ftb:Toolbar>
            </Toolbars>
        </ftb:FreeTextBox>
        <h2>
            Link Tracking</h2>
        <iframe id="iframe1" height="200" width="100%" frameborder="0" runat="server" />
    </asp:Panel>
</asp:Content>

<%@ Page Language="c#" Inherits="i386.Newsletter.FormGenerator" CodeFile="FormGenerator.aspx.cs" MasterPageFile="~/MasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="0" cellpadding="5" width="800" border="0" class="frame">
        <tr>
            <td colspan="2" class="toolbar">
                <img src="../../images/buttons/lists.gif" border="0" onmouseover="return escape('Returns Lists')"
                    width="32" height="32">
            </td>
        </tr>
        <tr valign="top">
            <td class="formtitle">
                Newsletter Lists</td>
            <td class="formdata">
                <asp:ListBox SelectionMode="Multiple" ID="Listbox1" runat="server" Rows="5" Width="336px">
                </asp:ListBox></td>
        </tr>
        <tr valign="top">
            <td class="formtitle">
                Fields</td>
            <td class="formdata">
                <asp:ListBox SelectionMode="Multiple" ID="FieldList" runat="server" Rows="5" Width="336px">
                    <asp:ListItem>Newsletter Format</asp:ListItem>
                </asp:ListBox><div align="right">
                    <asp:Button ID="GenerateButton" runat="server" Text="Generate Form" OnClick="GenerateButton_Click">
                    </asp:Button></div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr valign="top">
                        <td width="50%">
                            Preview<br>
                            <asp:Literal runat="server" ID="FormPreview" /></td>
                        <td width="50%">
                            Cut'n'Paste Code<br>
                            <textarea runat="server" id="CutPasteBox" rows="10" cols="40" name="FormCode"></textarea></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

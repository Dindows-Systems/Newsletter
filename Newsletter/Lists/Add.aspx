<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="Lists_Add" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        Create a new Mailing List</h2>
    <div id="addbox">
        <dl>
            <dt>
                <asp:Literal ID="ltlListName" runat="server" Text="<%$ Resources:labels, listname%>" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                    ControlToValidate="ListName" />
            </dt>
            <dd>
                <asp:TextBox ID="ListName" runat="server" Width="90%" />
            </dd>
        </dl>
        <p align="right">
            <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" Text="<%$ Resources:labels, listname%>" />
        </p>
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Subscribers_Add"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="addbox">
        <h2>
            Add New Subscriber</h2>
        <dl>
            <dt>
                <asp:Literal runat="server" ID="ltrlEmail" Text="<%$ Resources:labels, emailaddress%>" /></dt>
            <dd>
               <asp:RegularExpressionValidator
                    ID="valUserName" runat="server" ControlToValidate="Email" ValidationExpression=".*@.*\..*"
                    ErrorMessage="* Your entry is not a valid e-mail address." Display="dynamic">*
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                    ControlToValidate="Email" /> <asp:TextBox ID="Email" runat="server" Width="90%" />
            </dd>
        </dl>
        <p>
            <asp:Label ID="Label_Error" runat="server" CssClass="errormsg" Visible="False">
                <asp:Literal runat="server" ID="ltrlErrEmailEx" Text="<%$ Resources:labels, erroremailexists%>" />
            </asp:Label>
        </p>
        <p align="right">
            <asp:Button ID="Button1" Text="<%$ Resources:labels, add%>" runat="server" OnClick="BtnAdd_Click" />
        </p>
    </div>
</asp:Content>


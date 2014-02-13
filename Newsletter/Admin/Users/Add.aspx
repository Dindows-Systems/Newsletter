<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="Admin_Users_Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<h2>Add New User</h2>
    <div id="addbox">
        <dl>
            <dt>
                <asp:Literal ID="ltrlUserNane" runat="server" Text="<%$ Resources:labels, username%>" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserName"
                    ForeColor="#FF9900" Text="*"/>
            </dt>
            <dd>
                <asp:TextBox ID="UserName" runat="server" Width="90%" />
            </dd>
            <dt>
                <asp:Literal ID="ltrlEmail" runat="server" Text="<%$ Resources:labels, emailAddress%>" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Email"
                    ForeColor="#FF9900" Text="*" />
                <asp:RegularExpressionValidator ID="valUserName" runat="server" ControlToValidate="Email"
                    ValidationExpression=".*@.*\..*" ErrorMessage="* Your entry is not a valid e-mail address."
                    Display="dynamic">*
                </asp:RegularExpressionValidator>
            </dt>
            <dd>
                <asp:TextBox ID="Email" runat="server" Width="90%" />
            </dd>
        </dl>
        <asp:Label ID="Label_Error" runat="server" CssClass="errormsg" Visible="False" />
        <asp:Label ID="Label_InvalidEmail" runat="server" CssClass="errormsg" Visible="False" /></p>
        <p align="right">
            <asp:Button ID="BtnAdd" Text="<%$ Resources:labels, add%>" runat="server" OnClick="BtnAdd_Click" />
        </p>
    </div>
</asp:Content>
